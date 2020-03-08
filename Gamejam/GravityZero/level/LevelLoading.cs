using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    List<GameObject> gameObjects;
    List<int> timeIntervals;

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
        Add(planet2);
    }


}