/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using SIO = System.IO;
using UTexture2D = UnityEngine.Texture2D;

namespace KSPe.IO
{
	// New Public interface for KSPe 2.1.
	public static class Save<T>
	{
		public class ConfigNode: WritableConfigNode
		{
			protected ConfigNode(string name, string fn): base(name)
			{
				this.Path = fn;
			}

			public new ConfigNode Load()
			{
				return (ConfigNode)base.Load();
			}

			public static ConfigNode For(string name = null)
			{
				string path = IO.File<T>.Save.FullPathName(true, (name ?? typeof(T).FullName) + ".cfg");
				return new ConfigNode(name, path);
			}

			public static ConfigNode For(string name, string filename)
			{
				string path = IO.File<T>.Save.FullPathName(true, filename);
				return new ConfigNode(name, path);
			}

			public static ConfigNode For(string name, string fn, params string[] fns)
			{
				string path = IO.File<T>.Save.FullPathName(true, fn, fns);
				return new ConfigNode(name, path);
			}

			public static string[] ListFor(string mask = "*.cfg", bool subdirs = false)
			{
				string dir = IO.File<T>.Save.FullPathName(false, ".");
				string[] files = ReadableConfigNode.ListFiles(dir, mask, subdirs);
				return files;
			}
		}

		public class FileStream : SIO.FileStream
		{
			protected FileStream(string filename, SIO.FileMode filemode) : base(filename, filemode) {}

			public static FileStream CreateFor(FileMode mode, string filename)
			{
				string path = File<T>.Save.FullPathName(true, filename);
				return new FileStream(path, (SIO.FileMode)mode);
			}

			public static FileStream CreateFor(FileMode mode, string fn, params string[] fns)
			{
				string path = File<T>.Save.FullPathName(true, fn, fns);
				return new FileStream(path, (SIO.FileMode)mode);
			}
		}

		public class PluginConfiguration : KSP.IO.PluginConfiguration
		{
			protected readonly string pathname;
			protected PluginConfiguration(string pathname) : base(pathname) {
				this.pathname = pathname;
			}

			public bool exists()
			{
				return SIO.File.Exists(this.pathname);
			}

			public void delete()
			{
				if (SIO.File.Exists(this.pathname))
					SIO.File.Delete(this.pathname);
			}
		
			public new PluginConfiguration load()
			{
				base.load();
				return this;
			}

			public static PluginConfiguration CreateFor(string filename)
			{
				string path = File<T>.Save.FullPathName(true, filename);
				return new PluginConfiguration(path);
			}

			public static PluginConfiguration CreateFor(string fn, params string[] fns)
			{
				string path = File<T>.Save.FullPathName(true, fn, fns);
				return new PluginConfiguration(path);
			}

			public static PluginConfiguration CreateFor(Vessel flight)
			{
				Type target = typeof(T);
				return CreateFor((flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
			}

			public static PluginConfiguration CreateFor(Vessel flight, params string[] fns)
			{
				Type target = typeof(T);
				string path = fns[0];
				string[] ffns = new string[fns.Length];
				Array.Copy(fns, 1, ffns, 0, ffns.Length);
				foreach (string s in ffns)
					path = Path.Combine(path, s);
				path = Path.Combine(path, (flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
				return CreateFor(path);
			}

			public static PluginConfiguration CreateFor()
			{
				return CreateFor((Vessel)null);
			}
		}

		public class StreamReader : SIO.StreamReader
		{
			protected StreamReader(string path) : base(path) {}

			public static StreamReader CreateFor(string filename)
			{
				string path = File<T>.Save.FullPathName(false, filename);
				return new StreamReader(path);
			}

			public static StreamReader CreateFor(string fn, params string[] fns)
			{
				string path = File<T>.Save.FullPathName(false, fn, fns);
				return new StreamReader(path);
			}
		}

		public class StreamWriter : SIO.StreamWriter
		{
			internal StreamWriter(string path) : base(path) {}

			public static StreamWriter CreateFor(string filename)
			{
				string path = File<T>.Save.FullPathName(true, filename);
				return new StreamWriter(path);
			}

			public static StreamWriter CreateFor(string fn, params string[] fns)
			{
				string path = File<T>.Save.FullPathName(true, fn, fns);
				return new StreamWriter(path);
			}
		}

		public static class Texture2D
		{
			public static UTexture2D LoadFromFile(String fn)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(fn);
			}

			public static UTexture2D LoadFromFile(String fn, params string[] fns)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(fn, fns);
			}

			public static UTexture2D LoadFromFile(int width, int height, string fn)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(width, height, fn);
			}

			public static UTexture2D LoadFromFile(int width, int height, string fn, params string[] fns)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(width, height, fn, fns);
			}

			public static UTexture2D LoadFromFile(bool mipmap, String fn)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(mipmap, fn);
			}

			public static UTexture2D LoadFromFile(bool mipmap, string fn, params string[] fns)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(mipmap, fn, fns);
			}

			public static UTexture2D LoadFromFile(int width, int height, bool mipmap, String fn)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(width, height, mipmap, fn);
			}

			public static UTexture2D LoadFromFile(int width, int height, bool mipmap, string fn, params string[] fns)
			{
				return KSPe.IO.Save.Texture2D<T>.LoadFromFile(width, height, mipmap, fn, fns);
			}
		}

	}
}
