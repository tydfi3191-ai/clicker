using System;

namespace FabiShared
{
	// Token: 0x0200000B RID: 11
	[Flags]
	public enum SLR_FLAGS
	{
		// Token: 0x04000033 RID: 51
		SLR_NO_UI = 1,
		// Token: 0x04000034 RID: 52
		SLR_ANY_MATCH = 2,
		// Token: 0x04000035 RID: 53
		SLR_UPDATE = 4,
		// Token: 0x04000036 RID: 54
		SLR_NOUPDATE = 8,
		// Token: 0x04000037 RID: 55
		SLR_NOSEARCH = 16,
		// Token: 0x04000038 RID: 56
		SLR_NOTRACK = 32,
		// Token: 0x04000039 RID: 57
		SLR_NOLINKINFO = 64,
		// Token: 0x0400003A RID: 58
		SLR_INVOKE_MSI = 128
	}
}
