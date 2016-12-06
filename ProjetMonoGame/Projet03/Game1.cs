using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projet03
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random de = new Random();
        GameObject tank;
        GameObject fond;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.graphics.PreferredBackBufferWidth = 3840;
            this.graphics.PreferredBackBufferHeight = 2160;
            this.graphics.ToggleFullScreen();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            tank = new GameObject();
            tank.vivant = true;
            tank.position.X = 1781;
            tank.position.Y = 1559;
            tank.vitesse = 24;
            tank.sprite = Content.Load<Texture2D>("Tank.png");

            fond = new GameObject();
            fond.position.X = -139;
            fond.position.Y = 0;
            fond.sprite = Content.Load<Texture2D>("Fond.png");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            UpdateTank();

            base.Update(gameTime);
        }

        public void UpdateTank()
        {
            if (tank.vivant == true)
            {
                fond.direction.X = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    fond.direction.X += tank.vitesse;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    fond.direction.X -= tank.vitesse;

                fond.position += fond.direction;

                if (fond.position.X > -139)
                    fond.position.X = -139;

                if (fond.position.X < -11381)
                    fond.position.X = -11381;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(fond.sprite, fond.position);

            if (tank.vivant == true)
                spriteBatch.Draw(tank.sprite, tank.position);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
