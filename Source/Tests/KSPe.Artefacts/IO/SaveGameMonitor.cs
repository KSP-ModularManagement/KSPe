namespace KSPe.IO
{
	internal class SaveGameMonitor
	{
		internal string saveName = null;
		internal string saveDirName = null;
		internal bool IsValid => (null != this.saveName && null != this.saveDirName);

		internal static SaveGameMonitor Instance { get => instance; } 
		private static SaveGameMonitor instance = new SaveGameMonitor();
	}
}
