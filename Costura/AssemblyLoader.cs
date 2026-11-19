using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
	// Token: 0x0200003E RID: 62
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000B3C3 File Offset: 0x000095C3
		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B3D4 File Offset: 0x000095D4
		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name2.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B43C File Offset: 0x0000963C
		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B470 File Offset: 0x00009670
		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using (DeflateStream deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
					{
						MemoryStream memoryStream = new MemoryStream();
						AssemblyLoader.CopyTo(deflateStream, memoryStream);
						memoryStream.Position = 0L;
						return memoryStream;
					}
				}
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B4F4 File Offset: 0x000096F4
		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			string fullName;
			if (resourceNames.TryGetValue(name, out fullName))
			{
				return AssemblyLoader.LoadStream(fullName);
			}
			return null;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B514 File Offset: 0x00009714
		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B53C File Offset: 0x0000973C
		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = requestedAssemblyName.CultureInfo.Name + "." + text;
			}
			byte[] rawAssembly;
			using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = AssemblyLoader.ReadStream(stream);
			}
			using (Stream stream2 = AssemblyLoader.LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B5FC File Offset: 0x000097FC
		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			object obj = AssemblyLoader.nullCacheLock;
			lock (obj)
			{
				if (AssemblyLoader.nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = AssemblyLoader.ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
			if (assembly == null)
			{
				obj = AssemblyLoader.nullCacheLock;
				lock (obj)
				{
					AssemblyLoader.nullCache[e.Name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B6DC File Offset: 0x000098DC
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyLoader()
		{
			AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.win32.primitives", "costura.microsoft.win32.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("netstandard", "costura.netstandard.dll.compressed");
			AssemblyLoader.assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sentry", "costura.sentry.dll.compressed");
			AssemblyLoader.symbolNames.Add("sentry", "costura.sentry.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("sentry.platformabstractions", "costura.sentry.platformabstractions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sentry.protocol", "costura.sentry.protocol.dll.compressed");
			AssemblyLoader.symbolNames.Add("sentry.protocol", "costura.sentry.protocol.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("system.appcontext", "costura.system.appcontext.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.concurrent", "costura.system.collections.concurrent.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections", "costura.system.collections.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.immutable", "costura.system.collections.immutable.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.nongeneric", "costura.system.collections.nongeneric.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.specialized", "costura.system.collections.specialized.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel", "costura.system.componentmodel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel.eventbasedasync", "costura.system.componentmodel.eventbasedasync.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel.primitives", "costura.system.componentmodel.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel.typeconverter", "costura.system.componentmodel.typeconverter.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.console", "costura.system.console.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.data.common", "costura.system.data.common.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.contracts", "costura.system.diagnostics.contracts.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.debug", "costura.system.diagnostics.debug.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.fileversioninfo", "costura.system.diagnostics.fileversioninfo.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.process", "costura.system.diagnostics.process.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.stacktrace", "costura.system.diagnostics.stacktrace.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.textwritertracelistener", "costura.system.diagnostics.textwritertracelistener.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.tools", "costura.system.diagnostics.tools.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.tracesource", "costura.system.diagnostics.tracesource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.tracing", "costura.system.diagnostics.tracing.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.drawing.primitives", "costura.system.drawing.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.dynamic.runtime", "costura.system.dynamic.runtime.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.globalization.calendars", "costura.system.globalization.calendars.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.globalization", "costura.system.globalization.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.globalization.extensions", "costura.system.globalization.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.compression", "costura.system.io.compression.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.compression.zipfile", "costura.system.io.compression.zipfile.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io", "costura.system.io.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem", "costura.system.io.filesystem.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem.driveinfo", "costura.system.io.filesystem.driveinfo.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem.primitives", "costura.system.io.filesystem.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem.watcher", "costura.system.io.filesystem.watcher.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.isolatedstorage", "costura.system.io.isolatedstorage.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.memorymappedfiles", "costura.system.io.memorymappedfiles.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.pipes", "costura.system.io.pipes.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.unmanagedmemorystream", "costura.system.io.unmanagedmemorystream.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq", "costura.system.linq.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq.expressions", "costura.system.linq.expressions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq.parallel", "costura.system.linq.parallel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq.queryable", "costura.system.linq.queryable.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.http", "costura.system.net.http.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.nameresolution", "costura.system.net.nameresolution.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.networkinformation", "costura.system.net.networkinformation.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.ping", "costura.system.net.ping.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.primitives", "costura.system.net.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.requests", "costura.system.net.requests.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.security", "costura.system.net.security.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.sockets", "costura.system.net.sockets.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.webheadercollection", "costura.system.net.webheadercollection.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.websockets.client", "costura.system.net.websockets.client.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.websockets", "costura.system.net.websockets.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.objectmodel", "costura.system.objectmodel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.reflection", "costura.system.reflection.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.reflection.extensions", "costura.system.reflection.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.reflection.primitives", "costura.system.reflection.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.resources.reader", "costura.system.resources.reader.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.resources.resourcemanager", "costura.system.resources.resourcemanager.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.resources.writer", "costura.system.resources.writer.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.compilerservices.visualc", "costura.system.runtime.compilerservices.visualc.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime", "costura.system.runtime.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.extensions", "costura.system.runtime.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.handles", "costura.system.runtime.handles.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.interopservices", "costura.system.runtime.interopservices.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.interopservices.runtimeinformation", "costura.system.runtime.interopservices.runtimeinformation.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.numerics", "costura.system.runtime.numerics.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.formatters", "costura.system.runtime.serialization.formatters.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.json", "costura.system.runtime.serialization.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.primitives", "costura.system.runtime.serialization.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.xml", "costura.system.runtime.serialization.xml.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.claims", "costura.system.security.claims.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.algorithms", "costura.system.security.cryptography.algorithms.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.csp", "costura.system.security.cryptography.csp.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.encoding", "costura.system.security.cryptography.encoding.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.primitives", "costura.system.security.cryptography.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.x509certificates", "costura.system.security.cryptography.x509certificates.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.principal", "costura.system.security.principal.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.securestring", "costura.system.security.securestring.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.encoding", "costura.system.text.encoding.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.encoding.extensions", "costura.system.text.encoding.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.regularexpressions", "costura.system.text.regularexpressions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading", "costura.system.threading.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.overlapped", "costura.system.threading.overlapped.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks", "costura.system.threading.tasks.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks.parallel", "costura.system.threading.tasks.parallel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.thread", "costura.system.threading.thread.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.threadpool", "costura.system.threading.threadpool.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.timer", "costura.system.threading.timer.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.valuetuple", "costura.system.valuetuple.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.readerwriter", "costura.system.xml.readerwriter.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xdocument", "costura.system.xml.xdocument.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xmldocument", "costura.system.xml.xmldocument.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xmlserializer", "costura.system.xml.xmlserializer.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xpath", "costura.system.xml.xpath.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xpath.xdocument", "costura.system.xml.xpath.xdocument.dll.compressed");
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BF31 File Offset: 0x0000A131
		public static void Attach()
		{
			if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
			{
				return;
			}
			AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// Token: 0x04000134 RID: 308
		private static object nullCacheLock = new object();

		// Token: 0x04000135 RID: 309
		private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		// Token: 0x04000136 RID: 310
		private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		// Token: 0x04000137 RID: 311
		private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		// Token: 0x04000138 RID: 312
		private static int isAttached;
	}
}
