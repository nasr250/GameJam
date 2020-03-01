using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class UpgradeMenustate : GameObjectList
{
    protected Button backButton;
    protected BuyButton buyButton;
    protected TextGameObject scoreText;
    protected int score = 100;

    public UpgradeMenustate()
    {
        SpriteGameObject upgradeMenu = new SpriteGameObject("UpgradeMenu", 0);
        upgradeMenu.Position = new Vector2(150, 150);
        Add(upgradeMenu);

        buyButton = new BuyButton("BuyMenu@3",2);
        buyButton.Position = new Vector2(1500,500);
        Add(buyButton);

        scoreText = new TextGameObject("Sprites/SpelFont", 1, "scoreText");
        scoreText.Position = new Vector2(400, 400);
        Add(scoreText);

        backButton = new Button("Sprites/spr_button_play", 2);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 540);
        Add(backButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

    }
   public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        scoreText.Text = "Score " + score;
        if (score == 99) {
            buyButton.hoover = true;
        }
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
    }
}
