using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;


public class PausingEnemy : Enemy
{
    Vector2 endPos = Vector2.Zero;
    Vector2 pausePos = new Vector2(GameEnvironment.windowSize.X, GameEnvironment.windowSize.Y);
    Vector2 direction, movement;
    double PauseTime;
    bool pause = false;

    public PausingEnemy(Vector2 StartPos, int pauseTime = 10) //pausetime is the time in seconds that a pause would last, for tougher enemies a larger time could be used
    {
        PauseTime = pauseTime;
        velocity.X = 120;
        position = StartPos;
        direction = endPos - position; //calculates the vector the enemy needs to travel
        float distance = direction.Length();
        direction.Normalize(); //calculates the normalized
        velocity = direction * 5;
    }

    public override void Update(GameTime gameTime)
    {
        if (pause)
        {
            PauseTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if(PauseTime <= 0)
            {
                pause = false;
            }
        }
        else
        {

            if (endPos != position)
            {
                movement += 5 * direction; //makes the movement of the enemy more towards the direction between the enemy and the endpoint
                position += movement;
            }

        }
        base.Update(gameTime);
    }

}