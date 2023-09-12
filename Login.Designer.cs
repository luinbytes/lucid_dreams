using System;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace lucid_dreams
{
    public class Config
    {
        public string UserKey { get; set; }
        // Add more properties as needed
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
            config = LoadConfig();
            SaveConfig(config);
            // Now you can use config.UserKey and other properties
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig(config);
        }

        public static string GlobalUserKey;
        public static string GlobalUsername;
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
       
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink800, Primary.Pink900, Primary.Pink500, Accent.Pink200, TextShade.WHITE);
            
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 160);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Lucid Dreams - Login";

            MaterialTextBox keyTextBox = new MaterialTextBox();
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
                        string url = $"https://constelia.ai/api.php?key={userKey}&cmd=getMember&username&level&protection&fid&unread_conversations&unread_alerts&register_date&posts&score&avatar";
                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            // Set the global user key
                            GlobalUserKey = userKey;                            

                            // Parse the response
                            var jsonDocument = JsonDocument.Parse(result);
                            var root = jsonDocument.RootElement;

                            // Set the global variables
                            GlobalUsername = root.TryGetProperty("username", out var memberProperty) ? memberProperty.GetString() : null;
                            GlobalLevel = root.TryGetProperty("level", out var levelProperty) ? levelProperty.GetInt32().ToString() : null;
                            GlobalProtection = root.TryGetProperty("protection", out var protectionProperty) ? protectionProperty.GetInt32().ToString() : null;
                            GlobalFid = root.TryGetProperty("fid", out var fidProperty) ? fidProperty.GetInt32().ToString() : null;
                            GlobalUnreadConversations = root.TryGetProperty("unread_conversations", out var unreadConversationsProperty) ? unreadConversationsProperty.GetInt32().ToString() : null;
                            GlobalUnreadAlerts = root.TryGetProperty("unread_alerts", out var unreadAlertsProperty) ? unreadAlertsProperty.GetInt32().ToString() : null;
                            GlobalRegisterDate = root.TryGetProperty("register_date", out var registerDateProperty) ? DateTimeOffset.FromUnixTimeSeconds(registerDateProperty.GetInt64()).DateTime.ToString() : null;
                            GlobalPosts = root.TryGetProperty("posts", out var postsProperty) ? postsProperty.GetInt32().ToString() : null;
                            GlobalScore = root.TryGetProperty("score", out var scoreProperty) ? scoreProperty.GetInt32().ToString() : null;
                            AvatarURL = root.TryGetProperty("avatar", out var avatarURLProperty) ? avatarURLProperty.GetString() : null;  
                            GlobalConfig = root.TryGetProperty("configuration", out var configProperty) ? configProperty.GetRawText() : default;                      

                            // Open Dashboard and close Login
                            this.Invoke(new Action(() => {
                                Dashboard Dashboard = new Dashboard();
                                Dashboard.FormClosed += (s, args) => Application.Exit();
                                Dashboard.Show();
                                this.Hide();
                            }));
                        }
                        else
                        {
                            MessageBox.Show($"Error: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Exception: {ex.Message}");
                    }
                }
            };
        }

        #endregion
    }
}