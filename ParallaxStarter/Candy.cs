using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParallaxStarter
{
    public class Candy : ISprite
    {
        /// <summary>
        /// A spritesheet containing the candy image
        /// </summary>
        Texture2D spritesheet;

        /// <summary>
        /// The portion of the spritesheet that is the candy
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
        /// The candy's position in the world
        /// </summary>
        public Vector2 Position;


        /// <summary>
        /// Random for candy's spawn
        /// </summary>
        public Random random;



        /// <summary>
        /// Constructs a piece of candy
        /// </summary>
        /// <param name="spritesheet">The present's spritesheet</param>
        public Candy(Texture2D spritesheet, Player player)
        {
            this.spritesheet = spritesheet;
            this.random = new Random();
            spawnCandy(player);
        }

        /// <summary>
        /// Updates the candy
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime, Player player)
        {
            respawnCandy(player);
        }

        /// <summary>
        /// Draws the candy sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, 0, origin, 0.0625f, SpriteEffects.None, 0.7f);
        }

        /// <summary>
        /// Spawns the candy
        /// </summary>
        /// <param name="player">the Game's player</param>
        public void spawnCandy(Player player)
        {
            Position = new Vector2(
                (float)random.Next((int)player.Position.X+1000, (int)player.Position.X + 1600),
                (float)random.Next(0, 420)
                );
        }

        /// <summary>
        /// gets the scaled width of the candy
        /// </summary>
        /// <returns>candy width</returns>
        public float getWidth()
        {
            return (0.0625f * sourceRect.Width);
        }

        /// <summary>
        /// gets the scaled height of the candy
        /// </summary>
        /// <returns>height of candy</returns>
        public float getHeight()
        {
            return (0.0625f * sourceRect.Height);
        }

        /// <summary>
        /// Respawns candy after it's been collided with
        /// </summary>
        /// <param name="player">game player</param>
        public void respawnCandy(Player player)
        {
            if (Position.X < (player.Position.X - 200) - 600)
            {
                spawnCandy(player);
            }
        }

    }
}
