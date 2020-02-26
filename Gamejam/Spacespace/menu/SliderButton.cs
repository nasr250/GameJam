using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

class SliderButton : GameObjectList
{
    protected int leftmargin, rightmargin;
    protected SpriteGameObject back, front;
    protected bool dragging;
    public SliderButton(string sliderback, string sliderfront, int layer = 0, string id = "") : base(layer, id)
    {
        leftmargin = 0;
        rightmargin = 0;

        back = new SpriteGameObject(sliderback, 0);
        this.Add(back);

        front = new SpriteGameObject(sliderfront, 1);
        front.Position = new Vector2(leftmargin, -10);
        this.Add(front);

        dragging = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (!inputHelper.MouseLeftButtonDown())
        {
            dragging = false;
            return;
        }
        if (back.BoundingBox.Contains(inputHelper.MousePosition) || dragging)
        {
            float newXpos = MathHelper.Clamp(inputHelper.MousePosition.X - back.GlobalPosition.X - front.Width / 2, back.Position.X + leftmargin, back.Position.X + back.Width - front.Width - rightmargin);
            front.Position = new Vector2(newXpos, front.Position.Y);
            dragging = true;
        }
    }
    
    public float Value
    {
        get
        {
            return (front.Position.X - back.Position.X - leftmargin) / (back.Width - leftmargin - rightmargin - front.Width);
        }
        set
        {
            float newXpos = value * (back.Width - front.Width - leftmargin - rightmargin) + back.Position.X + leftmargin;
            front.Position = new Vector2(newXpos, front.Position.Y);
        }
    }
}

