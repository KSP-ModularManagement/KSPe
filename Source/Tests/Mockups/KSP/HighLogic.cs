using System;

public class HighLogic
{
	public static HighLogic fetch = new HighLogic();
	public static Game CurrentGame => new Game();
	public String GameSaveFolder = "";
}
