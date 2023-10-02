using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Data;

namespace lucid_dreams
{

    public partial class Dashboard : MaterialForm
    {
        string version = "v0.6.8.1";
        string userkey = Login.GlobalUserKey;
        string username = Login.GlobalUsername;
        string linkKey = Login.GlobalKeyLink;
        string stopKey = Login.GlobalKeyStop;
        string level = Login.GlobalLevel;
        string protection = Login.GlobalProtection;
        string fid = Login.GlobalFid;
        string unreadConversations = Login.GlobalUnreadConversations;
        string unreadAlerts = Login.GlobalUnreadAlerts;
        string registerDate = Login.GlobalRegisterDate;
        string posts = Login.GlobalPosts;
        string score = Login.GlobalScore;
        string avatarURL = Login.AvatarURL;
        string sessionHistory = Login.SessionHistory;

        private PictureBox avatarPictureBox;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector;

        private System.ComponentModel.IContainer components = null;
        private Label usernameLabel;

        Dictionary<int, string> keyCodes = new Dictionary<int, string>
        {
            { 1, "Left Click" },
            { 2, "Right Click" },
            { 3, "Cancel" },
            { 4, "Middle Mouse" },
            { 5, "Mouse 4" },
            { 6, "Mouse 5" },
            { 8, "Back" },
            { 9, "Tab" },
            { 12, "Clear" },
            { 13, "Return" },
            { 16, "Shift" },
            { 17, "Control" },
            { 18, "Menu" },
            { 19, "Pause" },
            { 20, "Caps Lock" },
            { 21, "Kana" },
            { 22 , "Undefined 1" },
            { 23 , "IME Junja mode" },
            { 24 , "IME Final mode" },
            { 25 , "" },
            { 26 , "" },
            { 27 , "" },
            { 28 , "" },
            { 29 , "" },
            { 30 , "" },
            { 31 , "" },
            { 32 , "" },
            { 33 , "" },
            { 34 , "" },
        };

        public class Script
        {
            public string Id { get; set; }
            public string Software { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
            public string Last_update { get; set; }
            public string Update_notes { get; set; }
            
            [JsonPropertyName("script")]
            public string ScriptContent { get; set; }
            
            public string Core { get; set; }
            public string Forums { get; set; }
            public string Library { get; set; }
            
            public string Elapsed { get; set; }
        }

        public class ApiResponse
        {
            public List<Script> Scripts { get; set; }
        }

        private void InitializeComponent()
        {

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink800, Primary.Pink900, Primary.Pink500, Accent.Pink200, TextShade.WHITE);
            
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Sizable = false;
            this.Text = "Lucid Dreams - Dashboard";

            this.materialTabSelector = new MaterialTabSelector();
            this.materialTabControl = new MaterialTabControl();

            
            this.materialTabSelector.BaseTabControl = this.materialTabControl;
            this.materialTabSelector.Depth = 0;
            this.materialTabControl.Dock = DockStyle.Fill;
            this.materialTabSelector.Location = new System.Drawing.Point(-10, 64);
            this.materialTabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector.Size = new System.Drawing.Size(900, 28);
            this.materialTabSelector.TabIndex = 0;

            
            this.materialTabControl.Depth = 0;
            this.materialTabControl.Location = new System.Drawing.Point(10, 84);
            this.materialTabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl.Size = new System.Drawing.Size(900, 452);
            this.materialTabControl.ItemSize = new Size(20, 20);
            this.materialTabControl.TabIndex = 1;

            this.materialTabControl.TabPages.Add(new TabPage("User Control"));
            this.materialTabControl.TabPages.Add(new TabPage("Configuration"));
            this.materialTabControl.TabPages.Add(new TabPage("Scripts"));
            this.materialTabControl.TabPages.Add(new TabPage("Team"));
            this.materialTabControl.TabPages.Add(new TabPage("Settings"));

            this.Controls.Add(this.materialTabSelector);
            this.Controls.Add(this.materialTabControl);

