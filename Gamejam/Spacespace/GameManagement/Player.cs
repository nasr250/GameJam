using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Player : AnimatedGameObject
{
    float speed = 10f;
    InputHelper inputHelper = new InputHelper();
    public int powerUpState { get; private set; }
    public double powerUpTimer { get; private set; }
    GameObjectList friendlyBullets;
    Vector2 ShootPosition;
    Shotbar bar;

    public Player(int layer = 0, string id = "") : base(layer, id)
    {
        LoadAnimation("Sprites/player@5x2", "player", true);
        PlayAnimation("player");
        position = new Vector2(0, 0);
        bar = new Shotbar("Sprites/BarFilling");
        health = 19;
        powerUpTimer = 0;
        powerUpState = 0;
    }

    public override void Update(GameTime gameTime)
    {
        if (health < 0)
        {
            GameEnvironment.GameStateManager.SwitchTo("GameOverState");
        }

        base.Update(gameTime);
        bar.Update(gameTime);
        bar.health = health;
        ShootPosition.X = position.X;
        ShootPosition.Y = position.Y - sprite.Height;
        HandleInput();
        powerUpTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (powerUpTimer >= 30)
        {
            powerUpTimer = 0;
            powerUpState = 0;
        }

        if (position.X < 480)
        {
            position.X = 480;
        }        
        if (position.X > 1440)
        {
            position.X = 1440;
        }
        if (position.Y < sprite.Height + 20)
        {
            position.Y = sprite.Height + 20;
        }
        if(position.Y > 1040)
        {
            position.Y = 1040;
        }
    }

    public void HandleInput()
    {
        inputHelper.Update(); //commented lines are for debugging purposes
        
        if (inputHelper.IsKeyDown(Keys.Up) && position.Y > 0) // move up
        {
            velocity.Y -= speed;
        }

        if (inputHelper.IsKeyDown(Keys.Down) && position.Y < GameEnvironment.Screen.Y - sprite.Height) // move down
        {
            velocity.Y += speed;
        }

        if (inputHelper.IsKeyDown(Keys.Left) && position.X > 0) // move left
        {
            velocity.X -= speed;
        }

        if (inputHelper.IsKeyDown(Keys.Right) && position.X < GameEnvironment.Screen.X - sprite.Width) // move right
        {
            velocity.X += speed;
        }
        

        if (inputHelper.KeyPressed(Keys.Z))
        {
            friendlyBullets = GameWorld.Find("friendlyBullets") as GameObjectList;
            switch (powerUpState) //decides which shot to use based on current powerUp
            {
                case 0:
                    SingleShot();
                    break;
                case 1:
                    Multishot();
                    break;
                case 2:
                    Bigwave();
                    break;
                case 3:
                    Curved();
                    break;
                case 4:
                    Beam();
                    break;
            }
            bar.size = 0;
        }

        if (inputHelper.KeyPressed(Keys.U)) // debug: temp power up switch
        {
            if (powerUpState <= 3)
                powerUpState++;
            else powerUpState = 0;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        switch (powerUpState) // to indicate which of the different states for different power ups is active.
        {
            case 0:
                bar.Draw(gameTime, spriteBatch, Color.White); // Default Shot
                break;
            case 1:
                bar.Draw(gameTime, spriteBatch, Color.Green); // Multi-Shot
                break;
            case 2:
                bar.Draw(gameTime, spriteBatch, Color.Blue); // Default + Multi-Shot (PowerUp)
                break;
            case 3:
                bar.Draw(gameTime, spriteBatch, Color.Purple); // Curved shot (incomplete experiment)
                break;
            case 4:
                bar.Draw(gameTime, spriteBatch, Color.Orange); // Beam of Bullets (experimental)
                break;
        }
    }

    void SingleShot() // Default Shot
    {
        friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270, 5, 2, 0, (int)bar.size / 1000));
    }

    void Multishot() // Multi-Shot
    {
        for (int w = 0; w < bar.size/1000; w += 2)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, 5, 2, 0, (int)bar.size / 1000));
        }
    }

    void Bigwave() // Default + Multi-Shot (PowerUp)
    {
        for (int w = 0; w < bar.size/1000; w++)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 240 + w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 240 - w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 300 + w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 300 - w, 5, 2, 0, (int)bar.size / 1000));
        }
    }

    void Curved() // Curved shot (incomplete experiment)
    {
        for (int w = 0; w < bar.size/1000; w++)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, 5, 2, 1, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, 5, 2, -1, (int)bar.size / 1000));
        }
    }

    void Beam() // Beam of Bullets (experimental)
    {
        for (int w = 0; w < bar.size/1000; w++)
        {
            friendlyBullets.Add(new FriendlyBullet(new Vector2(ShootPosition.X, ShootPosition.Y - 4 * w), 270, 5, 2, 0, (int)bar.size / 1000));
        }
    }

    public void PowerUp()
    {
        Random random = GameEnvironment.Random;
        powerUpState = random.Next(1, 4);
        powerUpTimer = 0;
    }
    public override void Reset()
    {
        base.Reset();
        health = 19;
        bar.Reset();
    }

}