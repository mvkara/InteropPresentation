namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FSharpExampleLib")>]
[<assembly: AssemblyProductAttribute("InteropPresentation")>]
[<assembly: AssemblyDescriptionAttribute("Code to accompany interop talk")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
    let [<Literal>] InformationalVersion = "1.0"
