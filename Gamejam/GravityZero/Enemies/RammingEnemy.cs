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

    public RammingEnemy()
	{
        Velocity /= 2;
        health = 1;

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
    }


}
