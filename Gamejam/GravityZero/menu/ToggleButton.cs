using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class ToggleButton : GameObjectList
{
    Button buttonOn, buttonOff;
    public bool ToggledOn;
    public ToggleButton(string imageassetOn, string imageassetOff, int layer = 0, string id = "") : base(layer, id)
    {
        buttonOn = new Button(imageassetOn);
        buttonOff = new Button(imageassetOff);
        Add(buttonOff);
        ToggledOn = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (buttonOff.Pressed)
        {
            ToggledOn = true;
            buttonOff.Reset();
            Remove(buttonOff);
            Add(buttonOn);
        }
        if (buttonOn.Pressed)
        {
            ToggledOn = false;
            buttonOn.Reset();
            Remove(buttonOn);
            Add(buttonOff);
        }
    }




}
