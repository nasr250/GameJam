using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;
    TextGameObject score;
    static public int Score;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject("Overlays/spr_gameover");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        score = new TextGameObject("Sprites/SpelFont"); //makes a new score object
        score.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y + 100) / 2;
        score.Text = Score.ToString();
        score.Color = Color.Black;
        Add(overlay);
        Add(score);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        playingState.HandleInput(inputHelper);
        if (!inputHelper.KeyPressed(Keys.Space))
        {
            return;
        }
        playingState.Reset();
        GameEnvironment.GameStateManager.SwitchTo("playingState");
    }

    public override void Update(GameTime gameTime)
    {
        playingState.Update(gameTime);
        score.Text = "Score: " + Score;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}