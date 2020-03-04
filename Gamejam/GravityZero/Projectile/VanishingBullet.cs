using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacespace.Projectile
{
    class VanishingBullet : Bullet
    {
        Color color = Color.White;
        bool white = true;
        float change, clrmod = 0;
        int timer, multiplier = 1;

        public VanishingBullet(Texture2D sprite, Vector2 startpos, int dir, float spd, int size, double curv, float clrchangespd) : base(sprite, startpos, dir, spd, size, curv)
        {
            change = clrchangespd * 1000;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            clrmod += 0.02f * multiplier;
            if(clrmod > 1) { clrmod = 1; }
            if(clrmod < 0) { clrmod = 0; }
            color = Color.Lerp(Color.White, Color.Transparent, clrmod);
            if(color == Color.White || color == Color.Transparent)
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;
                if(timer > change)
                {
                    timer = 0;
                    multiplier *= -1;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, hitbox, null, color, rotation, new Vector2(sprite.Width / 2, sprite.Height / 2), SpriteEffects.None, 1);
        }
    }
}
