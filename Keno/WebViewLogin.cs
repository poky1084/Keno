using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keno
{
    public class WebViewLogin : Form
    {
        private WebView2 webView;
        private Label statusLabel;
        private Button doneButton;

        public string CapturedClearance { get; private set; } = "";
        public string CapturedUserAgent { get; private set; } = "";

        private string targetSite;

        public WebViewLogin(string site)
        {
            targetSite = site;
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Size = new System.Drawing.Size(500, 350);
            this.Text = "Login - Complete Cloudflare challenge then click DONE";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Status label at top
            statusLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 30,
                Text = "Loading...",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Segoe UI", 9),
                Padding = new Padding(5, 0, 0, 0)
            };

            // DONE button at bottom — user clicks this after logging in
            doneButton = new Button
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                Text = "✅ Done — Click here",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.MediumSeaGreen,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false // disabled until page loads
            };
            doneButton.Click += async (s, e) => await OnDoneClicked();

            webView = new WebView2 { Dock = DockStyle.Fill };

            this.Controls.Add(webView);
            this.Controls.Add(statusLabel);
            this.Controls.Add(doneButton);

            // Block accidental close before grabbing
          

            this.Load += async (s, e) => await InitWebView();
        }

        private async Task InitWebView()
        {
            var options = new CoreWebView2EnvironmentOptions
            {
                // Add a real Chrome User Agent to help bypass basic filters
                // Using a common Windows Chrome UA
                AdditionalBrowserArguments = "--disable-blink-features=AutomationControlled " +
                                             "--user-agent=\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36\""
            };

            var env = await CoreWebView2Environment.CreateAsync(
                userDataFolder: System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "KenoBot", "WebViewData"),
                options: options
            );

            await webView.EnsureCoreWebView2Async(env);

            // More aggressive stealth script
            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(@"
        // 1. Remove the webdriver property
        Object.defineProperty(navigator, 'webdriver', { get: () => undefined });

        // 2. Mock Chrome-specific properties
        window.chrome = { runtime: {} };

        // 3. Fake permissions
        const originalQuery = window.navigator.permissions.query;
        window.navigator.permissions.query = (parameters) => (
            parameters.name === 'notifications' ?
            Promise.resolve({ state: Notification.permission }) :
            originalQuery(parameters)
        );
    ");

            webView.CoreWebView2.NavigationCompleted += OnNavCompleted;
            webView.Source = new Uri("https://" + targetSite);
        }

        private async void OnNavCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            var title = await webView.CoreWebView2.ExecuteScriptAsync("document.title");
            title = title.Trim('"');

            if (title.Contains("Just a moment") || title.Contains("Attention Required"))
            {
                statusLabel.Text = "⏳ Cloudflare challenge in progress — please wait...";
                statusLabel.ForeColor = System.Drawing.Color.OrangeRed;
                doneButton.Enabled = false;
            }
            else
            {
                statusLabel.Text = $"✅ Ready: {title} — Log in if needed, then click Done below.";
                statusLabel.ForeColor = System.Drawing.Color.DarkGreen;
                doneButton.Enabled = true;
            }
        }

        private async Task OnDoneClicked()
        {
            doneButton.Enabled = false;
            doneButton.Text = "Grabbing cookies...";
            statusLabel.Text = "⏳ Capturing cookies and user agent...";

            try
            {
                var cookieManager = webView.CoreWebView2.CookieManager;

                // Get ALL cookies for the site
                var cookies = await cookieManager.GetCookiesAsync("https://" + targetSite);

                var cfCookie = cookies.FirstOrDefault(c => c.Name == "cf_clearance");
                if (cfCookie != null)
                {
                    CapturedClearance = cfCookie.Value;
                }

                // Get real user agent from the running browser
                var ua = await webView.CoreWebView2.ExecuteScriptAsync("navigator.userAgent");
                CapturedUserAgent = ua.Trim('"');

                if (string.IsNullOrEmpty(CapturedClearance))
                {
                    statusLabel.Text = "⚠️ cf_clearance not found — make sure you passed the CF challenge.";
                    statusLabel.ForeColor = System.Drawing.Color.OrangeRed;
                    doneButton.Enabled = true;
                    doneButton.Text = "✅ Done — Click here";
                    return;
                }

                //statusLabel.Text = $"✅ Captured! cf_clearance: {CapturedClearance[..Math.Min(20, CapturedClearance.Length)]}...";
                statusLabel.ForeColor = System.Drawing.Color.DarkGreen;

                // Signal success and close
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"❌ Error: {ex.Message}";
                statusLabel.ForeColor = System.Drawing.Color.Red;
                doneButton.Enabled = true;
                doneButton.Text = "✅ Done — Click here";
            }
        }
    }
}