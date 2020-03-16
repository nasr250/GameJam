using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

class BlackHole : Planet
{
    static  Vector2 poss = new Vector2(2000, 10);
    public BlackHole() : base(poss, "Sprites/blackhole@1x1")
    {
        //mass = 1000;
    }
}
