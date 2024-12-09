/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

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
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace KSPe.IO
{
	// Why in hell KSP is, now, instantiating this thing twice?
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	public class SaveGameMonitor : MonoBehaviour
	{
		public interface SaveGameLoadedListener
		{
			void OnSaveGameLoaded(string name);
			void OnSaveGameClosed();
		}

		internal string saveName = null;
		internal string saveDirName = null;

		public bool IsValid => (null != this.saveName && null != this.saveDirName);

		internal static SaveGameMonitor instance = null;
		internal static readonly SaveGameMonitor fakeInstance = new SaveGameMonitor(true);
		public static SaveGameMonitor Instance => instance ?? fakeInstance;

		private readonly HashSet<SaveGameLoadedListener> listeners = new HashSet<SaveGameLoadedListener>();
		private readonly HashSet<SaveGameLoadedListener> singleShotListeners = new HashSet<SaveGameLoadedListener>();

		private SaveGameMonitor()
		{
			Log.debug("SaveGameMonitor instantiated.");
		}
		private SaveGameMonitor(bool isFake) { }

		~SaveGameMonitor()
		{	// Better safer then sorrier!
			this.listeners.Clear();
			this.singleShotListeners.Clear();
		}

		public bool AddSingleShot(SaveGameLoadedListener listener) => this.singleShotListeners.Add(listener);
		public bool Add(SaveGameLoadedListener listener) => this.listeners.Add(listener);
		public bool Remove(SaveGameLoadedListener listener) => this.listeners.Remove(listener);

		private void Awake()
		{
			if (null != instance)
			{
				Log.debug("SaveGameMonitor.OnAwake aborted. Already initialized!");
				return;
			}
			Log.debug("SaveGameMonitor.OnAwake");
			GameObject.DontDestroyOnLoad(this.gameObject);
			GameEvents.onGameSceneLoadRequested.Add(this.OnGameSceneLoadRequested);
			instance = this;
			this.enabled = false;
		}

		private void Start()
		{
			Log.debug("SaveGameMonitor.Start");
		}

		private void OnDestroy()
		{
			Log.debug("SaveGameMonitor.OnDestroy");
			GameEvents.onGameSceneLoadRequested.Remove(this.OnGameSceneLoadRequested);
			this.listeners.Clear();
			this.singleShotListeners.Clear();
		}

		private void OnGameSceneLoadRequested(GameScenes data)
		{
			Log.debug("SaveGameMonitor.data = {0}; HighLogic.fetch.GameSaveFolder = {1}", data, HighLogic.fetch.GameSaveFolder);
			this.enabled = true;
		}

		private void Update()
		{
			Log.debug("SaveGameMonitor.Update @ {0}", HighLogic.LoadedScene);
			this.enabled = false;
			if (HighLogic.LoadedScene >= GameScenes.SPACECENTER && !this.IsValid)
			{
				this.saveName = HighLogic.CurrentGame.Title;
				this.saveDirName = HighLogic.fetch.GameSaveFolder;
				Log.detail("SaveGameMonitor.saveName = {0}; SaveGameMonitor.saveDirName = {1}", this.saveName, this.saveDirName);
				this.NotifyListeners(true);
				return;
			}
			this.saveName = null;
			this.saveDirName = null;
			this.NotifyListeners(false);
		}

		private void NotifyListeners(bool isLoaded)
		{
			HashSet<SaveGameLoadedListener> listeners = new HashSet<SaveGameLoadedListener>();
			listeners.UnionWith(this.listeners);
			listeners.UnionWith(this.singleShotListeners);
			this.singleShotListeners.Clear();
			Log.debug("SaveGameMonitor.NotifyListeners({0}) {1} listeners", isLoaded, listeners.Count);
			StartCoroutine(NotifyListeners(isLoaded, listeners));
		}

		private IEnumerator NotifyListeners(bool isLoaded, HashSet<SaveGameLoadedListener> listeners)
		{
			foreach (SaveGameLoadedListener l in listeners)
			{
				if (isLoaded) l.OnSaveGameLoaded(this.saveName);
				else l.OnSaveGameClosed();

				yield return null;
			}
		}
	}
}
