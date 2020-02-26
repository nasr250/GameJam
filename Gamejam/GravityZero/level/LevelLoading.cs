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
        StreamReader levelReader = new StreamReader("Content/Levels/" + levelIndex.ToString() + ".txt");
        gameObjects = new List<GameObject>();
        timeIntervals = new List<int>();
        string line = levelReader.ReadLine();
        while(line != null)
        {
            ReadClass(line);
            line = levelReader.ReadLine();
        }
        
    }
    public void ReadClass(string line)
    {
        string[] lines = line.Split(',');
        switch (lines[0])
        {
            case "bullet":
                gameObjects.Add(new Bullet(new Vector2(float.Parse(lines[1]), float.Parse(lines[2])), dir: int.Parse(lines[3]), spd: float.Parse(lines[4]), curv: double.Parse(lines[5]), damage: int.Parse(lines[6])));
                timeIntervals.Add(int.Parse(lines[7]));
                break;
            case "Menemy":
                gameObjects.Add(new MovingEnemy(new Vector2(float.Parse(lines[3]), float.Parse(lines[4]))));
                gameObjects[gameObjects.Count - 1].Position = new Vector2(float.Parse(lines[1]), float.Parse(lines[2]));
                timeIntervals.Add(int.Parse(lines[5]));
                break;
            case "Penemy":
                gameObjects.Add(new PausingEnemy(int.Parse(lines[3])));
                gameObjects[gameObjects.Count - 1].Position = new Vector2(float.Parse(lines[1]), float.Parse(lines[2]));
                timeIntervals.Add(int.Parse(lines[4]));
                break;
            case "Renemy":
                gameObjects.Add(new RammingEnemy());
                gameObjects[gameObjects.Count - 1].Position = new Vector2(float.Parse(lines[1]), float.Parse(lines[2]));
                timeIntervals.Add(int.Parse(lines[3]));
                break;
            case "Senemy":
                gameObjects.Add(new SpinningEnemy());
                gameObjects[gameObjects.Count - 1].Position = new Vector2(float.Parse(lines[1]), float.Parse(lines[2]));
                timeIntervals.Add(int.Parse(lines[3]));
                break;
            case "laser":
                gameObjects.Add(new Laser(new Vector2(float.Parse(lines[1]), float.Parse(lines[2])), int.Parse(lines[3]), int.Parse(lines[4]), int.Parse(lines[5]), (string)lines[6]));
                timeIntervals.Add(int.Parse(lines[7]));
                break;
                
        }
    }


}