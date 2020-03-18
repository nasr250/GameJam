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
    Vector2 direction = new Vector2(0, 0);
    public static float speed = 1f;
    InputHelper inputHelper = new InputHelper();
    public static int powerUpState, shootTimer, shootCooldown, reducedFuelCost, ironCount, carbonCount;
    public static float Health;
    public static bool isDead, upgrade1, upgrade2;
    public bool hasExploded, isDead2, upgradeReset;
    public double powerUpTimer { get; private set; }
    public int mass = 10;
    GameObjectList friendlyBullets;
    Vector2 ShootPosition;
    public Shotbar bar;
    int counter;
    int maxspeed = 800;

    public Player(int layer = 0, string id = "") : base(layer, id)
    {
        LoadAnimation("Sprites/player1@4x1", "player", true);
        PlayAnimation("player");
        position = new Vector2(0, 0);
        bar = new Shotbar("Sprites/BarFilling");
        shootCooldown = 50;
        Health = 190;
        powerUpTimer = 0;
        powerUpState = 0;
        isDead = false;
        hasExploded = false;
        isDead2 = true;
    }

    public override void Update(GameTime gameTime)
    {
        
        if (Velocity.X > maxspeed)
        {
            velocity.X = maxspeed;
        }
        if (Velocity.Y > maxspeed)
        {
            velocity.Y = maxspeed;
        }
        if (Velocity.X < -maxspeed)
        {
            velocity.X = -maxspeed;
        }
        if (Velocity.Y < -maxspeed)
        {
            velocity.Y = -maxspeed;
        }
        if (position.X > 10000)
        {
            position.X = 10000;
        }
        else if (position.X < -10000)
        {
            position.X = -10000;
        }
        if (position.Y > 10000)
        {
            position.Y = 10000;
        }
        else if (position.Y < -10000)
        {
            position.Y = -10000;
        }
        base.Update(gameTime);
        //Upgrades the ship when all the requirements are reached in the upgrade menu.
        if (upgrade1)
        {
            LoadAnimation("Sprites/player2@4x1", "player", true);
            PlayAnimation("player");
            upgrade1 = false;
        }
        //Upgrades the ship for a second time when all the requirements are reached in the upgrade menu.
        if (upgrade2)
        {
            LoadAnimation("Sprites/player3@4x1", "player", true);
            PlayAnimation("player");
            upgrade2 = false;
        }
        //When the player dies the ship will reset.
        if (upgradeReset)
        {
            LoadAnimation("Sprites/player1@4x1", "player", true);
            PlayAnimation("player");
            upgradeReset = false;
        }
/*      When the ship is exploding it will start a timer. Right before the timer reaches 60 it will reset the spaceship to its original form.
        When the timer reaches 60 it will switch to the GameOverState.*/
        if (hasExploded)
        {
            counter++;
            isDead = true;
            if (counter == 58)
            {
                upgradeReset = true;
            }
            if (counter == 59) {
                upgradeReset = false; ;
            }
            if (counter == 60)
            {
                counter = 0;
                GameEnvironment.GameStateManager.SwitchTo("GameOverState");
                hasExploded = false;
            }
        }
        //If health goes below 0 the ship will explode.
        if (Health <= 0 && isDead2)
        {
            LoadAnimation("Sprites/explosie@12x1", "player", true);
            PlayAnimation("player");
            hasExploded = true;
            isDead2 = false;
        }

        //ShootTimer
        shootTimer++;

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

    float VectorToAngle(Vector2 vector)
    {
        return (float)Math.Atan2(vector.Y, vector.X);
    }

    public void HandleInput()
    {
        inputHelper.Update(); //commented lines are for debugging purposes
        Camera camera = GameWorld.Find("camera") as Camera;
        double x = inputHelper.MousePosition.X - GameEnvironment.Screen.X / 2 + Width / 2;
        double y = inputHelper.MousePosition.Y - GameEnvironment.Screen.Y / 2 + Height / 2;
        double z = Math.Atan2(y, x) + 0.5 * Math.PI;
        string tempstring = z.ToString("0.0000");
        sprite.spriteRotation = float.Parse(tempstring);

        if (inputHelper.IsKeyDown(Keys.Space))
        {
            //reduce fuel:
            bar.size -= 20;
            //double angle = 0.5 * Math.PI;
            double angle = Math.Atan2(y, x);
            double x2 = Math.Cos(angle);
            double y2 = Math.Sin(angle);
            string temp2x = x2.ToString("0.0000");
            string temp2y = y2.ToString("0.0000");

            direction = new Vector2(float.Parse(temp2x), float.Parse(temp2y));
            velocity.X = Velocity.X + direction.X * 22 * speed;
            velocity.Y = Velocity.Y + direction.Y * 22 * speed;
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
            if (bar.size > 0 && shootTimer == 0)
                bar.size -= (500 - reducedFuelCost);
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
        if (shootTimer > shootCooldown)
        {
            double dir = sprite.spriteRotation - 0.5 * Math.PI;
            dir *= Math.PI / 180;
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, dir, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            shootTimer = 0;
        }
    }
    float GetBulletSpeed()
    {
        double value =  3 + 0.03f * Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);
        string temp = value.ToString("0.0000");
        return float.Parse(temp);
    }
    void Multishot() // Multi-Shot
    {
        for (int w = 0; w < bar.size / 1000; w += 2)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
        }
    }

    void Bigwave() // Default + Multi-Shot (PowerUp)
    {
        for (int w = 0; w < bar.size / 1000; w++)
        {
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 240 + w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 240 - w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 + w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 270 - w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 300 + w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
            friendlyBullets.Add(new FriendlyBullet(ShootPosition, 300 - w, GetBulletSpeed(), 2, 0, (int)bar.size / 1000));
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

    public void updateFuel
        (int fuelChange)
    {
        bar.size += fuelChange;
    }

    public override void Reset()
    {
        base.Reset();
        shootCooldown = 50;
        isDead2 = true;
        ironCount = 0;
        carbonCount = 0;
        powerUpState = 0;
        velocity = Vector2.Zero;
        speed = 1f;
        Position = new Vector2(0, 0);
        Health = 190;
        bar.Reset();
        counter = 0;
        velocity = new Vector2(0, 0);
    }

}