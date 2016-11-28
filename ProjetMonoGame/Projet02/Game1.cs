using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        GameObject fond;
        GameObject projectileTank;
        GameObject[] ennemi = new GameObject[10];
        GameObject projectileEnnemi;
        int nombreEnnemis = 0;
        int tirs = 0;
        int secondes = 0;
        int ennemiAleatoire;
        Rectangle fenetre;
        Random de = new Random();
        SpriteFont font;

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

            fenetre = new Rectangle(0, 0, 3840, 2160);

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
            tank.vitesse = 12;
            tank.sprite = Content.Load<Texture2D>("Tank.png");

            fond = new GameObject();
            fond.position.X = 0;
            fond.position.Y = 0;
            fond.sprite = Content.Load<Texture2D>("Fond.png");

            projectileTank = new GameObject();
            projectileTank.vivant = false;
            projectileTank.vitesse = 36;
            projectileTank.sprite = Content.Load<Texture2D>("Projectile.png");

            for (int i = 0; i < ennemi.Length; i++)
            {
                ennemi[i] = new GameObject();
                ennemi[i].vivant = false;
                ennemi[i].position.Y = -161;
                ennemi[i].position.X = de.Next(0, 3645);
                ennemi[i].vitesse = 12;
                ennemi[i].vitesse2 = de.Next(-12, 13);
                ennemi[i].toucheSol = false;
                ennemi[i].sprite = Content.Load<Texture2D>("Ennemi.png");
            }

            projectileEnnemi = new GameObject();
            projectileEnnemi.vivant = false;
            projectileEnnemi.vitesse = 36;
            projectileEnnemi.sprite = Content.Load<Texture2D>("Projectile.png");

            font = Content.Load<SpriteFont>("Font");
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

            if (nombreEnnemis * 5 < gameTime.TotalGameTime.Seconds)
            {
                nombreEnnemis++;

                if (nombreEnnemis <= ennemi.Length)
                {
                    ennemi[nombreEnnemis - 1].vivant = true;
                }
            }

            if (tirs * 1 < gameTime.TotalGameTime.Seconds)
            {
                tirs++;

                if (nombreEnnemis > 0)
                {
                    if (projectileEnnemi.vivant == false)
                    {
                        if (nombreEnnemis <= ennemi.Length)
                        {
                            ennemiAleatoire = de.Next(0, nombreEnnemis);

                            if (ennemi[ennemiAleatoire].vivant == true)
                            {
                                projectileEnnemi.position.X = ennemi[ennemiAleatoire].position.X + 82;
                                projectileEnnemi.position.Y = ennemi[ennemiAleatoire].position.Y + 65;
                                projectileEnnemi.vivant = true;
                            }
                        }
                    }
                }
            }

            if (secondes < gameTime.TotalGameTime.Seconds)
                secondes++;
                
            UpdateTank();
            UpdateProjectileTank();
            UpdateEnnemi();
            UpdateProjectileEnnemi();

            base.Update(gameTime);
        }

        public void UpdateTank()
        {
            if (tank.vivant == true)
            {
                tank.direction.X = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    tank.direction.X -= tank.vitesse;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    tank.direction.X += tank.vitesse;

                tank.position += tank.direction;

                if (tank.position.X < fenetre.Left)
                    tank.position.X = fenetre.Left;

                if (tank.position.X + 278 > fenetre.Right)
                    tank.position.X = fenetre.Right - 278;

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (projectileTank.vivant == false)
                    {
                        projectileTank.position.X = tank.position.X + 123;
                        projectileTank.position.Y = tank.position.Y;
                        projectileTank.vivant = true;
                    }
                }
            }
        }

        public void UpdateProjectileTank()
        {
            if (projectileTank.vivant == true)
            {
                projectileTank.direction.Y = 0;
                projectileTank.direction.Y -= projectileTank.vitesse;
                projectileTank.position.Y += projectileTank.direction.Y;

                if (projectileTank.position.Y + 31 < fenetre.Top)
                    projectileTank.vivant = false;

                for (int i = 0; i < ennemi.Length; i++)
                {
                    if (ennemi[i].vivant == true)
                    {
                        if (ennemi[i].GetRect().Intersects(projectileTank.GetRect()))
                        {
                            ennemi[i].vivant = false;
                            projectileTank.vivant = false;
                        }
                    }
                }
            }
        }

        public void UpdateEnnemi()
        {
            for (int i = 0; i < ennemi.Length; i++)
            {
                if (ennemi[i].vivant == true)
                {
                    ennemi[i].direction.X = 0;
                    ennemi[i].direction.Y = 0;

                    ennemi[i].direction.X += ennemi[i].vitesse2;
                    ennemi[i].direction.Y += ennemi[i].vitesse;

                    ennemi[i].position.X += ennemi[i].direction.X;
                    ennemi[i].position.Y += ennemi[i].direction.Y;

                    if (ennemi[i].position.X < fenetre.Left)
                        ennemi[i].vitesse2 -= ennemi[i].vitesse2 * 2;

                    if (ennemi[i].position.X + 196 > fenetre.Right)
                        ennemi[i].vitesse2 -= ennemi[i].vitesse2 * 2;

                    if (ennemi[i].position.Y + 161 > 1781)
                    {
                        ennemi[i].vitesse -= ennemi[i].vitesse * 2;
                        ennemi[i].toucheSol = true;
                    }

                    if (ennemi[i].position.Y < fenetre.Top && ennemi[i].toucheSol == true)
                        ennemi[i].vitesse -= ennemi[i].vitesse * 2;

                    if (tank.GetRect().Intersects(ennemi[i].GetRect()))
                        tank.vivant = false;
                }
            }
        }

        public void UpdateProjectileEnnemi()
        {
            if (projectileEnnemi.vivant == true)
            {
                projectileEnnemi.direction.Y = 0;
                projectileEnnemi.direction.Y += projectileEnnemi.vitesse;
                projectileEnnemi.position.Y += projectileEnnemi.direction.Y;
         
                if (projectileEnnemi.position.Y + 31 > 1781)
                        projectileEnnemi.vivant = false;

                if (tank.vivant == true)
                {
                    if (tank.GetRect().Intersects(projectileEnnemi.GetRect()))
                    {
                        projectileEnnemi.vivant = false;
                        tank.vivant = false;
                    }
                }
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

            spriteBatch.Draw(fond.sprite, fond.position);

            if (projectileTank.vivant == true)
                spriteBatch.Draw(projectileTank.sprite, projectileTank.position);

            if (tank.vivant == true)
                spriteBatch.Draw(tank.sprite, tank.position);

            if (projectileEnnemi.vivant == true)
                spriteBatch.Draw(projectileEnnemi.sprite, projectileEnnemi.position);

            for (int i = 0; i < ennemi.Length; i++)
            {
                if (ennemi[i].vivant == true)
                    spriteBatch.Draw(ennemi[i].sprite, ennemi[i].position);
            }

            spriteBatch.DrawString(font, "Temps de jeu: " + secondes + " secondes", new Vector2(100, 100), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