            PictureBox avatarPictureBox = new PictureBox
            {
                Size = new Size(100, 100),
                ImageLocation = avatarURL,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            avatarPictureBox.Location = new Point(3, this.ClientSize.Height - avatarPictureBox.Height - 30);

            this.materialTabControl.TabPages[0].Controls.Add(avatarPictureBox);

            Label usernameLabel = new Label
            {
                Location = new Point(avatarPictureBox.Width + 5, this.ClientSize.Height - avatarPictureBox.Height + 55),
                Text = $"Welcome {username}!",
                AutoSize = true
            };
            
            this.materialTabControl.TabPages[0].Controls.Add(usernameLabel);

            Label versionLabel = new Label
            {
                Location = new Point(avatarPictureBox.Width + 5, this.ClientSize.Height - avatarPictureBox.Height + 35),
                Text = $"Lucid Dreams {version}",
                AutoSize = true
            };
            
            this.materialTabControl.TabPages[0].Controls.Add(versionLabel);
            
            Panel forumStatsPanel = new Panel
            {
                Location = new Point(10, 40),
                Size = new Size(200, 120),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label forumStatsLabel = new Label
            {
                Location = new Point(0, -2),
                Text = "Forum Stats",
                BackColor = Color.Gray,
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(forumStatsLabel);

            Label unreadConvLabel = new Label
            {
                Location = new Point(5, 20),
                Text = $"Unread Messages: {unreadConversations}",
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(unreadConvLabel);

            Label unreadAlertLabel = new Label
            {
                Location = new Point(5, 35),
                Text = $"Unread Messages: {unreadAlerts}",
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(unreadAlertLabel);

            Label scoreLabel = new Label
            {
                Location = new Point(5, 50),
                Text = $"Score: {score}",
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(scoreLabel);

            Label postsLabel = new Label
            {
                Location = new Point(5, 65),
                Text = $"Posts: {posts}",
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(postsLabel);

            Label fidLabel = new Label
            {
                Location = new Point(5, 80),
                Text = $"Fantasy ID: {fid}",
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(fidLabel);

            Label regLabel = new Label
            {
                Location = new Point(5, 95),
                Text = $"Registered: {registerDate}",
                AutoSize = true
            };

            forumStatsPanel.Controls.Add(regLabel);

            this.materialTabControl.TabPages[0].Controls.Add(forumStatsPanel);

            Panel sessionStatsPanel = new Panel
            {
                Location = new Point(220, 40),
                Size = new Size(500, 120),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            Label sessionStatsLabel = new Label
            {
                Location = new Point(0, -2),
                Text = "Session Stats",
                BackColor = Color.Gray,
                AutoSize = true
            };

            sessionStatsPanel.Controls.Add(sessionStatsLabel);

            if (Login.Root2.TryGetProperty("session_history", out var sessionProperty))
            {
                var sessionHistoryObj = JsonDocument.Parse(sessionProperty.GetRawText());

                StringBuilder successText = new StringBuilder();
                successText.AppendLine("Success History:");

                if (sessionHistoryObj.RootElement.TryGetProperty("success", out var successArrayProperty) && successArrayProperty.ValueKind == JsonValueKind.Array)
                {
                    var successArray = successArrayProperty.EnumerateArray();
                    foreach (var successItem in successArray)
                    {
                        if (successItem.TryGetProperty("time", out var timeProperty) && timeProperty.ValueKind == JsonValueKind.String)
                        {
                            long time = long.Parse(timeProperty.GetString());
                            DateTime timeConverted = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;

                            string software = successItem.GetProperty("software").GetString();
                            string version = successItem.GetProperty("version").GetString();
                            string directory = successItem.GetProperty("directory").GetString();

                            successText.AppendLine($"Time: {timeConverted}, Software: {software}, Version: {version}, DIR: {directory}");
                        }
                    }
                }

                Label successLabel = new Label
                {
                    Location = new Point(5, 20),
                    Text = successText.ToString(),
                    AutoSize = true
                };

                sessionStatsPanel.Controls.Add(successLabel);

                StringBuilder failureText = new StringBuilder();
                failureText.AppendLine("\nFailure History:");

                if (sessionHistoryObj.RootElement.TryGetProperty("failure", out var failureArrayProperty) && failureArrayProperty.ValueKind == JsonValueKind.Array)
                {
                    var failureArray = failureArrayProperty.EnumerateArray();
                    foreach (var failureItem in failureArray)
                    {
                        if (failureItem.TryGetProperty("time", out var timeProperty) && timeProperty.ValueKind == JsonValueKind.String)
                        {
                            long time = long.Parse(timeProperty.GetString());
                            DateTime timeConverted = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;

                            string software = failureItem.GetProperty("software").GetString();
                            string version = failureItem.GetProperty("version").GetString();
                            string directory = failureItem.GetProperty("directory").GetString();

                            failureText.AppendLine($"Time: {timeConverted}, Software: {software}, Version: {version}, DIR: {directory}");
                        }
                    }
                }

                Label failureLabel = new Label
                {
                    Location = new Point(5, successLabel.Height + 30),
                    Text = failureText.ToString(),
                    AutoSize = true,
                    ForeColor = Color.Red
                };

                sessionStatsPanel.Controls.Add(failureLabel);
            }

            this.materialTabControl.TabPages[0].Controls.Add(sessionStatsPanel);

            MaterialSkin.Controls.MaterialButton launchUniverseButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(10, 165),
                Size = new Size(200, 36), 
                Text = "Launch Universe4",
                AutoSize = false
            };

            this.materialTabControl.TabPages[0].Controls.Add(launchUniverseButton);

            launchUniverseButton.Click += (sender, e) =>
            {
                string pathToBatchFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.bat");
                if (File.Exists(pathToBatchFile))
                {
                    Process.Start("cmd.exe", $"/c start \"\" \"{pathToBatchFile}\"");
                }
                else
                {
                    MessageBox.Show("Error: File launch.bat does not exist.");
                }
            };

            MaterialSkin.Controls.MaterialButton launchConstellationButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(10, 210),
                Size = new Size(200, 36), 
                Text = "Launch Constellation4",
                AutoSize = false
            };

            this.materialTabControl.TabPages[0].Controls.Add(launchConstellationButton);

            launchConstellationButton.Click += (sender, e) =>
            {
                string pathToBatchFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "constellation.bat");
                if (File.Exists(pathToBatchFile))
                {
                    Process.Start("cmd.exe", $"/c start \"\" \"{pathToBatchFile}\"");
                }
                else
                {
                    MessageBox.Show("Error: File constellation.bat does not exist.");
                }
            };

            //To-Do
            MaterialSkin.Controls.MaterialTextBox linkKeyBindBox = new MaterialSkin.Controls.MaterialTextBox
            {
                Location = new Point(10, 400),
                Size = new Size(200, 36),
                ReadOnly = true,
                Text = ($"{linkKey}")
            };

            linkKeyBindBox.KeyDown += (sender, e) =>
            {
                linkKeyBindBox.Text = Enum.GetName(typeof(Keys), e.KeyCode);
                e.SuppressKeyPress = true;
            };

            linkKeyBindBox.MouseDown += (sender, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        linkKeyBindBox.Text = "VK_LBUTTON";
                        break;
                    case MouseButtons.Right:
                        linkKeyBindBox.Text = "VK_RBUTTON";
                        break;
                    case MouseButtons.Middle:
                        linkKeyBindBox.Text = "VK_MBUTTON";
                        break;
                    //TODO add the rest lmao lazy bastard
                }
            };

            this.materialTabControl.TabPages[0].Controls.Add(linkKeyBindBox);

            MaterialSkin.Controls.MaterialComboBox protectionComboBox = new MaterialSkin.Controls.MaterialComboBox
            {
                Location = new Point(625, 520),
                AutoResize = true,
                Size = new Size(200, 26),
                AutoCompleteSource = AutoCompleteSource.ListItems,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend
            };

            int protectionLevel;
            if (int.TryParse(Login.GlobalProtection, out protectionLevel))
            {
                protectionComboBox.Items.AddRange(new string[] { "Standard", "Zombie", "Kernel", "Min (Usr)", "Min (Ker)" });
                protectionComboBox.SelectedIndex = protectionLevel;
            }
            else
            {
                MessageBox.Show("Error!");
            }

            MaterialSkin.Controls.MaterialButton setProtectionButton = new MaterialSkin.Controls.MaterialButton
            {
                AutoSize = false,
                Location = new Point(protectionComboBox.Location.X + protectionComboBox.Width + 10, protectionComboBox.Location.Y), 
                Size = new Size(100, 49), 
                Text = "Set Protection" 
            };

            setProtectionButton.Click += async (sender, e) =>
            {
                try
                {
                    
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=setProtection&protection={protectionComboBox.SelectedIndex}");
                                
                    if (response.IsSuccessStatusCode)
                    {

                        
                        setProtectionButton.Text = "Protection set!";

                        
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 5000; 

                        
                        timer.Tick += (s, ea) =>
                        {
                            
                            setProtectionButton.Text = "Set Protection";

                            
                            timer.Stop();
                        };

                        
                        timer.Start();
                    }
                    else
                    {
                        MessageBox.Show("Failed to set the protection.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            };

            
            this.materialTabControl.TabPages[0].Controls.Add(setProtectionButton);
            

            
            this.materialTabControl.TabPages[0].Controls.Add(protectionComboBox);
            

            JsonDocument document = JsonDocument.Parse(Login.GlobalConfig);
            string formattedJson = System.Text.Json.JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });

            
            
            MaterialSkin.Controls.MaterialMultiLineTextBox multilineTextField = new MaterialSkin.Controls.MaterialMultiLineTextBox
            {
                Location = new Point(10, 40), 
                Size = new Size(680, 520), 
                Multiline = true, 
                
                Text = formattedJson 
            };

            
            this.materialTabControl.TabPages[1].Controls.Add(multilineTextField);

            
            MaterialSkin.Controls.MaterialButton loadConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, multilineTextField.Location.Y), 
                Size = new Size(100, 36), 
                Text = "Load Config" 
            };

            loadConfigButton.Click += async (sender, e) =>
            {
                try
                {
                    
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=getConfiguration");
                            
                    if (response.IsSuccessStatusCode)
                    {
                        string config = await response.Content.ReadAsStringAsync();

                        
                        JsonDocument document = JsonDocument.Parse(config);
                        string formattedJson = System.Text.Json.JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });

                        
                        multilineTextField.Text = formattedJson;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load the configuration.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            };

            
            this.materialTabControl.TabPages[1].Controls.Add(loadConfigButton);

            
            MaterialSkin.Controls.MaterialButton saveConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, multilineTextField.Location.Y + loadConfigButton.Height + 10), 
                Size = new Size(100, 36), 
                Text = "Save Config" 
            };

            saveConfigButton.Click += async (sender, e) =>
            {
                try
                {
                    
                    string config = multilineTextField.Text;

                    
                    string encodedConfig = HttpUtility.UrlEncode(config);

                    
                    HttpClient client = new HttpClient();

                    
                    StringContent content = new StringContent($"value={encodedConfig}", Encoding.UTF8, "application/x-www-form-urlencoded");

                    
                    HttpResponseMessage response = await client.PostAsync($"https://constelia.ai/api.php?key={userkey}&cmd=setConfiguration", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Configuration saved successfully.");
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            };

            
            this.materialTabControl.TabPages[1].Controls.Add(saveConfigButton);

            
            int resetClickCount = 0;
            MaterialSkin.Controls.MaterialButton resetConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, multilineTextField.Location.Y + saveConfigButton.Height + loadConfigButton.Height + 20), 
                AutoSize = false,
                Size = new Size(115, 36), 
                Text = "Reset"
            };

            resetConfigButton.Click += async (sender, e) =>
            {
                
                resetClickCount++;

                
                resetConfigButton.Text = $"Reset ({resetClickCount})";

                
                if (resetClickCount >= 5)
                {
                    
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=resetConfiguration");

                    if (response.IsSuccessStatusCode)
                    {
                        
                        resetClickCount = 0;

                        
                        resetConfigButton.Text = "Config Reset!";

                        
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 3000; 

                        
                        timer.Tick += (s, ea) =>
                        {
                            
                            resetConfigButton.Text = "Reset";

                            
                            timer.Stop();
                        };

                        
                        timer.Start();
                    }
                    else
                    {
                        MessageBox.Show("Failed to reset the config.");
                    }
                }
            };

            
            this.materialTabControl.TabPages[1].Controls.Add(resetConfigButton);
            
            
            MaterialSkin.Controls.MaterialButton faqConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, 520), 
                AutoSize = false,
                Size = new Size(115, 36), 
                Text = "FAQ" 
            };

            faqConfigButton.Click += (sender, e) =>
            {
                MessageBox.Show("Q: Error! The input does not contain any JSON tokens.\nA: Your config was most likely reset and is now empty. I'm not smart enough to figure out the error handling so here I am, a lonely, ugly messagebox.\nYou're welcome.\n\nQ: WTHECK is Gen Config?\nA: Reset your config by accident? Hit this to generate a default config.", "Lucid Dreams - FAQ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            
            this.materialTabControl.TabPages[1].Controls.Add(faqConfigButton);

            MaterialSkin.Controls.MaterialButton generateConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, 475), 
                AutoSize = false,
                Size = new Size(115, 36), 
                Text = "Gen Config" 
            };

            generateConfigButton.Click += (sender, e) =>
            {
                
                string config = @"{
                ""bones"": [4, 7, 10],
                ""constelia.lua"": null,
                ""constellation.lua"": {
                    ""esp"": false,
                    ""esp_fov"": 10,
                    ""esp_sonar"": false,
                    ""esp_surround"": true,
                    ""humanizer"": true,
                    ""humanizer_debug"": true,
                    ""humanizer_mouse_threshold"": 32,
                    ""humanizer_range_max"": 10,
                    ""humanizer_range_min"": 0.6,
                    ""iterations"": 1
                },
                ""fc2.lua"": {
                    ""anti_aliasing"": false,
                    ""change_compositor"": true,
                    ""fps_lock"": false,
                    ""linux_sound_command"": ""play -q"",
                    ""multicore"": null,
                    ""no_base"": false,
                    ""xdotool"": true,
                    ""zombie"": ""explorer.exe"",
                    ""zombie_mm"": true
                }
            }";

                
                multilineTextField.Text = config;
            };

            
            this.materialTabControl.TabPages[1].Controls.Add(generateConfigButton);
            
