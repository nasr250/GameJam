using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class TitleMenuState : GameObjectList
{
    protected Button playButton, optionButton;

    public TitleMenuState()
    {
        // load the title screen
        SpriteGameObject titleScreen = new SpriteGameObject("Backgrounds/titlebackground", 0, "background");
        Add(titleScreen);

        // add a play button
        playButton = new Button("Sprites/spr_button_play", 1);
        playButton.Position = new Vector2((GameEnvironment.Screen.X - playButton.Width) / 2, 540);
        Add(playButton);

        optionButton = new Button("Sprites/spr_button_cameramenu",1);
        optionButton.Position = new Vector2((GameEnvironment.Screen.X - playButton.Width) / 2, 600);
        Add(optionButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Enter))
        {
            playButton.Pressed = true;
        }
        if (playButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
        if (optionButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("optionMenuState");
        }
        //else if (helpButton.Pressed)
        //{
        //    GameEnvironment.GameStateManager.SwitchTo("helpState");
        //}
    }
}
