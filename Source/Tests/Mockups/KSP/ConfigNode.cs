using System;

public class ConfigNode
{
	public class Value
	{
		internal string name;
		internal string value;
		internal string comment;

		public Value()
		{
			this.name = "name";
			this.value = "value";
			this.comment = "comment";
		}

		public Value(string name, string value, string comment)
		{
			this.name = name;
			this.value = value;
			this.comment = comment;
		}
	}

	public class ValueList
	{
		public Value this[int index]
		{
			get => new Value();
			set { }
		}
		public int Count { get; internal set; }

		internal void Add(Value nv)
		{
			throw new NotImplementedException();
		}
	}

	public class ConfigNodeList
	{
		public ConfigNode this[int index]
		{
			get => new ConfigNode();
			set { }
		}
		public int Count => 0;

		internal void Add(ConfigNode n)
		{
			throw new NotImplementedException();
		}
	}

	public string id;
	public string name;
	public string comment;

	public ValueList values { get; internal set; }
	public ConfigNodeList nodes { get; internal set; }

	public ConfigNode()
	{
	}
}
