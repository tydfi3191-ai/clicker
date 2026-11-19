namespace AutoClicker.Wizard
{
	// Token: 0x02000039 RID: 57
	public partial class ClickForm : global::AutoClicker.Wizard.WizardForm
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000AE27 File Offset: 0x00009027
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000AE46 File Offset: 0x00009046
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.Text = "ClickForm";
		}

		// Token: 0x0400012C RID: 300
		private global::System.ComponentModel.IContainer components;
	}
}
