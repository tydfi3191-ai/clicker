using System;
using System.Runtime.InteropServices;

namespace FabiShared
{
	// Token: 0x0200000F RID: 15
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010B-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IPersistFile
	{
		// Token: 0x0600004C RID: 76
		void GetClassID(out Guid pClassID);

		// Token: 0x0600004D RID: 77
		[PreserveSig]
		int IsDirty();

		// Token: 0x0600004E RID: 78
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x0600004F RID: 79
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x06000050 RID: 80
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x06000051 RID: 81
		void GetCurFile(out IntPtr ppszFileName);
	}
}
