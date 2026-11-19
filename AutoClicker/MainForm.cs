using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AutoClicker.Properties;
using AutoClicker.Wizard;
using FabiShared;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000025 RID: 37
	internal partial class MainForm : Form
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000061A4 File Offset: 0x000043A4
		public MainForm()
		{
			this.InitializeComponent();
			this.graphics = base.CreateGraphics();
			this.ADV_SETTINGS_PANEL_HEIGHT = (int)(220f * (this.graphics.DpiY / 96f));
			this.exitForUpdate = false;
			if (Settings.Default.UpgradeRequired)
			{
				Settings.Default.Upgrade();
				Settings.Default.UpgradeRequired = false;
			}
			this.InitI18n();
			Updater up = new Updater("https://fabi.me/wp-json/", 713L);
			up.Check(delegate(Updater.CheckResponse result)
			{
				if (result.updateAvailable && MessageBox.Show(this.UpdateAvailableMsg, "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Process.Start(result.articleUrl);
					this.exitForUpdate = true;
					SettingsLib.SetStartup(false);
					Application.Exit();
				}
			});
			this.ac = new AC();
			this.ac.StateChanged += this.ac_StateChanged;
			try
			{
				this._ds = new DeamonScheduler();
			}
			catch
			{
			}
			base.Icon = Resources.TrayIcon;
			this.trayIcon.Icon = base.Icon;
			this.numClickRate.Maximum = decimal.MaxValue;
			this.numClickLimit.Maximum = 4294967295m;
			this.ApplySettings();
			this.ac.Start();
			string[] args = Environment.GetCommandLineArgs();
			bool isAutostart = Array.IndexOf<string>(args, "-startup") >= 0;
			bool selfstart = Array.IndexOf<string>(args, "-selfstart") >= 0;
			if (isAutostart)
			{
				base.WindowState = FormWindowState.Minimized;
			}
			if (!selfstart && !isAutostart && !string.IsNullOrEmpty(this.trayIcon.BalloonTipText))
			{
				this.trayIcon.ShowBalloonTip(5000);
			}
			this.ApplyStyle();
			this.prevClickRate = this.numClickRate.Value;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000634C File Offset: 0x0000454C
		private void ApplyStyle()
		{
			this.lbActKey.BackColor = Color.FromArgb(236, 240, 241);
			this.lbActKey.FlatStyle = FlatStyle.Flat;
			this.ForAllControls(this, delegate(Control c)
			{
				if (c.GetType() == typeof(Button))
				{
					Button btn = c as Button;
					if (btn.Text.Length > 0)
					{
						btn.BackColor = Color.FromArgb(52, 152, 219);
						btn.FlatStyle = FlatStyle.Flat;
						btn.FlatAppearance.BorderSize = 0;
						btn.Text = btn.Text.ToUpper();
						btn.Cursor = Cursors.Hand;
						btn.ForeColor = Color.White;
					}
				}
			});
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000063AC File Offset: 0x000045AC
		public void ForAllControls(Control parent, Action<Control> action)
		{
			foreach (object obj in parent.Controls)
			{
				Control c = (Control)obj;
				action(c);
				this.ForAllControls(c, action);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006410 File Offset: 0x00004610
		private void ac_StateChanged(bool active, bool enabled)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new AC.StateChangedEventHandler(this.ac_StateChanged), new object[]
				{
					active,
					enabled
				});
				return;
			}
			if ((this.trayIcon.Tag != null && (bool)this.trayIcon.Tag) != active)
			{
				this.trayIcon.Icon = (active ? Resources.TrayIconActive : Resources.TrayIcon);
				this.trayIcon.Tag = active;
			}
			base.Icon = this.trayIcon.Icon;
			DeamonScheduler ds = this._ds;
			if (ds == null)
			{
				return;
			}
			ds.SetACState(active);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000064C4 File Offset: 0x000046C4
		~MainForm()
		{
			if (this.ac != null)
			{
				this.ac.Stop();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006500 File Offset: 0x00004700
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.ac.Stop();
			this.UpdateSettings();
			DeamonScheduler ds = this._ds;
			if (ds != null)
			{
				ds.Stop();
			}
			if (this._ds != null)
			{
				this.ac.SubmitStats(this._ds.GetRunTimeRatio());
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006550 File Offset: 0x00004750
		private void ApplySettings()
		{
			try
			{
				if (Settings.Default == null)
				{
					throw new Exception("Settings.Default is null");
				}
				Settings sets = Settings.Default;
				List<Keys> keys = new List<Keys>();
				if (sets.Hotkey == null)
				{
					sets.Hotkey = new StringCollection();
				}
				foreach (string s in sets.Hotkey)
				{
					keys.Add((Keys)Enum.Parse(typeof(Keys), s));
				}
				this.SetActivationKey(keys.ToArray());
				this.ac.ActivationMode = sets.ActivationMode;
				this.rbtnModeHold.Checked = (this.ac.ActivationMode == ActivationMode.Hold);
				this.rbtnModeSwitch.Checked = !this.rbtnModeHold.Checked;
				this.SetMouseButton(sets.MouseButton);
				try
				{
					this.numClickRate.Value = Math.Max((decimal)sets.ClickRate, this.numClickRate.Minimum);
				}
				catch (Exception e)
				{
					SentrySdk.CaptureException(e);
				}
				this.chkUnlimitedClickRate.Checked = sets.UnlimitedClickRate;
				this.chkRandomizeClickRate.Checked = sets.RandomizeClickRate;
				try
				{
					this.numClickCycle.Value = Math.Min(100m, (decimal)sets.ClickCycle);
				}
				catch (Exception e2)
				{
					SentrySdk.CaptureException(e2);
				}
				try
				{
					this.numClickLimit.Value = sets.ClickLimit;
				}
				catch (Exception e3)
				{
					SentrySdk.CaptureException(e3);
				}
				this.chkClickLimit.Checked = sets.EnableClickLimit;
				this.chkStartup.Checked = sets.Startup;
				SettingsLib.SetStartup(sets.Startup, true);
				this.chkStats.Checked = (this.ac.SendStats = sets.SendStats);
				this.pnlAdvSettings.Visible = sets.ShowAdvSettings;
				this.pnlAdvSettings.Height = (sets.ShowAdvSettings ? this.ADV_SETTINGS_PANEL_HEIGHT : 0);
				if (sets.WindowTitles == null)
				{
					sets.WindowTitles = new StringCollection();
				}
				string[] tits = new string[sets.WindowTitles.Count];
				sets.WindowTitles.CopyTo(tits, 0);
				this.ac.WindowTitles = (sets.AllWindows ? new string[0] : tits);
			}
			catch (Exception e4)
			{
				SentrySdk.CaptureException(e4);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006838 File Offset: 0x00004A38
		private void UpdateSettings()
		{
			if (Settings.Default == null)
			{
				throw new Exception("Settings.Default is null");
			}
			Settings sets = Settings.Default;
			StringCollection sc = new StringCollection();
			for (int i = 0; i < this.ac.ActivationKey.Length; i++)
			{
				sc.Add(this.ac.ActivationKey[i].ToString());
			}
			sets.Hotkey = sc;
			sets.ActivationMode = this.ac.ActivationMode;
			sets.ClickRate = this.ac.ClickRate;
			sets.UnlimitedClickRate = this.ac.UnlimitedClickRate;
			sets.RandomizeClickRate = this.ac.RandomizeClickRate;
			sets.ClickCycle = this.ac.ClickCycle;
			sets.EnableClickLimit = this.chkClickLimit.Checked;
			sets.ClickLimit = this.ac.ClickLimit;
			sets.MouseButton = this.ac.MouseButton;
			sets.SendStats = this.chkStats.Checked;
			sets.ShowAdvSettings = this.pnlAdvSettings.Visible;
			try
			{
				sets.Save();
			}
			catch (IOException e)
			{
			}
			catch (Exception e2)
			{
				SentrySdk.CaptureException(e2);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006984 File Offset: 0x00004B84
		public void SetActivationKey(Keys[] key)
		{
			this.ac.ActivationKey = key;
			string[] keyNames = new string[key.Length];
			for (int i = 0; i < key.Length; i++)
			{
				keyNames[i] = key[i].ToString();
			}
			this.lbActKey.Text = string.Join("+", keyNames);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000069E0 File Offset: 0x00004BE0
		public void SetMouseButton(MouseBtns btn)
		{
			Image btnOverlay = null;
			if (btn != MouseBtns.Left)
			{
				if (btn != MouseBtns.Right)
				{
					if (btn == MouseBtns.Middle)
					{
						if (!this.rbtnMouseMiddle.Checked)
						{
							this.rbtnMouseMiddle.Checked = true;
						}
						btnOverlay = Resources.MouseBtnMiddle;
					}
				}
				else
				{
					if (!this.rbtnMouseRight.Checked)
					{
						this.rbtnMouseRight.Checked = true;
					}
					btnOverlay = Resources.MouseBtnRight;
				}
			}
			else
			{
				if (!this.rbtnMouseLeft.Checked)
				{
					this.rbtnMouseLeft.Checked = true;
				}
				btnOverlay = Resources.MouseBtnLeft;
			}
			this.picbxMouse.Image = btnOverlay;
			this.ac.MouseButton = btn;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006A77 File Offset: 0x00004C77
		private Keys mouseBtnToKey(MouseBtns btn)
		{
			if (btn == MouseBtns.Left)
			{
				return Keys.LButton;
			}
			if (btn == MouseBtns.Right)
			{
				return Keys.RButton;
			}
			if (btn != MouseBtns.Middle)
			{
				return Keys.None;
			}
			return Keys.MButton;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006A90 File Offset: 0x00004C90
		private void btnSelectActKey_Click(object sender, EventArgs e)
		{
			this.ac.Stop();
			List<Keys> i;
			using (KeySelectForm f = new KeySelectForm())
			{
				if (f.ShowDialog() == DialogResult.Cancel)
				{
					return;
				}
				i = f.PressedKeys;
			}
			this.SetActivationKey(i.ToArray());
			base.BringToFront();
			this.ac.Start();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006AFC File Offset: 0x00004CFC
		private void rbtnMode_CheckedChanged(object sender, EventArgs e)
		{
			this.ac.ActivationMode = (this.rbtnModeHold.Checked ? ActivationMode.Hold : ActivationMode.Switch);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006B1C File Offset: 0x00004D1C
		private void rbtnMouseButton_CheckedChanged(object sender, EventArgs e)
		{
			MouseBtns btn;
			if (this.rbtnMouseLeft.Checked)
			{
				btn = MouseBtns.Left;
			}
			else if (this.rbtnMouseMiddle.Checked)
			{
				btn = MouseBtns.Middle;
			}
			else
			{
				btn = MouseBtns.Right;
			}
			this.SetMouseButton(btn);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006B58 File Offset: 0x00004D58
		private void numClickRate_ValueChanged(object sender, EventArgs e)
		{
			decimal val = this.numClickRate.Value;
			if (val == 1m)
			{
				this.numClickRate.Increment = 1m;
			}
			else if (val < 1m)
			{
				this.numClickRate.Increment = 0.01m;
			}
			else if (val < 10m)
			{
				this.numClickRate.Increment = 1m;
			}
			else
			{
				this.numClickRate.Increment = 10m;
			}
			this.ac.ClickRate = (float)this.numClickRate.Value;
			this.updateClickTimeInfo();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006C09 File Offset: 0x00004E09
		private void btnExit_Click(object sender, EventArgs e)
		{
			Settings.Default.Startup = false;
			SettingsLib.SetStartup(false, true);
			Settings.Default.StartMinimized = false;
			base.Close();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006C30 File Offset: 0x00004E30
		private void chkUnlimitedClickRate_CheckedChanged(object sender, EventArgs e)
		{
			this.ac.UnlimitedClickRate = this.chkUnlimitedClickRate.Checked;
			this.numClickRate.Enabled = (this.chkRandomizeClickRate.Enabled = (this.numClickCycle.Enabled = !this.ac.UnlimitedClickRate));
			this.updateClickTimeInfo();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006C8E File Offset: 0x00004E8E
		private void btnResetSettings_Click(object sender, EventArgs e)
		{
			SettingsLib.DeleteSettings();
			ACSettingsProvider.Default.Reset();
			Settings.Default.Reset();
			Settings.Default.Save();
			Settings.Default.Reload();
			this.ApplySettings();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private void chkStartup_CheckedChanged(object sender, EventArgs e)
		{
			this.chkStartup.Checked = (Settings.Default.Startup = (SettingsLib.SetStartup(this.chkStartup.Checked, true) && this.chkStartup.Checked));
			SettingsLib.SetStartup(Settings.Default.Startup, true);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006D1B File Offset: 0x00004F1B
		private void btnOk_Click(object sender, EventArgs e)
		{
			Settings.Default.StartMinimized = true;
			this.UpdateSettings();
			this.ApplySettings();
			if (Settings.Default.HideWindow)
			{
				base.Hide();
			}
			else
			{
				base.WindowState = FormWindowState.Minimized;
			}
			Misc.FlushMemory();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006D54 File Offset: 0x00004F54
		private void MainForm_SizeChanged(object sender, EventArgs e)
		{
			if (Settings.Default.HideWindow && base.WindowState == FormWindowState.Minimized)
			{
				base.Hide();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006D71 File Offset: 0x00004F71
		private void trayIcon_Click(object sender, EventArgs e)
		{
			if (!base.Visible)
			{
				base.Show();
				base.WindowState = FormWindowState.Normal;
			}
			base.BringToFront();
			base.Focus();
			FabiProgram.BringToFront(base.Handle);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006DA4 File Offset: 0x00004FA4
		private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			using (AboutForm f = new AboutForm())
			{
				f.ShowDialog();
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006DE4 File Offset: 0x00004FE4
		private void chkClickLimit_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.EnableClickLimit = (this.numClickLimit.Enabled = (this.lbClickLimit1.Enabled = (this.lbClickLimit2.Enabled = this.chkClickLimit.Checked)));
			this.ac.ClickLimit = (Settings.Default.EnableClickLimit ? Math.Max(1U, Settings.Default.ClickLimit) : 0U);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006E5A File Offset: 0x0000505A
		private void numClickLimit_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.ClickLimit = (uint)this.numClickLimit.Value;
			this.ac.ClickLimit = (Settings.Default.EnableClickLimit ? Settings.Default.ClickLimit : 0U);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006E9A File Offset: 0x0000509A
		private void chkRandomizeClickRate_CheckedChanged(object sender, EventArgs e)
		{
			this.ac.RandomizeClickRate = this.chkRandomizeClickRate.Checked;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006EB2 File Offset: 0x000050B2
		private void numClickCycle_ValueChanged(object sender, EventArgs e)
		{
			this.ac.ClickCycle = (float)this.numClickCycle.Value;
			this.updateClickTimeInfo();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006ED8 File Offset: 0x000050D8
		private void updateClickTimeInfo()
		{
			float totalTime = 1000f / this.ac.ClickRate;
			float clickTime = totalTime * this.ac.ClickCycle / 100f;
			float waitTime = totalTime - clickTime;
			this.lbClickTimeInfo.Text = string.Format(this.ClickTimeInfoStr, this.formatTime(clickTime), this.formatTime(waitTime));
			this.lbClickTimeInfo.Enabled = !this.ac.UnlimitedClickRate;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006F50 File Offset: 0x00005150
		private string formatTime(float ms)
		{
			if (ms >= 60000f)
			{
				float min = ms / 60000f;
				return string.Format("{0:0.0} {1}", min, "min");
			}
			if (ms >= 1000f)
			{
				float sec = ms / 1000f;
				return string.Format("{0:0.0} {1}", sec, "sec");
			}
			if (ms < 1f)
			{
				float sec2 = ms * 1000f;
				return string.Format("{0:0.0} {1}", sec2, "µs");
			}
			return string.Format("{0:0.0} {1}", ms, "ms");
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006FE8 File Offset: 0x000051E8
		private void btnWizard_Click(object sender, EventArgs e)
		{
			using (ActivationForm f = new ActivationForm())
			{
				f.RunWizard();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007020 File Offset: 0x00005220
		private void chkStats_CheckedChanged(object sender, EventArgs e)
		{
			this.ac.SendStats = (Settings.Default.SendStats = this.chkStats.Checked);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007050 File Offset: 0x00005250
		private void btnAdvSettings_Click(object sender, EventArgs e)
		{
			Settings.Default.ShowAdvSettings = !Settings.Default.ShowAdvSettings;
			if (this.advSettingsOpening == 0f)
			{
				if (this.pnlAdvSettings.Visible)
				{
					this.advSettingsAnim = 1f;
					this.advSettingsOpening = -0.05f;
				}
				else
				{
					this.advSettingsAnim = 0f;
					this.pnlAdvSettings.Show();
					this.advSettingsOpening = 0.05f;
				}
				this.animTimer.Start();
				return;
			}
			this.advSettingsOpening = -this.advSettingsOpening;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000070E0 File Offset: 0x000052E0
		private void animTimer_Tick(object sender, EventArgs e)
		{
			if ((double)this.advSettingsOpening != 0.0)
			{
				this.advSettingsAnim += this.advSettingsOpening;
				if (this.advSettingsAnim >= 1f)
				{
					this.advSettingsAnim = 1f;
					this.advSettingsOpening = 0f;
				}
				else if (this.advSettingsAnim <= 0f)
				{
					this.advSettingsAnim = 0f;
					this.pnlAdvSettings.Hide();
					this.advSettingsOpening = 0f;
				}
				this.pnlAdvSettings.Height = (int)Math.Round((double)(this.advSettingsAnim * this.advSettingsAnim * (float)this.ADV_SETTINGS_PANEL_HEIGHT));
				return;
			}
			this.animTimer.Stop();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000719B File Offset: 0x0000539B
		private void llnCopyright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://fabi.me/");
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000071A8 File Offset: 0x000053A8
		private void cbxLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Default.Language = ((CultureInfo)this.cbxLanguage.SelectedItem).TwoLetterISOLanguageName;
			this.ApplyLanguage();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000071CF File Offset: 0x000053CF
		private void btnFb_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.facebook.com/speedautoclicker");
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000071DC File Offset: 0x000053DC
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Settings.Default.Startup = false;
				SettingsLib.SetStartup(false, true);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000071FC File Offset: 0x000053FC
		private void button1_Click(object sender, EventArgs e)
		{
			using (SelectAppsForm f = new SelectAppsForm(this.ac.Stats))
			{
				f.AllWindows = Settings.Default.AllWindows;
				f.SetSelectedWindowTitles(Settings.Default.WindowTitles);
				f.ShowDialog();
				Settings.Default.WindowTitles = new StringCollection();
				Settings.Default.WindowTitles.AddRange(f.SelectedWindowTitles);
				Settings.Default.AllWindows = f.AllWindows;
				this.ac.WindowTitles = (f.AllWindows ? new string[0] : f.SelectedWindowTitles);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000072B4 File Offset: 0x000054B4
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.UpdateSettings();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000072BC File Offset: 0x000054BC
		private void lbLButton_Click(object sender, EventArgs e)
		{
			this.rbtnMouseLeft.Checked = true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000072CA File Offset: 0x000054CA
		private void lbMButton_Click(object sender, EventArgs e)
		{
			this.rbtnMouseMiddle.Checked = true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000072D8 File Offset: 0x000054D8
		private void lbRButton_Click(object sender, EventArgs e)
		{
			this.rbtnMouseRight.Checked = true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009730 File Offset: 0x00007930
		private void InitI18n()
		{
			this.cbxLanguage.Items.Add(CultureInfo.GetCultureInfoByIetfLanguageTag("en"));
			this.cbxLanguage.Items.Add(CultureInfo.GetCultureInfoByIetfLanguageTag("de"));
			if (string.IsNullOrEmpty(Settings.Default.Language))
			{
				Settings.Default.Language = ((Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "de") ? "de" : "en");
			}
			this.cbxLanguage.SelectedIndex = ((Settings.Default.Language == "de") ? 1 : 0);
			this.ApplyLanguage();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000097E0 File Offset: 0x000079E0
		private void ApplyLanguage()
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfoByIetfLanguageTag(Settings.Default.Language);
			string language = Settings.Default.Language;
			if (language != null)
			{
				if (language == "de")
				{
					this.TranslateDE();
					goto IL_59;
				}
				if (language == "en")
				{
					this.TranslateEN();
					goto IL_59;
				}
			}
			this.TranslateEN();
			IL_59:
			this.ApplyStyle();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000984C File Offset: 0x00007A4C
		private void TranslateDE()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
			this.ClickTimeInfoStr = "Klicken / Warten: {0} / {1}";
			this.UpdateAvailableMsg = "Es gibt ein Update für Speed AutoClicker. Möchtest du es laden?";
			this.label1.Text = "Aktivierungstaste:";
			this.toolTip.SetToolTip(this.lbActKey, "Die Taste, mit der der AutoClicker aktiviert wird.");
			this.btnSelectActKey.Text = "Taste wählen";
			this.btnChooseApps.Text = "Apps wählen";
			this.toolTip.SetToolTip(this.btnSelectActKey, "Hier klicken, um die Taste auszuwählen.");
			this.groupBox1.Text = "Aktivierung";
			this.rbtnModeHold.Text = "halten";
			this.toolTip.SetToolTip(this.rbtnModeHold, "Der AutoClicker klickt solange, wie die Taste festgehalten wird.");
			this.rbtnModeSwitch.Text = "umschalten";
			this.toolTip.SetToolTip(this.rbtnModeSwitch, "Der AutoClicker wird beim ersten Tastendruck aktiviert und bleibt solange aktiv, bis die Taste ein zweites Mal gedrückt wird.");
			this.label2.Text = "Aktivierungsmodus:";
			this.groupBox2.Text = "Klicks";
			this.toolTip.SetToolTip(this.btnAdvSettings, "Zeige erweiterte Einstellungen");
			this.rbtnMouseMiddle.Text = "mittlere Maustaste (Scrollrad)";
			this.rbtnMouseLeft.Text = "linke Maustaste";
			this.rbtnMouseRight.Text = "rechte Maustaste";
			this.gbxClickRate.Text = "Klickrate";
			this.toolTip.SetToolTip(this.numClickCycle, "Der Klickgrad gibt das zeitliche Verhältnis zwischen Drücken und Loslassen der Maustaste an.\r\nDer Wert 0 % bedeutet, dass die Maustaste nur extrem Kurz gedrückt wird.\r\nBei 100 % wird die Taste kurz losgelassen.\r\nBei 50 % wird die Hälfte der Zeit geklickt und gewartet.");
			this.label3.Text = "Klickgrad:";
			this.chkRandomizeClickRate.Text = "zufällig";
			this.toolTip.SetToolTip(this.chkRandomizeClickRate, resources.GetString("chkRandomizeClickRate.ToolTip"));
			this.toolTip.SetToolTip(this.numClickRate, "Die Anzahl der Klicks pro Sekunde.\r\nVORSICHT: Hohe Werte (ab ca. 500) können die Programme überlasten und die Stabilität beeinträchtigen!");
			this.chkUnlimitedClickRate.Text = "unbegrenzt";
			this.toolTip.SetToolTip(this.chkUnlimitedClickRate, "ACHTUNG: Eine unbegrenzte Klickrate führt zu hoher Prozessor-Auslastung und kann die Stabilität des Systems beeinträchtigen!");
			this.label4.Text = "Klicks pro Sekunde";
			this.groupBox4.Text = "Klickbegrenzung";
			this.lbClickLimit2.Text = "Klicks";
			this.lbClickLimit1.Text = "Maximal";
			this.toolTip.SetToolTip(this.numClickLimit, "Die Anzahl der Klicks die nach der Aktivierung des AutoClickers ausgeführt werden soll");
			this.chkClickLimit.Text = "Klickbegrenzung aktivieren";
			this.toolTip.SetToolTip(this.chkClickLimit, "Bei aktivierter Klickbegrenzung werden beim Drücken der Aktivierungstaste nur die unten eingegebene Anzahl an Klicks ausgeführt.");
			this.btnOk.Text = "&OK";
			this.btnExit.Text = "Be&enden";
			this.groupBox3.Text = "Sonstiges";
			this.chkStats.Text = "Anonyme Nutzungsstatistiken senden";
			this.toolTip.SetToolTip(this.chkStats, "Es werden nur die Anzahl der Klicks für die Klickstatistik übertragen, keine persönlichen Daten.");
			this.btnResetSettings.Text = "Alles zurücksetzen";
			this.toolTip.SetToolTip(this.btnResetSettings, "Setzt alle Einstellungen auf die Standardwerte zurück.");
			this.chkStartup.Text = "Mit Windows starten";
			this.toolTip.SetToolTip(this.chkStartup, "Wenn aktiviert, startet der AutoClicker automatisch beim Hochfahren des PCs.");
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009B5C File Offset: 0x00007D5C
		private void TranslateEN()
		{
			this.ClickTimeInfoStr = "Click / Wait: {0} / {1}";
			this.UpdateAvailableMsg = "There is an update for Speed AutoClicker. Do you want to get it?";
			this.label1.Text = "Activation Key:";
			this.toolTip.SetToolTip(this.lbActKey, "This Key activates AutoClicker.");
			this.btnSelectActKey.Text = "Select...";
			this.btnChooseApps.Text = "Choose Apps";
			this.toolTip.SetToolTip(this.btnSelectActKey, "Click here, to select the Key.");
			this.groupBox1.Text = "Activation";
			this.rbtnModeHold.Text = "hold";
			this.toolTip.SetToolTip(this.rbtnModeHold, "AutoClicker is clicking as long as the Key is pressed.");
			this.rbtnModeSwitch.Text = "toggle";
			this.toolTip.SetToolTip(this.rbtnModeSwitch, "AutoClicker is activated on first key press and keeps running until the key is pressed a second time.");
			this.label2.Text = "Activation Mode:";
			this.groupBox2.Text = "Clicks";
			this.toolTip.SetToolTip(this.btnAdvSettings, "Show advanced Settings");
			this.rbtnMouseMiddle.Text = "middle Mouse Button (Scroll Wheel)";
			this.rbtnMouseLeft.Text = "left Mouse Button";
			this.rbtnMouseRight.Text = "right Mouse Button";
			this.gbxClickRate.Text = "Click rate";
			this.toolTip.SetToolTip(this.numClickCycle, "Der Klickgrad gibt das zeitliche Verhältnis zwischen Drücken und Loslassen der Maustaste an.\r\nDer Wert 0 % bedeutet, dass die Maustaste nur extrem Kurz gedrückt wird.\r\nBei 100 % wird die Taste kurz losgelassen.\r\nBei 50 % wird die Hälfte der Zeit geklickt und gewartet.");
			this.label3.Text = "Click duty cycle:";
			this.chkRandomizeClickRate.Text = "random";
			this.toolTip.SetToolTip(this.chkRandomizeClickRate, "Clicks are made with random intervals, where the mean Click rate equals to the value specified above. This can by-pass some AutoClicker protections of some games.");
			this.toolTip.SetToolTip(this.numClickRate, "Clicks per second.\r\nATTENTION: High values (> 500) can overload applications and lead to system instability!");
			this.chkUnlimitedClickRate.Text = "unlimited";
			this.toolTip.SetToolTip(this.chkUnlimitedClickRate, "CAUTION: Unlimited click rates can casue high CPU usages and lead to system instability!");
			this.label4.Text = "Clicks per second";
			this.groupBox4.Text = "Click limit";
			this.lbClickLimit2.Text = "Clicks";
			this.lbClickLimit1.Text = "Maximum";
			this.toolTip.SetToolTip(this.numClickLimit, "Number of clicks for a single activation of the AutoClicker.");
			this.chkClickLimit.Text = "Enable Click Limit";
			this.toolTip.SetToolTip(this.chkClickLimit, "If Click Limit is enabled, AutoClicker will stop clicking after the specified number.");
			this.btnOk.Text = "&OK";
			this.btnExit.Text = "&Exit";
			this.groupBox3.Text = "Misc";
			this.chkStats.Text = "Send anonymous usage data";
			this.toolTip.SetToolTip(this.chkStats, "Only click statistics are used, no personal data.");
			this.btnResetSettings.Text = "Reset Settings";
			this.toolTip.SetToolTip(this.btnResetSettings, "Set all settings to their default values");
			this.chkStartup.Text = "Autostart with Windows";
			this.toolTip.SetToolTip(this.chkStartup, "AutoClicker will be started when you login to Windows");
		}

		// Token: 0x040000B2 RID: 178
		private int ADV_SETTINGS_PANEL_HEIGHT = 180;

		// Token: 0x040000B3 RID: 179
		private AC ac;

		// Token: 0x040000B4 RID: 180
		private bool exitForUpdate;

		// Token: 0x040000B5 RID: 181
		private readonly DeamonScheduler _ds;

		// Token: 0x040000B6 RID: 182
		private decimal prevClickRate;

		// Token: 0x040000B7 RID: 183
		private Graphics graphics;

		// Token: 0x040000B8 RID: 184
		private float advSettingsOpening;

		// Token: 0x040000B9 RID: 185
		private float advSettingsAnim;

		// Token: 0x040000EA RID: 234
		private string ClickTimeInfoStr;

		// Token: 0x040000EB RID: 235
		private string UpdateAvailableMsg;
	}
}
