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
using System.Collections.Generic;
using KSP.UI.Screens;
using UnityEngine;

namespace KSPe.UI.Toolbar
{
	public class Button
	{
		public class Event
		{
			public delegate void Handler();

			internal readonly Handler raisingEdge;
			internal readonly Handler fallingEdge;

			public Event(Handler raisingEdge, Handler fallingEdge)
			{
				this.raisingEdge = raisingEdge;
				this.fallingEdge = fallingEdge;
			}
		}

		public class MouseEvents
		{
			public enum Kind
			{
				Left,
				Right,
				Middle,
				Scroll
			};

			public class Handler
			{
				public delegate void ClickHandler();
				public delegate float ScrollHandler();

				public readonly Kind kind;
				internal readonly ClickHandler clickHandler;
				internal readonly ScrollHandler scrollHandler;
				internal readonly KeyCode[] modifier;


				internal Handler(Kind kind, ClickHandler handler, KeyCode[] modifier)
				{
					this.kind = kind;
					this.clickHandler = handler;
					this.scrollHandler = null;
					this.modifier = modifier;
				}

				internal Handler(Kind kind, ScrollHandler handler, KeyCode[] modifier)
				{
					this.kind = kind;
					this.clickHandler = null;
					this.scrollHandler = handler;
					this.modifier = modifier;
				}
			}

			internal readonly Dictionary<Kind, Handler> events = new Dictionary<Kind, Handler>();
			private static readonly KeyCode[] EMPTY_MODIFIER = new KeyCode[]{}; 
			internal MouseEvents() { }

			public MouseEvents Add(Handler handler)
			{
				if (this.events.ContainsKey(handler.kind)) this.events.Remove(handler.kind);
				this.events.Add(handler.kind, handler);
				return this;
			}

			public MouseEvents Add(MouseEvents.Kind kind, Handler.ClickHandler handler)
			{
				Handler h = new Handler(kind, handler, EMPTY_MODIFIER);
				return this.Add(h);
			}

			public MouseEvents Add(MouseEvents.Kind kind, Handler.ClickHandler handler, KeyCode[] modifiler)
			{
				Handler h = new Handler(kind, handler, modifiler);
				return this.Add(h);
			}

			public MouseEvents Add(Handler.ScrollHandler handler)
			{
				Handler h = new Handler(MouseEvents.Kind.Scroll, handler, EMPTY_MODIFIER);
				return this.Add(h);
			}

			public MouseEvents Add(Handler.ScrollHandler handler, KeyCode[] modifiler)
			{
				Handler h = new Handler(MouseEvents.Kind.Scroll, handler, modifiler);
				return this.Add(h);
			}

			public Handler this[Kind index] => this.events[index];
			public bool Has(Kind kind)
			{
				return this.events.ContainsKey(kind);
			}
		}

		public class ToolbarEvents
		{
			public enum Kind
			{
				Active,		// OnTrue, OnFalse
				Hover,
				Enabled,	// OnEnable, OnDisable
			};

			private readonly Dictionary<Kind, Event> events = new Dictionary<Kind,Event>();

			internal ToolbarEvents() {	}

			public ToolbarEvents Add(Kind kind, Event @event)
			{
				if (this.events.ContainsKey(kind)) this.events.Remove(kind);
				this.events.Add(kind, @event);
				return this;
			}

			public Event this[Kind index] => this.events[index];
			public bool Has(Kind kind)
			{
				return this.events.ContainsKey(kind);
			}
		}

		public readonly object owner;
		public readonly string ID;

		internal ApplicationLauncherButton control { get; private set; }
		internal readonly ApplicationLauncher.AppScenes visibleInScenes;
		internal readonly Texture2D largeIconActive;
		internal readonly Texture2D largeIconInactive;
		internal readonly Texture2D smallIconActive;
		internal readonly Texture2D smallIconInactive;
		internal readonly string toolTip;
		private readonly ToolbarEvents toolbarEvents = new ToolbarEvents();
		private readonly MouseEvents mouseEvents = new MouseEvents();

