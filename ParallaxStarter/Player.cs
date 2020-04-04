using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace ParallaxStarter
{
    public class Player : ISprite
    {
        /// <summary>
        /// A spritesheet containing a helicopter image
        /// </summary>
        Texture2D spritesheet;

        /// <summary>
        /// The portion of the spritesheet that is the helicopter
        /// </summary>
        Rectangle sourceRect = new Rectangle
        {
            X = 0,
            Y = 0,
            Width = 475, //scaled by 0.2 = 95
            Height = 339 //scaled by 0.2 = 67.8
        };

        /// <summary>
        /// The origin of the helicopter sprite
        /// </summary>
        Vector2 origin = new Vector2(0, 0);

        /// <summary>
        /// The angle the helicopter should tilt
        /// </summary>
        float angle = 0;

        /// <summary>
        /// The player's position in the world
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// How fast the player moves
        /// </summary>
        public float Speed { get; set; } = 100;

        Game1 game;
        /// <summary>
        /// Constructs a player
        /// </summary>
        /// <param name="spritesheet">The player's spritesheet</param>
        public Player(Texture2D spritesheet)
        {
            this.spritesheet = spritesheet;
            this.Position = new Vector2(200, 200);
        }

        /// <summary>
        /// Updates the player position based on Keyboard Input
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;
            float fishWidth = (0.2f * sourceRect.Width);
            float fishHeight = (0.2f * sourceRect.Height);


            // Override with keyboard input
            var keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A))
            {
                direction.X -= 1;
                angle = 0;
            }
            if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D)) 
            {
                direction.X += 1;
                angle = 0;
            }
            if(keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W))
            {
                direction.Y -= 1;
                angle = 0.5f * direction.Y;
            }
            if(keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S))
            {
                direction.Y += 1;
                angle = 0.5f * direction.Y;
            }

            if(Position.Y <= 0)
            {
                angle = 0;
                Position.Y = 0;
            }
            if (Position.Y + fishHeight >= 480)
            {
                angle = 0;
                Position.Y = 480 - fishHeight;
            }
         
            // Move the fish
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * direction;
        }

        /// <summary>
        /// Draws the player sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Render the helicopter, rotating about the rotors
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, angle, origin, 0.2f, SpriteEffects.None, 0.7f);
        }

    }
}
