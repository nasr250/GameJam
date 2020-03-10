using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

partial class Level : GameObjectList
{
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Back))
        {
            quitButton.Pressed = true;
        }
        if (quitButton.Pressed)
        {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }

    public override void Update(GameTime gameTime)
    {

        if (player != null && !GameOver) //handles scoring
        {
            timeText.Text = "Time: " + levelTime.Minutes + ":" + levelTime.Seconds + "." + levelTime.Milliseconds;
            killsText.Text = "Enemies killed: " + killsText.health;
            score = (int)levelTime.TotalMilliseconds + killsText.health * 25000;
        }

        scoreText.Text = "Score: " + score; 
        if (player != null) //handles power up message 
        {
            if (Player.powerUpState > 0)
            {
                powerUpState.Visible = true;
                powerUpTimer.Visible = true;
                switch (Player.powerUpState)
                {
                    case 1:
                        powerUpState.Text = "Power-Up: MultiShot";
                        break;
                    case 2:
                        powerUpState.Text = "Power-Up: Spread Shots";
                        break;
                    case 3:
                        powerUpState.Text = "Power-Up: Curved Shots";
                        break;
                    case 4:
                        powerUpState.Text = "Power-Up: Beam";
                        break;
                }
                powerUpTimer.Text = "Duration: " + (int)(30 - player.powerUpTimer);
            }
            else
            {
                powerUpState.Visible = false;
                powerUpTimer.Visible = false;
            }
        }

        levelTime = levelTime.Add(gameTime.ElapsedGameTime);
        Timer += gameTime.ElapsedGameTime.Milliseconds;
        player = GameWorld.Find("player") as Player;
        if (player == null && !GameOver)
        {
            GameOver = true;
            GameOverState.Score = score; //gives the game-over screen the score value
        }

        if (upgradeButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("upgradeMenuState");
        }
        base.Update(gameTime);

    }

    public override void Reset()
    {
        base.Reset();
        bullets.Reset();
        enemies.Reset();
        Timer = 0;
        killsText.health = 0;
        levelTime = new System.TimeSpan();
        if (player != null)
        {
            player.Reset();

        }
        else
        {
            player = new Player(100);
            Add(player);

        }
        GameOver = false;
        LoadLevel(levelIndex);
    }
}
