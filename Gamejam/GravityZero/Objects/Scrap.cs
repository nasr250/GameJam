using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

public class Scrap : AnimatedGameObject
{
    int scrapType;
    public Scrap()
    {
        Random random = new Random();
        //List<string> materials = new List<string> { "iron", "carbon", "fuel" };
        //scrapType = random.Next(materials.Count);
        //string path = "Sprites/" + materials[scrapType] + "@1x1";
        string path = "Sprites/scrap@1x1";
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
                case 1:
                    Player.ironCount += 10;
                    break;
                case 2:
                    player.updateFuel(5000);
                    break;
/*                case 2:
                    Player.carbonCount += 10;
                    break;*/
/*                default:
                    player.updateFuel(5000);
                    break;*/
                default:
                    Player.carbonCount += 10;
                    break;
            }
        }
    }
}