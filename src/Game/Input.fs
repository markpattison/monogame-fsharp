module Input

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

type State = 
    {
        KeyboardState: KeyboardState
        PreviousKeyboardState: KeyboardState
    }

let initialState() =
    let state = Microsoft.Xna.Framework.Input.Keyboard.GetState()
    {
        KeyboardState = state
        PreviousKeyboardState = state
    }

let updated inputState keyboardState =
    {
        KeyboardState = keyboardState
        PreviousKeyboardState = inputState.KeyboardState
    }

let justPressed inputState key =
    inputState.KeyboardState.IsKeyDown(key) && inputState.PreviousKeyboardState.IsKeyUp(key)
