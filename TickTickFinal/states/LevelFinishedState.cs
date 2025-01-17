﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class LevelFinishedState : GameObjectList
{
    protected IGameLoopObject playingState;

    public LevelFinishedState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject("Overlays/spr_welldone", Camera.UILayer);
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        Add(overlay);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!inputHelper.KeyPressed(Keys.Space))
        {
            return;
        }
        GameEnvironment.Camera.Reset();
        GameEnvironment.GameStateManager.SwitchTo("playingState");
        (playingState as PlayingState).NextLevel();
    }

    public override void Update(GameTime gameTime)
    {
        playingState.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}