            List<Script> scripts = null;
            this.materialTabControl.Selected += async (sender, e) =>
            {
                if (e.TabPageIndex == 2) 
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=getAllScripts");
                        HttpResponseMessage response2 = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=getMember&scripts");

                        if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                        {
                            string scriptsJson = await response.Content.ReadAsStringAsync();
                            string activeScriptsJson = await response2.Content.ReadAsStringAsync();

                            ApiResponse responseObj = JsonConvert.DeserializeObject<ApiResponse>(activeScriptsJson);

                            
                            List<Script> scripts = JsonConvert.DeserializeObject<List<Script>>(scriptsJson);
                            List<Script> activeScripts = responseObj.Scripts;
                            this.materialTabControl.TabPages[2].Dock = DockStyle.Fill;
                            
                            DataGridView scriptsDataGridView = new DataGridView
                            {
                                Location = new Point(0, 30), 
                                Size = new Size(860, 540), 
                                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                                ReadOnly = true,
                                AllowUserToAddRows = false,
                                BackgroundColor = Color.White,
                                DefaultCellStyle = new DataGridViewCellStyle
                                {
                                    Font = new Font("Roboto", 12),
                                    ForeColor = Color.White,
                                    BackColor = Color.FromArgb(88, 88, 88)
                                },
                                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                                {
                                    Font = new Font("Roboto", 9, FontStyle.Bold),
                                    ForeColor = Color.White,
                                    BackColor = Color.FromArgb(173, 20, 87) 
                                },
                                RowHeadersVisible = false,
                                GridColor = Color.FromArgb(189, 189, 189), 
                                EnableHeadersVisualStyles = false,
                                
                            };

                            
                            scriptsDataGridView.Columns.Add("Id", "ID");
                            scriptsDataGridView.Columns.Add("Name", "Name");
                            scriptsDataGridView.Columns.Add("Author", "Author");
                            scriptsDataGridView.Columns.Add("LastUpdate", "Last Updated");

                            
                            scriptsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                            
                            scriptsDataGridView.Columns[scriptsDataGridView.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                            
                            DataGridViewCheckBoxColumn statusColumn = new DataGridViewCheckBoxColumn
                            {
                                HeaderText = "Active",
                                Name = "Active",
                                SortMode = DataGridViewColumnSortMode.Automatic
                            };
                            scriptsDataGridView.Columns.Add(statusColumn);

                            foreach (var script in scripts)
                            {
                                bool isActive = activeScripts.Any(activeScript => activeScript.Id == script.Id);

                                
                                DateTime lastUpdate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(script.Last_update)).DateTime;

                                
                                scriptsDataGridView.Rows.Add(script.Id, script.Name, script.Author, lastUpdate, isActive);
                            }

                            
                            DataGridViewButtonColumn moreInfoButtonColumn = new DataGridViewButtonColumn
                            {
                                Name = "MoreInfo",
                                HeaderText = "Info",
                                Text = "Info",
                                UseColumnTextForButtonValue = true
                            };

                            
                            scriptsDataGridView.Columns.Add(moreInfoButtonColumn);

                            
                            DataGridViewButtonColumn forumButtonColumn = new DataGridViewButtonColumn
                            {
                                Name = "Forum",
                                HeaderText = "Forum",
                                Text = "Forum",
                                UseColumnTextForButtonValue = true
                            };

                            
                            scriptsDataGridView.Columns.Add(forumButtonColumn);

                            
                            scriptsDataGridView.CellClick += (sender, e) =>
                            {
                                
                                if (scriptsDataGridView.Columns[e.ColumnIndex].Name == "Forum")
                                {
                                    
                                    string scriptId = scriptsDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                                    
                                    Script script = scripts.Find(s => s.Id == scriptId);

                                    
                                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                    {
                                        FileName = script.Forums,
                                        UseShellExecute = true
                                    });
                                }
                            };

                            
                            scriptsDataGridView.CellClick += (sender, e) =>
                            {
                                
                                if (scriptsDataGridView.Columns[e.ColumnIndex].Name == "MoreInfo")
                                {
                                    
                                    string scriptId = scriptsDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                                    
                                    Script script = scripts.Find(s => s.Id == scriptId);
                                    DateTime lastUpdate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(script.Last_update)).DateTime;
                                    
                                    string message = $"Name: {script.Name}\n" +
                                                    $"Author: {script.Author}\n" +
                                                    $"Last Updated: {lastUpdate}\n" +
                                                    $"Is Core Script: {script.Core}\n" +
                                                    $"Is Library Script: {script.Library}\n" +
                                                    $"Elapsed Time: {script.Elapsed}\n\n" +
                                                    $"Update Notes: {script.Update_notes}";

                                    
                                    MessageBox.Show(message, "Script Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            };

                            
                            scriptsDataGridView.CellContentClick += async (sender, e) =>
                            {
                                
                                if (e.RowIndex >= 0)
                                {
                                
                                    if (scriptsDataGridView.Columns[e.ColumnIndex].Name == "Active")
                                    {
                                        
                                        string scriptId = scriptsDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                                        
                                        Script script = scripts.Find(s => s.Id == scriptId);

                                        
                                        bool isEnabled = !(bool)scriptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                                        
                                        scriptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = isEnabled;

                                        
                                        HttpClient client = new HttpClient();
                                        HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=toggleScriptStatus&id={scriptId}");

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            
                                            MessageBox.Show($"Failed to set the status of the script. Error: {response.StatusCode}");
                                        }
                                    }
                                }
                            };

                            
                            this.materialTabControl.TabPages[2].Controls.Add(scriptsDataGridView);
                        }
                        else
                        {
                            MessageBox.Show("Failed to load the scripts.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            };
        }
    }
}