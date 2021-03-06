﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera : GameObject
{
    Vector2 centre;

    public Camera()
    {
        id = "camera";
        Reset();
    }



    public override void Update(GameTime gameTime)
    {
        Player player = GameWorld.Find("player") as Player;
        if (player != null)
        {
            centre = new Vector2(GameEnvironment.Screen.X / 2 - player.Width / 2, GameEnvironment.Screen.Y / 2 - player.Height / 2);
            if (true)
            {
                position.X = centre.X - player.Position.X;
            }
            if (true)
            {
                position.Y = centre.Y - player.Position.Y;
            }
        }
    }
    public override void Reset()
    {
        position = Vector2.Zero;
    }
}