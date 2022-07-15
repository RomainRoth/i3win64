namespace i3win64
{
    partial class ZPool
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.groupBoxWindows = new System.Windows.Forms.GroupBox();
            this.panelWindow = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tbClassName = new System.Windows.Forms.TextBox();
            this.buttonBind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbWindowID = new System.Windows.Forms.TextBox();
            this.tbWindowName = new System.Windows.Forms.TextBox();
            this.tbProcessID = new System.Windows.Forms.TextBox();
            this.tbProcessName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxWindows = new System.Windows.Forms.ListBox();
            this.groupBoxProcesses = new System.Windows.Forms.GroupBox();
            this.listBoxProcesses = new System.Windows.Forms.ListBox();
            this.buttonRefreshProcesses = new System.Windows.Forms.Button();
            this.rtbWS = new System.Windows.Forms.RichTextBox();
            this.rtbWS_EX = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.groupBoxWindows.SuspendLayout();
            this.panelWindow.SuspendLayout();
            this.groupBoxProcesses.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "i3win64";
            this.notifyIcon.Visible = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDebug);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.groupBoxWindows);
            this.tabDebug.Controls.Add(this.groupBoxProcesses);
            this.tabDebug.Location = new System.Drawing.Point(4, 24);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(792, 422);
            this.tabDebug.TabIndex = 0;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // groupBoxWindows
            // 
            this.groupBoxWindows.Controls.Add(this.panelWindow);
            this.groupBoxWindows.Controls.Add(this.listBoxWindows);
            this.groupBoxWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxWindows.Location = new System.Drawing.Point(203, 3);
            this.groupBoxWindows.Name = "groupBoxWindows";
            this.groupBoxWindows.Size = new System.Drawing.Size(586, 416);
            this.groupBoxWindows.TabIndex = 1;
            this.groupBoxWindows.TabStop = false;
            this.groupBoxWindows.Text = "Process Windows";
            // 
            // panelWindow
            // 
            this.panelWindow.Controls.Add(this.label7);
            this.panelWindow.Controls.Add(this.label6);
            this.panelWindow.Controls.Add(this.rtbWS_EX);
            this.panelWindow.Controls.Add(this.rtbWS);
            this.panelWindow.Controls.Add(this.label5);
            this.panelWindow.Controls.Add(this.tbClassName);
            this.panelWindow.Controls.Add(this.buttonBind);
            this.panelWindow.Controls.Add(this.label4);
            this.panelWindow.Controls.Add(this.label3);
            this.panelWindow.Controls.Add(this.label2);
            this.panelWindow.Controls.Add(this.tbWindowID);
            this.panelWindow.Controls.Add(this.tbWindowName);
            this.panelWindow.Controls.Add(this.tbProcessID);
            this.panelWindow.Controls.Add(this.tbProcessName);
            this.panelWindow.Controls.Add(this.label1);
            this.panelWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWindow.Location = new System.Drawing.Point(207, 19);
            this.panelWindow.Name = "panelWindow";
            this.panelWindow.Size = new System.Drawing.Size(376, 394);
            this.panelWindow.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Class Name";
            // 
            // tbClassName
            // 
            this.tbClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClassName.Location = new System.Drawing.Point(103, 119);
            this.tbClassName.Name = "tbClassName";
            this.tbClassName.Size = new System.Drawing.Size(270, 23);
            this.tbClassName.TabIndex = 9;
            // 
            // buttonBind
            // 
            this.buttonBind.Location = new System.Drawing.Point(103, 368);
            this.buttonBind.Name = "buttonBind";
            this.buttonBind.Size = new System.Drawing.Size(197, 23);
            this.buttonBind.TabIndex = 8;
            this.buttonBind.Text = "Bind Window to i3";
            this.buttonBind.UseVisualStyleBackColor = true;
            this.buttonBind.Click += new System.EventHandler(this.buttonBind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Window ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Window Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Process ID";
            // 
            // tbWindowID
            // 
            this.tbWindowID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWindowID.Location = new System.Drawing.Point(103, 90);
            this.tbWindowID.Name = "tbWindowID";
            this.tbWindowID.Size = new System.Drawing.Size(270, 23);
            this.tbWindowID.TabIndex = 4;
            // 
            // tbWindowName
            // 
            this.tbWindowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWindowName.Location = new System.Drawing.Point(103, 61);
            this.tbWindowName.Name = "tbWindowName";
            this.tbWindowName.Size = new System.Drawing.Size(270, 23);
            this.tbWindowName.TabIndex = 3;
            // 
            // tbProcessID
            // 
            this.tbProcessID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProcessID.Location = new System.Drawing.Point(103, 32);
            this.tbProcessID.Name = "tbProcessID";
            this.tbProcessID.Size = new System.Drawing.Size(270, 23);
            this.tbProcessID.TabIndex = 2;
            // 
            // tbProcessName
            // 
            this.tbProcessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProcessName.Location = new System.Drawing.Point(103, 3);
            this.tbProcessName.Name = "tbProcessName";
            this.tbProcessName.Size = new System.Drawing.Size(270, 23);
            this.tbProcessName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process Name";
            // 
            // listBoxWindows
            // 
            this.listBoxWindows.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxWindows.FormattingEnabled = true;
            this.listBoxWindows.ItemHeight = 15;
            this.listBoxWindows.Location = new System.Drawing.Point(3, 19);
            this.listBoxWindows.Name = "listBoxWindows";
            this.listBoxWindows.Size = new System.Drawing.Size(204, 394);
            this.listBoxWindows.TabIndex = 0;
            this.listBoxWindows.SelectedIndexChanged += new System.EventHandler(this.listBoxWindows_SelectedIndexChanged);
            // 
            // groupBoxProcesses
            // 
            this.groupBoxProcesses.Controls.Add(this.listBoxProcesses);
            this.groupBoxProcesses.Controls.Add(this.buttonRefreshProcesses);
            this.groupBoxProcesses.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxProcesses.Location = new System.Drawing.Point(3, 3);
            this.groupBoxProcesses.Name = "groupBoxProcesses";
            this.groupBoxProcesses.Size = new System.Drawing.Size(200, 416);
            this.groupBoxProcesses.TabIndex = 0;
            this.groupBoxProcesses.TabStop = false;
            this.groupBoxProcesses.Text = "Processes";
            // 
            // listBoxProcesses
            // 
            this.listBoxProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxProcesses.FormattingEnabled = true;
            this.listBoxProcesses.ItemHeight = 15;
            this.listBoxProcesses.Location = new System.Drawing.Point(3, 42);
            this.listBoxProcesses.Name = "listBoxProcesses";
            this.listBoxProcesses.Size = new System.Drawing.Size(194, 371);
            this.listBoxProcesses.TabIndex = 1;
            this.listBoxProcesses.SelectedIndexChanged += new System.EventHandler(this.listBoxProcesses_SelectedIndexChanged);
            // 
            // buttonRefreshProcesses
            // 
            this.buttonRefreshProcesses.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRefreshProcesses.Location = new System.Drawing.Point(3, 19);
            this.buttonRefreshProcesses.Name = "buttonRefreshProcesses";
            this.buttonRefreshProcesses.Size = new System.Drawing.Size(194, 23);
            this.buttonRefreshProcesses.TabIndex = 0;
            this.buttonRefreshProcesses.Text = "Refresh Processes";
            this.buttonRefreshProcesses.UseVisualStyleBackColor = true;
            this.buttonRefreshProcesses.Click += new System.EventHandler(this.buttonRefreshProcesses_Click);
            // 
            // rtbWS
            // 
            this.rtbWS.Location = new System.Drawing.Point(103, 148);
            this.rtbWS.Name = "rtbWS";
            this.rtbWS.Size = new System.Drawing.Size(270, 106);
            this.rtbWS.TabIndex = 11;
            this.rtbWS.Text = "";
            // 
            // rtbWS_EX
            // 
            this.rtbWS_EX.Location = new System.Drawing.Point(103, 256);
            this.rtbWS_EX.Name = "rtbWS_EX";
            this.rtbWS_EX.Size = new System.Drawing.Size(270, 106);
            this.rtbWS_EX.TabIndex = 12;
            this.rtbWS_EX.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "WS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "WS_EX";
            // 
            // ZPool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "ZPool";
            this.Text = "ZPool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZPool_FormClosing);
            this.Load += new System.EventHandler(this.ZPool_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZPool_KeyPress);
            this.tabControl.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.groupBoxWindows.ResumeLayout(false);
            this.panelWindow.ResumeLayout(false);
            this.panelWindow.PerformLayout();
            this.groupBoxProcesses.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.GroupBox groupBoxWindows;
        private System.Windows.Forms.Panel panelWindow;
        private System.Windows.Forms.Button buttonBind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbWindowID;
        private System.Windows.Forms.TextBox tbWindowName;
        private System.Windows.Forms.TextBox tbProcessID;
        private System.Windows.Forms.TextBox tbProcessName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxWindows;
        private System.Windows.Forms.GroupBox groupBoxProcesses;
        private System.Windows.Forms.ListBox listBoxProcesses;
        private System.Windows.Forms.Button buttonRefreshProcesses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbClassName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbWS_EX;
        private System.Windows.Forms.RichTextBox rtbWS;
    }
}