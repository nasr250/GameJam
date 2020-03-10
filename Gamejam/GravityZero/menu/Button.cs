using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Button : SpriteGameObject
{
    protected bool pressed;

    public Button(string imageAsset, int layer = 0, string id = "")
        : base(imageAsset, layer, id)
    {
        pressed = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, this.GlobalPosition, origin);
        base.Update(gameTime);
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
        set { pressed = value; }
    }
}
