// /*
// 	This file is part of KSPe, a component for KSP API Extensions/L
// 	(C) 2018-20 Lisias T : http://lisias.net <support@lisias.net>
//
// 	KSPe API Extensions/L is double licensed, as follows:
//
// 	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
// 	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt
//
// 	And you are allowed to choose the License that better suit your needs.
//
// 	KSPe API Extensions/L is distributed in the hope that it will be useful,
// 	but WITHOUT ANY WARRANTY; without even the implied warranty of
// 	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//
// 	You should have received a copy of the SKL Standard License 1.0
// 	along with KSPe API Extensions/L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
//
// 	You should have received a copy of the GNU General Public License 2.0
// 	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.
//
// */
using System;
using KSP.UI.Screens;
using UnityEngine;

namespace KSPe.UI.Toolbar
{
	public class Button
	{
		public delegate void ClickHandler();

		public readonly Type owner;
		public string NameSpace => this.owner.Namespace;
		public readonly string ID;

		private readonly ClickHandler OnShow;
		private readonly ClickHandler OnHide;
		private readonly ClickHandler OnHoverIn;
		private readonly ClickHandler OnHoverOut;
		private readonly ClickHandler OnEnable;
		private readonly ClickHandler OnDisable;
		private readonly ClickHandler OnTrue;
		private readonly ClickHandler OnFalse;
		private readonly ApplicationLauncher.AppScenes visibleInScenes;
		private readonly Texture2D largeIconActive;
		private readonly Texture2D largeIconInactive;
		private readonly Texture2D smallIconActive;
		private readonly Texture2D smallIconInactive;
		private readonly string toolTip;

		internal Button(
				Type owner, string id
				, ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, ClickHandler onHoverIn, ClickHandler onHoverOut
				, ClickHandler onEnable, ClickHandler onDisable
				, ClickHandler onTrue, ClickHandler onFalse
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip
			)
		{
			this.owner = owner;
			this.ID = id;
			this.visibleInScenes = visibleInScenes;
			this.OnShow = onShow;
			this.OnHide = onHide;
			this.OnHoverIn = onHoverIn;
			this.OnHoverOut = onHoverOut;
			this.OnEnable = onEnable;
			this.OnDisable = onDisable;
			this.OnTrue = onTrue;
			this.OnFalse = onFalse;
			this.largeIconActive = largeIconActive;
			this.largeIconInactive = largeIconInactive;
			this.smallIconActive = smallIconActive;
			this.smallIconInactive = smallIconInactive;
			this.toolTip = toolTip;
		}
	}

	public class Button<T>:Button
	{
		private Button(
				string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, ClickHandler onHoverIn, ClickHandler onHoverOut
				, ClickHandler onEnable, ClickHandler onDisable
				, ClickHandler onTrue, ClickHandler onFalse
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip
			) : base(typeof(T), id, visibleInScenes
					, onShow, onHide
					, onHoverIn, onHoverOut
					, onEnable, onDisable
					, onTrue, onFalse
					, largeIconActive, largeIconInactive
					, smallIconActive, smallIconInactive
					, toolTip
				)
		{ }

		public static Button<T> Create(string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, ClickHandler onHoverIn, ClickHandler onHoverOut
				, ClickHandler onEnable, ClickHandler onDisable
				, ClickHandler onTrue, ClickHandler onFalse
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip = ""
			)
		{
			return new Button<T>(id, visibleInScenes
					, onShow, onHide
					, onHoverIn, onHoverOut
					, onEnable, onDisable
					, onTrue, onFalse
					, largeIconActive, largeIconInactive
					, smallIconActive, smallIconInactive
					, toolTip
				);
		}

