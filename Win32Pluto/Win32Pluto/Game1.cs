using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        const float sunScale = 0.3f;
        const float planetScale = 0.03f;

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
            sunManager.Update();
            orbitManager.Update(GraphicsDevice, planetManager);
            planetManager.Update(GraphicsDevice.Viewport);
            asteroidManager.Update(gameTime, GraphicsDevice, sunManager, scoreManager);

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
            asteroid.sprite.texture = Content.Load<Texture2D>("Brown_Comet");
            asteroid.sprite.position = new Vector2(asteroid.sprite.texture.Width / 2 , 1920);
            asteroid.sprite.rotation = 0f;
            asteroid.sprite.scale = new Vector2(1f, 1f);
            asteroid.sprite.origin = new Vector2((asteroid.sprite.texture.Width / 29)/2, asteroid.sprite.texture.Height / 2);
            asteroidManager.Add(asteroid);
        }

        public void LoadSpace() {
            Space space = new Space();
            space.sprite.texture = Content.Load<Texture2D>("Space");
            space.sprite.position = new Vector2(0, 0);
            space.sprite.rotation = 0f;
            space.sprite.scale = new Vector2(1f, 1f);
            space.sprite.origin = new Vector2(space.sprite.texture.Width / 2, space.sprite.texture.Height / 2);
            spaceManager.Add(space);
        }

        public void LoadSun() {
            Sun sun = new Sun();
            sun.sprite.texture = Content.Load<Texture2D>("Sun");
            sun.sprite.scale = new Vector2(sunScale, sunScale);
            sun.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            sun.sprite.rotation = 0f;
            sun.sprite.origin = new Vector2(sun.sprite.texture.Width /2, sun.sprite.texture.Height /2);
            sunManager.Add(sun);
        }

        public void LoadOrbits() {
            // Mercury
            Orbit mercuryOrbit = new Orbit();
            mercuryOrbit.name = "MercuryOrbit";
            mercuryOrbit.radius = 100;
            mercuryOrbit.defaultTexture = Content.Load<Texture2D>("Orbit_Normal");
            mercuryOrbit.selectedTexture = Content.Load<Texture2D>("Orbit_Selected");
            mercuryOrbit.sprite.texture = mercuryOrbit.defaultTexture;
            mercuryOrbit.sprite.scale = new Vector2(0.22f, 0.22f);
            mercuryOrbit.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - (mercuryOrbit.sprite.texture.Width * mercuryOrbit.sprite.scale.X) / 2, GraphicsDevice.Viewport.Bounds.Height / 2 - (mercuryOrbit.sprite.texture.Height * mercuryOrbit.sprite.scale.Y) / 2);
            mercuryOrbit.sprite.rotation = 0f;
            mercuryOrbit.sprite.origin = new Vector2(mercuryOrbit.sprite.texture.Width / 2, mercuryOrbit.sprite.texture.Height / 2);
            orbitManager.Add(mercuryOrbit);

            // Venus
            Orbit venusOrbit = new Orbit();
            venusOrbit.name = "VenusOrbit";
            venusOrbit.radius = 200;
            venusOrbit.defaultTexture = Content.Load<Texture2D>("Orbit_Normal");
            venusOrbit.selectedTexture = Content.Load<Texture2D>("Orbit_Selected");
            venusOrbit.sprite.texture = venusOrbit.defaultTexture;
            venusOrbit.sprite.scale = new Vector2(0.43f, 0.43f);
            venusOrbit.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - (venusOrbit.sprite.texture.Width * venusOrbit.sprite.scale.X) / 2, GraphicsDevice.Viewport.Bounds.Height / 2 - (venusOrbit.sprite.texture.Height * venusOrbit.sprite.scale.Y) / 2);
            venusOrbit.sprite.rotation = 0f;
            venusOrbit.sprite.origin = new Vector2(venusOrbit.sprite.texture.Width / 2, venusOrbit.sprite.texture.Height / 2);
            orbitManager.Add(venusOrbit);

            // Earth
            Orbit earthOrbit = new Orbit();
            earthOrbit.name = "EarthOrbit";
            earthOrbit.radius = 300;
            earthOrbit.defaultTexture = Content.Load<Texture2D>("Orbit_Normal");
            earthOrbit.selectedTexture = Content.Load<Texture2D>("Orbit_Selected");
            earthOrbit.sprite.texture = earthOrbit.defaultTexture;
            earthOrbit.sprite.scale = new Vector2(0.64f, 0.64f);
            earthOrbit.sprite.position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - (earthOrbit.sprite.texture.Width * earthOrbit.sprite.scale.X)/ 2, GraphicsDevice.Viewport.Bounds.Height / 2 - (earthOrbit.sprite.texture.Height * earthOrbit.sprite.scale.Y) / 2);
            earthOrbit.sprite.rotation = 0f;
            earthOrbit.sprite.origin = new Vector2(earthOrbit.sprite.texture.Width / 2, earthOrbit.sprite.texture.Height / 2);
            orbitManager.Add(earthOrbit);
        }

        public void LoadPlanets() {
            // Mercury
            Planet mercury = new Planet();
            mercury.name = "Mercury";
            mercury.radius = 100;
            mercury.sprite.texture = Content.Load<Texture2D>("Pluto");
            mercury.sprite.position = new Vector2(300, 300);
            mercury.sprite.scale = new Vector2(planetScale, planetScale);
            mercury.sprite.rotation = 0f;
            mercury.sprite.origin = new Vector2(mercury.sprite.texture.Width / 2, mercury.sprite.texture.Height / 2);
            planetManager.Add(mercury);

            // Venus
            Planet venus = new Planet();
            venus.name = "Venus";
            venus.radius = 200;
            venus.sprite.texture = Content.Load<Texture2D>("Pluto");
            venus.sprite.position = new Vector2(300, 300);
            venus.sprite.scale = new Vector2(planetScale, planetScale);
            venus.sprite.rotation = 0f;
            venus.sprite.origin = new Vector2(venus.sprite.texture.Width / 2, venus.sprite.texture.Height / 2);
            planetManager.Add(venus);

            // Earth
            Planet earth = new Planet();
            earth.name = "Earth";
            earth.radius = 300;
            earth.sprite.texture = Content.Load<Texture2D>("Pluto");
            earth.sprite.position = new Vector2(400, 400);
            earth.sprite.scale = new Vector2(planetScale, planetScale);
            earth.sprite.rotation = 0f;
            earth.sprite.origin = new Vector2(earth.sprite.texture.Width / 2, earth.sprite.texture.Height / 2);
            planetManager.Add(earth);
        }

        public void LoadUserInterface() {
            Score score = new Score();
            score.font = Content.Load<SpriteFont>("ScoreFont");
            score.text = "Sun Health : ";
            score.sunHealth = 100;
            score.position = new Vector2(100, 100);
            score.color = Color.White;
            score.type = "SunHealth";
            scoreManager.Add(score);
        }

        public void LoadAudio() {
            Audio audioBGM = new Audio();
            audioBGM.backgroundAudio = Content.Load<Song>("Background_Audio");
            audioManager.turnBGMOn(audioBGM);
        }

        #endregion
    }
}
