using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;

class GravityZero : GameEnvironment
{
    [STAThread]
    static void Main()
    {
        GravityZero game = new GravityZero();
        game.Run();
    }
    int once = 1;
    public GravityZero()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void LoadContent()
    {
        base.LoadContent();
        screen = new Point(1920, 1080);
        windowSize = new Point(1920, 1080);
        FullScreen = false;
        gameStateManager.AddGameState("titleMenu", new TitleMenuState());  //establishes all the gamestates for the game, 
        gameStateManager.AddGameState("playingState", new PlayingState(Content));
        gameStateManager.AddGameState("gameOverState", new GameOverState());
        gameStateManager.AddGameState("optionMenuState", new OptionMenuState());
        gameStateManager.AddGameState("upgradeMenuState", new UpgradeMenustate());
        gameStateManager.SwitchTo("titleMenu");


        MediaPlayer.Volume = 0.1f;
    }
    protected override void Update(GameTime gameTime)
    {

        base.Update(gameTime);
        AssetManager.AddMusic("Audio/intro");
        AssetManager.AddMusic("Audio/arpanauts");
        AssetManager.AddMusic("Audio/searching");

        if (MediaPlayer.State == MediaState.Stopped)
        {
            AssetManager.PlaySong();
        }

    }
}