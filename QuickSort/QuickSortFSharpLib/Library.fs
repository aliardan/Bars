namespace QuickSortFSharpLib

module QuickSort =
    let rec Qsort  = function
    | [] ->  []
    | pivot::rest -> let left, right = rest |> List.partition(fun i -> i < pivot)
                     (Qsort left) @ [pivot] @ Qsort right