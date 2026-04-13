using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BrowserFetch
{
    private static IWebSocketConnection _extensionSocket;
    private static readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pending =
        new ConcurrentDictionary<string, TaskCompletionSource<string>>();
    private static WebSocketServer _server;

    /// <summary>True while a browser extension socket is open and available.</summary>
    public static bool IsConnected => _extensionSocket != null && _extensionSocket.IsAvailable;

    /// <summary>True when the user has selected Extension mode; false for Cookie mode.</summary>
    public static bool ExtensionMode { get; private set; }

    /// <summary>Raised on the Fleck thread when the extension connects.</summary>
    public static event EventHandler Connected;

    /// <summary>Raised on the Fleck thread when the extension disconnects.</summary>
    public static event EventHandler Disconnected;

    public static void StartServer()
    {
        // Avoid starting a second server if already running.
        if (_server != null) return;
        ExtensionMode = true;
        _server = new WebSocketServer("ws://0.0.0.0:4444");
        _server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                _extensionSocket = socket;
                Console.WriteLine("Extension connected");
                Connected?.Invoke(null, EventArgs.Empty);
            };
            socket.OnClose = () =>
            {
                _extensionSocket = null;
                Console.WriteLine("Extension disconnected");
                Disconnected?.Invoke(null, EventArgs.Empty);
            };
            socket.OnMessage = message =>
            {
                var msg = JsonConvert.DeserializeObject<dynamic>(message);
                string id = msg.id;
                if (_pending.TryRemove(id, out var tcs))
                    tcs.SetResult(message); // raw JSON string
            };
        });
    }

    /// <summary>
    /// Closes the active socket and shuts down the WebSocket server.
    /// Call this when switching back to Cookie mode.
    /// </summary>
    public static void StopServer()
    {
        ExtensionMode = false;

        // Cancel all in-flight requests so callers don't hang.
        foreach (var kv in _pending)
            kv.Value.TrySetCanceled();
        _pending.Clear();

        try { _extensionSocket?.Close(); } catch { }
        _extensionSocket = null;

        try { _server?.Dispose(); } catch { }
        _server = null;
    }

    /// <summary>
    /// Sends a fetch request through the browser extension.
    /// options: e.g. new { method = "POST", headers = new { ... }, body = "..." }
    /// </summary>
    public static async Task<string> FetchAsync(string url, object options = null, int timeoutMs = 10000)
    {
        if (_extensionSocket == null || !_extensionSocket.IsAvailable)
            throw new Exception("Browser extension not connected");

        var id = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<string>();
        _pending[id] = tcs;

        var payload = new
        {
            id,
            payload = new { url, options = options ?? new { } }
        };

        _extensionSocket.Send(JsonConvert.SerializeObject(payload));

        var timeoutTask = Task.Delay(timeoutMs);
        var completed = await Task.WhenAny(tcs.Task, timeoutTask);

        if (completed == timeoutTask)
        {
            _pending.TryRemove(id, out _);
            throw new TimeoutException($"Request timed out: {url}");
        }

        // Extract the "response" field from { id, response: {...} }
        var result = JsonConvert.DeserializeObject<dynamic>(await tcs.Task);
        return JsonConvert.SerializeObject(result.response);
    }
}
