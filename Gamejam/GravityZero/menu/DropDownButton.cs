using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class DropDownButton : GameObjectList
{
    Button button = new Button("Sprites/spr_button_dropdownmenu", 1);
    List<Button> options = new List<Button>();
    List<string> optionNames;
    bool DropDownActive = false;
    SpriteFont spriteFont;
    public int selectedOption { get; private set; }

    public DropDownButton(int NumberOfOptions, List<string> optionNames, int layer = 0, string id = "") : base(layer, id)
    {
        selectedOption = 0;
        Add(button);
        for (int i = 0; i < NumberOfOptions; i++)
            options.Add(new Button("Sprites/spr_button_dropdownmenu2", 1));
        this.optionNames = optionNames;
        spriteFont = GameEnvironment.AssetManager.GetSpriteFont("Sprites/SpelFont");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);


        if (button.Pressed)
        {
            DropDownActive = true;
            foreach (Button b in options)
                Add(b);

        }
        if (DropDownActive)
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Position = new Vector2(0, button.BoundingBox.Height * (i + 1));
                options[i].Update(gameTime);
                if (options[i].Pressed)
                {
                    selectedOption = i;
                    DropDownActive = false;
                    button.Reset();
                    foreach (Button b in options)
                    {
                        b.Reset();
                        Remove(b);
                    }
                }
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        if (DropDownActive)
        {
            for(int i = 0; i < options.Count; i++)
            {
                options[i].Draw(gameTime, spriteBatch);
                if (selectedOption == i)
                    spriteBatch.DrawString(spriteFont, optionNames[i], new Vector2(Position.X + 20, options[i].BoundingBox.Center.Y - 20), Color.Red);
                else spriteBatch.DrawString(spriteFont, optionNames[i], new Vector2(Position.X + 20, options[i].BoundingBox.Center.Y - 20), Color.White);
            }
        }
    }
}



