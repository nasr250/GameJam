using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class Bullet : Projectile
{
    protected float speed, rotation;
    protected double curve;
    protected int time;
    Rectangle screen;

    public Bullet(Vector2 startpos, int dir = 1, float spd = 1, double curv = 0, int damage = 1)
    {
        direction = dir; speed = spd; position = startpos; curve = curv;
        while (direction >= 360) { direction -= 360; }
        direction *= Math.PI / 180;
        curve *= Math.PI / 180;
        velocity.Y = -5;
        health = 1;
        screen = new Rectangle(460,40,1000,1000);
        LoadAnimation("Sprites/laser@2x2", "default", true);
        PlayAnimation("default");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        time += gameTime.ElapsedGameTime.Milliseconds;
        sprite.spriteRotation = (float)direction + (float)(Math.PI / 2);
        position.Y += (float)Math.Sin(direction) * speed; position.X += (float)Math.Cos(direction) * speed;
        direction += curve / 100 * speed;
        hitbox.X = (int)position.X; hitbox.Y = (int)position.Y;
        CheckCollision();
        if(position.X < 460 - sprite.Width * 2 || position.X > 1460 + sprite.Width)
        {
            health = -1;
        }
        if (position.Y < 40 - sprite.Height * 2 || position.Y > 1040 + sprite.Height)
        {
            health = -1;
        }
    }

    protected virtual void CheckCollision()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            Player.Health--;
            health--;
        }
    }

    public bool CollidesWith(Rectangle player)
    {
        if ((hitbox.Left >= player.Left && hitbox.Left <= player.Right) || (hitbox.Right >= player.Left && hitbox.Right <= player.Right))
        {
            if ((hitbox.Top >= player.Top && hitbox.Top <= player.Bottom) || (hitbox.Bottom >= player.Top && hitbox.Bottom <= player.Bottom))
            {
                return true;
            }
        }

        if ((player.Left >= hitbox.Left && player.Left <= hitbox.Right) || (player.Right >= hitbox.Left && player.Right <= hitbox.Right))
        {
            if ((player.Top >= hitbox.Top && player.Top <= hitbox.Bottom) || (player.Bottom >= hitbox.Top && player.Bottom <= hitbox.Bottom))
            {
                return true;
            }
        }

        return false;
    }

}

