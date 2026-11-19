using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FabiShared
{
	// Token: 0x02000011 RID: 17
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("000214F9-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IShellLinkW
	{
		// Token: 0x06000064 RID: 100
		void GetPath([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATAW pfd, SLGP_FLAGS fFlags);

		// Token: 0x06000065 RID: 101
		void GetIDList(out IntPtr ppidl);

		// Token: 0x06000066 RID: 102
		void SetIDList(IntPtr pidl);

		// Token: 0x06000067 RID: 103
		void GetDescription([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszName, int cchMaxName);

		// Token: 0x06000068 RID: 104
		void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

		// Token: 0x06000069 RID: 105
		void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszDir, int cchMaxPath);

		// Token: 0x0600006A RID: 106
		void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

		// Token: 0x0600006B RID: 107
		void GetArguments([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszArgs, int cchMaxPath);

		// Token: 0x0600006C RID: 108
		void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

		// Token: 0x0600006D RID: 109
		void GetHotkey(out short pwHotkey);

		// Token: 0x0600006E RID: 110
		void SetHotkey(short wHotkey);

		// Token: 0x0600006F RID: 111
		void GetShowCmd(out int piShowCmd);

		// Token: 0x06000070 RID: 112
		void SetShowCmd(int iShowCmd);

		// Token: 0x06000071 RID: 113
		void GetIconLocation([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

		// Token: 0x06000072 RID: 114
		void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

		// Token: 0x06000073 RID: 115
		void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

		// Token: 0x06000074 RID: 116
		void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

		// Token: 0x06000075 RID: 117
		void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
	}
}
