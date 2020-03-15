using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Planet : AnimatedGameObject
{
    public const int G = 10000;
    public const int mass = 100;
    public Planet(Vector2 pos, string sprite = "Sprites/planet@2x1") : base(id: "planet")
    {
        LoadAnimation(sprite, "default", true);
        PlayAnimation("default");
        position = pos;
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
                player.health -= 10;
                player.Velocity /= 2;
            }
            if (!CollidesWith(player))
            {
                Vector2 uv = player.Position - position;
                Vector2 unit = uv / Vector2.Distance(position, player.Position);
                player.Velocity += -G * mass * unit / Vector2.DistanceSquared(position, player.Position);
            }
        }
        GameObjectList bullets = GameWorld.Find("bullets") as GameObjectList;
        for (int i = bullets.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject bullet = bullets.children[i] as SpriteGameObject;
            if (CollidesWith(bullet))
            {
                bullets.children[i].health--;
                this.health--;
                if (bullets.children[i].health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }
            }
            if (!CollidesWith(bullet))
            {
                Vector2 uv = bullet.Position - position;
                Vector2 unit = uv / Vector2.Distance(position, bullet.Position);
                bullet.Velocity += -G * mass * unit / Vector2.DistanceSquared(position, bullet.Position);
            }

        }

        GameObjectList friendlyBullets = GameWorld.Find("friendlyBullets") as GameObjectList;
        for (int i = friendlyBullets.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject friendlyBullet = friendlyBullets.children[i] as SpriteGameObject;
            if (CollidesWith(friendlyBullet))
            {
                friendlyBullets.children[i].health--;
                this.health--;
                if (friendlyBullets.children[i].health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }
            }
            if (!CollidesWith(friendlyBullet))
            {
                Vector2 uv = friendlyBullet.Position - position;
                Vector2 unit = uv / Vector2.Distance(position, friendlyBullet.Position);
                friendlyBullet.Velocity += -G * mass * unit / Vector2.DistanceSquared(position, friendlyBullet.Position);
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