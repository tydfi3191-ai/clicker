namespace AutoClicker
{
	// Token: 0x02000025 RID: 37
	internal partial class MainForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000072E6 File Offset: 0x000054E6
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007308 File Offset: 0x00005508
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoClicker.MainForm));
			this.label1 = new global::System.Windows.Forms.Label();
			this.lbActKey = new global::System.Windows.Forms.Label();
			this.btnSelectActKey = new global::System.Windows.Forms.Button();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.btnChooseApps = new global::System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new global::System.Windows.Forms.FlowLayoutPanel();
			this.rbtnModeHold = new global::System.Windows.Forms.RadioButton();
			this.rbtnModeSwitch = new global::System.Windows.Forms.RadioButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.lbLButton = new global::System.Windows.Forms.Label();
			this.lbRButton = new global::System.Windows.Forms.Label();
			this.lbMButton = new global::System.Windows.Forms.Label();
			this.btnAdvSettings = new global::System.Windows.Forms.Button();
			this.rbtnMouseMiddle = new global::System.Windows.Forms.RadioButton();
			this.picbxMouse = new global::System.Windows.Forms.PictureBox();
			this.rbtnMouseLeft = new global::System.Windows.Forms.RadioButton();
			this.rbtnMouseRight = new global::System.Windows.Forms.RadioButton();
			this.gbxClickRate = new global::System.Windows.Forms.GroupBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.numClickCycle = new global::System.Windows.Forms.NumericUpDown();
			this.label3 = new global::System.Windows.Forms.Label();
			this.chkRandomizeClickRate = new global::System.Windows.Forms.CheckBox();
			this.numClickRate = new global::System.Windows.Forms.NumericUpDown();
			this.chkUnlimitedClickRate = new global::System.Windows.Forms.CheckBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.groupBox4 = new global::System.Windows.Forms.GroupBox();
			this.lbClickLimit2 = new global::System.Windows.Forms.Label();
			this.lbClickLimit1 = new global::System.Windows.Forms.Label();
			this.numClickLimit = new global::System.Windows.Forms.NumericUpDown();
			this.chkClickLimit = new global::System.Windows.Forms.CheckBox();
			this.lbClickTimeInfo = new global::System.Windows.Forms.Label();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.btnExit = new global::System.Windows.Forms.Button();
			this.groupBox3 = new global::System.Windows.Forms.GroupBox();
			this.cbxLanguage = new global::System.Windows.Forms.ComboBox();
			this.chkStats = new global::System.Windows.Forms.CheckBox();
			this.btnResetSettings = new global::System.Windows.Forms.Button();
			this.chkStartup = new global::System.Windows.Forms.CheckBox();
			this.trayIcon = new global::System.Windows.Forms.NotifyIcon(this.components);
			this.toolTip = new global::System.Windows.Forms.ToolTip(this.components);
			this.btnFb = new global::System.Windows.Forms.Button();
			this.pnlAdvSettings = new global::System.Windows.Forms.Panel();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.llnCopyright = new global::System.Windows.Forms.LinkLabel();
			this.animTimer = new global::System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picbxMouse).BeginInit();
			this.gbxClickRate.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.numClickCycle).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numClickRate).BeginInit();
			this.groupBox4.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.numClickLimit).BeginInit();
			this.groupBox3.SuspendLayout();
			this.pnlAdvSettings.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(10, 31);
			this.label1.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(142, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Aktivierungstaste:";
			this.lbActKey.BackColor = global::System.Drawing.Color.Silver;
			this.lbActKey.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbActKey.Font = new global::System.Drawing.Font("Calibri", 10.125f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbActKey.Location = new global::System.Drawing.Point(210, 24);
			this.lbActKey.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbActKey.Name = "lbActKey";
			this.lbActKey.Size = new global::System.Drawing.Size(229, 34);
			this.lbActKey.TabIndex = 1;
			this.lbActKey.Text = "KEYS";
			this.lbActKey.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip.SetToolTip(this.lbActKey, "Die Taste, mit der der AutoClicker aktiviert wird.");
			this.btnSelectActKey.Location = new global::System.Drawing.Point(449, 24);
			this.btnSelectActKey.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnSelectActKey.Name = "btnSelectActKey";
			this.btnSelectActKey.Size = new global::System.Drawing.Size(178, 34);
			this.btnSelectActKey.TabIndex = 2;
			this.btnSelectActKey.Text = "Taste wählen";
			this.toolTip.SetToolTip(this.btnSelectActKey, "Hier klicken, um die Taste auszuwählen.");
			this.btnSelectActKey.UseVisualStyleBackColor = false;
			this.btnSelectActKey.Click += new global::System.EventHandler(this.btnSelectActKey_Click);
			this.groupBox1.Controls.Add(this.btnChooseApps);
			this.groupBox1.Controls.Add(this.flowLayoutPanel1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btnSelectActKey);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lbActKey);
			this.groupBox1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.groupBox1.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new global::System.Drawing.Point(10, 9);
			this.groupBox1.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox1.Size = new global::System.Drawing.Size(637, 111);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Aktivierung";
			this.btnChooseApps.Location = new global::System.Drawing.Point(449, 71);
			this.btnChooseApps.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnChooseApps.Name = "btnChooseApps";
			this.btnChooseApps.Size = new global::System.Drawing.Size(178, 34);
			this.btnChooseApps.TabIndex = 5;
			this.btnChooseApps.Text = "Apps wählen";
			this.toolTip.SetToolTip(this.btnChooseApps, "Hier klicken, um die Taste auszuwählen.");
			this.btnChooseApps.UseVisualStyleBackColor = false;
			this.btnChooseApps.Click += new global::System.EventHandler(this.button1_Click);
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.rbtnModeHold);
			this.flowLayoutPanel1.Controls.Add(this.rbtnModeSwitch);
			this.flowLayoutPanel1.Location = new global::System.Drawing.Point(213, 71);
			this.flowLayoutPanel1.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new global::System.Drawing.Size(211, 32);
			this.flowLayoutPanel1.TabIndex = 4;
			this.rbtnModeHold.AutoSize = true;
			this.rbtnModeHold.Location = new global::System.Drawing.Point(5, 4);
			this.rbtnModeHold.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rbtnModeHold.Name = "rbtnModeHold";
			this.rbtnModeHold.Size = new global::System.Drawing.Size(75, 24);
			this.rbtnModeHold.TabIndex = 1;
			this.rbtnModeHold.TabStop = true;
			this.rbtnModeHold.Text = "halten";
			this.toolTip.SetToolTip(this.rbtnModeHold, "Der AutoClicker klickt solange, wie die Taste festgehalten wird.");
			this.rbtnModeHold.UseVisualStyleBackColor = true;
			this.rbtnModeHold.CheckedChanged += new global::System.EventHandler(this.rbtnMode_CheckedChanged);
			this.rbtnModeSwitch.AutoSize = true;
			this.rbtnModeSwitch.Location = new global::System.Drawing.Point(90, 4);
			this.rbtnModeSwitch.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rbtnModeSwitch.Name = "rbtnModeSwitch";
			this.rbtnModeSwitch.Size = new global::System.Drawing.Size(116, 24);
			this.rbtnModeSwitch.TabIndex = 0;
			this.rbtnModeSwitch.TabStop = true;
			this.rbtnModeSwitch.Text = "umschalten";
			this.toolTip.SetToolTip(this.rbtnModeSwitch, "Der AutoClicker wird beim ersten Tastendruck aktiviert und bleibt solange aktiv, bis die Taste ein zweites Mal gedrückt wird.");
			this.rbtnModeSwitch.UseVisualStyleBackColor = true;
			this.rbtnModeSwitch.CheckedChanged += new global::System.EventHandler(this.rbtnMode_CheckedChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(10, 74);
			this.label2.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(155, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Aktivierungsmodus:";
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new global::System.Drawing.Point(10, 120);
			this.groupBox2.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox2.Size = new global::System.Drawing.Size(637, 271);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Klicks";
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.lbLButton);
			this.panel1.Controls.Add(this.lbRButton);
			this.panel1.Controls.Add(this.lbMButton);
			this.panel1.Controls.Add(this.btnAdvSettings);
			this.panel1.Controls.Add(this.rbtnMouseMiddle);
			this.panel1.Controls.Add(this.picbxMouse);
			this.panel1.Controls.Add(this.rbtnMouseLeft);
			this.panel1.Controls.Add(this.rbtnMouseRight);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new global::System.Drawing.Point(5, 24);
			this.panel1.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(627, 242);
			this.panel1.TabIndex = 6;
			this.lbLButton.AutoSize = true;
			this.lbLButton.Font = new global::System.Drawing.Font("Calibri", 10.125f, global::System.Drawing.FontStyle.Bold);
			this.lbLButton.Location = new global::System.Drawing.Point(127, 98);
			this.lbLButton.Name = "lbLButton";
			this.lbLButton.Size = new global::System.Drawing.Size(65, 21);
			this.lbLButton.TabIndex = 10;
			this.lbLButton.Text = "LButton";
			this.lbLButton.Click += new global::System.EventHandler(this.lbLButton_Click);
			this.lbRButton.AutoSize = true;
			this.lbRButton.Font = new global::System.Drawing.Font("Calibri", 10.125f, global::System.Drawing.FontStyle.Bold);
			this.lbRButton.Location = new global::System.Drawing.Point(432, 98);
			this.lbRButton.Name = "lbRButton";
			this.lbRButton.Size = new global::System.Drawing.Size(68, 21);
			this.lbRButton.TabIndex = 9;
			this.lbRButton.Text = "RButton";
			this.lbRButton.Click += new global::System.EventHandler(this.lbRButton_Click);
			this.lbMButton.AutoSize = true;
			this.lbMButton.Font = new global::System.Drawing.Font("Calibri", 10.125f, global::System.Drawing.FontStyle.Bold);
			this.lbMButton.Location = new global::System.Drawing.Point(345, 28);
			this.lbMButton.Name = "lbMButton";
			this.lbMButton.Size = new global::System.Drawing.Size(73, 21);
			this.lbMButton.TabIndex = 8;
			this.lbMButton.Text = "MButton";
			this.lbMButton.Click += new global::System.EventHandler(this.lbMButton_Click);
			this.btnAdvSettings.FlatAppearance.BorderSize = 0;
			this.btnAdvSettings.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnAdvSettings.Image = (global::System.Drawing.Image)resources.GetObject("btnAdvSettings.Image");
			this.btnAdvSettings.Location = new global::System.Drawing.Point(544, 179);
			this.btnAdvSettings.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnAdvSettings.Name = "btnAdvSettings";
			this.btnAdvSettings.Size = new global::System.Drawing.Size(67, 59);
			this.btnAdvSettings.TabIndex = 7;
			this.toolTip.SetToolTip(this.btnAdvSettings, "Zeige erweiterte Einstellungen");
			this.btnAdvSettings.UseVisualStyleBackColor = true;
			this.btnAdvSettings.Click += new global::System.EventHandler(this.btnAdvSettings_Click);
			this.rbtnMouseMiddle.AutoSize = true;
			this.rbtnMouseMiddle.Location = new global::System.Drawing.Point(205, 4);
			this.rbtnMouseMiddle.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rbtnMouseMiddle.Name = "rbtnMouseMiddle";
			this.rbtnMouseMiddle.Size = new global::System.Drawing.Size(253, 24);
			this.rbtnMouseMiddle.TabIndex = 1;
			this.rbtnMouseMiddle.TabStop = true;
			this.rbtnMouseMiddle.Text = "mittlere Maustaste (Scrollrad)";
			this.rbtnMouseMiddle.UseVisualStyleBackColor = true;
			this.rbtnMouseMiddle.CheckedChanged += new global::System.EventHandler(this.rbtnMouseButton_CheckedChanged);
			this.picbxMouse.BackgroundImage = global::AutoClicker.Properties.Resources.Mouse;
			this.picbxMouse.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.picbxMouse.Location = new global::System.Drawing.Point(235, 26);
			this.picbxMouse.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.picbxMouse.Name = "picbxMouse";
			this.picbxMouse.Size = new global::System.Drawing.Size(167, 212);
			this.picbxMouse.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picbxMouse.TabIndex = 5;
			this.picbxMouse.TabStop = false;
			this.rbtnMouseLeft.AutoSize = true;
			this.rbtnMouseLeft.Location = new global::System.Drawing.Point(64, 74);
			this.rbtnMouseLeft.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rbtnMouseLeft.Name = "rbtnMouseLeft";
			this.rbtnMouseLeft.RightToLeft = global::System.Windows.Forms.RightToLeft.Yes;
			this.rbtnMouseLeft.Size = new global::System.Drawing.Size(147, 24);
			this.rbtnMouseLeft.TabIndex = 0;
			this.rbtnMouseLeft.TabStop = true;
			this.rbtnMouseLeft.Text = "linke Maustaste";
			this.rbtnMouseLeft.UseVisualStyleBackColor = true;
			this.rbtnMouseLeft.CheckedChanged += new global::System.EventHandler(this.rbtnMouseButton_CheckedChanged);
			this.rbtnMouseRight.AutoSize = true;
			this.rbtnMouseRight.Location = new global::System.Drawing.Point(412, 74);
			this.rbtnMouseRight.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rbtnMouseRight.Name = "rbtnMouseRight";
			this.rbtnMouseRight.Size = new global::System.Drawing.Size(160, 24);
			this.rbtnMouseRight.TabIndex = 2;
			this.rbtnMouseRight.TabStop = true;
			this.rbtnMouseRight.Text = "rechte Maustaste";
			this.rbtnMouseRight.UseVisualStyleBackColor = true;
			this.rbtnMouseRight.CheckedChanged += new global::System.EventHandler(this.rbtnMouseButton_CheckedChanged);
			this.gbxClickRate.Controls.Add(this.label5);
			this.gbxClickRate.Controls.Add(this.numClickCycle);
			this.gbxClickRate.Controls.Add(this.label3);
			this.gbxClickRate.Controls.Add(this.chkRandomizeClickRate);
			this.gbxClickRate.Controls.Add(this.numClickRate);
			this.gbxClickRate.Controls.Add(this.chkUnlimitedClickRate);
			this.gbxClickRate.Controls.Add(this.label4);
			this.gbxClickRate.Location = new global::System.Drawing.Point(0, 0);
			this.gbxClickRate.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.gbxClickRate.Name = "gbxClickRate";
			this.gbxClickRate.Padding = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.gbxClickRate.Size = new global::System.Drawing.Size(333, 151);
			this.gbxClickRate.TabIndex = 11;
			this.gbxClickRate.TabStop = false;
			this.gbxClickRate.Text = "Klickrate";
			this.label5.AutoSize = true;
			this.label5.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(258, 106);
			this.label5.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(21, 18);
			this.label5.TabIndex = 14;
			this.label5.Text = "%";
			this.numClickCycle.DecimalPlaces = 2;
			this.numClickCycle.Location = new global::System.Drawing.Point(140, 106);
			this.numClickCycle.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.numClickCycle.Name = "numClickCycle";
			this.numClickCycle.Size = new global::System.Drawing.Size(115, 27);
			this.numClickCycle.TabIndex = 13;
			this.numClickCycle.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.toolTip.SetToolTip(this.numClickCycle, resources.GetString("numClickCycle.ToolTip"));
			this.numClickCycle.ValueChanged += new global::System.EventHandler(this.numClickCycle_ValueChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(10, 109);
			this.label3.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(83, 20);
			this.label3.TabIndex = 12;
			this.label3.Text = "Klickgrad:";
			this.chkRandomizeClickRate.AutoSize = true;
			this.chkRandomizeClickRate.Location = new global::System.Drawing.Point(152, 66);
			this.chkRandomizeClickRate.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.chkRandomizeClickRate.Name = "chkRandomizeClickRate";
			this.chkRandomizeClickRate.Size = new global::System.Drawing.Size(84, 24);
			this.chkRandomizeClickRate.TabIndex = 11;
			this.chkRandomizeClickRate.Text = "zufällig";
			this.toolTip.SetToolTip(this.chkRandomizeClickRate, resources.GetString("chkRandomizeClickRate.ToolTip"));
			this.chkRandomizeClickRate.UseVisualStyleBackColor = true;
			this.chkRandomizeClickRate.CheckedChanged += new global::System.EventHandler(this.chkRandomizeClickRate_CheckedChanged);
			this.numClickRate.DecimalPlaces = 2;
			global::System.Windows.Forms.NumericUpDown numericUpDown = this.numClickRate;
			int[] array = new int[4];
			array[0] = 10;
			numericUpDown.Increment = new decimal(array);
			this.numClickRate.Location = new global::System.Drawing.Point(10, 27);
			this.numClickRate.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.numClickRate.Minimum = new decimal(new int[]
			{
				1,
				0,
				0,
				131072
			});
			this.numClickRate.Name = "numClickRate";
			this.numClickRate.Size = new global::System.Drawing.Size(104, 27);
			this.numClickRate.TabIndex = 8;
			this.numClickRate.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.toolTip.SetToolTip(this.numClickRate, "Die Anzahl der Klicks pro Sekunde.\r\nVORSICHT: Hohe Werte (ab ca. 500) können die Programme überlasten und die Stabilität beeinträchtigen!");
			global::System.Windows.Forms.NumericUpDown numericUpDown2 = this.numClickRate;
			int[] array2 = new int[4];
			array2[0] = 1;
			numericUpDown2.Value = new decimal(array2);
			this.numClickRate.ValueChanged += new global::System.EventHandler(this.numClickRate_ValueChanged);
			this.chkUnlimitedClickRate.AutoSize = true;
			this.chkUnlimitedClickRate.Location = new global::System.Drawing.Point(10, 66);
			this.chkUnlimitedClickRate.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.chkUnlimitedClickRate.Name = "chkUnlimitedClickRate";
			this.chkUnlimitedClickRate.Size = new global::System.Drawing.Size(114, 24);
			this.chkUnlimitedClickRate.TabIndex = 10;
			this.chkUnlimitedClickRate.Text = "unbegrenzt";
			this.toolTip.SetToolTip(this.chkUnlimitedClickRate, "ACHTUNG: Eine unbegrenzte Klickrate führt zu hoher Prozessor-Auslastung und kann die Stabilität des Systems beeinträchtigen!");
			this.chkUnlimitedClickRate.UseVisualStyleBackColor = true;
			this.chkUnlimitedClickRate.CheckedChanged += new global::System.EventHandler(this.chkUnlimitedClickRate_CheckedChanged);
			this.label4.Location = new global::System.Drawing.Point(119, 29);
			this.label4.Margin = new global::System.Windows.Forms.Padding(0, 0, 5, 0);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(214, 31);
			this.label4.TabIndex = 9;
			this.label4.Text = "Klicks pro Sekunde";
			this.groupBox4.Controls.Add(this.lbClickLimit2);
			this.groupBox4.Controls.Add(this.lbClickLimit1);
			this.groupBox4.Controls.Add(this.numClickLimit);
			this.groupBox4.Controls.Add(this.chkClickLimit);
			this.groupBox4.Location = new global::System.Drawing.Point(343, 0);
			this.groupBox4.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox4.Size = new global::System.Drawing.Size(293, 107);
			this.groupBox4.TabIndex = 12;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Klickbegrenzung";
			this.lbClickLimit2.AutoSize = true;
			this.lbClickLimit2.Enabled = false;
			this.lbClickLimit2.Location = new global::System.Drawing.Point(215, 68);
			this.lbClickLimit2.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbClickLimit2.Name = "lbClickLimit2";
			this.lbClickLimit2.Size = new global::System.Drawing.Size(54, 20);
			this.lbClickLimit2.TabIndex = 3;
			this.lbClickLimit2.Text = "Klicks";
			this.lbClickLimit1.AutoSize = true;
			this.lbClickLimit1.Enabled = false;
			this.lbClickLimit1.Location = new global::System.Drawing.Point(10, 66);
			this.lbClickLimit1.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbClickLimit1.Name = "lbClickLimit1";
			this.lbClickLimit1.Size = new global::System.Drawing.Size(71, 20);
			this.lbClickLimit1.TabIndex = 2;
			this.lbClickLimit1.Text = "Maximal";
			this.numClickLimit.Enabled = false;
			this.numClickLimit.Location = new global::System.Drawing.Point(95, 61);
			this.numClickLimit.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.numClickLimit.Name = "numClickLimit";
			this.numClickLimit.Size = new global::System.Drawing.Size(117, 27);
			this.numClickLimit.TabIndex = 1;
			this.toolTip.SetToolTip(this.numClickLimit, "Die Anzahl der Klicks die nach der Aktivierung des AutoClickers ausgeführt werden soll");
			this.numClickLimit.ValueChanged += new global::System.EventHandler(this.numClickLimit_ValueChanged);
			this.chkClickLimit.AutoSize = true;
			this.chkClickLimit.Location = new global::System.Drawing.Point(10, 27);
			this.chkClickLimit.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.chkClickLimit.Name = "chkClickLimit";
			this.chkClickLimit.Size = new global::System.Drawing.Size(230, 24);
			this.chkClickLimit.TabIndex = 0;
			this.chkClickLimit.Text = "Klickbegrenzung aktivieren";
			this.toolTip.SetToolTip(this.chkClickLimit, "Bei aktivierter Klickbegrenzung werden beim Drücken der Aktivierungstaste nur die unten eingegebene Anzahl an Klicks ausgeführt.");
			this.chkClickLimit.UseVisualStyleBackColor = true;
			this.chkClickLimit.CheckedChanged += new global::System.EventHandler(this.chkClickLimit_CheckedChanged);
			this.lbClickTimeInfo.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbClickTimeInfo.Location = new global::System.Drawing.Point(343, 112);
			this.lbClickTimeInfo.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.lbClickTimeInfo.Name = "lbClickTimeInfo";
			this.lbClickTimeInfo.Size = new global::System.Drawing.Size(294, 39);
			this.lbClickTimeInfo.TabIndex = 13;
			this.lbClickTimeInfo.Text = "Click / Wait";
			this.lbClickTimeInfo.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.btnOk.Location = new global::System.Drawing.Point(367, 9);
			this.btnOk.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new global::System.Drawing.Size(125, 34);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.btnExit.Location = new global::System.Drawing.Point(502, 9);
			this.btnExit.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new global::System.Drawing.Size(125, 34);
			this.btnExit.TabIndex = 7;
			this.btnExit.Text = "Be&enden";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new global::System.EventHandler(this.btnExit_Click);
			this.groupBox3.Controls.Add(this.cbxLanguage);
			this.groupBox3.Controls.Add(this.chkStats);
			this.groupBox3.Controls.Add(this.btnResetSettings);
			this.groupBox3.Controls.Add(this.chkStartup);
			this.groupBox3.Location = new global::System.Drawing.Point(0, 151);
			this.groupBox3.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox3.Size = new global::System.Drawing.Size(637, 118);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Sonstiges";
			this.cbxLanguage.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxLanguage.FormattingEnabled = true;
			this.cbxLanguage.Location = new global::System.Drawing.Point(425, 29);
			this.cbxLanguage.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.cbxLanguage.Name = "cbxLanguage";
			this.cbxLanguage.Size = new global::System.Drawing.Size(199, 28);
			this.cbxLanguage.TabIndex = 3;
			this.cbxLanguage.SelectedIndexChanged += new global::System.EventHandler(this.cbxLanguage_SelectedIndexChanged);
			this.chkStats.AutoSize = true;
			this.chkStats.Location = new global::System.Drawing.Point(15, 68);
			this.chkStats.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.chkStats.Name = "chkStats";
			this.chkStats.Size = new global::System.Drawing.Size(311, 24);
			this.chkStats.TabIndex = 2;
			this.chkStats.Text = "Anonyme Nutzungsstatistiken senden";
			this.toolTip.SetToolTip(this.chkStats, "Es werden nur die Anzahl der Klicks für die Klickstatistik übertragen, keine persönlichen Daten.");
			this.chkStats.UseVisualStyleBackColor = true;
			this.chkStats.CheckedChanged += new global::System.EventHandler(this.chkStats_CheckedChanged);
			this.btnResetSettings.Location = new global::System.Drawing.Point(368, 74);
			this.btnResetSettings.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnResetSettings.Name = "btnResetSettings";
			this.btnResetSettings.Size = new global::System.Drawing.Size(258, 34);
			this.btnResetSettings.TabIndex = 1;
			this.btnResetSettings.Text = "btnReset";
			this.toolTip.SetToolTip(this.btnResetSettings, "Setzt alle Einstellungen auf die Standardwerte zurück.");
			this.btnResetSettings.UseVisualStyleBackColor = true;
			this.btnResetSettings.Click += new global::System.EventHandler(this.btnResetSettings_Click);
			this.chkStartup.AutoSize = true;
			this.chkStartup.Location = new global::System.Drawing.Point(15, 34);
			this.chkStartup.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.chkStartup.Name = "chkStartup";
			this.chkStartup.Size = new global::System.Drawing.Size(184, 24);
			this.chkStartup.TabIndex = 0;
			this.chkStartup.Text = "Mit Windows starten";
			this.toolTip.SetToolTip(this.chkStartup, "Wenn aktiviert, startet der AutoClicker automatisch beim Hochfahren des PCs.");
			this.chkStartup.UseVisualStyleBackColor = true;
			this.chkStartup.CheckedChanged += new global::System.EventHandler(this.chkStartup_CheckedChanged);
			this.trayIcon.BalloonTipIcon = global::System.Windows.Forms.ToolTipIcon.Info;
			this.trayIcon.BalloonTipTitle = "Speed AutoClicker";
			this.trayIcon.Text = "Speed AutoClicker";
			this.trayIcon.Visible = true;
			this.trayIcon.Click += new global::System.EventHandler(this.trayIcon_Click);
			this.btnFb.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btnFb.FlatAppearance.BorderSize = 0;
			this.btnFb.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnFb.Image = (global::System.Drawing.Image)resources.GetObject("btnFb.Image");
			this.btnFb.Location = new global::System.Drawing.Point(0, 3);
			this.btnFb.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.btnFb.Name = "btnFb";
			this.btnFb.Size = new global::System.Drawing.Size(53, 48);
			this.btnFb.TabIndex = 9;
			this.toolTip.SetToolTip(this.btnFb, "Facebook Page");
			this.btnFb.UseVisualStyleBackColor = true;
			this.btnFb.Click += new global::System.EventHandler(this.btnFb_Click);
			this.pnlAdvSettings.Controls.Add(this.lbClickTimeInfo);
			this.pnlAdvSettings.Controls.Add(this.groupBox4);
			this.pnlAdvSettings.Controls.Add(this.gbxClickRate);
			this.pnlAdvSettings.Controls.Add(this.groupBox3);
			this.pnlAdvSettings.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.pnlAdvSettings.Location = new global::System.Drawing.Point(10, 391);
			this.pnlAdvSettings.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.pnlAdvSettings.Name = "pnlAdvSettings";
			this.pnlAdvSettings.Size = new global::System.Drawing.Size(637, 268);
			this.pnlAdvSettings.TabIndex = 10;
			this.panel3.AutoSize = true;
			this.panel3.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel3.Controls.Add(this.btnFb);
			this.panel3.Controls.Add(this.llnCopyright);
			this.panel3.Controls.Add(this.btnOk);
			this.panel3.Controls.Add(this.btnExit);
			this.panel3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new global::System.Drawing.Point(10, 659);
			this.panel3.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(637, 55);
			this.panel3.TabIndex = 11;
			this.llnCopyright.AutoSize = true;
			this.llnCopyright.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 6.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.llnCopyright.Location = new global::System.Drawing.Point(140, 19);
			this.llnCopyright.Margin = new global::System.Windows.Forms.Padding(5, 0, 5, 0);
			this.llnCopyright.Name = "llnCopyright";
			this.llnCopyright.Size = new global::System.Drawing.Size(112, 15);
			this.llnCopyright.TabIndex = 8;
			this.llnCopyright.TabStop = true;
			this.llnCopyright.Text = "© Fabian Schlieper";
			this.llnCopyright.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llnCopyright_LinkClicked);
			this.animTimer.Interval = 10;
			this.animTimer.Tick += new global::System.EventHandler(this.animTimer_Tick);
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(120f, 120f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = global::System.Drawing.Color.White;
			base.ClientSize = new global::System.Drawing.Size(657, 829);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.pnlAdvSettings);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			this.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10.125f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.HelpButton = true;
			base.Margin = new global::System.Windows.Forms.Padding(5, 4, 5, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(663, 62);
			base.Name = "MainForm";
			base.Padding = new global::System.Windows.Forms.Padding(10, 9, 10, 9);
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Speed AutoClicker";
			base.HelpButtonClicked += new global::System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			base.Load += new global::System.EventHandler(this.MainForm_Load);
			base.SizeChanged += new global::System.EventHandler(this.MainForm_SizeChanged);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picbxMouse).EndInit();
			this.gbxClickRate.ResumeLayout(false);
			this.gbxClickRate.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.numClickCycle).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numClickRate).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.numClickLimit).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.pnlAdvSettings.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000BA RID: 186
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000BB RID: 187
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000BC RID: 188
		private global::System.Windows.Forms.Label lbActKey;

		// Token: 0x040000BD RID: 189
		private global::System.Windows.Forms.Button btnSelectActKey;

		// Token: 0x040000BE RID: 190
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x040000BF RID: 191
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x040000C0 RID: 192
		private global::System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

		// Token: 0x040000C1 RID: 193
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000C2 RID: 194
		private global::System.Windows.Forms.RadioButton rbtnModeHold;

		// Token: 0x040000C3 RID: 195
		private global::System.Windows.Forms.RadioButton rbtnModeSwitch;

		// Token: 0x040000C4 RID: 196
		private global::System.Windows.Forms.RadioButton rbtnMouseRight;

		// Token: 0x040000C5 RID: 197
		private global::System.Windows.Forms.RadioButton rbtnMouseMiddle;

		// Token: 0x040000C6 RID: 198
		private global::System.Windows.Forms.RadioButton rbtnMouseLeft;

		// Token: 0x040000C7 RID: 199
		private global::System.Windows.Forms.PictureBox picbxMouse;

		// Token: 0x040000C8 RID: 200
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.NumericUpDown numClickRate;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.Button btnExit;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.CheckBox chkUnlimitedClickRate;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.GroupBox groupBox3;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.Button btnResetSettings;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.CheckBox chkStartup;

		// Token: 0x040000D1 RID: 209
		private global::System.Windows.Forms.NotifyIcon trayIcon;

		// Token: 0x040000D2 RID: 210
		private global::System.Windows.Forms.ToolTip toolTip;

		// Token: 0x040000D3 RID: 211
		private global::System.Windows.Forms.GroupBox gbxClickRate;

		// Token: 0x040000D4 RID: 212
		private global::System.Windows.Forms.GroupBox groupBox4;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.Label lbClickLimit2;

		// Token: 0x040000D6 RID: 214
		private global::System.Windows.Forms.Label lbClickLimit1;

		// Token: 0x040000D7 RID: 215
		private global::System.Windows.Forms.NumericUpDown numClickLimit;

		// Token: 0x040000D8 RID: 216
		private global::System.Windows.Forms.CheckBox chkClickLimit;

		// Token: 0x040000D9 RID: 217
		private global::System.Windows.Forms.CheckBox chkRandomizeClickRate;

		// Token: 0x040000DA RID: 218
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000DB RID: 219
		private global::System.Windows.Forms.NumericUpDown numClickCycle;

		// Token: 0x040000DC RID: 220
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040000DD RID: 221
		private global::System.Windows.Forms.Label lbClickTimeInfo;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.CheckBox chkStats;

		// Token: 0x040000DF RID: 223
		private global::System.Windows.Forms.Button btnAdvSettings;

		// Token: 0x040000E0 RID: 224
		private global::System.Windows.Forms.Panel pnlAdvSettings;

		// Token: 0x040000E1 RID: 225
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040000E2 RID: 226
		private global::System.Windows.Forms.Timer animTimer;

		// Token: 0x040000E3 RID: 227
		private global::System.Windows.Forms.LinkLabel llnCopyright;

		// Token: 0x040000E4 RID: 228
		private global::System.Windows.Forms.ComboBox cbxLanguage;

		// Token: 0x040000E5 RID: 229
		private global::System.Windows.Forms.Button btnFb;

		// Token: 0x040000E6 RID: 230
		private global::System.Windows.Forms.Button btnChooseApps;

		// Token: 0x040000E7 RID: 231
		private global::System.Windows.Forms.Label lbMButton;

		// Token: 0x040000E8 RID: 232
		private global::System.Windows.Forms.Label lbLButton;

		// Token: 0x040000E9 RID: 233
		private global::System.Windows.Forms.Label lbRButton;
	}
}
