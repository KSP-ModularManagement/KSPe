# KSP API Extensions /L

New extensions and utilities for Kerbal Space Program by Lisias.


## In a Hurry

* [Latest Release](https://github.com/net-lisias-ksp/KSPAPIExtensions/releases)
    + [Binaries](https://github.com/net-lisias-ksp/KSPAPIExtensions/tree/Archive)
* [Source](https://github.com/net-lisias-ksp/KSPAPIExtensions)
* [Issue Tracker](https://github.com/net-lisias-ksp/KSPAPIExtensions/issues)
* Documentation	
	+ [Homepage](http://ksp.lisias.net/add-ons/KSPAPIExtensions) on L Aerospace KSP Division
	+ [Project's README](https://github.com/net-lisias-ksp/KSPAPIExtensions/blob/master/README.md)
	+ [Install Instructions](https://github.com/net-lisias-ksp/KSPAPIExtensions/blob/master/INSTALL.md)
	+ [Change Log](./CHANGE_LOG.md)
	+ [Known Issues](./KNOWN_ISSUES.md)
	+ [TODO](./TODO.md) list
* Official Distribution Sites:
	+ [Homepage](http://ksp.lisias.net/add-ons/KSPAPIExtensions) on L Aerospace
	+ [Source and Binaries](https://github.com/net-lisias-ksp/KSPAPIExtensions) on GitHub.


## Description
This add in is useful for providing some functions that make interacting with the KSP API functionally nicer and with an improved interface. 

### Utility classes
There are a number of utility classes available that do different functions. If you don't want to distribute the whole DLL with your plugin, you can just copy the appropriate source file into your project.

#### Math Utils
The main feature of this is formatting of floats and doubles with SI prefixes. 

Examples:

````
	12.ToStringExt("S") -> "12"
	12.ToStringExt("S3") -> "12.0"
	120.ToStringExt("S3") -> "120"
	1254.ToStringExt("S3") -> "1250"  (4 digit numbers do not use k as a special case)
	12540.ToStringExt("S3") -> "1.25 k"  (using SI prefixes)
	12540.ToStringExt("S4") -> "1.254 k"  (more significant figures)
	(1.254).ToStringExt("S4+3") -> "1.254 k"  (+3 means the 'natural prefix' is k)
	(1.254).ToStringExt("S4-3") -> "1.254 m"  (-3 means the 'natural prefix' is m)
````

#### Other utility classes
Utility methods to determine relationships between parts, plus some debugging code.

### Improvements to tweakables
Available is two extra tweakable controls, plus improvements to the stock tweakers. To use these you *must* include the KSIAPIUtils.dll in your project rather than just copying the code as there's an election process to ensure the latest version is being run. If backwards compatibility breaks, I will ensure that the user is warned to upgrade plugins.

#### SI Formatting for label fields.
Just use an 'S' type format code and SI prefixes will be handled. 

````c#
	[KSPField(guiActive = true, guiActiveEditor = true, guiName = "Volume", guiFormat = "S3+3", guiUnits = "L")]
	public float tankVolume = 0.0f;
````

For this example, the tankVolume variable is in kL, and will be displayed with 3 sig figs of accuracy.

#### SI Formatting and dynamic updating for resources in the editor
This happens transparently. You can change the amount and maxAmount variables in your PartResource object in the editor, and they will be updated in the tweaker.

The amount and max amount are displayed with four significant figures of accuracy.

If you add and remove resources, you will need to force a refresh of the part tweaker window like this:

````c#
	UIPartActionWindow window = part.FindActionWindow();
	if (window != null)
		window.displayDirty = true;
````

#### UI_ChooseOption
This allows the user to chose from a range of options. It's equivalent to a dropdown list only without the dropdown (dropdowns were difficult to do with the API).

Use it like this:

````c#
	[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "Option"), 
		UI_ChooseOption(options=new string [] { "cheese", "pickles" })]
	public string toppingOption;
````

You can also have one set of options for the field, and one set to display if you use the display parameter.

Usually it's more appropriate to set the list of options at runtime:

````c#
	[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "Tank Type"), UI_ChooseOption(scene=UI_Scene.Editor)]
	public string tankType;
	
	\\ ...
	
	public override void OnStart(PartModule.StartState state)
	{
            BaseField field = Fields["tankType"];
            UI_ChooseOption options = (UI_ChooseOption)field.uiControlEditor;

            options.options = new string [] { "cheese", "pickles" };
	}
````

#### UI_FloatEdit
This is a much improved version of UI_FloatRange. 

You can select a float value with a set range. The value can be edited with optional large and small offsets, plus a slider to choose values between. Naturally SI formatting is available.

````c#
	[KSPField(isPersistant = true, guiActiveEditor = true, guiActive = false, guiName = "Top", guiFormat = "S4", guiUnits="m"),
	 UI_FloatEdit(scene = UI_Scene.Editor, minValue = 0.25f, incrementLarge = 1.25f, incrementSmall = 0.25f, incrementSlide = 0.001f)]
	public float topDiameter = 1.25f;
````

if incrementSmall is not set, then no button is visible in the control. If incrementLarge is not set then it has just a slider.

The slider is set to run between the smallest available increment, so in the above if the current value was 1.3, then the slider would run from 1.25 to 1.5.

### PartMessage
This is an improvement over the existing SendMessage system of Unity. It creates a publish/subscribe model for messages both within and between parts on a vessel. It will automatically wire message event senders and listeners in a PartModule or Part if they have the appropriate attributes.

There's also a set of common messages defined, so addon builders can send and listen for those events in their code.

Once again, to use these you *must* include the KSIAPIUtils.dll in your project rather than just copying the code as there's an election process to ensure the latest version is being run. If backwards compatibility breaks, I will ensure that the user is warned to upgrade plugins.

#### Initialization of message system
Any class or module that uses part messages must initialize the system. The easiest way is like this:

````c#
    public abstract class ProceduralAbstractShape : PartModule
    {
        public override void OnAwake()
        {
            base.OnAwake();
            PartMessageService.Register(this);
        }
````

You need to do this in OnAwake, as the part variable needs to be initialized.

#### Part messages
Event types are declared as delegates with a special marker interface:

````c#
    [PartMessage(isAbstract: true)]
    public delegate void PartPhysicsChanged();

    [PartMessageDelegate(typeof(PartPhysicsChanged))]
    public delegate void PartMassChanged([UseLatest] float mass);
````

These two events are in the common library. There's no constraints on the argument list, however you do not need to have the sending part as an argument as that is handled.

Note that the PartMassChanged message has a parent event - PartPhysicsChanged. If you ever raise a PartMassChanged message then any listeners for PartPhysicsChanged will also be informed. The isAbstract flag indicates this should not be sent directly as an event. Currently this is not enforced but may be in the future.

The argument list for any parent should either be the same, or a truncation of the list for the child event. Truncation is handled gracefully.

Note the [UseLatest] attribute for the PartMassChanged event. This indicates that if a whole pile of PartMassChanged events gets sent from the same source that differ only in their mass arguments, then these can be consolidated into one event where the mass is the last one recieved. 

````c#
    [PartMessageDelegate(typeof(PartResourcesChanged))]
    public delegate void PartResourceMaxAmountChanged(string resource, [UseLatest] double maxAmount);
````

Above is an example of where this is not the case, the resource name uniquely identifies a message, however multiple messages with the same source but differ only in the maxAmount can be consolidated.

#### Message sending
In your PartModule and Part objects, you can just declare and use an event like so:

````c#
	[PartMessageEvent]
	public event PartMassChanged MassChanged;

	private void UpdateMass(float value) 
	{
		part.mass = mass;
		MassChanged(mass);
	}
````

If you can send the update message from some other source, see the code for details of how to do it.

#### Message receiving
You can listen for messages by just declaring a method with a marker attribute:

````c#
	[PartMessageListener(typeof(PartMassChanged), scenes:GameSceneFilter.AnyEditor, relations:PartRelationship.Self)]
	private void ChangeVolume(float volume)
````

Note the two optional filtering properties.

*scenes:* this allows you to only listen in certain game scenes. Good ones to use are GameSceneFilter.AnyEditor for editor mode, and GameSceneFilter.Flight for flight mode. Note that messages can still get sent during the GetInfo phase of initialization, which is useful if you'd like to tweak the VAB icon.

*relations:* If not specified, the default is to only listen for messages from the same part, however you can listen to messages from Parents, Siblings, Ancestors... others.

#### Advanced message management
You can filter messages, buffer them up and send them in one hit, and send them dynamically. I won't go into the full details in this document. Have a look at the source for more details.

### KSPe

Documentation is Work In Progress. See the source for while. :)


## Installation

Detailed installation instructions are now on its own file (see the [In a Hurry](#in-a-hurry) section) and on the distribution file.

### Licensing
* KSPAPIExtensions.dll
	+ [CC BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/). See [here](./LICENSE.KSPAPIExtensions).
		+ You are free to:
			- Share : copy and redistribute the material in any medium or format
			- Adapt : remix, transform, and build upon the material for any purpose, even commercially. 
		+ Under the following terms:
			- Attribution : You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
			- ShareAlike : If you remix, transform, or build upon the material, you must distribute your contributions under the same license as the original.
* KSPe.dll is double licensed at user discretion:
	+ [SKL 1.0](https://ksp.lisias.net/SKL-1_0.txt). See [here](./LICENSE.KSPe.SKL-1_0)
		+ You are free to:
			- Use : unpack and use the material in any computer or device
			- Redistribute: redistribute the original package in any medium
		+ Under the following terms:
			- You agree to use the material only on (or to) KSP
			- You don't alter the package in any form or way (but you can embedded it)
			- You don't change the material in any way, and retain any copyright notices
			- You must explicitly state the author's Copyright, as well an Official Site for downloading the original and new versions (the one you used to download is good enough) 
	+ [GPL 2.0](https://www.gnu.org/licenses/gpl-2.0.txt). See [here](./LICENSE.KSPe.GPL-2_0)
		+ You are free to:
			- Use : unpack and use the material in any computer or device
			- Redistribute : redistribute the original package in any medium
			- Adapt : Reuse, modify or incorporate source code into your works (and redistribute it!) 
		+ Under the following terms:
			- You retain any copyright notices
			- You recognize and respect any trademarks
			- You don't impersonate the authors, neither redistribute a derivative that could be misrepresented as theirs.
			- You credit the author and republish the copyright notices on your works where the code is used.
			- You relicense (and fully comply) your works using GPL 2.0 (or later)
			- You don't mix your work with GPL incompatible works.
	* If by some reason the GPL would be invalid for you, rest assured that you still retain the right to Use the Work under SKL 1.0. 
* See [3rd Parties License](./LICENSE.3rdParties) for additional licenses for Thirt Parties material used with permission (implicit or explicit).

Please note the copyrights and trademarks in [NOTICE](./NOTICE).


## UPSTREAM

* [swamp_ig](https://forum.kerbalspaceprogram.com/index.php?/profile/85299-pellinor/): ROOT
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/73648-104-kspapiextensions-v175-utilities-for-shared-mod-use-25-jun/)
	+ [GitHub](https://github.com/Swamp-Ig/KSPAPIExtensions)
* [toadicus](https://forum.kerbalspaceprogram.com/index.php?/profile/67745-toadicus/): Parallel Fork
	+ [GitHub](https://github.com/toadicus/KSPAPIExtensions)
