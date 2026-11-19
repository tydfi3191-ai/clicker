using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FabiShared
{
	// Token: 0x02000010 RID: 16
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("000214EE-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IShellLinkA
	{
		// Token: 0x06000052 RID: 82
		void GetPath([MarshalAs(UnmanagedType.LPStr)] [Out] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATAA pfd, SLGP_FLAGS fFlags);

		// Token: 0x06000053 RID: 83
		void GetIDList(out IntPtr ppidl);

		// Token: 0x06000054 RID: 84
		void SetIDList(IntPtr pidl);

		// Token: 0x06000055 RID: 85
		void GetDescription([MarshalAs(UnmanagedType.LPStr)] [Out] StringBuilder pszName, int cchMaxName);

		// Token: 0x06000056 RID: 86
		void SetDescription([MarshalAs(UnmanagedType.LPStr)] string pszName);

		// Token: 0x06000057 RID: 87
		void GetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] [Out] StringBuilder pszDir, int cchMaxPath);

		// Token: 0x06000058 RID: 88
		void SetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] string pszDir);

		// Token: 0x06000059 RID: 89
		void GetArguments([MarshalAs(UnmanagedType.LPStr)] [Out] StringBuilder pszArgs, int cchMaxPath);

		// Token: 0x0600005A RID: 90
		void SetArguments([MarshalAs(UnmanagedType.LPStr)] string pszArgs);

		// Token: 0x0600005B RID: 91
		void GetHotkey(out short pwHotkey);

		// Token: 0x0600005C RID: 92
		void SetHotkey(short wHotkey);

		// Token: 0x0600005D RID: 93
		void GetShowCmd(out int piShowCmd);

		// Token: 0x0600005E RID: 94
		void SetShowCmd(int iShowCmd);

		// Token: 0x0600005F RID: 95
		void GetIconLocation([MarshalAs(UnmanagedType.LPStr)] [Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

		// Token: 0x06000060 RID: 96
		void SetIconLocation([MarshalAs(UnmanagedType.LPStr)] string pszIconPath, int iIcon);

		// Token: 0x06000061 RID: 97
		void SetRelativePath([MarshalAs(UnmanagedType.LPStr)] string pszPathRel, int dwReserved);

		// Token: 0x06000062 RID: 98
		void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

		// Token: 0x06000063 RID: 99
		void SetPath([MarshalAs(UnmanagedType.LPStr)] string pszFile);
	}
}
