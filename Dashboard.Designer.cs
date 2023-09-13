using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;
using Newtonsoft.Json;
using System.Data;

namespace lucid_dreams
{

    public partial class Dashboard : MaterialForm
    {
        string userkey = Login.GlobalUserKey;
        string username = Login.GlobalUsername;
        string level = Login.GlobalLevel;
        string protection = Login.GlobalProtection;
        string fid = Login.GlobalFid;
        string unreadConversations = Login.GlobalUnreadConversations;
        string unreadAlerts = Login.GlobalUnreadAlerts;
        string registerDate = Login.GlobalRegisterDate;
        string posts = Login.GlobalPosts;
        string score = Login.GlobalScore;
        string avatarURL = Login.AvatarURL;

        private PictureBox avatarPictureBox;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector;

        private System.ComponentModel.IContainer components = null;
        private Label usernameLabel;

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
            public List<string> Team { get; set; }
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

            // Tab Selector
            this.materialTabSelector.BaseTabControl = this.materialTabControl;
            this.materialTabSelector.Depth = 0;
            this.materialTabControl.Dock = DockStyle.Fill;
            this.materialTabSelector.Location = new System.Drawing.Point(-10, 64);
            this.materialTabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector.Size = new System.Drawing.Size(900, 28);
            this.materialTabSelector.TabIndex = 0;

            // Tab Control
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

            MaterialSkin.Controls.MaterialComboBox testOptionsComboBox = new MaterialSkin.Controls.MaterialComboBox
            {
                Location = new Point(625, 520), // Adjust these values to position the ComboBox
                AutoResize = true,
                Size = new Size(200, 26), // Adjust these values to change the size of the ComboBox
                AutoCompleteSource = AutoCompleteSource.ListItems, // Set the AutoCompleteSource to ListItems so the ComboBox suggests items as the user types
                AutoCompleteMode = AutoCompleteMode.SuggestAppend // Set the AutoCompleteMode to SuggestAppend so the ComboBox appends the suggested text to the user's input
            };

            // Add the test options to the ComboBox
            int protectionLevel;
            if (int.TryParse(Login.GlobalProtection, out protectionLevel))
            {
                testOptionsComboBox.Items.AddRange(new string[] { "Standard", "Zombie", "Kernel", "Min (Usr)", "Min (Ker)" });
                testOptionsComboBox.SelectedIndex = protectionLevel;
            }
            else
            {
                MessageBox.Show("Error!");
            }

            // Create a new MaterialButton for setting the protection
            MaterialSkin.Controls.MaterialButton setProtectionButton = new MaterialSkin.Controls.MaterialButton
            {
                AutoSize = false,
                Location = new Point(testOptionsComboBox.Location.X + testOptionsComboBox.Width + 10, testOptionsComboBox.Location.Y), // Position the Button next to the ComboBox
                Size = new Size(100, 49), // Adjust these values to change the size of the Button
                Text = "Set Protection" // Set the Text to "Set Protection"
            };

            setProtectionButton.Click += async (sender, e) =>
            {
                try
                {
                    // Make an API call to Constelia AI to set the protection
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=setProtection&protection={testOptionsComboBox.SelectedIndex}");
                                
                    if (response.IsSuccessStatusCode)
                    {

                        // Change the button text
                        setProtectionButton.Text = "Protection set!";

                        // Create a timer
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 5000; // Set the timer interval to 5 seconds

                        // Add an event handler for the Tick event
                        timer.Tick += (s, ea) =>
                        {
                            // Reset the button text
                            setProtectionButton.Text = "Set Protection";

                            // Stop the timer
                            timer.Stop();
                        };

                        // Start the timer
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

            // Add the Button to the "User Control" tab
            this.materialTabControl.TabPages[0].Controls.Add(setProtectionButton);
            

            // Add the MaterialComboBox to the "Configuration" tab
            this.materialTabControl.TabPages[0].Controls.Add(testOptionsComboBox);
            

            JsonDocument document = JsonDocument.Parse(Login.GlobalConfig);
            string formattedJson = System.Text.Json.JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });

            // Create a new TextBox
            // Create a new MaterialMultiLineTextBox
            MaterialSkin.Controls.MaterialMultiLineTextBox multilineTextField = new MaterialSkin.Controls.MaterialMultiLineTextBox
            {
                Location = new Point(10, 40), // Adjust these values to position the TextBox
                Size = new Size(680, 520), // Adjust these values to change the size of the TextBox
                Multiline = true, // Set Multiline to true
                //ScrollBars = ScrollBars.Both, // Add both vertical and horizontal scrollbars
                Text = formattedJson // Set the Text to the GlobalConfig
            };

            // Add the MaterialMultiLineTextBox to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(multilineTextField);

            // Create a new Button for loading the configuration
            MaterialSkin.Controls.MaterialButton loadConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, multilineTextField.Location.Y), // Position the Button to the right of the TextBox
                Size = new Size(100, 36), // Adjust these values to change the size of the Button
                Text = "Load Config" // Set the Text to "Load Config"
            };

