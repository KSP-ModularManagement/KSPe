using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("KSPe.UI.14")]
[assembly: AssemblyDescription("User Interface Extensions for KSPe. Specialized KSPe.UI for [1.4 <= KSP < 1.8]. This DLL should be used at runtime.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(KSPe.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(KSPe.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(KSPe.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(KSPe.LegalMamboJambo.Trademark)]
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

// Not really needed as this DLL is not being loaded by KSP, but it's nice to have it.
[assembly: KSPAssemblyDependency("ClickThroughBlocker", 1, 6)]
