using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

partial class Level : GameObjectList
{

    protected Button quitButton, upgradeButton;
    public bool Completed;
    public bool GameOver;
    public int Timer;
    public static int score;
    public int levelIndex = 0;
    GameObjectList enemies, friendlyBullets, bullets;
    Player player;
    Rectangle Screenbox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
    TextGameObject timeText, killsText, scoreText, powerUpState, powerUpTimer;
    TimeSpan levelTime;
    public Level(int levelIndex) //this method loads a level, this is the place to put all the code that needs to be shared across all levels
    {
        SpriteGameObject background = new SpriteGameObject("Backgrounds/background", 0, "background"); //source: https://rafaeldejongh.artstation.com/projects/1gX22
        Add(background);

        SpriteGameObject chargeBar = new SpriteGameObject("Sprites/BarBorder", 100);
        chargeBar.Position = new Vector2(80, 550);
        chargeBar.Sprite.drawColor = Color.Gray;
        Add(chargeBar);
        TextGameObject chargeBarName = new TextGameObject("Sprites/SpelFont", 100);
        chargeBarName.Text = "Charge Power";
        chargeBarName.Position = new Vector2(70, 600);
        Add(chargeBarName);

        SpriteGameObject healthBar = new SpriteGameObject("Sprites/BarBorder", 100);
        healthBar.Position = new Vector2(80, 20);
        Add(healthBar);
        TextGameObject healthBarName = new TextGameObject("Sprites/SpelFont", 100);
        healthBarName.Text = "Health";
        healthBarName.Position = new Vector2(70, 70);
        Add(healthBarName);


        upgradeButton = new Button("Sprites/spr_button_quit", 100);
        upgradeButton.Position = new Vector2(1500, 400);
        Add(upgradeButton);

        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        Add(quitButton);

        timeText = new TextGameObject("Sprites/SpelFont", 100);
        timeText.Text = "Time: 0:0";
        timeText.Position = new Vector2(1500, 100);
        Add(timeText);

        killsText = new TextGameObject("Sprites/SpelFont", 100, "killsText");
        killsText.Position = new Vector2(1500, 120);
        killsText.health = 0;
        killsText.Text = "Enemies killed: " + killsText.health;
        Add(killsText);

        scoreText = new TextGameObject("Sprites/SpelFont", 100, "scoreText");
        scoreText.Position = new Vector2(1500, 140);
        Add(scoreText);

        powerUpState = new TextGameObject("Sprites/SpelFont", 100, "powerUpStateText");
        powerUpState.Position = new Vector2(1500, 180);
        powerUpState.Visible = false;
        Add(powerUpState);

        powerUpTimer = new TextGameObject("Sprites/SpelFont", 100, "powerUpTimerText");
        powerUpTimer.Position = new Vector2(1500, 200);
        powerUpTimer.Visible = false;
        Add(powerUpTimer);

        levelTime = new TimeSpan();

        player = new Player(100);
        player.Position = new Vector2(500, 500);
        enemies = new GameObjectList(1, "enemies");
        Add(enemies);
        friendlyBullets = new GameObjectList(1, "friendlyBullets");
        Add(friendlyBullets);
        bullets = new GameObjectList(1, "bullets");
        Add(bullets);
        Add(player);
        LoadLevel(levelIndex); //this method loads a specific level, and contains the enemies and projectiles of that level.
    }
}


