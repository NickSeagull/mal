module REPL
    open System

    let read input =
        try
            Reader.read_str input
        with
        | Types.ReaderError(msg) ->
            printfn "%s" msg
            []

    let eval env ast =
        try
            Some(Eval.eval env ast)
        with
        | Types.EvalError(msg) ->
            printfn "%s" msg
            None

    let print v =
        v
        |> Seq.singleton
        |> Printer.pr_str
        |> printfn "%s"

    let rep env input =
        read input
        |> Seq.ofList
        |> Seq.choose (fun form -> eval env form)
        |> Seq.iter (fun value -> print value)

    let getReadlineMode (args : string array) =
        if args.Length > 0 && args.[0] = "--raw" then
            Readline.Mode.Raw
        else
            Readline.Mode.Terminal

    [<EntryPoint>]
    let main args =
        let mode = getReadlineMode args
        let env = Env.makeRootEnv ()
        let rec loop () =
            match Readline.read "user> " mode with
            | null -> 0
            | input ->
                rep env input
                loop ()
        loop ()
