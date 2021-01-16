#r "paket: groupref build //"
#load "./.fake/build.fsx/intellisense.fsx"

open System

open Fake.Core
open Fake.DotNet
open Fake.IO.Globbing.Operators

let contentFiles = !! "./content/**/*.fx" ++ "./content/**/*.spritefont"
let projectFile = "./src/Game/Game.fsproj"

Target.create "BuildContent" (fun _ ->
    let toBuild =
        contentFiles
        |> Seq.map (fun filepath -> sprintf "/b:%s" filepath)

    let args = "/platform:Windows /o:src/Game /n:intermediateContent " + String.Join(" ", toBuild)
    let result = DotNet.exec id "mgcb" args
    if result.ExitCode <> 0 then failwithf "'dotnet mgcb %s' failed" args
)

Target.create "Build" (fun _ ->
    DotNet.build id projectFile
)

Target.create "Run" (fun _ ->
    CreateProcess.fromRawCommand "src/Game/bin/Release/netcoreapp3.1/Game.exe" []
    |> Proc.startRawSync
    |> ignore
    Process.setKillCreatedProcesses false
)

open Fake.Core.TargetOperators

"BuildContent" ==> "Build" ==> "Run"

Target.runOrDefaultWithArguments "Build"
