using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AForge.Video;
using AForge;
using AForge.Video.DirectShow;

namespace CameraTests
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Camera camera;
        Texture2D poggers;
        Vector2 center;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 360;
        }


        protected override void Initialize()
        {


            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera(GraphicsDevice);
            camera.Initialize();
            font = Content.Load<SpriteFont>("SpelFont");
            poggers = Content.Load<Texture2D>("Pogger");
            center = new Vector2(poggers.Width / 2 - 640, poggers.Height / 2);

            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                camera.EndVideoFeed();
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                camera.cameraStatus = Camera.CameraStatus.Tracking;
            camera.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cyan);
            spriteBatch.Begin();
            camera.Draw(gameTime, spriteBatch);
            if (camera.cameraStatus == Camera.CameraStatus.Tracking && camera.TrackedObject != null)
            {
                
                spriteBatch.Draw(poggers, camera.TrackedObject - center, Color.White);

            }
            spriteBatch.End();

        }
    }
}
