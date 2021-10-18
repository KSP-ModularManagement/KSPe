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
using System.Resources;
using KSP.UI.Screens;
using UnityEngine;

namespace KSPe.UI.Toolbar
{
	public class State
	{
		internal interface Interface
		{
			ApplicationLauncherButton Control { get; }
		}

		public class Data
		{
			internal readonly Texture2D largeIcon;
			internal readonly Texture2D smallIcon;

			private Data(Texture2D largeIcon, Texture2D smallIcon)
			{
				this.largeIcon = largeIcon;
				this.smallIcon = smallIcon;
			}

			public static Data Create(Texture2D largeIcon, Texture2D smallIcon)
			{
				return new Data(largeIcon, smallIcon);
			}
		}

		private readonly Interface owner;
		private readonly Dictionary<Type, Dictionary<object, Data>> states = new Dictionary<Type, Dictionary<object, Data>>();
		private object currentState = null;

		internal bool isEmpty => null == this.currentState || 0 == this.states.Count;

		internal State(Interface owner)
		{
			this.owner = owner;
		}

		public State Create<T>(Dictionary<object, Data> data)
		{
			if (this.states.ContainsKey(typeof(T))) this.states.Remove(typeof(T));
			this.states.Add(typeof(T), data);
			return this;
		}

		public object CurrentState { get => this.currentState; set => this.Set(value); }
		public State Set(object value)
		{
			if (this.currentState == value) return this;

			this.currentState = value;
			this.update();
			return this;
		}

		public State Clear()
		{
			this.currentState = null;
			return this;
		}

		internal void update()
		{
			if (null == this.currentState) return;
			Type t = this.currentState.GetType();
			if (!this.states.ContainsKey(t)) return;
			Dictionary<object, Data> dict = this.states[t];
			if (!dict.ContainsKey(this.currentState)) return;
			Data d = dict[this.currentState];
			this.owner.Control.SetTexture(d.largeIcon);
		}
	}

