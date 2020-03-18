using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

public class MovingEnemy : Enemy
{
    Vector2 endPos = new Vector2(-10, -10);
    Vector2 speed, direction;
    Vector2 movement = Vector2.Zero;


    public MovingEnemy(Vector2 StartPos):base("Sprites/enemy1@2x1")
    {
        position = StartPos;
        speed = new Vector2(-50, -100);
    }

    public override void Update(GameTime gameTime)
    {
        Player player = GameWorld.Find("player") as Player;
        if (player != null)
        {
            endPos = player.Position;
        }
        direction = endPos - position; //calculates the vector the enemy needs to travel
        float distance = direction.Length();
        direction.Normalize(); //calculates the normalized
        movement = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (distance >= 400 && distance <= 3000)
        {
            movement += 5 * direction; //makes the movement of the enemy more towards the direction between the enemy and the endpoint
            position += movement;
        }
        base.Update(gameTime);
    }
}