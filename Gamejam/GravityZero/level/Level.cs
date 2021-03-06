﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

partial class Level : GameObjectList
{

    protected Button quitButton, upgradeButton;
    public bool Completed;
    public bool GameOver;
    public int Timer;
    public int score;
    public int levelIndex = 0;
    GameObjectList enemies, friendlyBullets, bullets;
    Player player;
    Rectangle Screenbox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
    TextGameObject timeText, killsText, scoreText, powerUpState, powerUpTimer, ironText, carbonText;
    TimeSpan levelTime;
    public Level(int levelIndex) //this method loads a level, this is the place to put all the code that needs to be shared across all levels
    {

        Add(new Camera());
        Camera camera = GameWorld.Find("camera") as Camera;

        SpriteGameObject background = new SpriteGameObject("Backgrounds/background", -10, "background"); //source: https://rafaeldejongh.artstation.com/projects/1gX22
        background.Position = new Vector2(-GameEnvironment.Screen.X / 2, -GameEnvironment.Screen.Y / 2);
        Add(background);
        SpriteGameObject stars = new SpriteGameObject("Backgrounds/stars", -9, "stars"); 
        stars.Position = new Vector2(-GameEnvironment.Screen.X / 2, -GameEnvironment.Screen.Y / 2);
        Add(stars);
        SpriteGameObject stars2 = new SpriteGameObject("Backgrounds/stars2", -8, "stars2");
        stars2.Position = new Vector2(-GameEnvironment.Screen.X / 2, -GameEnvironment.Screen.Y / 2);
        Add(stars2);

        TextGameObject chargeBarName = new TextGameObject("Sprites/SpelFont", 100);
        chargeBarName.Text = "Fuel";
        chargeBarName.Position = new Vector2(70, 180);
        Add(chargeBarName);

        TextGameObject healthBarName = new TextGameObject("Sprites/SpelFont", 100);
        healthBarName.Text = "Health";
        healthBarName.Position = new Vector2(70, 70);
        Add(healthBarName);


        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        Add(quitButton);

        upgradeButton = new Button("Sprites/spr_upgrade_button", 100);
        upgradeButton.Position = new Vector2(GameEnvironment.Screen.X - upgradeButton.Width - 10, 300);
        Add(upgradeButton);

        timeText = new TextGameObject("Sprites/SpelFont", 100);
        timeText.Text = "Time: 0:0";
        timeText.Position = new Vector2(1500, 100);
        Add(timeText);

        killsText = new TextGameObject("Sprites/SpelFont", 100, "killsText");
        killsText.Position = new Vector2(1500, 120);
        killsText.health = 0;
        killsText.Text = "Enemies killed: " + killsText.health;
        Add(killsText);

        ironText = new TextGameObject("Sprites/SpelFont", 100, "ironText");
        ironText.Position = new Vector2(1500, 240);
        Add(ironText);

        carbonText = new TextGameObject("Sprites/SpelFont", 100, "carbonText");
        carbonText.Position = new Vector2(1500, 340);
        Add(carbonText);

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
        player.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
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


