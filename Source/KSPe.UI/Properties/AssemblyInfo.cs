using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("KSPe.UI")]
[assembly: AssemblyDescription("User Interface Extensions for KSPe.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("L Aerospace KSP Division")]
[assembly: AssemblyProduct("KSP API Extensions/L")]
[assembly: AssemblyCopyright("©2018-19 Lisias")]
[assembly: AssemblyTrademark("KSPe.UI")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion(KSPe.Version.Number)]
[assembly: AssemblyFileVersion(KSPe.Version.Number)]
[assembly: KSPAssembly("KSPe.UI", KSPe.Version.major, KSPe.Version.minor)]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]
[assembly: KSPAssemblyDependency("KSPe", KSPe.Version.major, KSPe.Version.minor)]
[assembly: KSPAssemblyDependency("ClickThroughBlocker", 1, 8)]