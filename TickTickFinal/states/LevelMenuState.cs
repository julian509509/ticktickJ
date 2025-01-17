﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

class LevelMenuState : GameObjectList
{
    protected Button backButton;

    public LevelMenuState()
    {
        PlayingState playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
        List<Level> levels = playingState.Levels;

        // add a background
        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_levelselect", Camera.UILayer, "background");
        Add(background);

        // add the level buttons
        for (int i = 0; i < 10; i++)
        {
            int row = i / 4;
            int column = i % 4;
            LevelButton level = new LevelButton(i + 1, levels[i], Camera.UILayer + 1);
            level.Position = new Vector2(column * (level.Width + 20), row * (level.Height + 20)) + new Vector2(390, 180);
            Add(level);
        }

        // add a back button
        backButton = new Button("Sprites/spr_button_back", Camera.UILayer + 1);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);
    }

    public int LevelSelected
    {
        get
        {
            foreach (GameObject obj in children)
            {
                LevelButton levelButton = obj as LevelButton;
                if (levelButton != null && levelButton.Pressed)
                {
                    return levelButton.LevelIndex;
                }
            }
            return -1;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (LevelSelected != -1)
        {
            PlayingState playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
            playingState.CurrentLevelIndex = LevelSelected - 1;
            playingState.CurrentLevel.PrepareCamera();
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
        else if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }
}

