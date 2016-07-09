namespace FSharpExampleLib
// This file contains my example library code written in F#

open System
open System.Runtime.CompilerServices

// A singly linked-list data structure. F# can be more concise for data structures and core library work.    
type public RecursiveList<'t> = 
    | Head of item:'t * tail: RecursiveList<'t>
    | Empty
    member x.Match(headHandler: Func<'t, RecursiveList<'t>, _>, emptyHandler: Func<_>) =
        match x with
        | Head(i, t) -> headHandler.Invoke(i, t)
        | Empty -> emptyHandler.Invoke()

// Lets follow the F# convention and define some higher order functions in an accompanying module
module public RecursiveList =

    /// Prepends an item to the list I've defined
    /// While this is inline in F# it won't be in C#.
    let public cons item previousList = Head(item, previousList)

    // Attribute if you care about C# camel casing
    [<CompiledName("EmptyList")>]
    let public emptyList = Empty

    /// Similar to List.Map or C#'s .Select() LINQ extension method.
    /// An example of a higher order function that is hard to expose to C#.
    let public map mapFunc l = 
        let rec mapRec acc rl = 
            match rl with
            | Head(i, cl) -> 
                let newList = cons (mapFunc i) acc
                mapRec newList cl
            | Empty -> acc
        mapRec Empty l

/// The C# friendly API. Using extension methods to expose it to LINQ query syntax
[<Extension>]
type public CSharpInterop = 
    [<Extension>]
    static member public Select<'t, 'r>(list: RecursiveList<'t>, funcSelector: Func<'t, 'r>) : RecursiveList<'r> =  
        RecursiveList.map funcSelector.Invoke list

