﻿using Microsoft.Xna.Framework;

class Rocket : Enemy
{
    protected double spawnTime;
    protected Vector2 startPosition;

    public Rocket(bool moveToLeft, Vector2 startPosition)
    {
        LoadAnimation("Sprites/Rocket/spr_rocket@3", "default", true, 0.2f);
        PlayAnimation("default");
        Mirror = moveToLeft;
        this.startPosition = startPosition;
        Reset();
    }

    public override void Reset()
    {
        visible = false;
        position = startPosition;
        velocity = Vector2.Zero;
        spawnTime = GameEnvironment.Random.NextDouble() * 5;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (spawnTime > 0)
        {
            spawnTime -= gameTime.ElapsedGameTime.TotalSeconds;
            return;
        }
        visible = true;
        velocity.X = 600;
        if (Mirror)
        {
            this.velocity.X *= -1;
        }
        CheckPlayerCollision();
        CheckBombCollision();
        // check if we are outside the screen
        Rectangle screenBox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        if (!screenBox.Intersects(this.BoundingBox))
        {
            Reset();
        }
    }

    public void CheckPlayerCollision()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && visible)
        {
            //If the player is above the rocket and the player is falling down
            if ((player.BoundingBox.Bottom < this.position.Y) && player.Velocity.Y > 0)
            {
                Reset();
                player.Jump();
            }
            else
            {
                player.Die(false);
            }
        }
    }

    public void CheckBombCollision()
    {
        Throwable bomb = GameWorld.Find("bomb") as Throwable;
        if (CollidesWith(bomb) && visible)
        {
            Reset();
            bomb.Die();
        }
    }
}