		public ToolbarEvents Toolbar => this.toolbarEvents;
		public MouseEvents Mouse => this.mouseEvents;

		private bool active = false;
		private bool enabled = true;
		private bool hovering = false;

		private Button(
				object owner, string id
				, ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip
			)
		{
			this.owner = owner;
			this.ID = this.owner.GetType().Namespace + "_" + id + "_Button";
			this.visibleInScenes = visibleInScenes;
			this.largeIconActive = largeIconActive;
			this.largeIconInactive = largeIconInactive;
			this.smallIconActive = smallIconActive;
			this.smallIconInactive = smallIconInactive;
			this.toolTip = toolTip;
		}

		public static Button Create(object owner, string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip = null
			)
		{
			return new Button(owner, id
					, visibleInScenes
					, largeIconActive, largeIconInactive
					, smallIconActive, smallIconInactive
					, toolTip
				);
		}

		public static Button Create<T>(object owner, string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, string largeIconActive, string largeIconInactive
				, string smallIconActive, string smallIconInactive
				, string toolTip = null
			)
		{
			return Create(owner, id
					, visibleInScenes
					, IO.Asset<T>.Texture2D.LoadFromFile(largeIconActive), IO.Asset<T>.Texture2D.LoadFromFile(largeIconInactive)
					, IO.Asset<T>.Texture2D.LoadFromFile(smallIconActive), IO.Asset<T>.Texture2D.LoadFromFile(smallIconInactive)
					, toolTip
				);
		}

		public static Button Create(object owner
				, ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip = null
			)
		{
			return new Button(owner, owner.GetType().Name
					, visibleInScenes
					, largeIconActive, largeIconInactive
					, smallIconActive, smallIconInactive
					, toolTip
				);
		}

		public static Button Create<T>(object owner
				, ApplicationLauncher.AppScenes visibleInScenes
				, string largeIconActive, string largeIconInactive
				, string smallIconActive, string smallIconInactive
				, string toolTip = null
			)
		{
			return Create(owner
					, visibleInScenes
					, IO.Asset<T>.Texture2D.LoadFromFile(largeIconActive), IO.Asset<T>.Texture2D.LoadFromFile(largeIconInactive)
					, IO.Asset<T>.Texture2D.LoadFromFile(smallIconActive), IO.Asset<T>.Texture2D.LoadFromFile(smallIconInactive)
					, toolTip
				);
		}

		public static Button Create(object owner
				, ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIcon
				, UnityEngine.Texture2D smallIcon
				, string toolTip = null
			)
		{
			return new Button(owner, owner.GetType().Name
					, visibleInScenes
					, largeIcon, largeIcon
					, smallIcon, smallIcon
					, toolTip
				);
		}

		public static Button Create<T>(object owner
				, ApplicationLauncher.AppScenes visibleInScenes
				, string largeIcon
				, string smallIcon
				, string toolTip = null
			)
		{
			return Create(owner
					, visibleInScenes
					, IO.Asset<T>.Texture2D.LoadFromFile(largeIcon)
					, IO.Asset<T>.Texture2D.LoadFromFile(smallIcon)
					, toolTip
				);
		}

		public bool Active
		{
			get { return this.active; }
			set { this.updateActiveState(value); }
		}

		public bool Enabled
		{
			get { return this.enabled; }
			set { this.updateEnableState(value); }
		}

		internal void set(ApplicationLauncherButton applicationLauncherButton)
		{
			this.control = applicationLauncherButton;
			this.control.onLeftClick = this.OnLeftClick;
			this.control.onRightClick = this.OnRightClick;
		}

		internal void clear()
		{
			this.control = null;
		}