            loadConfigButton.Click += async (sender, e) =>
            {
                try
                {
                    // Make an API call to Constelia AI to get the updated configuration
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=getConfiguration"); // Replace with the actual API endpoint
                            
                    if (response.IsSuccessStatusCode)
                    {
                        string config = await response.Content.ReadAsStringAsync();

                        // Parse the JSON and format it
                        JsonDocument document = JsonDocument.Parse(config);
                        string formattedJson = System.Text.Json.JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });

                        // Update the TextBox with the new configuration
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

            // Add the Button to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(loadConfigButton);

            // Create a new Button for saving the configuration
            MaterialSkin.Controls.MaterialButton saveConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, multilineTextField.Location.Y + loadConfigButton.Height + 10), // Position the Button below the Load Config Button
                Size = new Size(100, 36), // Adjust these values to change the size of the Button
                Text = "Save Config" // Set the Text to "Save Config"
            };

            saveConfigButton.Click += async (sender, e) =>
            {
                try
                {
                    // Get the configuration from the TextBox
                    string config = multilineTextField.Text;

                    // URL-encode the configuration
                    string encodedConfig = HttpUtility.UrlEncode(config);

                    // Create a new HttpClient
                    HttpClient client = new HttpClient();

                    // Create the content for the POST request
                    StringContent content = new StringContent($"value={encodedConfig}", Encoding.UTF8, "application/x-www-form-urlencoded");

                    // Make a POST request to the Constelia AI API
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
                    // Display the error message
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            };

            // Add the Button to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(saveConfigButton);

            // Create a new Button for resetting the configuration
            int resetClickCount = 0;
            MaterialSkin.Controls.MaterialButton resetConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, multilineTextField.Location.Y + saveConfigButton.Height + loadConfigButton.Height + 20), // Position the Button below the Save Config Button
                AutoSize = false,
                Size = new Size(115, 36), // Adjust these values to change the size of the Button
                Text = "Reset"
            };

            resetConfigButton.Click += async (sender, e) =>
            {
                // Increment the counter
                resetClickCount++;

                // Update the button text
                resetConfigButton.Text = $"Reset ({resetClickCount})";

                // Check if the counter has reached 5
                if (resetClickCount >= 5)
                {
                    // Reset the configuration in the TextBox to the original configuration
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=resetConfiguration");

                    if (response.IsSuccessStatusCode)
                    {
                        // Reset the counter
                        resetClickCount = 0;

                        // Change the button text
                        resetConfigButton.Text = "Config Reset!";

                        // Create a timer
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 3000; // Set the timer interval to 3 seconds

                        // Add an event handler for the Tick event
                        timer.Tick += (s, ea) =>
                        {
                            // Reset the button text
                            resetConfigButton.Text = "Reset";

                            // Stop the timer
                            timer.Stop();
                        };

                        // Start the timer
                        timer.Start();
                    }
                    else
                    {
                        MessageBox.Show("Failed to reset the config.");
                    }
                }
            };

            // Add the Button to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(resetConfigButton);
            
            // Create a new Button for FAQ
            MaterialSkin.Controls.MaterialButton faqConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, 520), // Position the Button below the Reset Config Button
                AutoSize = false,
                Size = new Size(115, 36), // Adjust these values to change the size of the Button
                Text = "FAQ" // Set the Text to "FAQ"
            };

            faqConfigButton.Click += (sender, e) =>
            {
                MessageBox.Show("Q: Error! The input does not contain any JSON tokens.\nA: Your config was most likely reset and is now empty. I'm not smart enough to figure out the error handling so here I am, a lonely, ugly messagebox.\nYou're welcome.\n\nQ: WTHECK is Gen Config?\nA: Reset your config by accident? Hit this to generate a default config.", "Lucid Dreams - FAQ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Add the Button to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(faqConfigButton);

            MaterialSkin.Controls.MaterialButton generateConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 30, 475), // Position the Button below the Reset Config Button
                AutoSize = false,
                Size = new Size(115, 36), // Adjust these values to change the size of the Button
                Text = "Gen Config" // Set the Text to "FAQ"
            };

            generateConfigButton.Click += (sender, e) =>
            {
                // Get the configuration from the TextBox
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

                // Set the TextBox contents to the generated config
                multilineTextField.Text = config;
            };

            // Add the Button to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(generateConfigButton);
            
            List<Script> scripts = null;
            this.materialTabControl.Selected += async (sender, e) =>
            {
                if (e.TabPageIndex == 2) // Replace 1 with the index of the "Scripts" tab
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

                            // Parse the JSON into a list of scripts
                            List<Script> scripts = JsonConvert.DeserializeObject<List<Script>>(scriptsJson);
                            List<Script> activeScripts = responseObj.Scripts;
                            this.materialTabControl.TabPages[2].Dock = DockStyle.Fill;
                            // Create a new DataGridView and set its properties
                            DataGridView scriptsDataGridView = new DataGridView
                            {
                                Location = new Point(0, 30), // Adjust these values to position the DataGridView
                                Size = new Size(860, 540), // Adjust these values to change the size of the DataGridView
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
                                    BackColor = Color.FromArgb(173, 20, 87) // Indigo 500
                                },
                                RowHeadersVisible = false,
                                GridColor = Color.FromArgb(189, 189, 189), // Gray 400
                                EnableHeadersVisualStyles = false,
                                //Dock = DockStyle.Fill
                            };

                            // Add columns to the DataGridView
                            scriptsDataGridView.Columns.Add("Id", "ID");
                            scriptsDataGridView.Columns.Add("Name", "Name");
                            scriptsDataGridView.Columns.Add("Author", "Author");
                            scriptsDataGridView.Columns.Add("LastUpdate", "Last Updated");

                            // Set the AutoSizeColumnsMode property for all columns
                            scriptsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                            // Set the AutoSizeMode property for the last column
                            scriptsDataGridView.Columns[scriptsDataGridView.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                            // Add a checkbox column for the status
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

                                // Convert Unix time to DateTime
                                DateTime lastUpdate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(script.Last_update)).DateTime;

                                // Add a row to the DataGridView for each script
                                scriptsDataGridView.Rows.Add(script.Id, script.Name, script.Author, lastUpdate, isActive);
                            }

                            // Create a new DataGridViewButtonColumn
                            DataGridViewButtonColumn moreInfoButtonColumn = new DataGridViewButtonColumn
                            {
                                Name = "MoreInfo",
                                HeaderText = "Info",
                                Text = "Info",
                                UseColumnTextForButtonValue = true
                            };

                            // Add the DataGridViewButtonColumn to the DataGridView
                            scriptsDataGridView.Columns.Add(moreInfoButtonColumn);

                            // Create a new DataGridViewButtonColumn
                            DataGridViewButtonColumn forumButtonColumn = new DataGridViewButtonColumn
                            {
                                Name = "Forum",
                                HeaderText = "Forum",
                                Text = "Forum",
                                UseColumnTextForButtonValue = true
                            };

                            // Add the DataGridViewButtonColumn to the DataGridView
                            scriptsDataGridView.Columns.Add(forumButtonColumn);

                            // Handle the CellClick event to perform an action when the button is clicked
                            scriptsDataGridView.CellClick += (sender, e) =>
                            {
                                // Check if the clicked cell is in the "Forum" column
                                if (scriptsDataGridView.Columns[e.ColumnIndex].Name == "Forum")
                                {
                                    // Get the ID of the script in the clicked row
                                    string scriptId = scriptsDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                                    // Find the script with the given ID
                                    Script script = scripts.Find(s => s.Id == scriptId);

                                    // Open the script's forum URL in the default browser
                                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                    {
                                        FileName = script.Forums,
                                        UseShellExecute = true
                                    });
                                }
                            };

                            // Handle the CellClick event to perform an action when the button is clicked
                            scriptsDataGridView.CellClick += (sender, e) =>
                            {
                                // Check if the clicked cell is in the "MoreInfo" column
                                if (scriptsDataGridView.Columns[e.ColumnIndex].Name == "MoreInfo")
                                {
                                    // Get the ID of the script in the clicked row
                                    string scriptId = scriptsDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                                    // Find the script with the given ID
                                    Script script = scripts.Find(s => s.Id == scriptId);
                                    DateTime lastUpdate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(script.Last_update)).DateTime;
                                    // Create the message with the script info
                                    string message = $"Name: {script.Name}\n" +
                                                    $"Author: {script.Author}\n" +
                                                    $"Last Updated: {lastUpdate}\n" +
                                                    $"Is Core Script: {script.Core}\n" +
                                                    $"Is Library Script: {script.Library}\n" +
                                                    $"Elapsed Time: {script.Elapsed}\n\n" +
                                                    $"Update Notes: {script.Update_notes}";

                                    // Show the message box with the script info
                                    MessageBox.Show(message, "Script Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            };

                            // Handle the CellContentClick event to perform an action when a checkbox is clicked
                            scriptsDataGridView.CellContentClick += async (sender, e) =>
                            {
                                
                                if (e.RowIndex >= 0)
                                {
                                // Check if the clicked cell is in the "Enabled" column
                                    if (scriptsDataGridView.Columns[e.ColumnIndex].Name == "Active")
                                    {
                                        // Get the ID of the script in the clicked row
                                        string scriptId = scriptsDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                                        // Find the script with the given ID
                                        Script script = scripts.Find(s => s.Id == scriptId);

                                        // Get the new status of the script
                                        bool isEnabled = !(bool)scriptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                                        // Update the checkbox value manually
                                        scriptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = isEnabled;

                                        // Perform the API call
                                        HttpClient client = new HttpClient();
                                        HttpResponseMessage response = await client.GetAsync($"https://constelia.ai/api.php?key={userkey}&cmd=toggleScriptStatus&id={scriptId}");

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            // Display an error message if the API call failed
                                            MessageBox.Show($"Failed to set the status of the script. Error: {response.StatusCode}");
                                        }
                                    }
                                }
                            };

                            // Add the DataGridView to the "Scripts" tab
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