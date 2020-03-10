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