using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class SpdChangeBullet : Bullet
{
    float acceleration;
    public SpdChangeBullet(Texture2D sprite, Vector2 startpos, int dir, float spd, int size, double curv, float acc) : base(startpos, dir, spd, size, curv)
    {
        acceleration = acc;
    }
    public override void Update(GameTime gameTime)
    {
        speed += acceleration;
        base.Update(gameTime);
    }
}