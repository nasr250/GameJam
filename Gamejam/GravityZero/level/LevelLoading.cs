using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    List<GameObject> gameObjects;
    List<int> timeIntervals;
    int safezone = 700;
    int planetsDensity = 1000;
    int minPlanetDistance = 200;
    List<Vector2> planetlocations;
    public int worldborder = 10000;
    int attempts;
    int planetcount;

    public void LoadLevel(int levelIndex)// here all entities used in a level can be loaded
    {
        planetlocations = new List<Vector2>();
        attempts = 0;
        Add(new Camera());
        Camera camera = GameWorld.Find("camera") as Camera;
        camera.Reset();
        levelIndex = 0;
        for (int x = 1; x < planetsDensity; x++)
        {
            attempts++;
            Console.WriteLine(attempts);
            bool overlap = false;
            Vector2 randomplanetpos = new Vector2((GameEnvironment.Random.Next(0, 2) * 2 - 1) * GameEnvironment.Random.Next(0, worldborder),
                                            (GameEnvironment.Random.Next(0, 2) * 2 - 1) * GameEnvironment.Random.Next(0, worldborder));
            foreach(Vector2 planetpos in planetlocations)
            {
                if((randomplanetpos.X + minPlanetDistance > planetpos.X && randomplanetpos.X - minPlanetDistance < planetpos.X) || (randomplanetpos.Y + minPlanetDistance > planetpos.Y && randomplanetpos.Y - minPlanetDistance < planetpos.Y) ) //planet overlaps other planet?
                {
                    //return to for loop
                    overlap = true;
                    break;
                }
            }
            if (!overlap)
            {
                planetlocations.Add(randomplanetpos);
                Add(new Planet(randomplanetpos));
                planetcount++;
            }
            


            /*

            Vector2 planetpos = new Vector2((GameEnvironment.Random.Next(0, 2) * 2 - 1) * GameEnvironment.Random.Next(0, worldborder),
                                            (GameEnvironment.Random.Next(0, 2) * 2 - 1) * GameEnvironment.Random.Next(0, worldborder));
            if(planetpos.X > -safezone && planetpos.X < safezone && planetpos.Y > -safezone && planetpos.Y < safezone)
            {
                x--;
            }
            else
            {
                Add(new Planet(planetpos));
            }*/
        }
        Console.WriteLine("created planets: " + planetcount);
    }


}