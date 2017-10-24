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
    class Plane
    {
        Texture2D airplane;
        Vector2 position,airplaneOrigin;
        int scale;

        public Plane(ContentManager Content)
        {
            //Intializing plane texture, start position, origin and scale
            airplane = Content.Load<Texture2D>("airplane");
            position = new Vector2(Bomber.Screen.X, airplane.Height/2*scale);
            airplaneOrigin = new Vector2(0, 0);
            scale = 3;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw plane sprite
            spriteBatch.Draw(airplane, position, null, Color.White, 0, airplaneOrigin, 1f/scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < 21; i++)
            {
                //Did plane touch a building? 
                if (position.Y >= (Bomber.Screen.Y - (Bomber.GameWorld.Angars.Levels[i] + 1) * (Bomber.GameWorld.Angars.Block.Height / Bomber.GameWorld.Angars.Scale))
    && position.X <= (Bomber.GameWorld.Angars.Block.Width / Bomber.GameWorld.Angars.Scale) * (i+1.8))
                {
                    //For testing reason this command just eliminate blocks of a building with which plane made a contact
                    //Generally here should be implementation of plane being destroyed with other game related consequences
                    Bomber.GameWorld.Angars.Levels[i]--;
                }
            }

            //If plane gone beyond a screen make it return on the right again but this time on lower altitude
            if (position.X <= -airplane.Width / scale)
            {   position.X = Bomber.Screen.X;
                position.Y += airplane.Height / scale;
            }
            
            //If not then just make it fly to the left
            position.X -= 3;
        }

        public Vector2 Position
        {
            get { return position; }
        } 

        public int Scale
        {
            get { return scale; }
        }
    }
}