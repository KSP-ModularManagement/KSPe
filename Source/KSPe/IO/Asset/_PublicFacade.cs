/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe API Extensions/L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe API Extensions/L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe API Extensions/L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using SIO = System.IO;
using UTexture2D = UnityEngine.Texture2D;

namespace KSPe.IO
{
	// New Public interface for KSPe 2.1.
	public static class Asset<T>
	{
		public class ConfigNode : ReadableConfigNode
		{
			protected ConfigNode(string name, string fn) : base(name)
			{
				this.Path = fn;
			}

			public new ConfigNode Load()
			{
				return (ConfigNode)base.Load();
			}

			public static ConfigNode For(string name, string filename)
			{
				string path = IO.File<T>.Asset.FullPathName(filename);
				return new ConfigNode(name, path);
			}

			public static ConfigNode For(string name, string fn, params string[] fns)
			{
				string path = IO.File<T>.Asset.FullPathName(fn, fns);
				return new ConfigNode(name, path);
			}

			public static string[] ListFor(string mask = "*.cfg", bool subdirs = false)
			{
				return ListFor(mask, subdirs, ".");
			}

			public static string[] ListFor(string mask, params string[] dirs)
			{
				return ListFor(mask, false, dirs);
			}

			public static string[] ListFor(string mask, bool subdirs, params string[] dirs)
			{
				string[] slice = new string[dirs.Length-1];
				Array.Copy(dirs, 1, slice, 0, dirs.Length-1);
				string dir = IO.File<T>.Asset.FullPathName(dirs[0], slice);
				string[] files = ReadableConfigNode.ListFiles(dir, mask, subdirs);
				return files;
			}
		}

		public class FileStream : SIO.FileStream
		{
			protected FileStream(string filename, SIO.FileMode filemode) : base(filename, filemode) {}

			public static FileStream CreateFor(FileMode mode, string filename) // To favor conformity
			{
				string path = File<T>.Asset.FullPathName(filename);
				return new FileStream(path, (SIO.FileMode)mode);
			}

			public static FileStream CreateFor(FileMode mode, string fn, params string[] fns)
			{
				string path = File<T>.Asset.FullPathName(fn, fns);
				return new FileStream(path, (SIO.FileMode)mode);
			}
		}

		public class PluginConfiguration : KSP.IO.PluginConfiguration
		{
			protected readonly string pathname;
			protected PluginConfiguration(string pathname) : base(pathname) {
				this.pathname = pathname;
			}

			protected new T GetValue<T>(string key) { throw new System.NotImplementedException(); }
			protected new T GetValue<T>(string key, T _default) { throw new System.NotImplementedException(); }

			public T GetValue (string key)
			{
				return base.GetValue<T>(key);
			}

			public T GetValue (string key, T _default)
			{
				return base.GetValue<T>(key, _default);
			}

			public new void SetValue (string key, object value)
			{
				base.SetValue(key, value);
			}

			public bool exists()
			{
				return SIO.File.Exists(this.pathname);
			}

			public bool delete() { throw new System.NotSupportedException(); }

			public new PluginConfiguration load()
			{
				base.load();
				return this;
			}

			public static PluginConfiguration CreateFor(string filename)
			{
				string path = File<T>.Asset.FullPathName(filename);
				return new PluginConfiguration(path);
			}

			public static PluginConfiguration CreateFor(string fn, params string[] fns)
			{
				string path = File<T>.Asset.FullPathName(fn, fns);
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
					path = IO.Path.Combine(path, s);
				path = IO.Path.Combine(path, (flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
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
				string path = File<T>.Asset.FullPathName(filename);
				return new StreamReader(path);
			}

			public static StreamReader CreateFor(string fn, params string[] fns)
			{
				string path = File<T>.Asset.FullPathName(fn, fns);
				return new StreamReader(path);
			}
		}

		public static class Texture2D
		{
			public static UTexture2D LoadFromFile(String fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(fn);
			}

			public static UTexture2D LoadFromFile(String fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(fn, fns);
			}

			public static UTexture2D LoadFromFile(int width, int height, string fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, fn);
			}

			public static UTexture2D LoadFromFile(int width, int height, string fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, fn, fns);
			}

			public static UTexture2D LoadFromFile(bool mipmap, String fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(mipmap, fn);
			}

			public static UTexture2D LoadFromFile(bool mipmap, string fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(mipmap, fn, fns);
			}

			public static UTexture2D LoadFromFile(int width, int height, bool mipmap, String fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, mipmap, fn);
			}

			public static UTexture2D LoadFromFile(int width, int height, bool mipmap, string fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, mipmap, fn, fns);
			}
		}

	}
}