		internal void OnTrue()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Active) && null != this.toolbarEvents[ToolbarEvents.Kind.Active].raisingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Active].raisingEdge();

			this.active = true;
			this.updateIcon();
		}

		internal void OnFalse()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Active) && null != this.toolbarEvents[ToolbarEvents.Kind.Active].fallingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Active].fallingEdge();

			this.active = false;
			this.updateIcon();
		}

		internal void OnHoverIn()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Hover) && null != this.toolbarEvents[ToolbarEvents.Kind.Hover].raisingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Hover].raisingEdge();

			this.hovering = true;
			this.updateIcon();
		}

		internal void OnHoverOut()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Hover) && null != this.toolbarEvents[ToolbarEvents.Kind.Hover].fallingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Hover].fallingEdge();

			this.hovering = false;
			this.updateIcon();
		}

		internal void OnEnable()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Enabled) && null != this.toolbarEvents[ToolbarEvents.Kind.Enabled].raisingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Enabled].raisingEdge();

			this.enabled = true;
			this.updateIcon();
		}

		internal void OnDisable()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Enabled) && null != this.toolbarEvents[ToolbarEvents.Kind.Enabled].fallingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Enabled].fallingEdge();

			this.enabled = true;
			this.updateIcon();
		}

		private void OnLeftClick()
		{
			if (!this.mouseEvents.Has(MouseEvents.Kind.Left)) return;
			this.HandleMouseClick(MouseEvents.Kind.Left);
		}

		private void OnRightClick()
		{
			if (!this.mouseEvents.Has(MouseEvents.Kind.Right)) return;
			this.HandleMouseClick(MouseEvents.Kind.Right);
		}

		private void HandleMouseClick(MouseEvents.Kind kind)
		{
			foreach (KeyCode k in this.mouseEvents[kind].modifier)
				if (!UnityEngine.Input.GetKeyDown(k)) return;
			this.mouseEvents[kind].clickHandler();
		}

		private void updateActiveState(bool newState)
		{
			if (newState == this.active) return;
			if (this.active) this.OnFalse(); else this.OnTrue();
		}

		private void updateEnableState(bool newState)
		{
			if (newState == this.enabled) return;
			if (this.enabled) this.OnDisable(); else this.OnEnable();
		}

		private void updateIcon()
		{
			this.control.SetTexture(this.enabled ? this.largeIconActive : this.largeIconInactive);
		}

	}

	public class Toolbar
	{
		private readonly Type type;
		private readonly string displayName;
		private readonly List<Button> buttons = new List<Button>();

		public Toolbar(Type type, string displayName)
		{
			this.type = type;
			this.displayName = displayName ?? type.Namespace;
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public bool BlizzyActive(bool? useBlizzy = null)
		{
			return false;
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public bool StockActive(bool? useStock = null)
		{
			return true;
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public void ButtonsActive(bool? useStock, bool? useBlizzy)
		{
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public Toolbar Add(Button button)
		{
			if (this.buttons.Contains(button))
			{
				this.buttons.Remove(button);
				ApplicationLauncher.Instance.RemoveModApplication(button.control);
				button.clear();
			}
			this.buttons.Add(button);
			button.set(
				ApplicationLauncher.Instance.AddModApplication(
					button.OnTrue, button.OnFalse
					, button.OnHoverIn, button.OnHoverOut
					, button.OnEnable, button.OnDisable
					, button.visibleInScenes
					, button.largeIconActive
				)
			);
			return this;
		}

		public void Destroy()
		{
			foreach(Button b in this.buttons) ApplicationLauncher.Instance.RemoveModApplication(b.control);
			this.buttons.Clear();
		}
	}

	public class Controller
	{
		private static Controller instance = null;
		public static Controller Instance = instance ?? (instance = new Controller());

		private readonly Dictionary<Type, Toolbar> toolbars = new Dictionary<Type, Toolbar>();

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public bool Register<T>(string displayName = null, bool useBlizzy = false, bool useStock = true, bool NoneAllowed = true)
		{
			Log.info("Toolbar is registering {0} with type {1}", displayName??typeof(T).Namespace, typeof(T));
			this.toolbars.Add(typeof(T), new Toolbar(typeof(T), displayName));
			return true;
		}

		public Toolbar Get<T>() => this.toolbars[typeof(T)];
	}

}
