using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

class OptionMenuState : GameObjectList
{

    //protected Player player;
    protected SpriteFont spriteFont;
    protected Button backButton;
    protected ToggleButton colorFilterToggle;
    protected List<string> cameraNames = new List<string>();
    protected SliderButton Red, Blue, Green, Sensitivity, Volume;


    public OptionMenuState()
    {
        spriteFont = GameEnvironment.AssetManager.GetSpriteFont("Sprites/SpelFont");
        SpriteGameObject background = new SpriteGameObject("Backgrounds/titlebackground", 0, "background"); 
        Add(background);

        //player = GameWorld.Find("player") as Player;
        backButton = new Button("Sprites/spr_button_back" , 1);
        backButton.Position = new Vector2(GameEnvironment.Screen.X - backButton.Width, 10);
        Add(backButton);

        Volume = new SliderButton("Sprites/spr_button_sliderback", "Sprites/spr_button_sliderfront");
        Add(Volume);
        Volume.Position = new Vector2(GameEnvironment.Screen.X /2 - 100, GameEnvironment.Screen.Y / 2);
        Volume.Value = 1;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        MediaPlayer.Volume = Volume.Value / 10;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(spriteFont, "Volume:", Volume.Position + new Vector2(0, -20), Color.White);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }
}

   

