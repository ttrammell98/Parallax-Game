using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParallaxStarter
{
    public class Coin : ISprite
    {
        /// <summary>
        /// A spritesheet containing the coin image
        /// </summary>
        Texture2D spritesheet;

        /// <summary>
        /// The portion of the spritesheet that is the coin
        /// </summary>
        public Rectangle sourceRect = new Rectangle
        {
            X = 0,
            Y = 0,
            Width = 512,
            Height = 512
        };

        /// <summary>
        /// The origin of the sprite
        /// </summary>
        Vector2 origin = new Vector2(0, 0);

        /// <summary>
        /// The coin's position in the world
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Speed of the coin
        /// </summary>
        int speed = 85;

        /// <summary>
        /// Random for coin's spawn
        /// </summary>
        public Random random;

        /// <summary>
        /// Want coin to be coming from right to left
        /// </summary>
        Vector2 direction = new Vector2(-1, 0);


        /// <summary>
        /// Constructs coin
        /// </summary>
        /// <param name="spritesheet">The present's spritesheet</param>
        public Coin(Texture2D spritesheet, Player player, Random r)
        {
            this.spritesheet = spritesheet;
            this.random = r;
            spawnCoin(player);
        }

        /// <summary>
        /// Updates the coin
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime, Player player)
        {
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * speed * direction;
            checkOffScreen(player);
        }

        /// <summary>
        /// Draws the coin sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, 0, origin, 0.055f, SpriteEffects.None, 0.7f);
        }

        /// <summary>
        /// Spawns coin
        /// </summary>
        /// <param name="player"></param>
        public void spawnCoin(Player player)
        {
            Position = new Vector2(
                player.Position.X + 600,
                (float)random.Next(0, 420)
                );
        }

        /// <summary>
        /// Checks to see if the coin is off the screen
        /// </summary>
        /// <param name="player"></param>
        public void checkOffScreen(Player player)
        {
            if (Position.X < player.Position.X - 200)
            {
                spawnCoin(player);
            }
        }

        /// <summary>
        /// gets the scaled width of the coin
        /// </summary>
        /// <returns>scaled width of the coin</returns>
        public float getWidth()
        {
            return (0.0625f * sourceRect.Width);
        }

        /// <summary>
        /// gets the scaled height of the coin
        /// </summary>
        /// <returns>scaled height of the coin</returns>
        public float getHeight()
        {
            return (0.0625f * sourceRect.Height);
        }
    }
}
