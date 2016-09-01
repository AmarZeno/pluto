using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Pluto.Custom_Class;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Pluto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D asteroidTexture;
        Texture2D sunTexture;
        Texture2D planetTexture;
        Texture2D backgroundTexture;
        float sunCircularRotationOffset;
        Vector2 mercuryPosition;
        Vector2 venusPosition;
        Vector2 earthPosition;
        Vector2 marsPosition;
        Vector2 jupiterPosition;
        Vector2 saturnPosition;
        Vector2 uranusPosition;
        Vector2 neptunePosition;
        Vector2 plutoPosition;
        int screenWidthCenter;
        int screenHeightCenter;
        float distanceFromCenter;
        int mercurySize;
        int venusSize;
        int earthSize;
        int marsSize;
        int jupiterSize;
        int saturnSize;
        int uranusSize;
        int neptuneSize;
        int plutoSize;
        float mercuryDistance;
        float venusDistance;
        float earthDistance;
        float marsDistance;
        float jupiterDistance;
        float saturnDistance;
        float uranusDistance;
        float neptuneDistance;
        float plutoDistance;
        float mercurySpeed;
        float venusSpeed;
        float earthSpeed;
        float marsSpeed;
        float jupiterSpeed;
        float saturnSpeed;
        float uranusSpeed;
        float neptuneSpeed;
        float plutoSpeed;
        Rectangle plutoRectangle;
        List<Orbit> orbitCollection;
        Texture2D orbitNormalTexture;
        Texture2D orbitSelectedTexture;
        Texture2D mercuryCurrentTexture;
        Texture2D venusCurrentTexture;
        Texture2D earthCurrentTexture;
        Texture2D marsCurrentTexture;
        Texture2D jupiterCurrentTexture;
        Texture2D saturnCurrentTexture;
        Texture2D uranusCurrentTexture;
        Texture2D neptuneCurrentTexture;
        Texture2D plutoCurrentTexture;
        Rectangle blueAsteroidRectangle;
        Rectangle blueAsteroidVirtualRectangle;
        Vector2 blueAsteroidPosition;
        Song backgroundAudio;
        // Animation variables
        // the spritesheet containing our animation frames
        Texture2D spriteSheet;
        // the elapsed amount of time the frame has been shown for
        float time;
        // duration of time to show each frame
        float frameTime = 0.01f;
        // an index of the current frame being shown
        int frameIndex = 1;
        // total number of frames in our spritesheet
        const int totalFrames = 29;
        // define the size of our animation frame
        int frameHeight = 164;
        int frameWidth = 27;
        int meteorPositionY = 1920;
        float meteorPositionFrameTime = 0.000001f;
        float meteorTime;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            initializeVariables();
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
            this.IsMouseVisible = true;
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
            spriteSheet = Content.Load<Texture2D>("CometAnimationSmall2");
            backgroundAudio = Content.Load<Song>("Background_Audio");
            // TODO: use this.Content to load your game content here
            // sunTexture = new Texture2D(graphics.GraphicsDevice, 50, 50);
            sunTexture = Content.Load<Texture2D>("TheSun");
           // planetTexture = new Texture2D(graphics.GraphicsDevice, 10, 10);
            planetTexture = Content.Load<Texture2D>("Pluto");
            backgroundTexture = Content.Load<Texture2D>("SpaceBackground");
            asteroidTexture = new Texture2D(graphics.GraphicsDevice, 20, 20);
            orbitNormalTexture = Content.Load<Texture2D>("OrbitNormal");
            orbitSelectedTexture = Content.Load<Texture2D>("OrbitSelected");
            // Planet distance calculation would require the sun texture to be initialized
            configurePlanetDistance();
            turnAudioOn();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            sunTexture.Dispose();
            planetTexture.Dispose();
            asteroidTexture.Dispose();
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

            customUpdate(gameTime);

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

            customDraw();
   
            base.Draw(gameTime);
        }

        #region Game1 CustomAccessors
        public void initializeVariables() {
            orbitCollection = new List<Orbit>();
            sunCircularRotationOffset = 0;
            mercuryPosition = new Vector2();
            configurePlanetSize();
        }

        public void configurePlanetSize() {
            mercurySize = 40;
            venusSize = 40;
            earthSize = 40;
            marsSize = 40;
            jupiterSize = 40;
            saturnSize = 40;
            uranusSize = 40;
            neptuneSize = 40;
            plutoSize = 40;
        }

        public void configurePlanetDistance() {
            distanceFromCenter = Math.Max(50, 50);
            mercuryDistance = distanceFromCenter + 50;
            venusDistance = mercuryDistance + 50;
            earthDistance = venusDistance + 50;
            marsDistance = earthDistance + 50;
            jupiterDistance = marsDistance + 50;
            saturnDistance = jupiterDistance + 50;
            uranusDistance = saturnDistance + 50;
            neptuneDistance = uranusDistance + 50;
            plutoDistance = neptuneDistance + 50;
        }

        public void configurePlanetSpeed() {
            mercurySpeed = mercurySpeed + 0.01f;
            venusSpeed = venusSpeed + 0.005f;
            earthSpeed = earthSpeed + 0.004f;
            marsSpeed = marsSpeed + 0.007f;
            jupiterSpeed = jupiterSpeed + 0.004f;
            saturnSpeed = saturnSpeed + 0.007f;
            uranusSpeed = uranusSpeed + 0.01f;
            neptuneSpeed = neptuneSpeed + 0.01f;
           // float defaultPlutoSpeed = plutoSpeed;
           // plutoSpeed = MathHelper.Lerp(defaultPlutoSpeed, plutoSpeed + 0.005f, 0.5f);
            plutoSpeed = plutoSpeed + 0.005f;
        }

        public void turnAudioOn() {
            MediaPlayer.Play(backgroundAudio);
            MediaPlayer.IsRepeating = true;
        }

        #endregion

        #region Game1 CustomUpdate
        public void customUpdate(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            Color[] sunColorData = new Color[50 * 50];
            for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Chocolate;
           // sunTexture.SetData(sunColorData);
            asteroidTexture.SetData(sunColorData);

            sunCircularRotationOffset = sunCircularRotationOffset + 0.01f;
            screenWidthCenter = GraphicsDevice.Viewport.Width / 2;
            screenHeightCenter = GraphicsDevice.Viewport.Height / 2;

            configurePlanetSpeed();

            Vector2 screenCenter = new Vector2(screenWidthCenter, screenHeightCenter);
            mercuryPosition = screenCenter + Vector2.Transform(new Vector2(mercuryDistance, 0),
                                                          Matrix.CreateRotationZ(mercurySpeed));
            venusPosition = screenCenter + Vector2.Transform(new Vector2(venusDistance, 0),
                                                          Matrix.CreateRotationZ(venusSpeed));
            earthPosition = screenCenter + Vector2.Transform(new Vector2(earthDistance, 0),
                                                          Matrix.CreateRotationZ(earthSpeed));
            marsPosition = screenCenter + Vector2.Transform(new Vector2(marsDistance, 0),
                                                          Matrix.CreateRotationZ(marsSpeed));
            jupiterPosition = screenCenter + Vector2.Transform(new Vector2(jupiterDistance, 0),
                                              Matrix.CreateRotationZ(jupiterSpeed));
            saturnPosition = screenCenter + Vector2.Transform(new Vector2(saturnDistance, 0),
                                              Matrix.CreateRotationZ(saturnSpeed));
            uranusPosition = screenCenter + Vector2.Transform(new Vector2(uranusDistance, 0),
                                              Matrix.CreateRotationZ(uranusSpeed));
            neptunePosition = screenCenter + Vector2.Transform(new Vector2(neptuneDistance, 0),
                                              Matrix.CreateRotationZ(neptuneSpeed));
            plutoPosition = screenCenter + Vector2.Transform(new Vector2(plutoDistance, 0),
                                              Matrix.CreateRotationZ(plutoSpeed));


            mercuryCurrentTexture = orbitNormalTexture;
            venusCurrentTexture = orbitNormalTexture;
            earthCurrentTexture = orbitNormalTexture;
            marsCurrentTexture = orbitNormalTexture;
            jupiterCurrentTexture = orbitNormalTexture;
            saturnCurrentTexture = orbitNormalTexture;
            uranusCurrentTexture = orbitNormalTexture;
            neptuneCurrentTexture = orbitNormalTexture;
            plutoCurrentTexture = orbitNormalTexture;

            //var plutoDestination = new Rectangle(100, 100, 500, 500);
            var plutoDestination = new Circle(screenCenter, plutoDistance);
            var neptuneDestination = new Circle(screenCenter, neptuneDistance + 20);
            var UranusDestination = new Circle(screenCenter, uranusDistance);
            bool plutoMouseOver = plutoDestination.Contains(new Vector2(state.X, state.Y));
            bool neptuneMouseOver = neptuneDestination.Contains(new Vector2(state.X, state.Y));
            bool uranusMouseOver = UranusDestination.Contains(new Vector2(state.X, state.Y));
            if (plutoMouseOver && !neptuneMouseOver)
            {
                Color[] newSunColorData = new Color[50 * 50];
                for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Red;
              //  sunTexture.SetData(sunColorData);
                plutoCurrentTexture = orbitSelectedTexture;
                if (state.LeftButton == ButtonState.Pressed)
                {
                    float defaultPlutoSpeed = plutoSpeed;
                    plutoSpeed = MathHelper.Lerp(defaultPlutoSpeed, plutoSpeed + 0.009f, 1.0f);
                    //plutoSpeed = plutoSpeed + 0.009f;
                }

                if (state.RightButton == ButtonState.Pressed)
                {
                    float defaultPlutoSpeed = plutoSpeed;
                    // plutoSpeed = MathHelper.Lerp(defaultPlutoSpeed, plutoSpeed - 0.003f, 0.1f);
                    plutoSpeed = plutoSpeed - 0.003f;
                }
            }
            else if (neptuneMouseOver && !uranusMouseOver) {
                Color[] newSunColorData = new Color[50 * 50];
                for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Red;
               // sunTexture.SetData(sunColorData);
                neptuneCurrentTexture = orbitSelectedTexture;
                if (state.LeftButton == ButtonState.Pressed)
                {
                    float defaultNeptuneSpeed = plutoSpeed;
                    neptuneSpeed = MathHelper.Lerp(defaultNeptuneSpeed, neptuneSpeed + 0.009f, 1.0f);
                    //plutoSpeed = plutoSpeed + 0.009f;
                }

                if (state.RightButton == ButtonState.Pressed)
                {
                    float defaultNeptuneSpeed = neptuneSpeed;
                    // plutoSpeed = MathHelper.Lerp(defaultPlutoSpeed, plutoSpeed - 0.003f, 0.1f);
                    neptuneSpeed = neptuneSpeed - 0.003f;
                }
            }

           // Process elapsed time
           time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > frameTime)
            {
                // Play the next frame in the SpriteSheet
                frameIndex++;

                // reset elapsed time
                time = 0f;
            }

            if (frameIndex > totalFrames) frameIndex = 1;

            // Movement

            meteorTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (meteorTime > meteorPositionFrameTime)
            {
                meteorPositionY = meteorPositionY-5;
                meteorTime = 0f;
            }

            if (meteorPositionY < 0) {
                meteorPositionY = 1920;
            }

            

            // Calculate position and origin to draw in the center of the screen
            blueAsteroidPosition = new Vector2(this.Window.ClientBounds.Width / 2,
                                           (int)meteorPositionY);

            // Calculate the source rectangle of the current frame.
            blueAsteroidRectangle = new Rectangle(frameIndex * frameWidth,
                                               0, frameWidth, frameHeight);

            // Creating virtual rectangle to avoid animation issues
            blueAsteroidVirtualRectangle = new Rectangle((int)blueAsteroidPosition.X, (int)blueAsteroidPosition.Y, frameWidth, frameHeight);

            Rectangle tempPlutoRectangle = new Rectangle((int)plutoPosition.X, (int)plutoPosition.Y, plutoSize, plutoSize);
            plutoRectangle = new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(plutoDistance) + 130, 2 * Convert.ToInt32(plutoDistance) + 130);
            bool doesCollideAsteroid = tempPlutoRectangle.Intersects(blueAsteroidVirtualRectangle);
            if (doesCollideAsteroid) {
                plutoSize = plutoSize + 1;
                meteorPositionY = 1920;
                System.Diagnostics.Debug.Write("hellop");
            }
            
        }
        #endregion

        #region Game1 CustomDraw
        public void customDraw() {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, destinationRectangle: new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height));

            spriteBatch.Draw(mercuryCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(mercuryDistance) + 40, 2 * Convert.ToInt32(mercuryDistance) + 40), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(venusCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(venusDistance) + 40, 2 * Convert.ToInt32(venusDistance) + 40), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(earthCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(earthDistance) + 50, 2 * Convert.ToInt32(earthDistance) + 50), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(marsCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(marsDistance) + 70, 2 * Convert.ToInt32(marsDistance) + 70), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(jupiterCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(jupiterDistance) + 80, 2 * Convert.ToInt32(jupiterDistance) + 80), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(saturnCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(saturnDistance) + 90, 2 * Convert.ToInt32(saturnDistance) + 90), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(uranusCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(uranusDistance) + 110, 2 * Convert.ToInt32(uranusDistance) + 110), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(neptuneCurrentTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 2 * Convert.ToInt32(neptuneDistance) + 130, 2 * Convert.ToInt32(neptuneDistance) + 130), origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));
            spriteBatch.Draw(plutoCurrentTexture, destinationRectangle: plutoRectangle, origin: new Vector2(orbitNormalTexture.Width / 2.0f, orbitNormalTexture.Height / 2));

            //   spriteBatch.Draw(sunTexture, new Vector2(screenWidthCenter, screenHeightCenter), rotation: sunCircularRotationOffset, origin: new Vector2(sunTexture.Width / 2, sunTexture.Height / 2));
            spriteBatch.Draw(sunTexture, destinationRectangle: new Rectangle(screenWidthCenter, screenHeightCenter, 700, 700), rotation: sunCircularRotationOffset, origin: new Vector2(sunTexture.Width/2, sunTexture.Height/2));
            // modify this
            spriteBatch.Draw(asteroidTexture, new Vector2(screenWidthCenter, screenHeightCenter), origin: new Vector2(100, 100));

            spriteBatch.Draw(planetTexture, destinationRectangle:new Rectangle(Convert.ToInt32(mercuryPosition.X - mercurySize / 2) , Convert.ToInt32(mercuryPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(venusPosition.X - venusSize / 2), Convert.ToInt32(venusPosition.Y - venusSize / 2), venusSize, venusSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(earthPosition.X - earthSize / 2), Convert.ToInt32(earthPosition.Y - earthSize / 2), mercurySize, earthSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(marsPosition.X - marsSize / 2), Convert.ToInt32(marsPosition.Y - marsSize / 2), marsSize, marsSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(jupiterPosition.X - jupiterSize / 2), Convert.ToInt32(jupiterPosition.Y - jupiterSize / 2), jupiterSize, jupiterSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(saturnPosition.X - saturnSize / 2), Convert.ToInt32(saturnPosition.Y - saturnSize / 2), saturnSize, saturnSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(uranusPosition.X - uranusSize / 2), Convert.ToInt32(uranusPosition.Y - uranusSize / 2), uranusSize, uranusSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(neptunePosition.X - neptuneSize / 2), Convert.ToInt32(neptunePosition.Y - neptuneSize / 2), neptuneSize, neptuneSize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(plutoPosition.X - plutoSize / 2), Convert.ToInt32(plutoPosition.Y - plutoSize / 2), plutoSize, plutoSize));

            Vector2 origin = new Vector2(frameWidth / 2.0f, frameHeight);
            // Draw the current frame.
               spriteBatch.Draw(spriteSheet, blueAsteroidPosition, blueAsteroidRectangle, Color.White, 0.0f,
               origin, 1.0f, SpriteEffects.None, 0.0f);
           // spriteBatch.Draw(spriteSheet, blueAsteroidRectangle, Color.White);

            spriteBatch.End();
        }
        #endregion
    }
}
