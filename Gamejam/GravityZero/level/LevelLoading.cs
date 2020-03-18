using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    int safezone = 800;
    int planetsDensity = 800;
    int enemyDensity = 25;
    int scrapDensity = 100;
    int minPlanetDistance = 50;
    List<Vector2> planetlocations;
    public int worldborder = 10000;
    int attempts;
    int planetcount;

    public void LoadLevel(int levelIndex)// here all entities used in a level can be loaded
    {
        LoadScraps();
        LoadPlanets();
        LoadEnemies();
    }

    Vector2 RandomPos()
    {
        return new Vector2((GameEnvironment.Random.Next(0, 2) * 2 - 1) * GameEnvironment.Random.Next(0, worldborder),
                           (GameEnvironment.Random.Next(0, 2) * 2 - 1) * GameEnvironment.Random.Next(0, worldborder));
    }

    void LoadScraps()
    {
        for (int x = 1; x < scrapDensity; x++)
        {
            Vector2 randomscrappos = RandomPos();
            Add(new Scrap(randomscrappos));
        }
    }

    void LoadEnemies()
    {
        for (int x = 1; x < enemyDensity; x++)
        {
            Vector2 randomenemypos = RandomPos();
            Add(new MovingEnemy(randomenemypos)); 
        }
    }

    void LoadPlanets()
    {
        planetlocations = new List<Vector2>();
        attempts = 0;
        for (int x = 1; x < planetsDensity; x++)
        {
            //attempts++;
            //Console.WriteLine(attempts);
            bool overlap = false;
            Vector2 randomplanetpos = RandomPos();
            foreach (Vector2 planetpos in planetlocations)
            {
                if (Math.Abs(randomplanetpos.X - planetpos.X) < minPlanetDistance || Math.Abs(randomplanetpos.Y - planetpos.Y) < minPlanetDistance ||
                    randomplanetpos.X > -safezone && randomplanetpos.X < safezone || randomplanetpos.Y > -safezone && randomplanetpos.Y < safezone) //planet overlaps other planet?
                {
                    //return to for loop
                    overlap = true;
                    break;
                }
            }
            if (!overlap)
            {
                if (GameEnvironment.Random.Next(0, 3) == 1)
                {
                    planetlocations.Add(randomplanetpos);
                    Add(new BlackHole(randomplanetpos));
                }
                else
                {
                    planetlocations.Add(randomplanetpos);
                    int randomtexture = (GameEnvironment.Random.Next(0, 5));
                    if (randomtexture == 1)
                    {
                        Add(new Planet(randomplanetpos, "Sprites/planet_1"));
                    }
                    else if (randomtexture == 2)
                    {
                        Add(new Planet(randomplanetpos, "Sprites/planet_2"));
                    }
                    else if (randomtexture == 3)
                    {
                        Add(new Planet(randomplanetpos, "Sprites/planet_3"));
                    }
                    else if (randomtexture == 4)
                    {
                        Add(new Planet(randomplanetpos, "Sprites/planet_4"));
                    }
                    else if (randomtexture == 5)
                    {
                        Add(new Planet(randomplanetpos, "Sprites/planet_5"));
                    }
                }
                planetcount++;
            }
        }
        Console.WriteLine("created planets: " + planetcount);
    }
}