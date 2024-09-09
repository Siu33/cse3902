using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class KeyboardController : IController
    {
        private Game1 game;
        private KeyboardState prevKeyboardState;

        public KeyboardController(Game1 game)
        {
            this.game = game;
            prevKeyboardState = Keyboard.GetState();

        }

        public void Update()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public void HandleInput(Game1 game)
        {
            var keyboardState = Keyboard.GetState();
            foreach(var keyCommand in game.keyCommands)
            {
                if(keyboardState.IsKeyDown(keyCommand.Key) && prevKeyboardState.IsKeyUp(keyCommand.Key))
                {

                    keyCommand.Value.Invoke();
                }

            }
        }




    }





}
