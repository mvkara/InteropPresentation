namespace FSharpExampleLib
// This file contains my example library code written in F#

open System
open System.Collections.Generic
open System.Runtime.CompilerServices

/// A singly linked-list data structure. F# can be more concise for data structures and core library work. 
/// Note this is pretty similar to the F# built-in list.   
type public RecursiveList<'t> = 
    | Head of item:'t * tail: RecursiveList<'t>
    | Empty
    // Some interop methods to allow the type to be useable as is from C#
    member x.Match(headHandler: Func<'t, RecursiveList<'t>, _>, emptyHandler: Func<_>) =
        match x with
        | Head(i, t) -> headHandler.Invoke(i, t)
        | Empty -> emptyHandler.Invoke()
    override x.ToString() = sprintf "%A" x

/// Lets follow the F# convention and define some higher order functions in an accompanying module.
/// Most F# names follow standard naming conventions.
/// There is a bug in these functions (reverse order) but left for simplicity.
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
    
    // Factory method to build a sequence
    [<CompiledName("OfSeq")>]
    let public ofSeq s = s |> Seq.fold (fun s t -> s |> cons t) emptyList

/// The C# friendly API. Using extension methods to expose it to LINQ query syntax.
/// Make sure to give it friendly C# names which tend to mirror SQL terminology.
[<Extension>]
type public CSharpInterop = 
    [<Extension>]
    static member public Select<'t, 'r>(list: RecursiveList<'t>, funcSelector: Func<'t, 'r>) : RecursiveList<'r> =  
        RecursiveList.map funcSelector.Invoke list
    [<Extension>]
    static member public Prepend<'t>(list: RecursiveList<'t>, item: 't) : RecursiveList<'t> =  
        RecursiveList.cons item list
    [<Extension>]
    static member public ToRecursiveList<'t>(list: seq<'t>, item: 't) : RecursiveList<'t> =  
        RecursiveList.ofSeq list


