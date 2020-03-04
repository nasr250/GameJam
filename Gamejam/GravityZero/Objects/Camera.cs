using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GravityZero.Objects
{
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
            centre = new Vector2(GameEnvironment.Screen.X / 2 - player.Width / 2, GameEnvironment.Screen.Y / 2 - player.Height / 2);
            if (player.Position.X > centre.X && player.Position.X < tiles.Columns * tiles.CellWidth - GameEnvironment.Screen.X / 2 - player.Width / 2)
            {
                position.X = centre.X - player.Position.X;
            }
            if (player.Position.Y < centre.Y - 100)
            {
                position.Y = centre.Y - 100 - player.Position.Y;
            }
        }
        public override void Reset()
        {
            position = Vector2.Zero;
        }
    }
}