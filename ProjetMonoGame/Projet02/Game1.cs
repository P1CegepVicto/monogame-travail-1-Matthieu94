using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet02
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject tank;
        Rectangle fenetre;

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

            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();

            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);

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
            tank.position.X = graphics.GraphicsDevice.DisplayMode.Width / 2 - 74;
            tank.position.Y = graphics.GraphicsDevice.DisplayMode.Height / 3 + graphics.GraphicsDevice.DisplayMode.Height / 3;
            tank.vitesse = 20;
            tank.sprite = Content.Load<Texture2D>("tank.png");
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
            tank.direction.X = 0;
            tank.direction.Y = 0;

            if (tank.vivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                    tank.direction.Y -= tank.vitesse;

                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                    tank.direction.Y += tank.vitesse;

                if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                    tank.direction.X -= tank.vitesse;

                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                    tank.direction.X += tank.vitesse;

                tank.position += tank.direction;

                if (tank.position.Y < fenetre.Top)
                    tank.position.Y = fenetre.Top;

                if (tank.position.Y + 236 > fenetre.Bottom)
                    tank.position.Y = fenetre.Bottom - 236;

                if (tank.position.X < fenetre.Left)
                    tank.position.X = fenetre.Left;

                if (tank.position.X + 148 > fenetre.Right)
                    tank.position.X = fenetre.Right - 148;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            if (tank.vivant == true)
                spriteBatch.Draw(tank.sprite, tank.position, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
