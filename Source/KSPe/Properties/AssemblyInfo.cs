using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("KSPe Loader")]
[assembly: AssemblyDescription("Loads KSPe.Main into memory.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(KSPe.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(KSPe.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(KSPe.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(KSPe.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion(KSPe.Loader.Version.Number)]
[assembly: AssemblyFileVersion(KSPe.Version.Number)]	// Yes, this one will use the Package Version!
[assembly: KSPAssembly("KSPe.Loader", KSPe.Loader.Version.major, KSPe.Loader.Version.minor)]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]
