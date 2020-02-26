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
    protected DropDownButton cameraSelectionButton;
    protected ToggleButton colorFilterToggle;
    protected int selectedCamera;
    protected List<string> cameraNames = new List<string>();
    protected SliderButton Red, Blue, Green, Sensitivity, Volume;
    protected string RedText, BlueText, GreenText, SensitivityText, VolumeText;
    protected Texture2D TrackedColor, TrackedObject;


    public OptionMenuState()
    {
        spriteFont = GameEnvironment.AssetManager.GetSpriteFont("Sprites/SpelFont");
        SpriteGameObject background = new SpriteGameObject("Backgrounds/background", 0, "background"); //source: https://rafaeldejongh.artstation.com/projects/1gX22
        Add(background);

        //player = GameWorld.Find("player") as Player;
        backButton = new Button("Sprites/spr_button_back" , 1);
        backButton.Position = new Vector2(GameEnvironment.Screen.X - backButton.Width, 10);
        Add(backButton);

        Volume = new SliderButton("Sprites/spr_button_sliderback", "Sprites/spr_button_sliderfront");
        Add(Volume);
        Volume.Position = new Vector2(1350, 300);
        Volume.Value = 1;

        TrackedColor = GameEnvironment.AssetManager.GetSprite("Sprites/spr_button_sliderfront");

        colorFilterToggle = new ToggleButton("Sprites/spr_button_colorfilteron", "Sprites/spr_button_colorfilteroff");
        colorFilterToggle.Position = new Vector2(260, 500);
        Add(colorFilterToggle);

        TrackedObject = GameEnvironment.AssetManager.GetSprite("Sprites/spr_targetrectangle");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);



      
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        spriteBatch.Draw(TrackedColor, new Vector2(900, 230), new Color(Red.Value, Green.Value, Blue.Value));
        spriteBatch.DrawString(spriteFont, RedText, Red.Position - new Vector2(83, -8), Color.White);
        spriteBatch.DrawString(spriteFont, BlueText, Blue.Position - new Vector2(85, -8), Color.White);
        spriteBatch.DrawString(spriteFont, GreenText, Green.Position - new Vector2(100, -8), Color.White);
        spriteBatch.DrawString(spriteFont, SensitivityText, Sensitivity.Position - new Vector2(130, -8), Color.White);
        spriteBatch.DrawString(spriteFont, VolumeText, Volume.Position - new Vector2(112, -8), Color.White);
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

   

