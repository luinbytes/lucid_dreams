using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Text.Json;
using System.Text;
using System.Web;

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

        private void InitializeComponent()
        {

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink800, Primary.Pink900, Primary.Pink500, Accent.Pink200, TextShade.WHITE);
            
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 500);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Lucid Dreams - Dashboard";

            this.materialTabSelector = new MaterialTabSelector();
            this.materialTabControl = new MaterialTabControl();

            // Tab Selector
            this.materialTabSelector.BaseTabControl = this.materialTabControl;
            this.materialTabSelector.Depth = 0;
            this.materialTabSelector.Location = new System.Drawing.Point(-10, 64);
            this.materialTabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector.Size = new System.Drawing.Size(800, 28);
            this.materialTabSelector.TabIndex = 0;

            // Tab Control
            this.materialTabControl.Depth = 0;
            this.materialTabControl.Location = new System.Drawing.Point(0, 84);
            this.materialTabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl.Size = new System.Drawing.Size(800, 452); // Adjust the width as needed
            this.materialTabControl.ItemSize = new Size(20, 20); // Adjust these values to change the size of the tabs
            this.materialTabControl.TabIndex = 1;

            // Tabs
            this.materialTabControl.TabPages.Add(new TabPage("User Control"));
            this.materialTabControl.TabPages.Add(new TabPage("Configuration"));
            this.materialTabControl.TabPages.Add(new TabPage("Scripts"));
            this.materialTabControl.TabPages.Add(new TabPage("Settings"));

            // Add the controls to the form
            this.Controls.Add(this.materialTabSelector);
            this.Controls.Add(this.materialTabControl);

            PictureBox avatarPictureBox = new PictureBox
            {
                Size = new Size(100, 100), // Adjust these values to change the size of the PictureBox
                ImageLocation = avatarURL, // Set the ImageLocation to the URL of the avatar
                SizeMode = PictureBoxSizeMode.StretchImage // Set the SizeMode to StretchImage so the avatar fits in the PictureBox
            };

            avatarPictureBox.Location = new Point(3, this.ClientSize.Height - avatarPictureBox.Height - 45);

            this.materialTabControl.TabPages[0].Controls.Add(avatarPictureBox);

            // Create a new Label
            Label usernameLabel = new Label
            {
                Location = new Point(avatarPictureBox.Width + 5, this.ClientSize.Height - avatarPictureBox.Height + 35), // Adjust these values to position the Label
                Text = $"Welcome {username}!", // Set the Text to the username
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the "User Control" tab
            this.materialTabControl.TabPages[0].Controls.Add(usernameLabel);

            // Create a new Panel that will act as the GroupBox
            Panel forumStatsPanel = new Panel
            {
                Location = new Point(10, 20), // Adjust these values to position the Panel
                Size = new Size(200, 120), // Adjust these values to change the size of the Panel
                BorderStyle = BorderStyle.FixedSingle // Set the BorderStyle to FixedSingle to mimic the border of a GroupBox
            };

            // Create a new Label for the title of the GroupBox
            Label forumStatsLabel = new Label
            {
                Location = new Point(0, -2), // Adjust these values to position the Label
                Text = "Forum Stats",
                BackColor = Color.Gray,
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            forumStatsPanel.Controls.Add(forumStatsLabel);

            Label unreadConvLabel = new Label
            {
                Location = new Point(5, 20), // Adjust these values to position the Label
                Text = $"Unread Messages: {unreadConversations}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            forumStatsPanel.Controls.Add(unreadConvLabel);

            Label unreadAlertLabel = new Label
            {
                Location = new Point(5, 35), // Adjust these values to position the Label
                Text = $"Unread Messages: {unreadAlerts}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the forumStatsPanel
            forumStatsPanel.Controls.Add(unreadAlertLabel);

            Label scoreLabel = new Label
            {
                Location = new Point(5, 50), // Adjust these values to position the Label
                Text = $"Score: {score}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the forumStatsPanel
            forumStatsPanel.Controls.Add(scoreLabel);

            Label postsLabel = new Label
            {
                Location = new Point(5, 65), // Adjust these values to position the Label
                Text = $"Posts: {posts}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the forumStatsPanel
            forumStatsPanel.Controls.Add(postsLabel);

            Label fidLabel = new Label
            {
                Location = new Point(5, 80), // Adjust these values to position the Label
                Text = $"Fantasy ID: {fid}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the forumStatsPanel
            forumStatsPanel.Controls.Add(fidLabel);

            Label regLabel = new Label
            {
                Location = new Point(5, 95), // Adjust these values to position the Label
                Text = $"Registered: {registerDate}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the forumStatsPanel
            forumStatsPanel.Controls.Add(regLabel);

            // Add the Panel to the "User Control" tab
            this.materialTabControl.TabPages[0].Controls.Add(forumStatsPanel);

            JsonDocument document = JsonDocument.Parse(Login.GlobalConfig);
            string formattedJson = JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });

            // Create a new TextBox
            // Create a new MaterialMultiLineTextBox
            MaterialSkin.Controls.MaterialMultiLineTextBox multilineTextField = new MaterialSkin.Controls.MaterialMultiLineTextBox
            {
                Location = new Point(10, 20), // Adjust these values to position the TextBox
                Size = new Size(500, 430), // Adjust these values to change the size of the TextBox
                Multiline = true, // Set Multiline to true
                //ScrollBars = ScrollBars.Both, // Add both vertical and horizontal scrollbars
                Text = formattedJson // Set the Text to the GlobalConfig
            };

            // Add the MaterialMultiLineTextBox to the "Configuration" tab
            this.materialTabControl.TabPages[1].Controls.Add(multilineTextField);

            // Create a new Button for loading the configuration
            MaterialSkin.Controls.MaterialButton loadConfigButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 25, multilineTextField.Location.Y), // Position the Button to the right of the TextBox
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
                        string formattedJson = JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });

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
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 25, multilineTextField.Location.Y + loadConfigButton.Height + 10), // Position the Button below the Load Config Button
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
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 25, multilineTextField.Location.Y + saveConfigButton.Height + loadConfigButton.Height + 20), // Position the Button below the Save Config Button
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
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 25, 400), // Position the Button below the Reset Config Button
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
                Location = new Point(multilineTextField.Location.X + multilineTextField.Width + 25, 350), // Position the Button below the Reset Config Button
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

        }
    }
}