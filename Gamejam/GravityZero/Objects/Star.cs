using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

public class Star : SpriteGameObject
{

    public Star(Vector2 pos, string type) : base(type, -7)
    {
        position = pos;
    }
}