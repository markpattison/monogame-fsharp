module Game

open System.IO
open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Game1() as _this =
    inherit Game()
    let mutable input = Unchecked.defaultof<Input.State>
    let mutable gameContent = Unchecked.defaultof<Sample.Content>

    let graphics = new GraphicsDeviceManager(_this)

    do graphics.GraphicsProfile <- GraphicsProfile.HiDef

    do graphics.PreferredBackBufferWidth <- 800
    do graphics.PreferredBackBufferHeight <- 600
    do graphics.IsFullScreen <- false

    do graphics.ApplyChanges()
    do base.Content.RootDirectory <- "content"

    let updateInputState() =
        input <- Keyboard.GetState() |> Input.updated input

    override _this.Initialize() =
        base.Initialize()

    override _this.LoadContent() =
        input <- Input.initialState()

        gameContent <- Sample.loadContent _this _this.GraphicsDevice

    override _this.Update(gameTime) =
        updateInputState()

        if Input.justPressed input Keys.Escape then _this.Exit()

        base.Update(gameTime)

    override _this.Draw(gameTime) =

        Sample.draw _this.GraphicsDevice gameContent gameTime
        
        base.Draw(gameTime)
