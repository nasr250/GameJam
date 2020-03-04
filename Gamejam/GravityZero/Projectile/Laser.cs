using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class Laser : Projectile
{
    int delay = 1000, duration, hitcd = 1000;
    double direction, dirChange;
    Vector2 posChange;
    bool started = false, thing = false;
    Texture2D ind;
    public Laser(Vector2 startpos, int dir, int dur, int twistperS, string movementperS)
    {
        position = startpos;
        while (direction >= 360) { direction -= 360; }
        direction = dir * Math.PI / 180;
        dirChange = twistperS * Math.PI / 180;
        duration = dur;
        LoadAnimation("Sprites/LaserInd@1x1", "start", true, 0.1f);
        LoadAnimation("Sprites/Laser@4x1", "default", true, 0.1f);
        LoadAnimation("Sprites/LaserEnd@13x1", "end", false, 0.02f);
        PlayAnimation("start");
        string movedir = movementperS.Substring(0, 1);
        string movevalue = movementperS.Substring(1);
        switch (movedir)
        {
            case "U":
                posChange = new Vector2(0, -float.Parse(movevalue));
                break;
            case "R":
                posChange = new Vector2(float.Parse(movevalue), 0);
                break;
            case "D":
                posChange = new Vector2(0, float.Parse(movevalue));
                break;
            case "L":
                posChange = new Vector2(-float.Parse(movevalue), 0);
                break;
        }
    }
    public override void Update(GameTime gametime)
    {
        base.Update(gametime);
        if (delay <= 0 && duration > 0)
        {
            hitcd += gametime.ElapsedGameTime.Milliseconds;
            duration -= gametime.ElapsedGameTime.Milliseconds;
            direction += dirChange * gametime.ElapsedGameTime.Milliseconds / 1000;
            position += posChange * gametime.ElapsedGameTime.Milliseconds / 1000;
            if (!started)
            {
                PlayAnimation("default");
                started = true;
            }
            Player player = GameWorld.Find("player") as Player;

            if(player != null)
            {
                if (CollidesWith(player.BoundingBox) && hitcd > 500)
                {
                    player.health--;
                    hitcd = 0;
                }
            }
        }
        else
        {
            delay -= gametime.ElapsedGameTime.Milliseconds;
        }
        if (duration <= 0)
        {
            PlayAnimation("end");
            if (animations["end"].AnimationEnded) { health = -1; }
        }
        sprite.spriteRotation = (float)(direction + (Math.PI / 2));
    }

    public bool CollidesWith(Rectangle obj)
    {
        double slope = Math.Tan(direction);

        double b = position.Y - slope * position.X;


        for(int i = obj.Left; i <= obj.Right; i++)
        {
            if(obj.Contains(i, (float)(slope * i + b)) || obj.Contains(i, (float)((slope * i + b) + sprite.Width)))
            {
                return true;
            }
            
            
        }
        
        return false;
    }
}
