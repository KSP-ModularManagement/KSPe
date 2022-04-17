using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class AssemblyLoader
{
	public class LoadedAssembly
	{
		public string name => "name";
		public string path => "path";
		public System.Reflection.Assembly assembly = typeof(AssemblyLoader).Assembly;

		public void Load()
		{
			throw new NotImplementedException();
		}
	}

	public class LoadedAssemblyList:IEnumerable, IEnumerable<LoadedAssembly>
	{
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator<LoadedAssembly> IEnumerable<LoadedAssembly>.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}

	public static LoadedAssemblyList loadedAssemblies;

	public static bool LoadPlugin(FileInfo fi, string absoluteUri, ConfigNode cn)
	{
		throw new NotImplementedException();
	}
}
