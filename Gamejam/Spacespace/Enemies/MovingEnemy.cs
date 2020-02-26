﻿using System;
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


    public MovingEnemy(Vector2 EndPos):base("Sprites/enemy1@2x1")
    {
        endPos = EndPos;
        speed = new Vector2(-50, -100);
    }

    public override void Update(GameTime gameTime)
    {
        direction = endPos - position; //calculates the vector the enemy needs to travel
        float distance = direction.Length();
        direction.Normalize(); //calculates the normalized
        movement = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (endPos != position)
        {
            movement += 5 * direction; //makes the movement of the enemy more towards the direction between the enemy and the endpoint
            position += movement;
        }
        base.Update(gameTime);
    }
}