	public class Button : State.Interface
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
			public bool Has(Kind kind) => this.events.ContainsKey(kind);
		}

		public class ToolbarEvents
		{
			public enum Kind
			{
				Active,		// OnTrue, OnFalse
				Hover,		// HoverIn, HoverOut
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

		internal readonly ApplicationLauncher.AppScenes visibleInScenes;
		internal readonly string toolTip;

		private readonly ToolbarEvents toolbarEvents = new ToolbarEvents();
		public ToolbarEvents Toolbar => this.toolbarEvents;

		private readonly MouseEvents mouseEvents = new MouseEvents();
		public MouseEvents Mouse => this.mouseEvents;

		private readonly State state;
		public State State => this.state;

		private ApplicationLauncherButton control;
		public ApplicationLauncherButton Control => this.control;

		// A bit messy, but I need the classes to use the Toolbar.States stunt!
		// So, instead of creating 3 dummy classes and then hard code the association with its boolean var,
		// I made the state and the selector class the same! :)
		// (I'm spending too much time programming in Python, I should code a bit more on Java as it appears!!)
		private class ActiveState	{ readonly bool v; protected ActiveState(bool v) { this.v = v; }  public static implicit operator ActiveState(bool v) => new ActiveState(v);   public static implicit operator bool(ActiveState s) => s.v;  public override bool Equals(object o) => o is ActiveState && this.v == ((ActiveState)o).v;   public override int GetHashCode() => this.v.GetHashCode(); }
		private class EnabledState	{ readonly bool v; protected EnabledState(bool v) { this.v = v; } public static implicit operator EnabledState(bool v) => new EnabledState(v); public static implicit operator bool(EnabledState s) => s.v; public override bool Equals(object o) => o is EnabledState && this.v == ((EnabledState)o).v; public override int GetHashCode() => this.v.GetHashCode(); }
		private class HoverState	{ readonly bool v; protected HoverState(bool v) { this.v = v; }   public static implicit operator HoverState(bool v) => new HoverState(v);     public static implicit operator bool(HoverState s) => s.v;   public override bool Equals(object o) => o is HoverState && this.v == ((HoverState)o).v;     public override int GetHashCode() => this.v.GetHashCode(); }
		private ActiveState active = false;
		private EnabledState enabled = true;
		private HoverState hovering = false;

		private Button(
				object owner, string id
				, ApplicationLauncher.AppScenes visibleInScenes
				, string toolTip
			)
		{
			this.owner = owner;
			this.ID = this.owner.GetType().Namespace + "_" + id + "_Button";
			this.visibleInScenes = visibleInScenes;
			this.toolTip = toolTip;
			this.state = new State(this);
		}

		public static Button Create(object owner, string id
				, ApplicationLauncher.AppScenes visibleInScenes
				, string toolTip = null
			)
		{
			Button r = new Button(owner, id
					, visibleInScenes
					, toolTip
				);
			return r;
		}

		public static Button Create(object owner, string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, State.Data iconActive, State.Data iconInactive
				, string toolTip = null
			)
		{
			Button r = new Button(owner, id
					, visibleInScenes
					, toolTip
				);
			r.Add(ToolbarEvents.Kind.Enabled, iconActive, iconInactive);
			return r;
		}

		public static Button Create(object owner, string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIconActive, UnityEngine.Texture2D largeIconInactive
				, UnityEngine.Texture2D smallIconActive, UnityEngine.Texture2D smallIconInactive
				, string toolTip = null
			)
		{
			return Create(owner, id
					, visibleInScenes
					, State.Data.Create(largeIconActive, smallIconActive)
					, State.Data.Create(largeIconInactive, smallIconInactive)
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
			return Create(owner, owner.GetType().Name
					, visibleInScenes
					, State.Data.Create(largeIconActive, smallIconActive)
					, State.Data.Create(largeIconInactive, smallIconInactive)
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
			return Create(owner, owner.GetType().Name
					, visibleInScenes
					, State.Data.Create(largeIcon, largeIcon)
					, State.Data.Create(smallIcon, smallIcon)
					, toolTip
				);
		}

		public bool Active
		{
			get { return this.active; }
			set { this.updateActiveState(value); }
		}

		private bool Hovering
		{
			get { return this.hovering; }
			set { this.updateHoverState(value); }
		}


		public bool Enabled
		{
			get { return this.enabled; }
			set { this.updateEnableState(value); }
		}

		public void Add(ToolbarEvents.Kind kind, State.Data iconActive, State.Data iconInactive)
		{
			switch (kind)
			{
				case ToolbarEvents.Kind.Active:
					this.state.Create<ActiveState>(
						new Dictionary<object, State.Data> {
							{ (ActiveState)false, iconInactive }, { (ActiveState)true, iconActive }
						})
					;
					break;
				case ToolbarEvents.Kind.Hover:
					this.state.Create<HoverState>(
						new Dictionary<object, State.Data> {
							{ (HoverState)false, iconInactive }, { (HoverState)true, iconActive }
						})
					;
					break;
				case ToolbarEvents.Kind.Enabled:
					this.state.Create<EnabledState>(
						new Dictionary<object, State.Data> {
							{ (EnabledState)false, iconInactive }, { (EnabledState)true, iconActive }
						})
					;
					break;
				default:
					break;
			}
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
		}

		internal void OnFalse()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Active) && null != this.toolbarEvents[ToolbarEvents.Kind.Active].fallingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Active].fallingEdge();

			this.active = false;
		}

		internal void OnHoverIn()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Hover) && null != this.toolbarEvents[ToolbarEvents.Kind.Hover].raisingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Hover].raisingEdge();

			this.hovering = true;
		}

		internal void OnHoverOut()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Hover) && null != this.toolbarEvents[ToolbarEvents.Kind.Hover].fallingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Hover].fallingEdge();

			this.hovering = false;
		}

		internal void OnEnable()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Enabled) && null != this.toolbarEvents[ToolbarEvents.Kind.Enabled].raisingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Enabled].raisingEdge();

			this.enabled = true;
		}

		internal void OnDisable()
		{
			if (this.toolbarEvents.Has(ToolbarEvents.Kind.Enabled) && null != this.toolbarEvents[ToolbarEvents.Kind.Enabled].fallingEdge)
				this.toolbarEvents[ToolbarEvents.Kind.Enabled].fallingEdge();

			this.enabled = false;
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
			this.states.Set(this.active);	// Now do you see why that mess above? ;)
		}

		private void updateHoverState(bool newState)
		{
			if (newState == this.hovering) return;
			if (this.hovering) this.OnHoverOut(); else this.OnHoverIn();
			this.states.Set(this.hovering);	// Now do you see why that mess above? ;)
		}

		private void updateEnableState(bool newState)
		{
			if (newState == this.enabled) return;
			if (this.enabled) this.OnDisable(); else this.OnEnable();
			this.states.Set(this.enabled);	// Now do you see why that mess above? ;)
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

		private static Texture2D defaultButton = null;
		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public Toolbar Add(Button button)
		{
			if (null == defaultButton) try
			{
				using (System.IO.Stream si = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("KSPe.UI.Resources.launch_icon.png"))
				{ 
					Byte[] data = new System.IO.BinaryReader(si).ReadBytes(int.MaxValue);
					if (!KSPe.Util.Image.File.Load(out defaultButton, data))
						defaultButton = new Texture2D(16, 16);
				}
			}
			catch (Exception e)
			{
				Log.error(e, this);
				defaultButton = new Texture2D(16, 16);
			}

			if (this.buttons.Contains(button))
			{
				this.buttons.Remove(button);
				ApplicationLauncher.Instance.RemoveModApplication(button.Control);
				button.clear();
			}
			this.buttons.Add(button);
			button.set(
				ApplicationLauncher.Instance.AddModApplication(
					button.OnTrue, button.OnFalse
					, button.OnHoverIn, button.OnHoverOut
					, button.OnEnable, button.OnDisable
					, button.visibleInScenes
					, defaultButton
				)
			);
			return this;
		}

		public void Destroy()
		{
			foreach(Button b in this.buttons) ApplicationLauncher.Instance.RemoveModApplication(b.Control);
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
