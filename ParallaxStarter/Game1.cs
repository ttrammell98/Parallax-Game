using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace ParallaxStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        int score = 0;
        int lives = 3;
        Player player;
        Hook hook;
        Font font;
        Candy candy;
        Coin coin;
        Random r = new Random();
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

            //Font
            spriteFont = Content.Load<SpriteFont>("Score");
            font = new Font(spriteFont, score, lives);
            var fontLayer = new ParallaxLayer(this);
            fontLayer.Sprites.Add(font);
            fontLayer.DrawOrder = 5;
            Components.Add(fontLayer);

            // TODO: use this.Content to load your game content here
            var spritesheet = Content.Load<Texture2D>("fish");
            var backgroundTexture = Content.Load<Texture2D>("Background");

            player = new Player(spritesheet);
            var playerLayer = new ParallaxLayer(this);
            playerLayer.Sprites.Add(player);
            playerLayer.DrawOrder = 2;
            Components.Add(playerLayer);

         
            //Hook
            var hookTexture = Content.Load<Texture2D>("hook");
            hook = new Hook(hookTexture, player);
            var hookLayer = new ParallaxLayer(this);
            hookLayer.Sprites.Add(hook);
            hookLayer.DrawOrder = 2;
            Components.Add(hookLayer);

            //Candy
            var candyTexture = Content.Load<Texture2D>("candy");
            candy = new Candy(candyTexture, player);
            var candyLayer = new ParallaxLayer(this);
            candyLayer.Sprites.Add(candy);
            candyLayer.DrawOrder = 2;
            Components.Add(candyLayer);


            //Coin
            var coinTexture = Content.Load<Texture2D>("coin");
            coin = new Coin(coinTexture, player, r);
            var coinLayer = new ParallaxLayer(this);
            coinLayer.Sprites.Add(coin);
            coinLayer.DrawOrder = 2;
            Components.Add(coinLayer);

            var backgroundSprite = new StaticSprite(backgroundTexture, new Vector2(0, 0));
            var backgroundLayer = new ParallaxLayer(this);
            backgroundLayer.Sprites.Add(backgroundSprite);
            backgroundLayer.DrawOrder = 0;
            Components.Add(backgroundLayer);

            var midgroundTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("midground1"),
                Content.Load<Texture2D>("midground2")
            };

            var midgroundSprites = new StaticSprite[]
            {
                new StaticSprite(midgroundTextures[0], new Vector2(0, 0)),
                new StaticSprite(midgroundTextures[1], new Vector2(3900, 0))
            };

            //midground
            var midgroundLayer = new ParallaxLayer(this);
            midgroundLayer.Sprites.AddRange(midgroundSprites);
            midgroundLayer.DrawOrder = 1;
            //var midgroundScrollController = midgroundLayer.ScrollController as AutoScrollController;
            //midgroundScrollController.Speed = 40f;
            Components.Add(midgroundLayer);

            //foreground
            var foregroundTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("foreground1"),
                Content.Load<Texture2D>("foreground2"),
                Content.Load<Texture2D>("foreground3"),
                Content.Load<Texture2D>("foreground4"),
                Content.Load<Texture2D>("foreground5"),
                Content.Load<Texture2D>("foreground6"),
                Content.Load<Texture2D>("foreground7")
            };

            var foregroundSprites = new List<StaticSprite>();
            for (int i = 0; i < foregroundTextures.Count; i++)
            {
                var position = new Vector2(i * 3500, 0);
                var sprite = new StaticSprite(foregroundTextures[i], position);
                foregroundSprites.Add(sprite);
            }
            var foregroundLayer = new ParallaxLayer(this);
            foreach (var sprite in foregroundSprites)
            {
                foregroundLayer.Sprites.Add(sprite);
            }

            foregroundLayer.DrawOrder = 4; //draw order in front of the player layer

            Components.Add(foregroundLayer);

            backgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.1f);
            midgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.4f);
            playerLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            foregroundLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            hookLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            candyLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            coinLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            fontLayer.ScrollController = new PlayerTrackingScrollController(player, 0.0f);
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
            var keyboard = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (lives <= 0)
            {
                Exit();
            }

            // TODO: Add your update logic here
            player.Update(gameTime);
            hook.Update(gameTime, player);
            candy.Update(gameTime, player);
            coin.Update(gameTime, player);
            font.Update(gameTime, score, lives);

            checkHookCollision();
            checkCandyCollision();
            checkCoinCollision();

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

            base.Draw(gameTime);
        }

        public int GetHeight()
        {
            return graphics.PreferredBackBufferHeight;
        }

        private void checkHookCollision()
        {
            if (player.Position.X < hook.Position.X + hook.getHookWidth() && player.Position.X + player.getFishWidth() > hook.Position.X
             && player.Position.Y < hook.Position.Y + hook.getHookHeight() && player.Position.Y + player.getFishHeight() > hook.Position.Y)
            {
                hook.spawnHook(player);
                lives--;
            }
        }

        private void checkCandyCollision()
        {
            if (player.Position.X < candy.Position.X + candy.getWidth() && player.Position.X + player.getFishWidth() > candy.Position.X
             && player.Position.Y < candy.Position.Y + candy.getHeight() && player.Position.Y + player.getFishHeight() > candy.Position.Y)
            {
                candy.spawnCandy(player); 
                lives++;
            }
        }

        private void checkCoinCollision()
        {
            if (player.Position.X < coin.Position.X + coin.getWidth() && player.Position.X + player.getFishWidth() > coin.Position.X
             && player.Position.Y < coin.Position.Y + coin.getHeight() && player.Position.Y + player.getFishHeight() > coin.Position.Y)
            {
                coin.spawnCoin(player);
                score++;
            }
        }
    }
}
