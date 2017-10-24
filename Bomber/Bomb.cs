using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Bomber
{
    class Bomb
    {
        Texture2D bomb;
        Vector2 position, velocity, bombOrigin;
        static bool shooting, falling;
        int scale;

        public Bomb(ContentManager Content)
        {
            //Initializing bomb object
            bomb = Content.Load<Texture2D>("bomb");
            position = new Vector2(Bomber.Screen.X, 20);
            velocity = Vector2.Zero;
            bombOrigin = new Vector2(bomb.Width / 2, bomb.Height);
            scale = 3;

            //We're not shooting (no tapping)
            //And bomb is not falling from a plane
            shooting = false;
            falling = false;
        }
        
        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            //If bomb is falling from a plane, draw it
            if (falling)
            spriteBatch.Draw(bomb, position, null, Color.White, 0, bombOrigin, 1f/scale, SpriteEffects.None,0);
        }

        public void Update(GameTime gameTime)
        {
            //If we're shooting and a bomb is not falling already
            if (shooting && !falling)
            {
                shooting = false;
                //If plane is on playable screen then make bomb to fall
                if (position.X >= Bomber.GameWorld.Angars.Block.Width/Bomber.GameWorld.Angars.Scale 
                    && position.X <= Bomber.Screen.X-(Bomber.GameWorld.Angars.Block.Width/Bomber.GameWorld.Angars.Scale))
                {
                    falling = true;
                }
            }

            //So if bomb is falling now
            if (falling)
            {
                //Determine which building are under bomb is now
                int i = (int)Math.Floor(position.X / (Bomber.GameWorld.Angars.Block.Width/Bomber.GameWorld.Angars.Scale)) - 1;
                if (i > 20) i = 20;
                if (i < 0) i = 0;

                //Making bomb fall down with some velocity
                velocity.Y += 7;
                position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //If bomb made contact with building or ground make it stop falling and return its position to plane
                if (position.Y >= (Bomber.Screen.Y - (Bomber.GameWorld.Angars.Levels[i]) * (Bomber.GameWorld.Angars.Block.Height / Bomber.GameWorld.Angars.Scale)))
                {
                    falling = false;
                    velocity.Y = 0;
                    Bomber.GameWorld.Angars.Levels[i]--;
                    position.Y = Bomber.GameWorld.Plane.Position.Y + 130;
                    position.X = Bomber.GameWorld.Plane.Position.X + 60;
                }
            }
            
            //If bomb was not falling after all then just make its position follow plane
            else
            {
                position.X = Bomber.GameWorld.Plane.Position.X + 60;
                position.Y = Bomber.GameWorld.Plane.Position.Y + 130;
            }

        }

        public static bool Shooting
        {
            get { return shooting; }
            set { shooting = value; }
        }
    }
}