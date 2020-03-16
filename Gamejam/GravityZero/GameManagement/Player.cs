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
    public static float speed = 0f;
    InputHelper inputHelper = new InputHelper();
    public static int powerUpState;
    public static float Health;
    public static bool isDead, upgrade1, upgrade2, upgradeReset;
    public double powerUpTimer { get; private set; }
    public int mass = 10;
    GameObjectList friendlyBullets;
    Vector2 ShootPosition;
    Shotbar bar;
    public static int ironCount, carbonCount;

    public Player(int layer = 0, string id = "") : base(layer, id)
    {
        LoadAnimation("Sprites/player1@4x1", "player", true);
        PlayAnimation("player");
        position = new Vector2(0, 0);
        bar = new Shotbar("Sprites/BarFilling");
        Health = 190;
        powerUpTimer = 0;
        powerUpState = 0;
        isDead = false;

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (upgrade1)
        {
            LoadAnimation("Sprites/player2@4x1", "player", true);
            PlayAnimation("player");
            upgrade1 = false;
        }
        if (upgrade2)
        {
            LoadAnimation("Sprites/player3@4x1", "player", true);
            PlayAnimation("player");
            upgrade2 = false;
        }
        if (upgradeReset)
        {
            LoadAnimation("Sprites/player1@4x1", "player", true);
            PlayAnimation("player");
            upgradeReset = false;
        }

        if (Health < 0)
        {
            isDead = true;
            upgradeReset = true;
            GameEnvironment.GameStateManager.SwitchTo("GameOverState");
        }
        bar.Update(gameTime);
        bar.health = Health;
        ShootPosition.X = position.X;
        ShootPosition.Y = position.Y;
        HandleInput();
        powerUpTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (bar.size <= 0)
        {
            Health -= 0.5f;
        }



    }

    public void HandleInput()
    {
        inputHelper.Update(); //commented lines are for debugging purposes
        Camera camera = GameWorld.Find("camera") as Camera;
        double x = inputHelper.MousePosition.X - GameEnvironment.Screen.X / 3 + Width / 2;
        double y = inputHelper.MousePosition.Y - GameEnvironment.Screen.Y / 3 + Height;
        double z = Math.Atan2(y, x) + 0.5 * Math.PI;
        string tempstring = z.ToString("0.0000");
        sprite.spriteRotation = float.Parse(tempstring);


        if (inputHelper.IsKeyDown(Keys.Space))
        {
            velocity = new Vector2((Velocity.X + (float)x + speed) / 1.5f, (Velocity.Y + (float)y + speed) / 1.5f);
        }

        if (inputHelper.MouseLeftButtonPressed())
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
            if (bar.size > 0)
                bar.size -= 500;
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
        double dir = sprite.spriteRotation - 0.5 * Math.PI;
        dir *= Math.PI / 180;
        friendlyBullets.Add(new FriendlyBullet(ShootPosition, dir, 5, 2, 0, (int)bar.size / 1000));
    }

    void Multishot() // Multi-Shot
    {
        for (int w = 0; w < bar.size / 1000; w += 2)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, 5, 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, 5, 2, 0, (int)bar.size / 1000));
        }
    }

    void Bigwave() // Default + Multi-Shot (PowerUp)
    {
        for (int w = 0; w < bar.size / 1000; w++)
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
        for (int w = 0; w < bar.size / 1000; w++)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, 5, 2, 1, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, 5, 2, -1, (int)bar.size / 1000));
        }
    }

    void Beam() // Beam of Bullets (experimental)
    {
        for (int w = 0; w < bar.size / 1000; w++)
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

    public void updateFuel(int fuelChange)
    {
        bar.size += fuelChange;
    }

    public override void Reset()
    {
        base.Reset();
        ironCount = 0;
        carbonCount = 0;
        powerUpState = 0;
        speed = 10f;
        Position = new Vector2(0, 0);
        Health = 190;
        bar.Reset();
    }

}