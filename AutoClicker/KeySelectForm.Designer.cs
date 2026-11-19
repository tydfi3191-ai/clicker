namespace AutoClicker
{
	// Token: 0x0200001F RID: 31
	internal partial class KeySelectForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005BA0 File Offset: 0x00003DA0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005BC0 File Offset: 0x00003DC0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoClicker.KeySelectForm));
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.BackColor = global::System.Drawing.Color.Transparent;
			resources.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = global::System.Drawing.Color.White;
			this.label1.Name = "label1";
			this.label1.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.KeySelectForm_MouseDown);
			this.label1.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.KeySelectForm_MouseUp);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.White;
			resources.ApplyResources(this, "$this");
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Name = "KeySelectForm";
			base.TopMost = true;
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.KeySelectForm_FormClosed);
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.KeySelectForm_KeyDown);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.KeySelectForm_KeyUp);
			base.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.KeySelectForm_MouseDown);
			base.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.KeySelectForm_MouseUp);
			base.ResumeLayout(false);
		}

		// Token: 0x0400009F RID: 159
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000A0 RID: 160
		private global::System.Windows.Forms.Label label1;
	}
}
