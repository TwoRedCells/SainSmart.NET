namespace RedCell.Devices.SainSmart.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.Net16Radio = new System.Windows.Forms.RadioButton();
            this.UsbRadio = new System.Windows.Forms.RadioButton();
            this.Net8Radio = new System.Windows.Forms.RadioButton();
            this.ControlsGroup = new System.Windows.Forms.GroupBox();
            this.StatusGroup = new System.Windows.Forms.GroupBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.HostText = new System.Windows.Forms.TextBox();
            this.PortText = new System.Windows.Forms.TextBox();
            this.RelayControl = new RedCell.Devices.SainSmart.RelaysControl();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.ControlsGroup.SuspendLayout();
            this.StatusGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(label11);
            this.groupBox1.Controls.Add(this.PortText);
            this.groupBox1.Controls.Add(label10);
            this.groupBox1.Controls.Add(this.HostText);
            this.groupBox1.Controls.Add(this.ConnectButton);
            this.groupBox1.Controls.Add(this.Net16Radio);
            this.groupBox1.Controls.Add(this.UsbRadio);
            this.groupBox1.Controls.Add(this.Net8Radio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.MinimumSize = new System.Drawing.Size(198, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select board";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(255, 62);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.TabStop = false;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // Net16Radio
            // 
            this.Net16Radio.AutoSize = true;
            this.Net16Radio.Location = new System.Drawing.Point(11, 65);
            this.Net16Radio.Name = "Net16Radio";
            this.Net16Radio.Size = new System.Drawing.Size(176, 17);
            this.Net16Radio.TabIndex = 12;
            this.Net16Radio.TabStop = true;
            this.Net16Radio.Text = "Network Relay board (16 relays)";
            this.Net16Radio.UseVisualStyleBackColor = true;
            this.Net16Radio.Click += new System.EventHandler(this.BoardRadioChanged);
            // 
            // UsbRadio
            // 
            this.UsbRadio.AutoSize = true;
            this.UsbRadio.Location = new System.Drawing.Point(11, 19);
            this.UsbRadio.Name = "UsbRadio";
            this.UsbRadio.Size = new System.Drawing.Size(107, 17);
            this.UsbRadio.TabIndex = 10;
            this.UsbRadio.TabStop = true;
            this.UsbRadio.Text = "USB Relay board";
            this.UsbRadio.UseVisualStyleBackColor = true;
            this.UsbRadio.CheckedChanged += new System.EventHandler(this.BoardRadioChanged);
            // 
            // Net8Radio
            // 
            this.Net8Radio.AutoSize = true;
            this.Net8Radio.Location = new System.Drawing.Point(11, 42);
            this.Net8Radio.Name = "Net8Radio";
            this.Net8Radio.Size = new System.Drawing.Size(170, 17);
            this.Net8Radio.TabIndex = 11;
            this.Net8Radio.TabStop = true;
            this.Net8Radio.Text = "Network Relay board (8 relays)";
            this.Net8Radio.UseVisualStyleBackColor = true;
            this.Net8Radio.Click += new System.EventHandler(this.BoardRadioChanged);
            // 
            // ControlsGroup
            // 
            this.ControlsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlsGroup.Controls.Add(this.RelayControl);
            this.ControlsGroup.Enabled = false;
            this.ControlsGroup.Location = new System.Drawing.Point(12, 169);
            this.ControlsGroup.MinimumSize = new System.Drawing.Size(160, 230);
            this.ControlsGroup.Name = "ControlsGroup";
            this.ControlsGroup.Size = new System.Drawing.Size(336, 230);
            this.ControlsGroup.TabIndex = 1;
            this.ControlsGroup.TabStop = false;
            this.ControlsGroup.Text = "Relay board control";
            // 
            // StatusGroup
            // 
            this.StatusGroup.Controls.Add(this.StatusLabel);
            this.StatusGroup.Location = new System.Drawing.Point(13, 117);
            this.StatusGroup.Name = "StatusGroup";
            this.StatusGroup.Size = new System.Drawing.Size(335, 46);
            this.StatusGroup.TabIndex = 2;
            this.StatusGroup.TabStop = false;
            this.StatusGroup.Text = "Status";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(10, 20);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(37, 13);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "Status";
            // 
            // HostText
            // 
            this.HostText.Enabled = false;
            this.HostText.Location = new System.Drawing.Point(255, 18);
            this.HostText.Name = "HostText";
            this.HostText.Size = new System.Drawing.Size(75, 20);
            this.HostText.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(212, 21);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(32, 13);
            label10.TabIndex = 14;
            label10.Text = "Host:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(212, 44);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(29, 13);
            label11.TabIndex = 16;
            label11.Text = "Port:";
            // 
            // PortText
            // 
            this.PortText.Enabled = false;
            this.PortText.Location = new System.Drawing.Point(255, 41);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(75, 20);
            this.PortText.TabIndex = 15;
            // 
            // RelayControl
            // 
            this.RelayControl.Board = null;
            this.RelayControl.Location = new System.Drawing.Point(11, 19);
            this.RelayControl.MaximumSize = new System.Drawing.Size(155, 205);
            this.RelayControl.MinimumSize = new System.Drawing.Size(155, 205);
            this.RelayControl.Name = "RelayControl";
            this.RelayControl.Size = new System.Drawing.Size(155, 205);
            this.RelayControl.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 411);
            this.Controls.Add(this.StatusGroup);
            this.Controls.Add(this.ControlsGroup);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(239, 450);
            this.Name = "Form1";
            this.Text = "SainSmart.NET";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ControlsGroup.ResumeLayout(false);
            this.StatusGroup.ResumeLayout(false);
            this.StatusGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Net16Radio;
        private System.Windows.Forms.RadioButton UsbRadio;
        private System.Windows.Forms.RadioButton Net8Radio;
        private System.Windows.Forms.GroupBox ControlsGroup;
        private RelaysControl RelayControl;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.GroupBox StatusGroup;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox PortText;
        private System.Windows.Forms.TextBox HostText;

    }
}

