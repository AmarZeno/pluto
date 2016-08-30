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
        Texture2D sunTexture;
        float sunCircularRotationOffset;
        Texture2D planetTexture;
        float rotationAngleInRadians;
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
        Texture2D testCircle;

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

            // TODO: use this.Content to load your game content here
            sunTexture = new Texture2D(graphics.GraphicsDevice, 50, 50);
           // planetTexture = new Texture2D(graphics.GraphicsDevice, 10, 10);
            planetTexture = Content.Load<Texture2D>("Pluto");

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

            customUpdate();

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
            rotationAngleInRadians = 0;
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
            plutoSpeed = plutoSpeed + 0.005f;
        }

        #endregion

        #region Game1 CustomUpdate
        public void customUpdate()
        {
            MouseState state = Mouse.GetState();

            Color[] sunColorData = new Color[50 * 50];
            for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Chocolate;
            sunTexture.SetData(sunColorData);

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
                    // Do cool stuff here
                    plutoSpeed = plutoSpeed + 0.009f;
                }
                
                if (state.RightButton == ButtonState.Pressed)
                {
                    plutoSpeed = plutoSpeed - 0.003f;
                }
            }
                
  
        }
        #endregion

        #region Game1 CustomDraw
        public void customDraw() {
            spriteBatch.Begin();

            spriteBatch.Draw(sunTexture, new Vector2(screenWidthCenter, screenHeightCenter), rotation: sunCircularRotationOffset, origin: new Vector2(sunTexture.Width / 2, sunTexture.Height / 2));

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
