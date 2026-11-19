namespace AutoClicker
{
	// Token: 0x02000031 RID: 49
	public partial class SelectAppsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000A39D File Offset: 0x0000859D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A3BC File Offset: 0x000085BC
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoClicker.SelectAppsForm));
			this.clbApps = new global::System.Windows.Forms.CheckedListBox();
			this.cbAll = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.clbApps.CheckOnClick = true;
			this.clbApps.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.clbApps.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.clbApps.FormattingEnabled = true;
			this.clbApps.HorizontalScrollbar = true;
			this.clbApps.Location = new global::System.Drawing.Point(16, 75);
			this.clbApps.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.clbApps.Name = "clbApps";
			this.clbApps.Size = new global::System.Drawing.Size(590, 292);
			this.clbApps.TabIndex = 1;
			this.clbApps.Click += new global::System.EventHandler(this.clbApps_Click);
			this.cbAll.AutoSize = true;
			this.cbAll.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.cbAll.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.cbAll.Location = new global::System.Drawing.Point(16, 16);
			this.cbAll.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cbAll.Name = "cbAll";
			this.cbAll.Padding = new global::System.Windows.Forms.Padding(11, 10, 11, 20);
			this.cbAll.Size = new global::System.Drawing.Size(590, 59);
			this.cbAll.TabIndex = 2;
			this.cbAll.Text = "AutoClicker in allen Anwendungen aktiveren";
			this.cbAll.UseVisualStyleBackColor = true;
			this.cbAll.CheckedChanged += new global::System.EventHandler(this.cbAll_CheckedChanged);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(622, 383);
			base.Controls.Add(this.clbApps);
			base.Controls.Add(this.cbAll);
			base.Icon = (global::System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(2, 2, 2, 2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(400, 200);
			base.Name = "SelectAppsForm";
			base.Padding = new global::System.Windows.Forms.Padding(16);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Apps";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400011D RID: 285
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400011E RID: 286
		private global::System.Windows.Forms.CheckedListBox clbApps;

		// Token: 0x0400011F RID: 287
		private global::System.Windows.Forms.CheckBox cbAll;
	}
}
