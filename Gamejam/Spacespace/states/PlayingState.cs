using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class PlayingState : GameObjectList
{
    protected List<Level> levels;
    protected int currentLevelIndex;
    protected ContentManager content;
    protected int difficulty = 0;

    public PlayingState(ContentManager content)
    {
       
        this.content = content;
        currentLevelIndex = 0;
        levels = new List<Level>();
        LoadLevels();

    }

    public Level CurrentLevel
    {
        get { return levels[currentLevelIndex]; }
    }

    public int CurrentLevelIndex
    {
        get { return currentLevelIndex; }
        set
        {
            if (value >= 0 && value < levels.Count)
            {
                currentLevelIndex = value;
                CurrentLevel.Reset();
            }
        }
    }

    public List<Level> Levels
    {
        get { return levels; }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        CurrentLevel.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CurrentLevel.Update(gameTime);
        if (CurrentLevel.GameOver)
        {
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        }
        else if (CurrentLevel.Completed)
        {
            GameEnvironment.GameStateManager.SwitchTo("levelFinishedState");
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        CurrentLevel.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        CurrentLevel.Reset();
    }

    public void NextLevel()
    {
        CurrentLevel.Reset();
        if (currentLevelIndex >= levels.Count - 1)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleState");
        }
        else
        {
            CurrentLevelIndex++;
        }
    }

    public void LoadLevels()
    {
            levels.Add(new Level(0));
    }
}