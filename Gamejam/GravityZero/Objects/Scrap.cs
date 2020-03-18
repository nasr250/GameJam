using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

public class Scrap : AnimatedGameObject
{
    int scrapType;

    public Scrap(Vector2 pos)
    {
        position = pos;
        List<string> materials = new List<string> { "iron", "carbon", "fuel" };
        scrapType = GameEnvironment.Random.Next(materials.Count + 2);
        if (scrapType > 2) { scrapType = 2; }
        string path = "Sprites/" + materials[scrapType] + "@1x1";
        LoadAnimation(path, "scrap", true);
        PlayAnimation("scrap");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            switch (scrapType)
            {
                case 0:
                    Player.ironCount += 10;
                    break;
                case 1:
                    Player.carbonCount += 10;
                    break;
                default:
                    player.updateFuel(5000);
                    break;

            }

            health = -1;
        }
    }
    public override void Reset()
    {
        health = -1;
    }
}