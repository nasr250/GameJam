using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

class BlackHole : Planet
{
    public BlackHole(Vector2 pos) : base(pos, "Sprites/black_hole")
    {
        mass = 150;
    }
}
