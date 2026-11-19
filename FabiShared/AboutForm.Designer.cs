namespace FabiShared
{
	// Token: 0x02000002 RID: 2
	public partial class AboutForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021AB File Offset: 0x000003AB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021CC File Offset: 0x000003CC
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.lbAppName = new global::System.Windows.Forms.Label();
			this.lbAppVersion = new global::System.Windows.Forms.Label();
			this.lbAppVersionVal = new global::System.Windows.Forms.Label();
			this.picIcon = new global::System.Windows.Forms.PictureBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lbCopyrightVal = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.llbWebsite = new global::System.Windows.Forms.LinkLabel();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picIcon).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 40.41096f));
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 59.58904f));
			this.tableLayoutPanel1.Controls.Add(this.lbAppName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbAppVersion, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbAppVersionVal, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.picIcon, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lbCopyrightVal, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.llbWebsite, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnOK, 1, 4);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 80f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 30f));
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(220, 201);
			this.tableLayoutPanel1.TabIndex = 0;
			this.lbAppName.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbAppName.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbAppName.Location = new global::System.Drawing.Point(91, 0);
			this.lbAppName.Name = "lbAppName";
			this.lbAppName.Size = new global::System.Drawing.Size(126, 80);
			this.lbAppName.TabIndex = 1;
			this.lbAppName.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lbAppVersion.AutoSize = true;
			this.lbAppVersion.Location = new global::System.Drawing.Point(3, 80);
			this.lbAppVersion.Name = "lbAppVersion";
			this.lbAppVersion.Size = new global::System.Drawing.Size(42, 13);
			this.lbAppVersion.TabIndex = 2;
			this.lbAppVersion.Text = "Version";
			this.lbAppVersionVal.AutoSize = true;
			this.lbAppVersionVal.Location = new global::System.Drawing.Point(91, 80);
			this.lbAppVersionVal.Name = "lbAppVersionVal";
			this.lbAppVersionVal.Size = new global::System.Drawing.Size(0, 13);
			this.lbAppVersionVal.TabIndex = 3;
			this.picIcon.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.picIcon.Location = new global::System.Drawing.Point(3, 3);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new global::System.Drawing.Size(82, 74);
			this.picIcon.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(3, 110);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(51, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Copyright";
			this.lbCopyrightVal.AutoSize = true;
			this.lbCopyrightVal.Location = new global::System.Drawing.Point(91, 110);
			this.lbCopyrightVal.Name = "lbCopyrightVal";
			this.lbCopyrightVal.Size = new global::System.Drawing.Size(0, 13);
			this.lbCopyrightVal.TabIndex = 5;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(3, 140);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(46, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Website";
			this.llbWebsite.AutoSize = true;
			this.llbWebsite.Location = new global::System.Drawing.Point(91, 140);
			this.llbWebsite.Name = "llbWebsite";
			this.llbWebsite.Size = new global::System.Drawing.Size(0, 13);
			this.llbWebsite.TabIndex = 7;
			this.llbWebsite.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbWebsite_LinkClicked);
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(91, 173);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 8;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(244, 225);
			base.Controls.Add(this.tableLayoutPanel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutForm";
			base.Padding = new global::System.Windows.Forms.Padding(12);
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutForm";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.AboutForm_FormClosed);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picIcon).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000001 RID: 1
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000002 RID: 2
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000003 RID: 3
		private global::System.Windows.Forms.PictureBox picIcon;

		// Token: 0x04000004 RID: 4
		private global::System.Windows.Forms.Label lbAppName;

		// Token: 0x04000005 RID: 5
		private global::System.Windows.Forms.Label lbAppVersion;

		// Token: 0x04000006 RID: 6
		private global::System.Windows.Forms.Label lbAppVersionVal;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.Label lbCopyrightVal;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.LinkLabel llbWebsite;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.Button btnOK;
	}
}
