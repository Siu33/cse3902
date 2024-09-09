using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    internal class TextSprite : ISprite
    {
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public bool Visible { get; set; } = true;

        public TextSprite(SpriteFont font, string text, Vector2 position, Color color)
        {
            Font = font;
            Text = text;
            Position = position;
            Color = color;
        }

        public void Update()
        {
            // No update logic needed for static text
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (Visible)
            {
                spriteBatch.DrawString(Font, Text, Position, Color);
            }
        }

    }
}
