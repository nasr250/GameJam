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

        for (int i = 0; i < GameEnvironment.Camera.Devices.Count; i++)
            cameraNames.Add(GameEnvironment.Camera.Devices[i].Name);
        cameraSelectionButton = new DropDownButton(GameEnvironment.Camera.Devices.Count, cameraNames);
        cameraSelectionButton.Position = new Vector2(10, 500);
        Add(cameraSelectionButton);

        Red = new SliderButton("Sprites/spr_button_sliderback", "Sprites/spr_button_sliderfront");
        Add(Red);
        Red.Position = new Vector2(800, 300);
        Red.Value = (float)GameEnvironment.Camera.FilterColor.Red/255;

        Blue = new SliderButton("Sprites/spr_button_sliderback", "Sprites/spr_button_sliderfront");
        Add(Blue);
        Blue.Position = new Vector2(800, 420);
        Blue.Value = (float)GameEnvironment.Camera.FilterColor.Blue/255;

        Green = new SliderButton("Sprites/spr_button_sliderback", "Sprites/spr_button_sliderfront");
        Add(Green);
        Green.Position = new Vector2(800, 360);
        Green.Value = (float)GameEnvironment.Camera.FilterColor.Green/255;

        Sensitivity = new SliderButton("Sprites/spr_button_sliderback", "Sprites/spr_button_sliderfront");
        Add(Sensitivity);
        Sensitivity.Position = new Vector2(800, 480);
        Sensitivity.Value = (float)GameEnvironment.Camera.FilterSensitivity/255;

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

        if(GameEnvironment.Camera.frame != null)
        {
            Red.Position = new Vector2(200 + GameEnvironment.Camera.frame.Width, 300);
            Blue.Position = new Vector2(200 + GameEnvironment.Camera.frame.Width, 420);
            Green.Position = new Vector2(200 + GameEnvironment.Camera.frame.Width, 360);
            Sensitivity.Position = new Vector2(200 + GameEnvironment.Camera.frame.Width, 480);
            Volume.Position = new Vector2(200 + GameEnvironment.Camera.frame.Width, 600);

            cameraSelectionButton.Position = new Vector2(10, 20 + GameEnvironment.Camera.frame.Height);
            colorFilterToggle.Position = new Vector2(260, 20 + GameEnvironment.Camera.frame.Height);

        }


        //Changes the camera when a different one is selected
        if (selectedCamera != cameraSelectionButton.selectedOption)
        {
            selectedCamera = cameraSelectionButton.selectedOption;

            GameEnvironment.Camera.SwitchWebcam(selectedCamera);
        }

        RedText = ("Red: " + ((int)(Red.Value * 255)).ToString());
        GameEnvironment.Camera.ChangefilterRed((int)(Red.Value * 255));

        BlueText = ("Blue: " + ((int)(Blue.Value * 255)).ToString());
        GameEnvironment.Camera.ChangefilterBlue((int)(Blue.Value * 255));

        GreenText = ("Green: " + ((int)(Green.Value * 255)).ToString());
        GameEnvironment.Camera.ChangefilterGreen((int)(Green.Value * 255));

        SensitivityText = ("Sensitivity: " + ((int)(Sensitivity.Value * 255)).ToString());
        GameEnvironment.Camera.ChangefilterSensitivity((int)(Sensitivity.Value * 255));

        VolumeText = ("Volume: " + ((int)(Volume.Value * 100)).ToString());
        MediaPlayer.Volume = Volume.Value / 10;

        if (colorFilterToggle.ToggledOn)
            GameEnvironment.Camera.SwitchCameraMode(Camera.CameraStatus.Tracking);
        else GameEnvironment.Camera.SwitchCameraMode(Camera.CameraStatus.Active);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        GameEnvironment.Camera.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(TrackedColor, new Vector2(900, 230), new Color(Red.Value, Green.Value, Blue.Value));
        spriteBatch.DrawString(spriteFont, RedText, Red.Position - new Vector2(83, -8), Color.White);
        spriteBatch.DrawString(spriteFont, BlueText, Blue.Position - new Vector2(85, -8), Color.White);
        spriteBatch.DrawString(spriteFont, GreenText, Green.Position - new Vector2(100, -8), Color.White);
        spriteBatch.DrawString(spriteFont, SensitivityText, Sensitivity.Position - new Vector2(130, -8), Color.White);
        spriteBatch.DrawString(spriteFont, VolumeText, Volume.Position - new Vector2(112, -8), Color.White);
        spriteBatch.Draw(TrackedObject, new Rectangle(GameEnvironment.Camera.BlobRectangle.X + (int)GameEnvironment.Camera.Position.X,
                                                      GameEnvironment.Camera.BlobRectangle.Y + (int)GameEnvironment.Camera.Position.Y,
                                                      GameEnvironment.Camera.BlobRectangle.Width, GameEnvironment.Camera.BlobRectangle.Height),
                                                      Color.White);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.Camera.SwitchCameraMode(Camera.CameraStatus.Tracking);
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }
}

   

