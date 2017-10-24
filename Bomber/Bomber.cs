using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace Bomber
{
    //Main Initialize-Load-Update-Draw module

    class Bomber : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static GameWorld gameWorld;
        static Point screen;
        static Random random;
        TouchCollection currentTouchState;

        public Bomber()
        {
            //Configuring gfx properties
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            //Initializing random function
            random = new Random();
        }

        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.Tap;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Setting SpriteBatch, 'screen' and 'GameWorld' implementation
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            gameWorld = new GameWorld(Content);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {

            //Establishing user control of the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            currentTouchState = TouchPanel.GetState();

            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                switch (gesture.GestureType)
                {
                    case GestureType.Tap:
                        Bomb.Shooting = true; //In case of taping initiate Shooting method of Bomb class
                        break;
                }
            }

            //Updating GameWorld
            gameWorld.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Clear screen before draw
            GraphicsDevice.Clear(Color.White);

            //Draw updated screen
            gameWorld.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        public static GameWorld GameWorld
        {
            get { return gameWorld; }
        }

        public static Point Screen
        {
            get { return screen; }
        }

        public static Random Random
        {
            get { return random; }
        }
    }
}
