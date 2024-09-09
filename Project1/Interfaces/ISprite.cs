using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Interfaces
{
    public interface ISprite
    {
        bool Visible { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
