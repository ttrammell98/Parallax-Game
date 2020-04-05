using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParallaxStarter
{
    public class Hook : ISprite
    {
        /// <summary>
        /// A spritesheet containing the hook image
        /// </summary>
        Texture2D spritesheet;

        /// <summary>
        /// The portion of the spritesheet that is the hook
        /// </summary>
        public Rectangle sourceRect = new Rectangle
        {
            X = 0,
            Y = 0,
            Width = 470,
            Height = 905
        };

        /// <summary>
        /// The origin of the fireball sprite
        /// </summary>
        Vector2 origin = new Vector2(100, 1);

        /// <summary>
        /// The hook's position in the world
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Speed of the hook
        /// </summary>
        int speed = 75;

        /// <summary>
        /// Random for hook's spawn
        /// </summary>
        public Random random;

        /// <summary>
        /// Want to hook to be coming from right to left
        /// </summary>
        Vector2 direction = new Vector2(-1, 0);

        int screenWidth = 800;

        /// <summary>
        /// Constructs a fireball
        /// </summary>
        /// <param name="spritesheet">The present's spritesheet</param>
        public Hook(Texture2D spritesheet)
        {
            this.spritesheet = spritesheet;
            this.random = new Random();
            spawnNewLocation();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * speed * direction;
            checkOffScreen();
        }

        /// <summary>
        /// Draws the hook sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, 0, origin, 0.055f, SpriteEffects.None, 0.7f);
        }

        public void spawnNewLocation()
        {
            Position = new Vector2(
                screenWidth,
                (float)random.Next(0, 420)
                );
        }

        private void checkOffScreen()
        {
            if (Position.X < 0)
            {
                //spawnNewLocation(player);
            }
        }
    }
}
