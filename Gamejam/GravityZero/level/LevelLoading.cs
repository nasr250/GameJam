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
        levelIndex = 0;
        Planet planet = new Planet();
        planet.Position = new Vector2(1000, 600);
        Add(planet);
    }


}