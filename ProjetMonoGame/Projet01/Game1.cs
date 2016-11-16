using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject hero;
        GameObject ennemi;
        GameObject projectileEnnemi;
        GameObject projectileHero;
        Rectangle fenetre;
        bool ennemiDirection = true;

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

            hero = new GameObject();
            hero.estVivant = true;
            hero.position.X = graphics.GraphicsDevice.DisplayMode.Width / 2 - 74;
            hero.position.Y = graphics.GraphicsDevice.DisplayMode.Height / 3 + graphics.GraphicsDevice.DisplayMode.Height / 3;
            hero.vitesse = 10;
            hero.sprite = Content.Load<Texture2D>("tank.png");

            ennemi = new GameObject();
            ennemi.estVivant = true;
            ennemi.position.X = graphics.GraphicsDevice.DisplayMode.Width / 2 - 83;
            ennemi.position.Y = graphics.GraphicsDevice.DisplayMode.Height / 12;
            ennemi.vitesse = 10;
            ennemi.sprite = Content.Load<Texture2D>("ennemi.png");

            projectileEnnemi = new GameObject();
            projectileEnnemi.estVivant = false;
            projectileEnnemi.vitesse = 20;
            projectileEnnemi.sprite = Content.Load<Texture2D>("projectile.png");

            projectileHero = new GameObject();
            projectileHero.estVivant = false;
            projectileHero.vitesse = 20;
            projectileHero.sprite = Content.Load<Texture2D>("projectile.png");

            // TODO: use this.Content to load your game content here
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

            UpdateHero();
            UpdateEnnemi();
            UpdateProjectileEnnemi();
            UpdateProjectileHero();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        public void UpdateHero()
        {
            hero.direction.X = 0;
            hero.direction.Y = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                hero.direction.X -= hero.vitesse;

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                hero.direction.X += hero.vitesse;

            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                hero.direction.Y -= hero.vitesse;

            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                hero.direction.Y += hero.vitesse;

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                hero.position.X = graphics.GraphicsDevice.DisplayMode.Width / 2 - 74;
                hero.position.Y = graphics.GraphicsDevice.DisplayMode.Height / 3 + graphics.GraphicsDevice.DisplayMode.Height / 3;
                hero.estVivant = true;

                ennemi.position.X = graphics.GraphicsDevice.DisplayMode.Width / 2 - 83;
                ennemi.position.Y = graphics.GraphicsDevice.DisplayMode.Height / 12;
                ennemi.estVivant = true;
                ennemiDirection = true;

                projectileEnnemi.estVivant = false;
                projectileHero.estVivant = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (projectileHero.estVivant == false)
                {
                    projectileHero.position.X = hero.position.X + 42;
                    projectileHero.position.Y = hero.position.Y;
                    projectileHero.estVivant = true;
                }
            }

            hero.position += hero.direction;

            if (hero.position.X < fenetre.Left)
                hero.position.X = fenetre.Left;

            if (hero.position.X + 148 > fenetre.Right)
                hero.position.X = fenetre.Right - 148;

            if (hero.position.Y + 236 > fenetre.Bottom)
                hero.position.Y = fenetre.Bottom - 236;

            if (hero.position.Y < fenetre.Top)
                hero.position.Y = fenetre.Top;

            if (hero.GetRect().Intersects(ennemi.GetRect()))
                hero.estVivant = false;

            if (hero.GetRect().Intersects(projectileEnnemi.GetRect()))
            {
                hero.estVivant = false;
                projectileEnnemi.estVivant = false;
            }
        }

        public void UpdateEnnemi()
        {
            ennemi.direction.X = 0;

            if (ennemiDirection == true)
                ennemi.direction.X -= ennemi.vitesse;

            if (ennemiDirection == false)
                ennemi.direction.X += ennemi.vitesse;

            ennemi.position.X += ennemi.direction.X;

            if (ennemi.position.X < fenetre.Left)
                ennemiDirection = false;

            if (ennemi.position.X + 165 > fenetre.Right)
                ennemiDirection = true;

            if (ennemi.GetRect().Intersects(projectileHero.GetRect()))
            {
                ennemi.estVivant = false;
                projectileHero.estVivant = false;
            }
        }

        public void UpdateProjectileEnnemi()
        {
                projectileEnnemi.direction.Y = 0;
                projectileEnnemi.direction.Y += projectileEnnemi.vitesse;
                projectileEnnemi.position.Y += projectileEnnemi.direction.Y;

            if (ennemi.estVivant == true)
            {
                if (projectileEnnemi.position.Y > fenetre.Bottom)
                    projectileEnnemi.estVivant = false;

                if (projectileEnnemi.estVivant == false)
                {
                    projectileEnnemi.position.X = ennemi.position.X + 51;
                    projectileEnnemi.position.Y = ennemi.position.Y + 51;
                    projectileEnnemi.estVivant = true;
                }
            }
        }

        public void UpdateProjectileHero()
        {
                projectileHero.direction.Y = 0;
                projectileHero.direction.Y -= projectileHero.vitesse;
                projectileHero.position.Y += projectileHero.direction.Y;

            if (hero.estVivant == true)
            {
                if (projectileHero.position.Y + 63 < fenetre.Top)
                    projectileHero.estVivant = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            if (projectileHero.estVivant == true)
                spriteBatch.Draw(projectileHero.sprite, projectileHero.position, Color.White);

            if (hero.estVivant == true)
                spriteBatch.Draw(hero.sprite, hero.position, Color.White);

            if (projectileEnnemi.estVivant == true)
                spriteBatch.Draw(projectileEnnemi.sprite, projectileEnnemi.position, Color.White);

            if (ennemi.estVivant == true)
                spriteBatch.Draw(ennemi.sprite, ennemi.position, Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
