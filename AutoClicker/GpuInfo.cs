using System;
using System.Management;
using System.Text;
using System.Windows.Forms;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000032 RID: 50
	internal class GpuInfo
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000A664 File Offset: 0x00008864
		public static bool isOnBattery()
		{
			bool result;
			try
			{
				result = (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline);
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public static string getString()
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");
				foreach (ManagementBaseObject managementBaseObject in objvide.Get())
				{
					ManagementObject obj = (ManagementObject)managementBaseObject;
					sb.Append(obj["Name"]).Append(",");
				}
				sb.Replace("(R)", "");
				sb.Replace("(TM)", "");
				sb.Replace("  ", " ");
				sb.Remove(sb.Length - 1, 1);
				return sb.ToString();
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
			return "<error>";
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A788 File Offset: 0x00008988
		public static string getCpuName()
		{
			try
			{
				ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = mos.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject mo = (ManagementObject)enumerator.Current;
						return mo["Name"].ToString().Replace("(R)", "").Replace("(TM)", "").Replace("  ", " ");
					}
				}
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
			return "<error>";
		}
	}
}
