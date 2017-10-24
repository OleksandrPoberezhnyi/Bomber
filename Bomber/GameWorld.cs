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

namespace Bomber
{
    //GameWorld Update-Draw module
    class GameWorld
    {
        //Our main game objects
        Plane plane;
        Angars angars;
        Bomb bomb;

        public GameWorld(ContentManager Content)
        {
            //Initializing them
            plane = new Plane(Content);
            angars = new Angars(Content);
            bomb = new Bomb(Content);
        }

        public void Update(GameTime gameTime)
        {
            //Updating plane and bomb objects
            //Angars are updating based on situations happening in Plane and Bomb classes
            plane.Update(gameTime);
            bomb.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw main game objects
            spriteBatch.Begin();
            plane.Draw(gameTime, spriteBatch);
            bomb.Draw(gameTime, spriteBatch);
            angars.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public Plane Plane
        {
            get { return plane; }
        }

        public Angars Angars
        {
            get { return angars; }
        }

    }
}