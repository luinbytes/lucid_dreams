using System;
using System.Drawing;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace lucid_dreams
{
    public class Config
    {
        public string UserKey { get; set; }
        
    }

    partial class Login
    {
        private const string ConfigFilePath = "./config.json";

        private Config LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    return JsonSerializer.Deserialize<Config>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load config: {ex.Message}");
            }

            return new Config();
        }

        private void SaveConfig(Config config)
        {
            try
            {
                string json = JsonSerializer.Serialize(config);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save config: {ex.Message}");
            }
        }
        private Config config;

        private void Login_Load(object sender, EventArgs e)
        {
            
            if (File.Exists("key.txt"))
            {
                
                string key = File.ReadAllText("key.txt");

                
                keyTextBox.Text = key;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig(config);
        }

        public static string GlobalUserKey;
        public static string GlobalUsername;
        // public static string GlobalKeyLink;
        // public static string GlobalKeyStop;
        public static string GlobalLevel;
        public static string GlobalProtection;
        public static string GlobalFid;
        public static string GlobalUnreadConversations;
        public static string GlobalUnreadAlerts;
        public static string GlobalRegisterDate;
        public static string GlobalPosts;
        public static string GlobalScore;
        public static string AvatarURL;
        public static string GlobalConfig;

        public static string SessionHistory;
        public static JsonElement Root2 { get; set; }

        private MaterialTextBox keyTextBox;
       
        
        
        
        private System.ComponentModel.IContainer components = null;

        
        
        
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        
        
        
        
        private void InitializeComponent()
        {
            this.Load += Login_Load;
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink800, Primary.Pink900, Primary.Pink500, Accent.Pink200, TextShade.WHITE);
            
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 160);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Sizable = false;
            this.Text = "Lucid Dreams - Login";

            keyTextBox = new MaterialTextBox();
            keyTextBox.Hint = "Enter your key";
            keyTextBox.Location = new Point(10, 80);
            keyTextBox.Size = new Size(315, 30);
            this.Controls.Add(keyTextBox);

            MaterialButton loginButton = new MaterialButton();
            loginButton.Text = "Login";
            loginButton.Location = new Point(this.ClientSize.Width - loginButton.Width - 115, this.ClientSize.Height - loginButton.Height + 29);
            this.Controls.Add(loginButton);

            MaterialButton faqButton = new MaterialButton();
            faqButton.Text = "FAQ";
            faqButton.Location = new Point(this.ClientSize.Width - faqButton.Width - 10, this.ClientSize.Height - faqButton.Height - 10);
            faqButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(faqButton);

            faqButton.Click += (sender, e) => {
                MessageBox.Show("Q: Error! Unautherized!\nA: Your hash is incorrect. Try making a new session.\n\n", "Lucid Dreams - FAQ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            loginButton.Click += async (sender, e) => {
                string userKey = keyTextBox.Text;

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string url = $"https://constelia.ai/api.php?key={userKey}";
                        HttpResponseMessage globalResponse = await client.GetAsync(url);
                        HttpResponseMessage sessionResponse = await client.GetAsync($"https://constelia.ai/api.php?key={userKey}&cmd=getMember&history");

                        if (globalResponse.IsSuccessStatusCode && sessionResponse.IsSuccessStatusCode)
                        {
                            string resultGlobal = await globalResponse.Content.ReadAsStringAsync();
                            string resultSession = await sessionResponse.Content.ReadAsStringAsync();
                            
                            GlobalUserKey = userKey;                          

                            
                            var jsonDocument1 = JsonDocument.Parse(resultGlobal);
                            var jsonDocument2 = JsonDocument.Parse(resultSession);
                            var root = jsonDocument1.RootElement;
                            var root2 = jsonDocument2.RootElement;
                            Root2 = jsonDocument2.RootElement;

                            
                            if (root.TryGetProperty("username", out var memberProperty) && memberProperty.ValueKind == JsonValueKind.String)
                            {
                                GlobalUsername = memberProperty.GetString();
                            }
                            else
                            {
                                GlobalUsername = "default";
                            }

                            if (root.TryGetProperty("level", out var levelProperty) && levelProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalLevel = levelProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalLevel = "0";
                            }

                            if (root.TryGetProperty("protection", out var protectionProperty) && protectionProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalProtection = protectionProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalProtection = "0";
                            }

                            if (root.TryGetProperty("fid", out var fidProperty) && fidProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalFid = fidProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalFid = "0";
                            }

                            if (root.TryGetProperty("unread_conversations", out var unreadConversationsProperty) && unreadConversationsProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalUnreadConversations = unreadConversationsProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalUnreadConversations = "0";
                            }

                            if (root.TryGetProperty("unread_alerts", out var unreadAlertsProperty) && unreadAlertsProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalUnreadAlerts = unreadAlertsProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalUnreadAlerts = "0";
                            }

                            if (root.TryGetProperty("register_date", out var registerDateProperty) && registerDateProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalRegisterDate = DateTimeOffset.FromUnixTimeSeconds(registerDateProperty.GetInt64()).DateTime.ToString();
                            }
                            else
                            {
                                GlobalRegisterDate = DateTime.Now.ToString();
                            }

                            if (root.TryGetProperty("posts", out var postsProperty) && postsProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalPosts = postsProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalPosts = "0";
                            }

                            if (root.TryGetProperty("score", out var scoreProperty) && scoreProperty.ValueKind == JsonValueKind.Number)
                            {
                                GlobalScore = scoreProperty.GetInt32().ToString();
                            }
                            else
                            {
                                GlobalScore = "0";
                            }

                            if (root.TryGetProperty("avatar", out var avatarURLProperty) && avatarURLProperty.ValueKind == JsonValueKind.String)
                            {
                                AvatarURL = avatarURLProperty.GetString();
                            }
                            else
                            {
                                AvatarURL = "default";
                            }

                            if (root.TryGetProperty("configuration", out var configProperty) && configProperty.ValueKind == JsonValueKind.Object)
                            {
                                GlobalConfig = configProperty.GetRawText();
                            }
                            else
                            {
                                GlobalConfig = "{}";
                            }

                            if (root2.TryGetProperty("session_history", out var sessionProperty) && sessionProperty.ValueKind == JsonValueKind.Object)
                            {
                                SessionHistory = sessionProperty.GetRawText();
                            }      
                            else
                            {
                                SessionHistory = "{}";
                            }
                            
                            this.Invoke(new Action(() => {
                                Dashboard Dashboard = new Dashboard();
                                Dashboard.FormClosed += (s, args) => Application.Exit();
                                Dashboard.Show();
                                this.Hide();
                            }));
                        }
                        else
                        {
                            if (globalResponse.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                DialogResult dialogResult = MessageBox.Show("This typically means your session hash is mismatched or you don't have a session.\n\nRun launch.bat to set a session?", "Error: Unauthorized", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    string pathToBatchFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.bat");
                                    if (File.Exists(pathToBatchFile))
                                    {
                                        try
                                        {
                                            Process.Start(pathToBatchFile);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Exception: {ex.Message}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Error: {globalResponse.StatusCode}");
                            }
                        }
                    }
                    catch (KeyNotFoundException knfEx)
                    {
                        MessageBox.Show($"Key not found: {knfEx.Message}\nException Source: {knfEx.Source}\nException Stack Trace: {knfEx.StackTrace}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Exception: {ex.Message}\nException Source: {ex.Source}\nException Stack Trace: {ex.StackTrace}");
                    }
                }
            };
        }

        #endregion
    }
}