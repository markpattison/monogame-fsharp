module Sample

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Content =
    {
        SpriteBatch: SpriteBatch
        SpriteFont: SpriteFont
        Effect: Effect
        Vertices: VertexPositionColor []
    }

let loadContent (_this: Game) device =
    {
        Effect = _this.Content.Load<Effect>("Effects/effects")
        SpriteFont = _this.Content.Load<SpriteFont>("Fonts/Arial")
        SpriteBatch = new SpriteBatch(device)

        Vertices =
            [|
                VertexPositionColor(Vector3( 0.0f,  0.8f, 0.0f), Color.Red)
                VertexPositionColor(Vector3( 0.8f, -0.8f, 0.0f), Color.Green)
                VertexPositionColor(Vector3(-0.8f, -0.8f, 0.0f), Color.Blue)
            |]
    }

let showParameters gameContent =
    let colour = Color.DarkSlateGray

    gameContent.SpriteBatch.Begin()
    gameContent.SpriteBatch.DrawString(gameContent.SpriteFont, "monogame-fsharp", Vector2(10.0f, 10.0f), colour)
    gameContent.SpriteBatch.End()

let draw (device: GraphicsDevice) gameContent (gameTime: GameTime) =
    let time = (single gameTime.TotalGameTime.TotalMilliseconds) / 100.0f

    do device.Clear(Color.LightGray)

    gameContent.Effect.CurrentTechnique <- gameContent.Effect.Techniques.["Coloured"]

    gameContent.Effect.CurrentTechnique.Passes |> Seq.iter
        (fun pass ->
            pass.Apply()
            device.DrawUserPrimitives(PrimitiveType.TriangleList, gameContent.Vertices, 0, 1)
        )

    showParameters gameContent
