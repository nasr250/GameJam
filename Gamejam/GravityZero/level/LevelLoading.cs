using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    int safezone = 800;
    int planetsDensity = 700;
    int enemyDensity = 13;
    int scrapDensity = 180;
    int starDensity = 0;
    int minPlanetDistance = 80;
    List<Vector2> planetlocations;
    public int worldborder = 8000;
    int attempts;
    int planetcount;

    public void LoadLevel(int levelIndex)// here all entities used in a level can be loaded
    {
        LoadScraps();
        LoadPlanets();
        LoadEnemies();
        LoadStars();
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
            Add(new Scrap(RandomPos()));
        }
    }
    void LoadStars()
    {
        string type = "Backgrounds/star1";
        int randomtexture = (GameEnvironment.Random.Next(0, 3));
        if (randomtexture == 1)
        {
            type = "Backgrounds/star1";
        }
        else if (randomtexture == 2)
        {
            type = "Backgrounds/star2";
        }
        else if (randomtexture == 3)
        {
            type = "Backgrounds/star3";
        }
        for (int x = 1; x < starDensity; x++)
        {
            Add(new Star(RandomPos(), type));
        }
    }

    void LoadEnemies()
    {
        for (int x = 1; x < enemyDensity; x++)
        {
            int random = GameEnvironment.Random.Next(0, 2);
            if (random == 1)
            {
                Add(new MovingEnemy(RandomPos()));
            }
            else
            {
                Add(new RammingEnemy(RandomPos()));
            }
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
                if (GameEnvironment.Random.Next(0, 6) == 1)
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