using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
	// Token: 0x02000031 RID: 49
	public partial class SelectAppsForm : Form
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000A178 File Offset: 0x00008378
		internal SelectAppsForm(ACStats acStats)
		{
			this.InitializeComponent();
			acStats.UpdateRunningAppList();
			ListBox.ObjectCollection items = this.clbApps.Items;
			object[] items2 = acStats.GetRunningApps().ToArray();
			items.AddRange(items2);
			if (Thread.CurrentThread.CurrentUICulture.IetfLanguageTag == "en")
			{
				this.cbAll.Text = "Activate AutoClicker in all apps";
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public void SetSelectedWindowTitles(string[] array)
		{
			int i = 0;
			List<string> ts = new List<string>();
			foreach (object title in this.clbApps.Items)
			{
				this.clbApps.SetItemChecked(i++, false);
				ts.Add(AC.normalizeWindowTitle(title as string));
			}
			foreach (string title2 in array)
			{
				int ti = ts.IndexOf(AC.normalizeWindowTitle(title2));
				if (ti == -1)
				{
					this.clbApps.Items.Add(title2, true);
				}
				else
				{
					this.clbApps.SetItemChecked(ti, true);
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public void SetSelectedWindowTitles(StringCollection col)
		{
			if (col == null)
			{
				this.SetSelectedWindowTitles(new string[0]);
				return;
			}
			string[] arr = new string[col.Count];
			col.CopyTo(arr, 0);
			this.SetSelectedWindowTitles(arr);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000A2EC File Offset: 0x000084EC
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000A2F9 File Offset: 0x000084F9
		public bool AllWindows
		{
			get
			{
				return this.cbAll.Checked;
			}
			set
			{
				this.cbAll.Checked = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000A308 File Offset: 0x00008508
		public string[] SelectedWindowTitles
		{
			get
			{
				List<string> ts = new List<string>();
				foreach (object title in this.clbApps.CheckedItems)
				{
					ts.Add(title as string);
				}
				return ts.ToArray();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A374 File Offset: 0x00008574
		private void cbAll_CheckedChanged(object sender, EventArgs e)
		{
			this.clbApps.Enabled = !this.cbAll.Checked;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A38F File Offset: 0x0000858F
		private void clbApps_Click(object sender, EventArgs e)
		{
			this.cbAll.Checked = false;
		}
	}
}
