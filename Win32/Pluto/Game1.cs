using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Pluto.Custom_Class;
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
        Texture2D testCircle;
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
        int frameHeight = 328;
        int frameWidth = 54;
        int meteorPositionY = 1920;
        float meteorPositionFrameTime = 0.000001f;
        float meteorTime;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            spriteSheet = Content.Load<Texture2D>("CometAnimation");

            // TODO: use this.Content to load your game content here
            sunTexture = new Texture2D(graphics.GraphicsDevice, 50, 50);
           // planetTexture = new Texture2D(graphics.GraphicsDevice, 10, 10);
            planetTexture = Content.Load<Texture2D>("Pluto");

            asteroidTexture = new Texture2D(graphics.GraphicsDevice, 20, 20);
            // Planet distance calculation would require the sun texture to be initialized
            configurePlanetDistance();
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
            distanceFromCenter = Math.Max(sunTexture.Width, sunTexture.Height);
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

        #endregion

        #region Game1 CustomUpdate
        public void customUpdate(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            Color[] sunColorData = new Color[50 * 50];
            for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Chocolate;
            sunTexture.SetData(sunColorData);
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
            

            //var plutoDestination = new Rectangle(100, 100, 500, 500);
            testCircle = CreateCircle(Convert.ToInt32(plutoDistance));
            var plutoDestination = new Circle(screenCenter, plutoDistance);
            var neptuneDestination = new Circle(screenCenter, neptuneDistance);
            bool plutoMouseOver = plutoDestination.Contains(new Vector2(state.X, state.Y));
            bool neptuneMouseOver = neptuneDestination.Contains(new Vector2(state.X, state.Y));
            if (plutoMouseOver && !neptuneMouseOver)
            {
                Color[] newSunColorData = new Color[50 * 50];
                for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Red;
                sunTexture.SetData(sunColorData);
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

            meteorTime += (float)gameTime.ElapsedGameTime.TotalSeconds + 10;
            while (meteorTime > meteorPositionFrameTime)
            {
                meteorPositionY = meteorPositionY-5;
                meteorTime = 0f;
            }

            if (meteorPositionY < 0) {
                meteorPositionY = 1920;
            }

        }
        #endregion

        #region Game1 CustomDraw
        public void customDraw() {
            spriteBatch.Begin();

            spriteBatch.Draw(sunTexture, new Vector2(screenWidthCenter, screenHeightCenter), rotation: sunCircularRotationOffset, origin: new Vector2(sunTexture.Width / 2, sunTexture.Height / 2));

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

            spriteBatch.Draw(testCircle, new Vector2(screenWidthCenter - plutoDistance, screenHeightCenter - plutoDistance));


            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(frameIndex * frameWidth,

                                               0, frameWidth, frameHeight);
            // Calculate position and origin to draw in the center of the screen
            Vector2 position = new Vector2(this.Window.ClientBounds.Width / 2,
                                           meteorPositionY);
            Vector2 origin = new Vector2(frameWidth / 2.0f, frameHeight);
            // Draw the current frame.
            spriteBatch.Draw(spriteSheet, position, source, Color.White, 0.0f,
              origin, 1.0f, SpriteEffects.None, 0.0f);


            spriteBatch.End();
        }
        #endregion

        #region Game1 research
        public Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
        #endregion
    }
}
