﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject
{
    protected SpriteSheet sprite;
    protected Vector2 origin;
    public bool PerPixelCollisionDetection = true;
    public bool isAlive = true;

    public SpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        if (assetName != "")
        {
            sprite = new SpriteSheet(assetName, sheetIndex);
        }
        else
        {
            sprite = null;
        }
    }    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null || !isAlive)
        {
            return;
        }
        else
        {
            Camera camera = GameWorld.Find("camera") as Camera;
            if (camera != null)
            {
                //background
                if (layer == -10)
                {
                    sprite.Draw(spriteBatch, this.GlobalPosition + camera.Position / 60, origin);
                }
                else if (layer == -9)
                {
                    sprite.Draw(spriteBatch, this.GlobalPosition + camera.Position / 50, origin);
                }
                else if (layer == -8)
                {
                    sprite.Draw(spriteBatch, this.GlobalPosition + camera.Position / 30, origin);
                }
                else if (layer == -7)
                {
                    sprite.Draw(spriteBatch, this.GlobalPosition + camera.Position / 2, origin);
                }
                else if (layer == 100)
                {
                    sprite.Draw(spriteBatch, this.GlobalPosition + camera.Position, origin);
                }
                else
                {
                    sprite.Draw(spriteBatch, this.GlobalPosition + camera.Position, origin);
                }
            }
            else
            {
                sprite.Draw(spriteBatch, this.GlobalPosition, origin);
            }
            //(texture, new Rectangle(400, 50, 100, 100), null, Color.Red, MathHelper.PiOver4, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
     
    public SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public virtual bool CollidesWith(SpriteGameObject obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
        {
            return false;
        }
        if (!PerPixelCollisionDetection)
        {
            return true;
        }
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
        {
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

