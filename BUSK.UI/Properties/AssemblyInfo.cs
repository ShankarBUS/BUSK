using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: InternalsVisibleTo("BUSK")]

[assembly: XmlnsPrefix("http://schemas.busk.com/2020", "busk")]
[assembly: XmlnsDefinition("http://schemas.busk.com/2020", "BUSK.Navigation")]
[assembly: XmlnsDefinition("http://schemas.busk.com/2020", "BUSK.UI")]
[assembly: XmlnsDefinition("http://schemas.busk.com/2020", "BUSK.UI.BuskBar")]
[assembly: XmlnsDefinition("http://schemas.busk.com/2020", "BUSK.UI.Commands")]