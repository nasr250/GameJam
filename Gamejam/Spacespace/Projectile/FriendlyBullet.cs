using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class FriendlyBullet : Bullet
{

    public FriendlyBullet(Vector2 startpos, int dir = 1, float spd = 1, int size = 1, double curv = 0, int damage = 1) : base(startpos, dir, spd, curv, damage)
    {

    }

    protected override void CheckCollision()
    {
        GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
        for (int i = enemies.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject enemy = enemies.children[i] as SpriteGameObject;
            if (CollidesWith(enemy))
            {
                enemies.children[i].health--;
                this.health--;
                if (enemies.children[i].health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }
            }
        }

    }



}

