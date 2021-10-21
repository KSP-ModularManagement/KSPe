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
	public class State
	{
		internal interface Interface
		{
			ApplicationLauncherButton Control { get; }
		}

		public abstract class Control {
			internal new abstract Type GetType();
			internal Type GetSurrogateType() => base.GetType();
		}
		[Serializable]
		public class Control<T> : Control
		{
			protected readonly T v;
			protected readonly Type myRealType = typeof(T);
			protected Control(T v) { this.v = v; }
			internal override Type GetType() => this.myRealType;
			public override bool Equals(object o) => o is Control<T> && this.v.Equals(((Control<T>)o).v);

			private int _hash = -1;
			public override int GetHashCode()
			{
				if (this._hash > 0) return this._hash;
				int hash = 7;
				hash = 31 * hash + this.myRealType.GetHashCode();
				hash = 31 * hash + this.v.GetHashCode();
				return (this._hash = hash);
			}

			public override string ToString()
			{
				return base.ToString() + ":" + this.v.ToString();
			}
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

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public static Data Create(Texture2D largeIcon, Texture2D smallIcon)
			{
				return new Data(largeIcon, smallIcon);
			}
		}

		private readonly Interface owner;
		private readonly Dictionary<Type, Dictionary<Control, Data>> states = new Dictionary<Type, Dictionary<Control, Data>>();
		private Control currentState = null;

		internal bool isEmpty => null == this.currentState || 0 == this.states.Count;

		internal State(Interface owner)
		{
			this.owner = owner;
		}

		public State Create<T>(Dictionary<Control, Data> data)
		{
			if (!(typeof(T).IsSubclassOf(typeof(Control))))
				throw new InvalidCastException(string.Format("Type {0} is not valid for the operation. It needs to be derived from  KSPe.UI.State.Control<?>!", typeof(T).FullName));

			foreach (Control i in data.Keys) if (i.GetSurrogateType() != typeof(T))
				throw new InvalidCastException(string.Format("State {0} is not valid for this dataset. It needs to be type {1}", i, typeof(T).FullName));

			if (this.states.ContainsKey(typeof(T))) this.states.Remove(typeof(T));
			this.states.Add(typeof(T), data);
			#if DEBUG
			{
				Control[] keys = new Control[data.Keys.Count];
				data.Keys.CopyTo(keys, 0);
				Log.debug("State.Data created for type {0} using Controls {1}", typeof(T).FullName, string.Join("; ", Array.ConvertAll(keys, item => item.ToString())));
			}
			#endif
			return this;
		}

		public Control CurrentState { get => this.currentState; set => this.Set(value); }
		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public State Set(Control value)
		{
			Log.debug("State.Data Set from {0} to {1}", this.currentState, value);
			if (this.currentState == value) return this;

			return this.set(value);
		}

		internal State set(Control value)
		{
			this.currentState = value;
			this.update();
			return this;
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public State Clear()
		{
			this.currentState = null;
			return this;
		}

		internal void update()
		{
			if (null == this.currentState) return;
			Type t = this.currentState.GetSurrogateType();
			if (!this.states.ContainsKey(t)) return;
			Dictionary<Control, Data> dict = this.states[t];

			bool haveTex = dict.ContainsKey(this.currentState);
			if (haveTex)
			{ 
				Data d = dict[this.currentState];
				this.owner.Control.SetTexture(d.largeIcon);
			}
			Log.debug("State.Control update type {0} using {1} {2} texture", t, this.currentState, haveTex ? "with" : "without");
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

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public MouseEvents Add(Handler handler)
			{
				if (this.events.ContainsKey(handler.kind)) this.events.Remove(handler.kind);
				this.events.Add(handler.kind, handler);
				#if DEBUG
				Log.debug("Button.MouseEvent.Add {0}{1}{2}{3}"
						, handler.kind
						, (null != handler.clickHandler) ? "with clickhandler" : ""
						, (null != handler.scrollHandler) ? "with scrollHandler" : ""
						, (null != handler.modifier) ? "with modifiers" : ""
					);
				#endif
				return this;
			}

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public MouseEvents Add(MouseEvents.Kind kind, Handler.ClickHandler handler)
			{
				Handler h = new Handler(kind, handler, EMPTY_MODIFIER);
				return this.Add(h);
			}

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public MouseEvents Add(MouseEvents.Kind kind, Handler.ClickHandler handler, KeyCode[] modifiler)
			{
				Handler h = new Handler(kind, handler, modifiler);
				return this.Add(h);
			}

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public MouseEvents Add(Handler.ScrollHandler handler)
			{
				Handler h = new Handler(MouseEvents.Kind.Scroll, handler, EMPTY_MODIFIER);
				return this.Add(h);
			}

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public MouseEvents Add(Handler.ScrollHandler handler, KeyCode[] modifiler)
			{
				Handler h = new Handler(MouseEvents.Kind.Scroll, handler, modifiler);
				return this.Add(h);
			}

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public Handler this[Kind index] => this.events[index];
			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
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

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public ToolbarEvents Add(Kind kind, Event @event)
			{
				if (this.events.ContainsKey(kind)) this.events.Remove(kind);
				this.events.Add(kind, @event);
				#if DEBUG
				Log.debug("Button.ToolbarEvents.Add {0} event{1}{2}"
						, kind
						, (null != @event.raisingEdge) ? " with raisingEdge" : ""
						, (null != @event.fallingEdge) ? " with fallingEdge" : ""
					);
				#endif
				return this;
			}

			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
			public Event this[Kind index] => this.events[index];
			[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
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
		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public ToolbarEvents Toolbar => this.toolbarEvents;

		private readonly MouseEvents mouseEvents = new MouseEvents();
		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public MouseEvents Mouse => this.mouseEvents;

		private readonly State state;
		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public State State => this.state;

		private ApplicationLauncherButton control;
		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public ApplicationLauncherButton Control => this.control;

		// A bit messy, but I need the classes to use the Toolbar.States stunt!
		// So, instead of creating 3 dummy classes and then hard code the association with its boolean var,
		// I made the state and the selector class the same! :)
		// (I'm spending too much time programming in Python, I should code a bit more on Java as it appears!!)
		private class ActiveState:State.Control<bool>  { protected ActiveState(bool v):base(v) { }  public static implicit operator ActiveState(bool v) => new ActiveState(v);   public static implicit operator bool(ActiveState s) => s.v; }
		private class EnabledState:State.Control<bool> { protected EnabledState(bool v):base(v) { } public static implicit operator EnabledState(bool v) => new EnabledState(v); public static implicit operator bool(EnabledState s) => s.v; }
		private class HoverState:State.Control<bool>   { protected HoverState(bool v):base(v) { }   public static implicit operator HoverState(bool v) => new HoverState(v);     public static implicit operator bool(HoverState s) => s.v; }
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
			#if DEBUG
			Log.debug("Button Created for {0}, ID {1} for Scenes {2} and toolTip {3}"
					, this.owner, this.ID
					, visibleInScenes
					, toolTip??"<none>"
				);
			#endif
		}

		public override bool Equals(object o)
		{
			if (!(o is Button)) return false;
			Button b = (Button)o;
			return this.owner.Equals(b.owner) && this.ID.Equals(b.ID);
		}

		private int _hash = -1;
		public override int GetHashCode()
		{
			if (this._hash > 0) return this._hash;
			int hash = 7;
			hash = 31 * hash + this.owner.GetHashCode();
			hash = 31 * hash + this.ID.GetHashCode();
			return (this._hash = hash);
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
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

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public static Button Create(object owner, string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, State.Data iconEnabled, State.Data iconDisabled
				, string toolTip = null
			)
		{
			Button r = new Button(owner, id
					, visibleInScenes
					, toolTip
				);
			r.Add(ToolbarEvents.Kind.Enabled, iconEnabled, iconDisabled);
			return r;
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public static Button Create(object owner, string id,
				ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIconEnabled, UnityEngine.Texture2D largeIconDisabled
				, UnityEngine.Texture2D smallIconEnabled, UnityEngine.Texture2D smallIconDisabled
				, string toolTip = null
			)
		{
			return Create(owner, id
					, visibleInScenes
					, State.Data.Create(largeIconEnabled, smallIconEnabled)
					, State.Data.Create(largeIconDisabled, smallIconDisabled)
					, toolTip
				);
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public static Button Create(object owner
				, ApplicationLauncher.AppScenes visibleInScenes
				, string toolTip = null
			)
		{
			Button r = new Button(owner, owner.GetType().Name
					, visibleInScenes
					, toolTip
				);
			return r;
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
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

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public static Button Create(object owner
				, ApplicationLauncher.AppScenes visibleInScenes
				, UnityEngine.Texture2D largeIcon
				, UnityEngine.Texture2D smallIcon
				, string toolTip = null
			)
		{
			return Create(owner, owner.GetType().Name
					, visibleInScenes
					, State.Data.Create(largeIcon, smallIcon)
					, State.Data.Create(largeIcon, smallIcon)
					, toolTip
				);
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
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

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public bool Enabled
		{
			get { return this.enabled; }
			set { this.updateEnableState(value); }
		}

		[Obsolete("Toobar Support is still alpha. Be aware that interfaces and contracts can break between releases. KSPe suggest to wait until v2.4.2.0 before using it on your plugins.")]
		public void Add(ToolbarEvents.Kind kind, State.Data iconActive, State.Data iconInactive)
		{
			switch (kind)
			{
				case ToolbarEvents.Kind.Active:
					this.state.Create<ActiveState>(
						new Dictionary<State.Control, State.Data> {
							{ (ActiveState)false, iconInactive }, { (ActiveState)true, iconActive }
						})
					;
					break;
				case ToolbarEvents.Kind.Hover:
					this.state.Create<HoverState>(
						new Dictionary<State.Control, State.Data> {
							{ (HoverState)false, iconInactive }, { (HoverState)true, iconActive }
						})
					;
					break;
				case ToolbarEvents.Kind.Enabled:
					this.state.Create<EnabledState>(
						new Dictionary<State.Control, State.Data> {
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
			this.state.set(this.enabled = true);
		}

		internal void clear()
		{
			this.control = null;
		}

		internal void OnTrue()
		{
			if (!this.enabled)	return;
			this.sendRaisingEvent(ToolbarEvents.Kind.Active);
			this.active = true;
		}

		internal void OnFalse()
		{
			if (!this.enabled)	return;
			this.sendFallingEvent(ToolbarEvents.Kind.Active);
			this.active = false;
		}

		internal void OnHoverIn()
		{
			if (!this.enabled)
			{ 
				if (this.hovering)
				{
					this.sendFallingEvent(ToolbarEvents.Kind.Hover);
					this.hovering = false;
				}
				return;
			}

			this.sendRaisingEvent(ToolbarEvents.Kind.Hover);
			this.hovering = true;
		}

		internal void OnHoverOut()
		{
			if (!this.enabled)	return;
			this.sendFallingEvent(ToolbarEvents.Kind.Hover);
			this.hovering = false;
		}

		internal void OnEnable()
		{
			this.sendRaisingEvent(ToolbarEvents.Kind.Enabled);
			this.enabled = true;
		}

		internal void OnDisable()
		{
			if (this.active)
			{
				this.sendFallingEvent(ToolbarEvents.Kind.Active);
				return;
			}

			this.sendFallingEvent(ToolbarEvents.Kind.Enabled);
			this.enabled = false;
		}

		private void OnLeftClick()
		{
			if (!this.enabled)	return;
			if (!this.mouseEvents.Has(MouseEvents.Kind.Left)) return;
			this.HandleMouseClick(MouseEvents.Kind.Left);
		}

		private void OnRightClick()
		{
			if (!this.enabled)	return;
			if (!this.mouseEvents.Has(MouseEvents.Kind.Right)) return;
			this.HandleMouseClick(MouseEvents.Kind.Right);
		}

		private void HandleMouseClick(MouseEvents.Kind kind)
		{
			if (!this.enabled)	return;
			foreach (KeyCode k in this.mouseEvents[kind].modifier)
				if (!UnityEngine.Input.GetKeyDown(k)) return;
			this.mouseEvents[kind].clickHandler();
		}

		private void updateActiveState(bool newState)
		{
			if (newState == this.active) return;
			#if DEBUG
			Log.debug("Button {0}::{1} updateActiveState set to {2}"
					, this.owner, this.ID
					, newState
				);
			#endif
			if (this.active) this.OnFalse(); else this.OnTrue();
			this.active = newState;
			this.state.set(this.active);	// Now do you see why that mess above? ;)
		}

		private void updateHoverState(bool newState)
		{
			if (newState == this.hovering) return;
			#if DEBUG
			Log.debug("Button {0}::{1} updateHoverState set to {2}"
					, this.owner, this.ID
					, newState
				);
			#endif
			if (this.hovering) this.OnHoverOut(); else this.OnHoverIn();
			this.hovering = newState;
			this.state.set(this.hovering);	// Now do you see why that mess above? ;)
		}

		private void updateEnableState(bool newState)
		{
			if (newState == this.enabled) return;
			#if DEBUG
			Log.debug("Button {0}::{1} updateEnableState set to {2}"
					, this.owner, this.ID
					, newState
				);
			#endif
			if (this.enabled) this.OnDisable(); else this.OnEnable();
			this.enabled = newState;
			this.state.set(this.enabled);	// Now do you see why that mess above? ;)
		}

		private void sendRaisingEvent(ToolbarEvents.Kind kind)
		{
			if (this.toolbarEvents.Has(kind) && null != this.toolbarEvents[kind].raisingEdge)
				this.toolbarEvents[kind].raisingEdge();
		}

		private void sendFallingEvent(ToolbarEvents.Kind kind)
		{
			if (this.toolbarEvents.Has(kind) && null != this.toolbarEvents[kind].fallingEdge)
				this.toolbarEvents[kind].fallingEdge();
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

		/**
		 * I know that you know that everybody knows that people will forget to call the Destroy
		 * on the Destroy of their MonoBehaviours...
		 * 
		 * It's still bad doing it here, but it's less worse than not doing it at all!
		 * 
		 * Calling Destroy twice will not hurt anyway.
		 */
		~Toolbar()
		{
			this.Destroy();
		}

		public void Destroy()
		{
			foreach(Button b in this.buttons) ApplicationLauncher.Instance.RemoveModApplication(b.Control);
			this.buttons.Clear();
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
				using (System.IO.Stream si = this.GetType().Assembly.GetManifestResourceStream("KSPe.UI.Resources.launchicon"))
				{ 
					Byte[] data = new System.IO.BinaryReader(si).ReadBytes(int.MaxValue);
					if (!KSPe.Util.Image.File.Load(out defaultButton, data))
						throw new NullReferenceException("KSPe.UI.Resources.launchicon");	// Screw the best practices, I will not rework ths just because of them.
				}
			}
			catch (Exception e)
			{
				Log.error(e, "Could not read the Default Button texture from Resources.");
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
