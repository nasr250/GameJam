using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

    public class Shotbar : SpriteGameObject
    {
        public double size = 0;
        InputHelper inputHelper = new InputHelper();
        GameObjectList friendlyBullets;

        public Shotbar(string assetName, int layer = 0, string id = "") : base (assetName, layer, id)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (size < 24000)
            {
                size += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

        

            if (inputHelper.KeyPressed(Keys.Z) && size >= 1)
            {
                friendlyBullets = GameWorld.Find("friendlyBullets") as GameObjectList;
                size = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(sprite.Sprite, new Vector2(80, 550 ), new Rectangle(0, 0, 300, (int)size/48), color); // draw for shotbar.
            spriteBatch.Draw(sprite.Sprite, new Vector2(80, 20), new Rectangle(0, 0, 300, (health + 1) * 25), Color.Red); // draw for healthbar.
        }
    public override void Reset()
    {
        base.Reset();
        size = 0;
    }
}

