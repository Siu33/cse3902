using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using System;

namespace Project1.Models
{
    internal class StaticSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        public bool Visible { get; set; } = false;

        public StaticSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (!Visible) return;

            // Draw the entire texture as a static sprite
            spriteBatch.Draw(Texture, location, Color.White);
        }
    }
}
