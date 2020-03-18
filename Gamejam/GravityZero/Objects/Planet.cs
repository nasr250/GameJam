using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Planet : SpriteGameObject
{
    public const int G = 10000;
    public static int mass = 50;
    Vector2 middle;
    public Planet(Vector2 pos, string sprite = "Sprites/planet_1") : base(sprite)
    {
        position = pos;
        middle = new Vector2(position.X + Width / 2, position.Y + Height / 2);
        health = 2;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (player != null)
        {
            if (CollidesWith(player))
            {
                Player.Health -= 10;
                player.Velocity /= 2;
            }
            else
            {
                Vector2 uv = player.Position - middle;
                Vector2 unit = uv / Vector2.Distance(middle, player.Position);
                player.Velocity += -G * mass * unit / Vector2.DistanceSquared(middle, player.Position);
            }
        }
        GameObjectList bullets = GameWorld.Find("bullets") as GameObjectList;
        for (int i = bullets.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject bullet = bullets.children[i] as SpriteGameObject;
            if (CollidesWith(bullet))
            {
                bullets.children[i].health--;
                /*if (bullets.children[i].health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }*/
            }
            else
            {
                Vector2 uv = bullet.Position - middle;
                Vector2 unit = uv / Vector2.Distance(middle, bullet.Position);
                bullet.Velocity += -G * mass * unit / Vector2.DistanceSquared(middle, bullet.Position);
            }

        }

        GameObjectList friendlyBullets = GameWorld.Find("friendlyBullets") as GameObjectList;
        for (int i = friendlyBullets.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject friendlyBullet = friendlyBullets.children[i] as SpriteGameObject;
            if (CollidesWith(friendlyBullet))
            {
                friendlyBullets.children[i].health--;
                //this.health--; //decreases planet health, could be used to destroy planets
                /*if (friendlyBullets.children[i].health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }*/
            }
            else
            {
                Vector2 uv = friendlyBullet.Position - middle;
                Vector2 unit = uv / Vector2.Distance(middle, friendlyBullet.Position);
                friendlyBullet.Velocity += -G * mass * unit / Vector2.DistanceSquared(middle, friendlyBullet.Position);
            }

        }

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        health = -1;
    }
}