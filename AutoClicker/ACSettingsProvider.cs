using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using AutoClicker.Properties;
using FabiShared;
using Sentry;

namespace AutoClicker
{
	// Token: 0x0200001A RID: 26
	internal class ACSettingsProvider : SettingsProvider
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004C57 File Offset: 0x00002E57
		public static ACSettingsProvider Default
		{
			get
			{
				return ACSettingsProvider.defaultProvider;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004C5E File Offset: 0x00002E5E
		public ACSettingsProvider()
		{
			this.SettingsDictionary = new Dictionary<string, ACSettingsProvider.SettingStruct>();
			ACSettingsProvider.defaultProvider = this;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004C77 File Offset: 0x00002E77
		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(this.ApplicationName, config);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004C86 File Offset: 0x00002E86
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003311 File Offset: 0x00001511
		public override string ApplicationName
		{
			get
			{
				return Assembly.GetExecutingAssembly().ManifestModule.Name;
			}
			set
			{
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004C97 File Offset: 0x00002E97
		public void Reset()
		{
			this.SettingsDictionary.Clear();
			this._loaded = false;
			if (File.Exists(this.UserConfigPath))
			{
				File.Delete(this.UserConfigPath);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private object changeType(object value, Type t)
		{
			if (value == null)
			{
				return null;
			}
			try
			{
				return Convert.ChangeType(value, t);
			}
			catch (Exception ex)
			{
				string str = value as string;
				if (t == typeof(StringCollection))
				{
					StringCollection sc = new StringCollection();
					if (string.IsNullOrEmpty(str))
					{
						return sc;
					}
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(str);
					XmlElement arr = xd["ArrayOfString"];
					foreach (object obj in arr)
					{
						XmlElement s = (XmlElement)obj;
						sc.Add(s.InnerText);
					}
					return sc;
				}
				else if (t.IsEnum)
				{
					return Enum.Parse(t, (string)value);
				}
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			if (!this._loaded)
			{
				ACSettingsProvider.defaultProvider = this;
				this._loaded = true;
				try
				{
					this.LoadValuesFromFile();
				}
				catch (IOException)
				{
				}
				catch (Exception e)
				{
					SentrySdk.CaptureException(e);
				}
			}
			SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
			foreach (object obj in collection)
			{
				SettingsProperty setting = (SettingsProperty)obj;
				SettingsPropertyValue value = new SettingsPropertyValue(setting);
				value.IsDirty = false;
				Type t = Type.GetType(setting.PropertyType.FullName);
				if (t == null)
				{
					t = setting.PropertyType;
				}
				if (this.SettingsDictionary.ContainsKey(setting.Name))
				{
					value.SerializedValue = this.SettingsDictionary[setting.Name].value;
					value.PropertyValue = this.changeType(this.SettingsDictionary[setting.Name].value, t);
				}
				else
				{
					value.SerializedValue = setting.DefaultValue;
					value.PropertyValue = this.changeType(setting.DefaultValue, t);
				}
				values.Add(value);
			}
			return values;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004F08 File Offset: 0x00003108
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			if (context == null)
			{
				throw new NullReferenceException("context is null");
			}
			if (collection == null)
			{
				throw new NullReferenceException("collection is null");
			}
			foreach (object obj in collection)
			{
				SettingsPropertyValue value = (SettingsPropertyValue)obj;
				if (value == null)
				{
					throw new NullReferenceException("value in collection is null");
				}
				if (value.Property == null)
				{
					throw new NullReferenceException("value.Property is null");
				}
				if (this.SettingsDictionary == null)
				{
					throw new NullReferenceException("SettingsDictionary is null");
				}
				ACSettingsProvider.SettingStruct setting = new ACSettingsProvider.SettingStruct
				{
					value = ((value.PropertyValue == null || value.SerializedValue == null) ? string.Empty : value.SerializedValue.ToString()),
					name = value.Name,
					serializeAs = value.Property.SerializeAs.ToString()
				};
				if (!this.SettingsDictionary.ContainsKey(value.Name))
				{
					this.SettingsDictionary.Add(value.Name, setting);
				}
				else
				{
					this.SettingsDictionary[value.Name] = setting;
				}
			}
			try
			{
				this.SaveValuesToFile();
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000506C File Offset: 0x0000326C
		private void LoadValuesFromFile()
		{
			if (!File.Exists(this.UserConfigPath))
			{
				try
				{
					this.CreateEmptyConfig();
				}
				catch (Exception ex)
				{
					SentrySdk.CaptureException(ex);
				}
			}
			XDocument configXml = XDocument.Load(this.UserConfigPath);
			IEnumerable<XElement> settingElements = configXml.Element("configuration").Element("userSettings").Element(typeof(Settings).FullName).Elements("setting");
			foreach (XElement element in settingElements)
			{
				ACSettingsProvider.SettingStruct newSetting = new ACSettingsProvider.SettingStruct
				{
					name = ((element.Attribute("name") == null) ? string.Empty : element.Attribute("name").Value),
					serializeAs = ((element.Attribute("serializeAs") == null) ? "String" : element.Attribute("serializeAs").Value),
					value = (element.Value ?? string.Empty)
				};
				this.SettingsDictionary.Add(element.Attribute("name").Value, newSetting);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000051E8 File Offset: 0x000033E8
		private void CreateEmptyConfig()
		{
			string dir = Path.GetDirectoryName(this.UserConfigPath);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			XDocument doc = new XDocument();
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "true");
			XElement config = new XElement("configuration");
			XElement userSettings = new XElement("userSettings");
			XElement group = new XElement(typeof(Settings).FullName);
			userSettings.Add(group);
			config.Add(userSettings);
			doc.Add(config);
			doc.Declaration = declaration;
			doc.Save(this.UserConfigPath);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005294 File Offset: 0x00003494
		private void SaveValuesToFile()
		{
			XDocument import = XDocument.Load(this.UserConfigPath);
			XElement settingsSection = import.Element("configuration").Element("userSettings").Element(typeof(Settings).FullName);
			using (Dictionary<string, ACSettingsProvider.SettingStruct>.Enumerator enumerator = this.SettingsDictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, ACSettingsProvider.SettingStruct> entry = enumerator.Current;
					XElement setting = settingsSection.Elements().FirstOrDefault((XElement e) => e.Attribute("name").Value == entry.Key);
					if (setting == null)
					{
						XElement newSetting = new XElement("setting");
						newSetting.Add(new XAttribute("name", entry.Value.name));
						newSetting.Add(new XAttribute("serializeAs", entry.Value.serializeAs));
						newSetting.Value = (entry.Value.value ?? string.Empty);
						settingsSection.Add(newSetting);
					}
					else
					{
						setting.Value = (entry.Value.value ?? string.Empty);
					}
				}
			}
			Misc.backoff(delegate
			{
				import.Save(this.UserConfigPath);
			});
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005428 File Offset: 0x00003628
		private string UserConfigPath
		{
			get
			{
				string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.CompanyName.Replace(' ', '_');
				return dir + "\\ac.config";
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005460 File Offset: 0x00003660
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00005468 File Offset: 0x00003668
		private Dictionary<string, ACSettingsProvider.SettingStruct> SettingsDictionary { get; set; }

		// Token: 0x0400007A RID: 122
		private const string NAME = "name";

		// Token: 0x0400007B RID: 123
		private const string SERIALIZE_AS = "serializeAs";

		// Token: 0x0400007C RID: 124
		private const string CONFIG = "configuration";

		// Token: 0x0400007D RID: 125
		private const string USER_SETTINGS = "userSettings";

		// Token: 0x0400007E RID: 126
		private const string SETTING = "setting";

		// Token: 0x0400007F RID: 127
		private static ACSettingsProvider defaultProvider;

		// Token: 0x04000081 RID: 129
		private bool _loaded;

		// Token: 0x0200001B RID: 27
		internal struct SettingStruct
		{
			// Token: 0x04000082 RID: 130
			internal string name;

			// Token: 0x04000083 RID: 131
			internal string serializeAs;

			// Token: 0x04000084 RID: 132
			internal string value;
		}
	}
}
