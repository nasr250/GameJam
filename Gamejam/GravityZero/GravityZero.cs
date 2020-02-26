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
        gameStateManager.SwitchTo("titleMenu");

        AssetManager.PlayMusic("Audio/telepathy"); //source: https://icons8.com/music/genre--synthwave Telepathy - Reminiscor
        MediaPlayer.Volume = 0.1f;
    }
}