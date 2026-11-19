namespace AutoClicker.Wizard
{
	// Token: 0x02000038 RID: 56
	public partial class ActivationForm : global::AutoClicker.Wizard.WizardForm
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000AD86 File Offset: 0x00008F86
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(494, 372);
			base.Name = "ActivationForm";
			base.NextForm = typeof(global::AutoClicker.Wizard.ClickForm);
			this.Text = "ActivationForm";
			base.ResumeLayout(false);
		}

		// Token: 0x0400012B RID: 299
		private global::System.ComponentModel.IContainer components;
	}
}
