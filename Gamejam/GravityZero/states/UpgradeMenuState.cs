using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class UpgradeMenustate : GameObjectList
{
    protected Button backButton;
    protected BuyButton buyButton1, buyButton2, buyButton3;
    protected TextGameObject scoreText, itemText1, itemText2, itemText3;
    protected int price1, price2, price3, itemLevel1, itemLevel2, itemLevel3;

    public UpgradeMenustate()
    {
        //item prices
        price1 = 100;
        price2 = 150;
        price3 = 100;
        //item level
        itemLevel1 = 1;
        itemLevel2 = 1;
        itemLevel3 = 1;

        SpriteGameObject upgradeMenu = new SpriteGameObject("Backgrounds/UpgradeMenu", 0);
        upgradeMenu.Position = new Vector2(0, 0);
        Add(upgradeMenu);

        backButton = new Button("Sprites/spr_button_play", 2);
        backButton.Position = new Vector2(1500, 150);
        Add(backButton);

        scoreText = new TextGameObject("Sprites/SpelFont", 1, "scoreText");
        scoreText.Position = new Vector2(400, 400);
        Add(scoreText);

        itemText1 = new TextGameObject("Sprites/SpelFont", 1, "itemText1");
        itemText1.Position = new Vector2(845, 770);
        Add(itemText1);

        itemText2 = new TextGameObject("Sprites/SpelFont", 1, "itemText2");
        itemText2.Position = new Vector2(1225, 770);
        Add(itemText2);

        itemText3 = new TextGameObject("Sprites/SpelFont", 1, "itemText3");
        itemText3.Position = new Vector2(1600, 770);
        Add(itemText3);

        buyButton1 = new BuyButton("Sprites/BuyButton@3x1", 2);
        buyButton1.Position = new Vector2(860, 800);
        Add(buyButton1);

        buyButton2 = new BuyButton("Sprites/BuyButton@3x1", 2);
        buyButton2.Position = new Vector2(1250, 800);
        Add(buyButton2);

        buyButton3 = new BuyButton("Sprites/BuyButton@3x1", 2);
        buyButton3.Position = new Vector2(1640, 800);
        Add(buyButton3);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

    }
    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        scoreText.Text = "Score " + Level.score;
        itemText1.Text = "Level " + itemLevel1 + " Gun: " + price1;
        itemText2.Text = "Level " + itemLevel2 + " Health: " + price2;
        itemText3.Text = "Level " + itemLevel3 +" Booster: " + price3;

        //Hoovers over the buy button and look if it is buyable.
        if (Level.score >= price1)
        {
            buyButton1.hoover = true;
        }
        else
        {
            buyButton1.hoover = false;
        }
        
        if (Level.score >= price2)
        {
            buyButton2.hoover = true;
        }
        else 
        {
            buyButton2.hoover = false;
        }

        if (Level.score >= price3)
        {
            buyButton3.hoover = true;
        }
        else 
        {
            buyButton3.hoover = false;
        }


        //Looks if upgrade prices are the same and makes them hooverable if they are the same.
        if (price1 == price2 && Level.score >= price1)
        {
            buyButton1.hoover = true;
            buyButton2.hoover = true;
        }
        else if (price2 == price3 && Level.score >= price2)
        {
            buyButton2.hoover = true;
            buyButton3.hoover = true;
        }
        else if (price1 == price3 && Level.score >= price1)
        {
            buyButton1.hoover = true;
            buyButton3.hoover = true;
        }
        else if (price1 == price2 || price2 == price3 || price1 == price3 && Level.score >= price1)
        {
            buyButton1.hoover = true;
            buyButton2.hoover = true;
            buyButton3.hoover = true;
        }

        //Buying upgrades
        if (buyButton1.Pressed && buyButton1.hoover == true && Level.score >= price1) {
            Player.isDead = false;
            Level.score -= price1;
            price1 += price1;
            itemLevel1++;
            Player.powerUpState += 1;
        }

        if (buyButton2.Pressed && buyButton2.hoover == true && Level.score >= price2)
        {
            Player.isDead = false;
            Level.score -= price2;
            price2 += price2;
            itemLevel2++;
            Player.Health += 20;
        }

        if (buyButton3.Pressed && buyButton3.hoover == true && Level.score >= price3)
        {
            Player.isDead = false;
            Level.score  -= price3;
            price3 += price3;
            itemLevel3++;
            Player.speed += 2f;
        }
        //shop reset
        if (Player.isDead == true)
        {
            Reset();
        }

        //backbutton
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
    }
    public override void Reset()
    {
        itemLevel1 = 1;
        itemLevel2 = 1;
        itemLevel3 = 1;
        price1 = 100;
        price2 = 150;
        price3 = 100;
    }
}
