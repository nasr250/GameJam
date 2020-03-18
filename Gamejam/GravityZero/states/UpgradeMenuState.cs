using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class UpgradeMenustate : GameObjectList
{
    protected Button backButton;
    protected BuyButton buyButton1, buyButton2, buyButton3;
    protected TextGameObject ironText, carbonText, itemText1, itemText2, itemText3, upgradeText;
    protected int price1, price2, price3, itemLevel1, itemLevel2, itemLevel3, upgrade;
    protected UpgradePreview preview;


    public UpgradeMenustate()
    {

        //item prices
        price1 = 50;
        price2 = 50;
        price3 = 50;
        //item level
        itemLevel1 = 1;
        itemLevel2 = 1;
        itemLevel3 = 1;
        //upgrade level
        upgrade = 2;

        SpriteGameObject upgradeMenu = new SpriteGameObject("Backgrounds/UpgradeMenu", 0);
        upgradeMenu.Position = new Vector2(0, 0);
        Add(upgradeMenu);

        backButton = new Button("Sprites/spr_button_back", 2);
        backButton.Position = new Vector2(1500, 150);
        Add(backButton);

        ironText = new TextGameObject("Sprites/SpelFont", 1, "ironText");
        ironText.Position = new Vector2(380, 400);
        Add(ironText);

        carbonText = new TextGameObject("Sprites/SpelFont", 1, "carbonText");
        carbonText.Position = new Vector2(500, 400);
        Add(carbonText);

        itemText1 = new TextGameObject("Sprites/SpelFont", 1, "itemText1");
        itemText1.Position = new Vector2(845, 760);
        Add(itemText1);

        itemText2 = new TextGameObject("Sprites/SpelFont", 1, "itemText2");
        itemText2.Position = new Vector2(1200, 760);
        Add(itemText2);

        itemText3 = new TextGameObject("Sprites/SpelFont", 1, "itemText3");
        itemText3.Position = new Vector2(1550, 760);
        Add(itemText3);

        upgradeText = new TextGameObject("Sprites/SpelFont", 1, "upgradetext");
        upgradeText.Position = new Vector2(240, 350);
        Add(upgradeText);

        buyButton1 = new BuyButton("Sprites/BuyButton@3x1", 2);
        buyButton1.Position = new Vector2(860, 800);
        Add(buyButton1);

        buyButton2 = new BuyButton("Sprites/BuyButton@3x1", 2);
        buyButton2.Position = new Vector2(1250, 800);
        Add(buyButton2);

        buyButton3 = new BuyButton("Sprites/BuyButton@3x1", 2);
        buyButton3.Position = new Vector2(1640, 800);
        Add(buyButton3);

        preview= new UpgradePreview("Sprites/ShipPreview@3x1", 2);
        preview.Position = new Vector2(265, 385);
        Add(preview);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

    }
    //ironCount, carbonCount
    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        ironText.Text = "Iron: " + Player.ironCount;
        carbonText.Text = "Carbon: " + Player.carbonCount;
        itemText1.Text = "Level " + itemLevel1 + " Gun (Iron): " + price1;
        itemText2.Text = "Level " + itemLevel2 + " Health (Iron): " + price2;
        itemText3.Text = "Level " + itemLevel3 + " Booster(Carbon): " + price3;
        upgradeText.Text = "Upgrade spaceship: Health Level " + upgrade + " & Gun Level " +  upgrade + " & Thruster Level " + upgrade;
        //Hoovers over the buy button and look if it is buyable.
        if (Player.ironCount >= price1)
        {
            buyButton1.hoover = true;
        }
        else
        {
            buyButton1.hoover = false;
        }
        
        if (Player.ironCount >= price2)
        {
            buyButton2.hoover = true;
        }
        else 
        {
            buyButton2.hoover = false;
        }

        if (Player.carbonCount >= price3)
        {
            buyButton3.hoover = true;
        }
        else 
        {
            buyButton3.hoover = false;
        }

        //When all items reach a certain level the ship will upgrade.
        if (itemLevel1 >= 2 && itemLevel2 >= 2 && itemLevel3 >= 2)
        {
            preview.preview1 = true;
            Player.upgrade1 = true;
            upgrade = 3;
        }
        else 
        {
            Player.upgrade1 = false;
            preview.preview2 = false;
            preview.preview1 = false;
        }
        if (itemLevel1 >= 3 && itemLevel2 >= 3 && itemLevel3 >= 3)
        {
            preview.preview2 = true;
            preview.preview1 = false;
            Player.upgrade1 = false;
            Player.upgrade2 = true;
        }
        else
        {
            Player.upgrade2 = false; 
        }


        //Looks if upgrade prices are the same and makes them hooverable if they are the same.
        if (price1 == price2 && Player.ironCount >= price1)
        {
            buyButton1.hoover = true;
            buyButton2.hoover = true;
        }

        //Buying upgrades
        //Shooting: By upgrading your gun you can shoot faster and it will cost less fuel.
        if (buyButton1.Pressed && buyButton1.hoover == true && Player.ironCount >= price1) {
            Player.isDead = false;
            Player.ironCount -= price1;
            price1 += price1;
            itemLevel1++;
            Player.shootCooldown -= 5;
            Player.reducedFuelCost += 50;
        }
        //Health: By upgrading health you will be able to take more damage before you die.
        if (buyButton2.Pressed && buyButton2.hoover == true && Player.ironCount >= price2)
        {
            Player.isDead = false;
            Player.ironCount -= price2;
            price2 += price2;
            itemLevel2++;
            Player.Health += 5;
        }
        //Speed: By upgrading speed you will be able to move faster.
        if (buyButton3.Pressed && buyButton3.hoover == true && Player.carbonCount >= price3)
        {
            Player.isDead = false;
            Player.carbonCount -= price3;
            price3 += price3;
            itemLevel3++;
            Player.speed += 2f;
        }
        //Shop reset
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
        upgrade = 2;
        itemLevel1 = 1;
        itemLevel2 = 1;
        itemLevel3 = 1;
        price1 = 50;
        price2 = 50;
        price3 = 50;
    }
}
