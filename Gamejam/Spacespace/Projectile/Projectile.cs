using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Projectile : AnimatedGameObject
{
    protected double direction;
    protected Rectangle hitbox;

    public Projectile()
    {

    }     

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {

    }
    public override void Reset()
    {
        health = -1;
    }
}
