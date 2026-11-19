using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace AutoClicker.Wizard
{
	// Token: 0x0200003B RID: 59
	public class WizardFormTypeConverter : TypeConverter
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000B2DA File Offset: 0x000094DA
		private static bool wizardFormTypeFilter(Type t, object obj)
		{
			return t.IsClass && t.IsSubclassOf(typeof(WizardForm));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<Type> list = new List<Type>();
			Type[] types = Assembly.GetAssembly(typeof(WizardForm)).ManifestModule.FindTypes(new TypeFilter(WizardFormTypeConverter.wizardFormTypeFilter), null);
			return new TypeConverter.StandardValuesCollection(types);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B33A File Offset: 0x0000953A
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B33A File Offset: 0x0000953A
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B33D File Offset: 0x0000953D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Type);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000B363 File Offset: 0x00009563
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				return Assembly.GetAssembly(typeof(WizardForm)).GetType(value.ToString());
			}
			return value;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000B398 File Offset: 0x00009598
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
