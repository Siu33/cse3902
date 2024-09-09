using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Global;


namespace Project1
{
    internal class MouseController : IController
    {
        private Game1 game;
        private MouseState prevMouseState;


        public MouseController(Game1 game)
        {
            this.game = game;
            prevMouseState = Mouse.GetState();
        }

        public void Update()
        {
            prevMouseState = Mouse.GetState();
        }
        public string DetermineMouseAction(Point mousePosition, int screenWidth, int screenHeight)
        {
            if (mousePosition.X < screenWidth / 2 && mousePosition.Y < screenHeight / 2)
            {
                return "TopLeft";
            }
            else if (mousePosition.X >= screenWidth / 2 && mousePosition.Y < screenHeight / 2)
            {
                return "TopRight";
            }
            else if (mousePosition.X < screenWidth / 2 && mousePosition.Y >= screenHeight / 2)
            {
                return "BottomLeft";
            }
            else
            {
                return "BottomRight";
            }
        }

        public void HandleInput(Game1 game) {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            var screenWidth = game.GraphicsDevice.Viewport.Width;
            var screenHeight = game.GraphicsDevice.Viewport.Height;

            if (mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
            {
                game.mouseActions["RightClick"].Invoke();
            }
            else if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                string actionKey = DetermineMouseAction(mousePosition, screenWidth, screenHeight);
                if (game.mouseActions.ContainsKey(actionKey))
                {
                    game.mouseActions[actionKey].Invoke();
                }
            }
        }
    }

}
