public class BuyButton : SpriteGameObject
{
    public bool pressed, hoover, contains;

    public BuyButton(string imageAsset, int layer = 0, string id = "")
        : base(imageAsset, layer, id)
    {
        pressed = false;
        contains = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
        contains = BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
        if (contains && hoover)
        {
            Sprite.SheetIndex = 1;
        }
        else if (contains)
        {
            sprite.SheetIndex = 2;
        }
        else {
            sprite.SheetIndex = 0;
        }
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

    public bool Contains
    {
        get { return contains; }
        set { contains = value; }
    }
}
