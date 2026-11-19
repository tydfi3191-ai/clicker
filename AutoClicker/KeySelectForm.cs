using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using AutoClicker.Properties;

namespace AutoClicker
{
	// Token: 0x0200001F RID: 31
	internal partial class KeySelectForm : Form
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005951 File Offset: 0x00003B51
		public List<Keys> PressedKeys
		{
			get
			{
				return this.keysPressed;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000595C File Offset: 0x00003B5C
		public KeySelectForm()
		{
			this.InitializeComponent();
			base.DialogResult = DialogResult.Cancel;
			this.BackColor = Color.FromArgb(243, 156, 18);
			string language = Settings.Default.Language;
			if (language != null && language == "en")
			{
				this.label1.Text = "Press any\nkey or\nkey sequence";
			}
			this.overlay = new Form();
			this.overlay.Opacity = 0.5;
			this.overlay.WindowState = FormWindowState.Maximized;
			this.overlay.BackColor = Color.Black;
			this.overlay.FormBorderStyle = FormBorderStyle.None;
			this.overlay.Show();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005A1B File Offset: 0x00003C1B
		private void KeySelectForm_KeyDown(object sender, KeyEventArgs e)
		{
			this.keyDown(e.KeyCode);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A29 File Offset: 0x00003C29
		private void KeySelectForm_KeyUp(object sender, KeyEventArgs e)
		{
			this.keyUp(e.KeyCode);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005A38 File Offset: 0x00003C38
		private void keyDown(Keys key)
		{
			if (key == Keys.None || this.keysPressed.Contains(key))
			{
				return;
			}
			this.keysPressed.Add(key);
			this.numKeysDown++;
			string[] kn = new string[this.keysPressed.Count];
			for (int i = 0; i < kn.Length; i++)
			{
				kn[i] = this.keysPressed[i].ToString();
			}
			this.label1.Text = string.Join(" + ", kn);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005AC4 File Offset: 0x00003CC4
		private void keyUp(Keys key)
		{
			this.numKeysDown--;
			if (this.numKeysDown <= 1)
			{
				this.BackColor = Color.FromArgb(46, 204, 113);
				base.Invalidate();
				Application.DoEvents();
				base.DialogResult = DialogResult.OK;
				Thread.Sleep(500);
				base.Close();
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005B1E File Offset: 0x00003D1E
		private void KeySelectForm_MouseDown(object sender, MouseEventArgs e)
		{
			this.keyDown(KeySelectForm.mouseBtnToKey(e.Button));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005B34 File Offset: 0x00003D34
		private static Keys mouseBtnToKey(MouseButtons btn)
		{
			if (btn <= MouseButtons.Right)
			{
				if (btn == MouseButtons.Left)
				{
					return Keys.LButton;
				}
				if (btn == MouseButtons.Right)
				{
					return Keys.RButton;
				}
			}
			else
			{
				if (btn == MouseButtons.Middle)
				{
					return Keys.MButton;
				}
				if (btn == MouseButtons.XButton1)
				{
					return Keys.XButton1;
				}
				if (btn == MouseButtons.XButton2)
				{
					return Keys.XButton2;
				}
			}
			return Keys.None;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005B80 File Offset: 0x00003D80
		private void KeySelectForm_MouseUp(object sender, MouseEventArgs e)
		{
			this.keyUp(KeySelectForm.mouseBtnToKey(e.Button));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005B93 File Offset: 0x00003D93
		private void KeySelectForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.overlay.Dispose();
		}

		// Token: 0x0400009C RID: 156
		private List<Keys> keysPressed = new List<Keys>();

		// Token: 0x0400009D RID: 157
		private int numKeysDown;

		// Token: 0x0400009E RID: 158
		private Form overlay;
	}
}
