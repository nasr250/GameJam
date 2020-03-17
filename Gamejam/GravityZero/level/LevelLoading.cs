using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    List<GameObject> gameObjects;
    List<int> timeIntervals;
    BlackHole bh;
    Scrap scrap;

    public void LoadLevel(int levelIndex)// here all entities used in a level can be loaded
    {
        Add(new Camera());
        Camera camera = GameWorld.Find("camera") as Camera;
        camera.Reset();
        levelIndex = 0;
        Planet planet = new Planet();
        planet.Position = new Vector2(600, 900);
        Add(planet);
        Planet planet2 = new Planet();
        planet2.Position = new Vector2(1200, 1000);
        //Add(planet2);

        scrap = new Scrap();
        scrap.Position = new Vector2(100, 100);
        Add(scrap);

        bh = new BlackHole();
        bh.Position = new Vector2(4000, 1000);
        Add(bh);
        Enemy enemy1 = new MovingEnemy(new Vector2(1000, 1000));
        enemy1.Position = new Vector2(500, 1000);
        Add(enemy1);
    }


}