namespace FSharpUsingCSharpLib

open System
open CSharpExampleLib

module OverloadingUsage = 
    let webserviceOverloads = WebserviceUsingOverloads()
    let niceWebservice = NiceWebservice()

    // Lets try to call each one
    // Note I need type annotations here.
    // If I add another predicate overload it breaks
    let workWithBadService (index: int) (name: string) (stringFilter: _ -> bool) 
        //(indexFilter: int -> bool) 
        =
        // Need to specify the types for all arguments now meaning inference breaks
        [
        webserviceOverloads.Get(index);
        webserviceOverloads.Get(name);
        webserviceOverloads.Get((Func<_, _> stringFilter))
        //webserviceOverloads.Get((Func<_, _> indexFilter))
        ]


    let workWithNiceService index name stringFilter indexFilter =
        [
        niceWebservice.GetByIndex(index);
        niceWebservice.GetByName(name);
        niceWebservice.GetByStringFilter((Func<_, _> stringFilter))
        niceWebservice.GetByIndexFilter((Func<_, _> indexFilter))
        ]
