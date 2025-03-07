open Amazon.CDK
open CdkStack

[<EntryPoint>]
let main _ =
    let app = App(null)

    CdkStack(app, "CdkStack", StackProps()) |> ignore

    app.Synth() |> ignore
    0
