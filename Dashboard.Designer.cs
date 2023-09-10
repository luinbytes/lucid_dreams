using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace lucid_dreams
{
    public partial class Dashboard : MaterialForm
    {
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
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Lucid Dreams - Dashboard";

            this.materialTabSelector = new MaterialTabSelector();
            this.materialTabControl = new MaterialTabControl();

            // Tab Selector
            this.materialTabSelector.BaseTabControl = this.materialTabControl;
            this.materialTabSelector.Depth = 0;
            this.materialTabSelector.Location = new System.Drawing.Point(0, 64);
            this.materialTabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector.Size = new System.Drawing.Size(620, 28);
            this.materialTabSelector.TabIndex = 0;

            // Tab Control
            this.materialTabControl.Depth = 0;
            this.materialTabControl.Location = new System.Drawing.Point(0, 84);
            this.materialTabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl.Size = new System.Drawing.Size(600, 452);
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

            avatarPictureBox.Location = new Point(0, this.ClientSize.Height - avatarPictureBox.Height - 45);

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

            // Add the Label to the Panel
            forumStatsPanel.Controls.Add(forumStatsLabel);

            // Create a new Label for FID
            Label unreadConvLabel = new Label
            {
                Location = new Point(5, 20), // Adjust these values to position the Label
                Text = $"Unread Messages: {unreadConversations}", // Set the Text to the FID
                AutoSize = true // Set AutoSize to true so the Label adjusts its size based on the text
            };

            // Add the Label to the forumStatsPanel
            forumStatsPanel.Controls.Add(unreadConvLabel);

                        // Create a new Label for FID
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

            // Create a new CheckBox
            CheckBox saveUserKeyCheckBox = new CheckBox
            {
                Location = new Point(20, 20), // Adjust these values to position the CheckBox
                Text = "Save user key locally",
                AutoSize = true // Set AutoSize to true so the CheckBox adjusts its size based on the text
            };

            // Add the CheckBox to the "Settings" tab
            this.materialTabControl.TabPages[2].Controls.Add(saveUserKeyCheckBox);

        }
    }
}