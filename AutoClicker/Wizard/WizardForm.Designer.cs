namespace AutoClicker.Wizard
{
	// Token: 0x0200003A RID: 58
	public partial class WizardForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000AF74 File Offset: 0x00009174
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000AF94 File Offset: 0x00009194
		private void InitializeComponent()
		{
			this.btnPrev = new global::System.Windows.Forms.Button();
			this.btnNext = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new global::System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.btnPrev.AutoSize = true;
			this.btnPrev.Location = new global::System.Drawing.Point(253, 3);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new global::System.Drawing.Size(75, 23);
			this.btnPrev.TabIndex = 101;
			this.btnPrev.Text = "< Zurück";
			this.btnPrev.UseVisualStyleBackColor = true;
			this.btnPrev.Click += new global::System.EventHandler(this.btnPrev_Click);
			this.btnNext.AutoSize = true;
			this.btnNext.Location = new global::System.Drawing.Point(334, 3);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new global::System.Drawing.Size(75, 23);
			this.btnNext.TabIndex = 102;
			this.btnNext.Text = "Weiter >";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new global::System.EventHandler(this.btnNext_Click);
			this.btnCancel.AutoSize = true;
			this.btnCancel.Location = new global::System.Drawing.Point(415, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(76, 23);
			this.btnCancel.TabIndex = 103;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Controls.Add(this.btnNext);
			this.flowLayoutPanel1.Controls.Add(this.btnPrev);
			this.flowLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.FlowDirection = global::System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new global::System.Drawing.Point(0, 336);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new global::System.Drawing.Size(494, 36);
			this.flowLayoutPanel1.TabIndex = 4;
			base.AcceptButton = this.btnNext;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(494, 372);
			base.Controls.Add(this.flowLayoutPanel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.Name = "WizardForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Speed AutoClicker Assistent";
			base.Load += new global::System.EventHandler(this.WizardForm_Load);
			base.Shown += new global::System.EventHandler(this.WizardForm_Shown);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400012F RID: 303
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000130 RID: 304
		private global::System.Windows.Forms.Button btnPrev;

		// Token: 0x04000131 RID: 305
		private global::System.Windows.Forms.Button btnNext;

		// Token: 0x04000132 RID: 306
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000133 RID: 307
		private global::System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	}
}
