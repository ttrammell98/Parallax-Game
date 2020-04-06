using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallaxStarter
{
    /// <summary>
    /// Font Sprite class
    /// </summary>
    public class Font : ISprite
    {
        /// <summary>
        /// Spritefont object
        /// </summary>
        SpriteFont font;

        /// <summary>
        /// Game score
        /// </summary>
        int score;

        /// <summary>
        /// Players # of lives
        /// </summary>
        int lives;
        /// <summary>
        /// Constructor for Font class
        /// </summary>
        /// <param name="font"></param>
        public Font(SpriteFont font, int score, int lives)
        {
            this.font = font;
            this.score = score;
            this.lives = lives;
        }

        /// <summary>
        /// Updates the font/on-screen text
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime, int score, int lives)
        {
            this.score = score;
            this.lives = lives;
        }

        /// <summary>
        /// Draws the font/text 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Lives: " + lives, new Vector2(730, 0), Color.White);
        }
    }
}
