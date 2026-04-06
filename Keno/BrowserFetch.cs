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

    public static void StartServer()
    {
        _server = new WebSocketServer("ws://0.0.0.0:4444");
        _server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                _extensionSocket = socket;
                Console.WriteLine("Extension connected");
            };
            socket.OnClose = () =>
            {
                _extensionSocket = null;
                Console.WriteLine("Extension disconnected");
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