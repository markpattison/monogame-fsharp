module Program

[<EntryPoint>]
let main argv =
    let game = new Game.Game1()
    do game.Run()
    0
