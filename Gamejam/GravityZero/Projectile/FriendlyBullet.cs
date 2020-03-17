using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class FriendlyBullet : Bullet
{

    public FriendlyBullet(Vector2 startpos, double dir = 1, float spd = 1, int size = 1, double curv = 0, int damage = 1) : base(startpos, dir, spd, curv, damage)
    {

    }
    protected override void CheckCollision() //this one is empty because the collision is checked by the enemies
    { }



}

