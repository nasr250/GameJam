﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public abstract class Enemy : AnimatedGameObject
{
    float shootTimer = 0.1f;
    Vector2 shootDirection;
    public Enemy(string sprite = "Sprites/enemy2@2x1") :base(id:"enemy")
    {
        LoadAnimation(sprite, "default", true);
        PlayAnimation("default");
        position = new Vector2(0, 0);
        health = 1;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (shootTimer > 0f)  //timer that makes the enemy shoot a projectile at a certain time interval
        {
            shootTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shootTimer <= 0.0f)
            {
               Shoot();
            }
        }
        else
        {
            shootTimer = 2f;
        }
        Player player = GameWorld.Find("player") as Player;
        if(player != null)
        {
            if (CollidesWith(player))
            {
                Player.Health -= 5;
                health--;
            }
            shootDirection = player.Position - position;
        }
        
        GameObjectList friendlyBullets = GameWorld.Find("friendlyBullets") as GameObjectList;
        for (int i = friendlyBullets.children.Count - 1; i >= 0; i--)
        {
            SpriteGameObject friendlyBullet = friendlyBullets.children[i] as SpriteGameObject;
            if (CollidesWith(friendlyBullet))
            {
                friendlyBullets.children[i].health--;
                this.health--;
                if (this.health < 0)
                {
                    TextGameObject kills = GameWorld.Find("killsText") as TextGameObject;
                    kills.health++;
                }
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

        base.Draw(gameTime, spriteBatch);
    }

    public virtual void Shoot()
    {
        GameObjectList bullets = GameWorld.Find("bullets") as GameObjectList;
        Bullet bullet = new Bullet(position, spd: 7, curv: 2, dir: (int)(Math.Atan2(shootDirection.Y, shootDirection.X) * 180/Math.PI));
        bullets.Add(bullet);
    }

    public override void Reset()
    {
        health = -1;
    }


}