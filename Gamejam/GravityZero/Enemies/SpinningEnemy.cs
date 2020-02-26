using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;


public class SpinningEnemy : Enemy
{
    float shootAngle = 0;

    public SpinningEnemy()
    {
        velocity.X = 120;
    }

    public override void Update(GameTime gameTime)
    {
        shootAngle += (float) gameTime.ElapsedGameTime.TotalSeconds; //changes angle continually
        ChangeRotation(shootAngle);
        base.Update(gameTime);
    }
    public override void Shoot()
    {
        GameObjectList bullets = GameWorld.Find("bullets") as GameObjectList;
        Bullet bullet = new Bullet(position, spd: 2, curv: 2, dir: (int)( 1.2* shootAngle * 180/Math.PI));
        bullet.Velocity *= 5;
        bullets.Add(bullet);
    }

}