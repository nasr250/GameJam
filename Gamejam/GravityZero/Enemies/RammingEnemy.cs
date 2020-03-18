using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

class RammingEnemy : Enemy
{
    Vector2 speed, direction;
    Vector2 movement = Vector2.Zero;

    public RammingEnemy(Vector2 StartPos)
	{
        Velocity /= 2;
        health = 1;
        position = StartPos;
	}

    public override void Update(GameTime gameTime)
    {
        GameObjectList gameWorld = Root as GameObjectList;
        Player player = gameWorld.Find("player") as Player;
        direction = player.Position - position; //calculates the vector the enemy needs to travel
        float distance = direction.Length();
        direction.Normalize(); //calculates the normalized vector
        movement = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (player.Position != position)
        {
            movement += 5 * direction; //makes the movement of the enemy more towards the direction between the enemy and the endpoint
            position += movement;
        }
        if (CollidesWith(player))
        {
            Player.Health -= 10;
            health--;
        }
        GameObjectList friendlyBullets = GameWorld.Find("friendlyBullets") as GameObjectList;
        for (int i = friendlyBullets.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject friendlyBullet = friendlyBullets.children[i] as SpriteGameObject;
            if (CollidesWith(friendlyBullet))
            {
                friendlyBullets.children[i].health--;
                this.health--;
                if (this.health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }
            }
        }
    }


}
