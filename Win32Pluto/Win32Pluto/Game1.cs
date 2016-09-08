﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using Win32Pluto.Managers;
using Win32Pluto.Models;

namespace Win32Pluto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        DisplayManager displayManager;
        SpaceManager spaceManager;
        SunManager sunManager;
        OrbitManager orbitManager;
        PlanetManager planetManager;
        AsteroidManager asteroidManager;
        ScoreManager scoreManager;
        AudioManager audioManager;

        // Constants
        const float sunScale = 0.8f;
        const float earthScale = 0.3f;
        const float saturnScale = 0.3f;
        const float plutoScale = 0.3f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            displayManager = new DisplayManager();
            spaceManager = new SpaceManager();
            sunManager = new SunManager();
            orbitManager = new OrbitManager();
            planetManager = new PlanetManager();
            asteroidManager = new AsteroidManager();
            scoreManager = new ScoreManager();
            audioManager = new AudioManager();
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
            displayManager.EnableFullScreen(graphics);
            displayManager.ShowMouseCursor(this);
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
            LoadSpace();
            LoadSun();
            LoadOrbits();
            LoadPlanets();
            LoadAsteroids();
            LoadUserInterface();
            LoadAudio();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            spaceManager.Dispose();
            sunManager.Dispose();
            planetManager.Dispose();
            asteroidManager.Dispose();
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
            sunManager.Update(gameTime, scoreManager);
            orbitManager.Update(GraphicsDevice, planetManager);
            planetManager.Update(GraphicsDevice, gameTime);
            asteroidManager.Update(gameTime, GraphicsDevice, sunManager, scoreManager, planetManager);

            base.Update(gameTime);
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

            spaceManager.Draw(spriteBatch);
            sunManager.Draw(spriteBatch);
            orbitManager.Draw(spriteBatch);
            planetManager.Draw(spriteBatch);
            asteroidManager.Draw(spriteBatch, sunManager);
            scoreManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        #region Game1 CustomAccessors

        public void LoadAsteroids() {
            Asteroid asteroid = new Asteroid();
            asteroid.type = "RedMeteor";
            asteroid.sprite.texture = Content.Load<Texture2D>("RedMeteor");
            asteroid.sprite.position = new Vector2(asteroid.sprite.texture.Width / 2 , 1920);
            asteroid.sprite.rotation = 0f;
            asteroid.sprite.scale = new Vector2(1f, 1f);
            asteroid.sprite.origin = new Vector2((asteroid.sprite.texture.Width / 29)/2, asteroid.sprite.texture.Height / 2);
            asteroidManager.Add(asteroid);

            Asteroid blueAsteroid = new Asteroid();
            blueAsteroid.type = "BlueMeteor";
            blueAsteroid.sprite.texture = Content.Load<Texture2D>("BlueMeteor");
            blueAsteroid.sprite.position = new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2);
            blueAsteroid.sprite.rotation = 0f;
            blueAsteroid.sprite.scale = new Vector2(1f, 1f);
            blueAsteroid.sprite.origin = new Vector2((blueAsteroid.sprite.texture.Width/29)/2, blueAsteroid.sprite.texture.Height/2);
            Random r = new Random();
            int randomValue = r.Next(0, 360);
            var angle = randomValue;
            int radius = Math.Max(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            radius = radius + 500;
            blueAsteroid.targetPosition = new Vector2((float)(Math.Cos(angle) * radius), (float)(Math.Sin(angle) * radius));
            asteroidManager.Add(blueAsteroid);
        }

        public void LoadSpace() {
            Space space = new Space();
            space.sprite.texture = Content.Load<Texture2D>("PlasmaBackground");
            space.sprite.position = new Vector2(0, 0);
            space.sprite.rotation = 0f;
            space.sprite.scale = new Vector2(1f, 1f);
            space.sprite.origin = new Vector2(space.sprite.texture.Width / 2, space.sprite.texture.Height / 2);
            spaceManager.Add(space);
        }

        public void LoadSun() {
            Sun sun = new Sun();
            sun.state = "Active";
            sun.starTextureState1 = Content.Load<Texture2D>("Life_Star");
            sun.starTextureState2 = Content.Load<Texture2D>("Life_Star_2");
            sun.starTextureState3 = Content.Load<Texture2D>("Life_Star_3");
            sun.starTextureState4 = Content.Load<Texture2D>("Life_Star_4");
            sun.starTextureState5 = Content.Load<Texture2D>("Life_Star_5");
            sun.starTextureState6 = Content.Load<Texture2D>("Life_Star_6");
            sun.sprite.texture = sun.starTextureState1;
            sun.sprite.scale = new Vector2(sunScale, sunScale);
            sun.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            sun.sprite.rotation = 0f;
            sun.sprite.origin = new Vector2((sun.sprite.texture.Width / 6)/2, (sun.sprite.texture.Height / 3)/2);
            sun.sprite.rectangle = new Rectangle(0, 0, (sun.sprite.texture.Width / 6), sun.sprite.texture.Height / 3);
            sunManager.Add(sun);
        }

        public void LoadOrbits() {
            // Earth
            Orbit earthOrbit = new Orbit();
            earthOrbit.name = "EarthOrbit";
            earthOrbit.radius = 200;
            earthOrbit.defaultTexture = Content.Load<Texture2D>("Orbit1_Normal");
            earthOrbit.selectedTexture = Content.Load<Texture2D>("Orbit1_Selected");
            earthOrbit.sprite.texture = earthOrbit.defaultTexture;
            earthOrbit.sprite.scale = new Vector2(0.9f, 0.9f);
            earthOrbit.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - (earthOrbit.sprite.texture.Width * earthOrbit.sprite.scale.X) / 2, GraphicsDevice.Viewport.Bounds.Height / 2 - (earthOrbit.sprite.texture.Height * earthOrbit.sprite.scale.Y) / 2);
            earthOrbit.sprite.rotation = 0f;
            earthOrbit.sprite.origin = new Vector2(earthOrbit.sprite.texture.Width / 2, earthOrbit.sprite.texture.Height / 2);
            orbitManager.Add(earthOrbit);

            // Saturn
            Orbit saturnOrbit = new Orbit();
            saturnOrbit.name = "SaturnOrbit";
            saturnOrbit.radius = 300;
            saturnOrbit.defaultTexture = Content.Load<Texture2D>("Orbit2_Normal");
            saturnOrbit.selectedTexture = Content.Load<Texture2D>("Orbit2_Selected");
            saturnOrbit.sprite.texture = saturnOrbit.defaultTexture;
            saturnOrbit.sprite.scale = new Vector2(0.94f, 0.94f);
            saturnOrbit.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - (saturnOrbit.sprite.texture.Width * saturnOrbit.sprite.scale.X) / 2, GraphicsDevice.Viewport.Bounds.Height / 2 - (saturnOrbit.sprite.texture.Height * saturnOrbit.sprite.scale.Y) / 2);
            saturnOrbit.sprite.rotation = 0f;
            saturnOrbit.sprite.origin = new Vector2(saturnOrbit.sprite.texture.Width / 2, saturnOrbit.sprite.texture.Height / 2);
            orbitManager.Add(saturnOrbit);

            // Pluto
            Orbit plutoOrbit = new Orbit();
            plutoOrbit.name = "PlutoOrbit";
            plutoOrbit.radius = 400;
            plutoOrbit.defaultTexture = Content.Load<Texture2D>("Orbit3_Normal");
            plutoOrbit.selectedTexture = Content.Load<Texture2D>("Orbit3_Selected");
            plutoOrbit.sprite.texture = earthOrbit.defaultTexture;
            plutoOrbit.sprite.scale = new Vector2(0.96f, 0.96f);
            plutoOrbit.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - (plutoOrbit.sprite.texture.Width * plutoOrbit.sprite.scale.X)/ 2, GraphicsDevice.Viewport.Bounds.Height / 2 - (plutoOrbit.sprite.texture.Height * plutoOrbit.sprite.scale.Y) / 2);
            plutoOrbit.sprite.rotation = 0f;
            plutoOrbit.sprite.origin = new Vector2(plutoOrbit.sprite.texture.Width / 2, plutoOrbit.sprite.texture.Height / 2);
            orbitManager.Add(plutoOrbit);
        }

        public void LoadPlanets() {
            // Earth
            Planet earth = new Planet();
            earth.name = "Earth";
            earth.radius = 200;
            earth.sprite.texture = Content.Load<Texture2D>("PlasmaBalls");
            earth.sprite.position = new Vector2(300, 300);
            earth.sprite.scale = new Vector2(earthScale, earthScale);
            earth.sprite.rotation = 0f;
            earth.sprite.rectangle = new Rectangle(0, 0, (earth.sprite.texture.Width / 13), earth.sprite.texture.Height / 4);
            earth.sprite.origin = new Vector2((earth.sprite.texture.Width/13) / 2, (earth.sprite.texture.Height /4)/ 2);
            planetManager.Add(earth);

            // Saturn
            Planet saturn = new Planet();
            saturn.name = "Saturn";
            saturn.radius = 300;
            saturn.sprite.texture = Content.Load<Texture2D>("PlasmaBalls");
            saturn.sprite.position = new Vector2(300, 300);
            saturn.sprite.scale = new Vector2(saturnScale, saturnScale);
            saturn.sprite.rotation = 0f;
            saturn.sprite.rectangle = new Rectangle(0, 0, (saturn.sprite.texture.Width / 13), saturn.sprite.texture.Height / 4);
            saturn.sprite.origin = new Vector2((saturn.sprite.texture.Width/13 )/ 2, (saturn.sprite.texture.Height /4)/ 2);
            planetManager.Add(saturn);

            // Pluto
            Planet pluto = new Planet();
            pluto.name = "Pluto";
            pluto.radius = 400;
            pluto.sprite.texture = Content.Load<Texture2D>("PlasmaBalls");
            pluto.sprite.position = new Vector2(400, 400);
            pluto.sprite.scale = new Vector2(plutoScale, plutoScale);
            pluto.sprite.rotation = 0f;
            pluto.sprite.rectangle = new Rectangle(0, 0,(pluto.sprite.texture.Width/13), pluto.sprite.texture.Height/4);
            pluto.sprite.origin = new Vector2((pluto.sprite.texture.Width / 13)/ 2, (pluto.sprite.texture.Height / 4)/ 2);
            planetManager.Add(pluto);
        }

        public void LoadUserInterface() {
            Score score = new Score();
            score.font = Content.Load<SpriteFont>("ScoreFont");
            score.text = "Core Charge : ";
            score.value = 100;
            score.position = new Vector2(GraphicsDevice.Viewport.Width - 400, 50);
            score.color = Color.White;
            score.type = "SunHealth";
            scoreManager.Add(score);

            Score mainScore = new Score();
            mainScore.font = Content.Load<SpriteFont>("ScoreFont");
            mainScore.text = "Score : ";
            mainScore.value = 0;
            mainScore.position = new Vector2(50, 50);
            mainScore.color = Color.White;
            mainScore.type = "MainScore";
            scoreManager.Add(mainScore);
        }

        public void LoadAudio() {
            Audio audioBGM = new Audio();
            audioBGM.backgroundAudio = Content.Load<Song>("Background_Audio");
            audioManager.turnBGMOn(audioBGM);
        }

        #endregion
    }
}
