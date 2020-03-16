using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class UpgradePreview : SpriteGameObject
    {
        public UpgradePreview(string imageAsset, int layer = 0, string id = "")
        : base(imageAsset, layer, id) { }

        public bool preview1 = false, preview2 = false;
        public override void HandleInput(InputHelper inputHelper)
        {
            if (preview1 == true && preview2 == false)
            {
                Sprite.SheetIndex = 1;
            }
            else if (preview2)
            {
                sprite.SheetIndex = 2;
            }
            else
            {
                sprite.SheetIndex = 0;
            }
        }
    }

