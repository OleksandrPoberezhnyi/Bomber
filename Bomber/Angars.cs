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
    class Angars
    {
        Texture2D block;
        Vector2 position, blockOrigin;
        int scale;
        int[] levels;
        Color[] colours;

        public Angars(ContentManager Content)
        {
            //Initializing angars (block)
            scale = 8;
            block = Content.Load<Texture2D>("block");
            position = new Vector2(20, Bomber.Screen.Y - block.Height/scale);
            blockOrigin = new Vector2(0, 0);

            //Making 21 buildings with different random colors and levels (floors)
            colours = new Color[21];
            int i = 20;
            levels = new int[21];
            while (i >= 0)
            {
                levels[i] = Bomber.Random.Next(1,10);
                colours[i].R = Convert.ToByte(Bomber.Random.Next(1, 250));
                colours[i].G = Convert.ToByte(Bomber.Random.Next(1, 250));
                colours[i].B = Convert.ToByte(Bomber.Random.Next(1, 250));
                colours[i].A = 255;
                i--;
            }
        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Drawing those 21 randomly generated buildings
            position.X = 0;
            int i = 0;
            while (i <= 20)
            {
                position.X += block.Width/scale;
                int j = levels[i];
                while (j > 0)
                {
                    spriteBatch.Draw(block, position, null, colours[i], 0, blockOrigin, 1f / scale, SpriteEffects.None, 0);
                    position.Y -= block.Height / scale;
                    j--;
                }
                position.Y = Bomber.Screen.Y - block.Height / scale;
                i++;
            }
        }

        public int[] Levels
        {
            get { return levels; }
            set { levels = value; }
        }

        public int Scale
        {
            get { return scale; }
        }

        public Texture2D Block
        {
            get { return block; }
        }
    }
}