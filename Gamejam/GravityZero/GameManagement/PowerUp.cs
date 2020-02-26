using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class PowerUp : AnimatedGameObject
{
    public PowerUp()
    {
        LoadAnimation("Sprites/powerup@2x2", "powerup", true);
        PlayAnimation("powerup");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            player.PowerUp();
            health = -1;
        }
    }
}
