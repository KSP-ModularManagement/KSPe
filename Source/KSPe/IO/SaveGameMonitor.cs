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
using UnityEngine;

namespace KSPe.IO
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class SaveGameMonitor : MonoBehaviour
	{
		internal string saveName = null;
		internal string saveDirName = null;

		public bool IsValid => (null != this.saveName && null != this.saveDirName);

		// Why in hell KSP is, now, instantiating this thing twice?
		internal static SaveGameMonitor Instance { get => instance; } 
		private static SaveGameMonitor instance = null;

		private bool HadMigrated = false;

		private void Awake()
		{
			if (null != instance)
			{
				Log.debug("SaveGameMonitor.OnAwake aborted. Already initialized!");
				return;
			}
			Log.debug("SaveGameMonitor.OnAwake");
			GameObject.DontDestroyOnLoad(this);
			GameEvents.onGameSceneLoadRequested.Add(this.OnGameSceneLoadRequested);
			instance = this;
		}

		private void Start()
		{
			Log.debug("SaveGameMonitor.Start");
			this.enabled = false;
		}

		private void OnDestroy()
		{
			Log.debug("SaveGameMonitor.OnDestroy");
			GameEvents.onGameSceneLoadRequested.Remove(this.OnGameSceneLoadRequested);
		}

		private void OnGameSceneLoadRequested(GameScenes data)
		{
			Log.debug("SaveGameMonitor.data = {0}; HighLogic.fetch.GameSaveFolder = {1}", data, HighLogic.fetch.GameSaveFolder);
			//this.enabled = true;
			this.Update();
		}

		private void Update()
		{
			this.saveName = null;
			this.saveDirName = null;
			if (HighLogic.LoadedScene >= GameScenes.SPACECENTER)
			{
				this.saveName = HighLogic.CurrentGame.Title;
				this.saveDirName = HighLogic.fetch.GameSaveFolder;
				Log.detail("SaveGameMonitor.saveName = {0}; SaveGameMonitor.saveDirName = {1}", this.saveName, this.saveDirName);
			}
			this.enabled = false;
		}
	}
}