		public static Button<T> Create(
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, ClickHandler onHoverIn, ClickHandler onHoverOut
				, ClickHandler onEnable, ClickHandler onDisable
				, ClickHandler onTrue, ClickHandler onFalse
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip = ""
			)
		{
			return Create(typeof(T).Namespace + "_Button"
					, visibleInScenes
					, onShow, onHide
					, onHoverIn, onHoverOut
					, onEnable, onDisable
					, onTrue, onFalse
					, largeIconActive, largeIconInactive
					, smallIconActive, smallIconInactive
					, toolTip
				);
		}

		public static Button<T> Create(
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, ClickHandler onHoverIn, ClickHandler onHoverOut
				, ClickHandler onEnable, ClickHandler onDisable
				, ClickHandler onTrue, ClickHandler onFalse
				, string largeIconActive, string largeIconInactive
				, string smallIconActive, string smallIconInactive
				, string toolTip = ""
			)
		{
			return Create(visibleInScenes
					, onShow, onHide
					, onHoverIn, onHoverOut
					, onEnable, onDisable
					, onTrue, onFalse
					, IO.Asset<T>.Texture2D.LoadFromFile(largeIconActive), IO.Asset<T>.Texture2D.LoadFromFile(largeIconInactive)
					, IO.Asset<T>.Texture2D.LoadFromFile(smallIconActive), IO.Asset<T>.Texture2D.LoadFromFile(smallIconInactive)
					, toolTip
				);
		}

		public static Button<T> Create(
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip = ""
			)
		{
			return Create(visibleInScenes
					, onShow, onHide
					, (ClickHandler)null, (ClickHandler)null
					, (ClickHandler)null, (ClickHandler)null
					, (ClickHandler)null, (ClickHandler)null
					, largeIconActive, largeIconInactive
					, smallIconActive, smallIconInactive
					, toolTip
				);
		}

		public static Button<T> Create(
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, string largeIconActive, string largeIconInactive
				, string smallIconActive, string smallIconInactive
				, string toolTip = ""
			)
		{
			return Create(visibleInScenes
					, onShow, onHide
					, IO.Asset<T>.Texture2D.LoadFromFile(largeIconActive), IO.Asset<T>.Texture2D.LoadFromFile(largeIconInactive)
					, IO.Asset<T>.Texture2D.LoadFromFile(smallIconActive), IO.Asset<T>.Texture2D.LoadFromFile(smallIconInactive)
					, toolTip
				);
		}

		public static Button<T> Create(
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, UnityEngine.Texture2D largeIconActive
				, UnityEngine.Texture2D smallIconActive
				, string toolTip = ""
			)
		{
			return Create(visibleInScenes
					, onShow, onHide
					, (ClickHandler)null, (ClickHandler)null
					, (ClickHandler)null, (ClickHandler)null
					, (ClickHandler)null, (ClickHandler)null
					, largeIconActive, null
					, smallIconActive, null
					, toolTip
				);
		}

		public static Button<T> Create(
				ApplicationLauncher.AppScenes visibleInScenes
				, ClickHandler onShow, ClickHandler onHide
				, string largeIconActive
				, string smallIconActive
				, string toolTip = ""
			)
		{
			return Create(visibleInScenes
					, onShow, onHide
					, IO.Asset<T>.Texture2D.LoadFromFile(largeIconActive), null
					, IO.Asset<T>.Texture2D.LoadFromFile(smallIconActive), null
					, toolTip
				);
		}
	}

	public class Controller
	{
		private static Controller instance = null;
		public static Controller Instance = instance ?? (instance = new Controller());

		public bool RegisterMod<T>(string DisplayName = "", bool useBlizzy = false, bool useStock = true, bool NoneAllowed = true)
		{
			return true;
		}

		public bool BlizzyActive(bool? useBlizzy = null)
		{
			return false;
		}

		public bool StockActive(bool? useStock = null)
		{
			return true;
		}

		public void ButtonsActive(bool? useStock, bool? useBlizzy)
		{
		}

		public void Add(Button button)
		{
		}

		public void RemoveAllFrom<T>()
		{
		}
	}

}
