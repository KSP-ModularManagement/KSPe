/*
	This file is part of KSPe.Loader, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.Loader is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe.Loader is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe.Loader. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;
namespace KSPe.Loader
{
	public static class Version
	{
		public const int major = 1;
		public const int minor = 0;
		public const int patch = 0;
		public const int build = 0;
		public static readonly System.Version V = new System.Version(major, minor, patch, build);
		public const string Suffix = "000_";
		public const string Number = "1.0.0.0";
#if DEBUG
		public const string Text = Number + " /L DEBUG";
#else
		public const string Text = Number + " /L";
#endif
	}
}
