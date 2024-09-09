using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Global;
using Project1.Interfaces;
using Project1.Models;
using System;
using System.Collections.Generic;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont textFont;

        private string creditStr = "Credits";
        private string authorString = "Program Made By: Bladen Siu";
        private string urlString = "sprites from: https://www.mariouniverse.com/wp-content/img/sprites/nes/smb/luigi.png";
        private Vector2 authorPosition;
        private Vector2 creditPosition;
        private Vector2 urlPosition;



        private Animations marioStandingStill;
        private Animations animatedSprite;
        private Animations runningSprite;
        private Animations jumpingSprite;

        private TextSprite creditTextSprite;
        private TextSprite authorTextSprite;
        private TextSprite urlTextSprite;

        private bool reverseX;
        private bool reverseY;

        private int runningXValue;
        private int jumpingYValue;

        public Dictionary<Keys, Action> keyCommands;
        public Dictionary<string, Action> mouseActions;

        private IController keyboardController;
        private IController mouseController;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            keyboardController = new KeyboardController(this);
            mouseController = new MouseController(this);
   
            runningXValue = 600;
            jumpingYValue = 400;
            reverseX = false;
            reverseY = false;

            keyCommands = new Dictionary<Keys, Action>
            {
                {Keys.D0, Exit},
                {Keys.D1, () => DisplaySprite(marioStandingStill)},
                {Keys.D2, () => DisplaySprite(animatedSprite)},
                {Keys.D3, () => DisplaySprite(jumpingSprite)},
                {Keys.D4, () => DisplaySprite(runningSprite)}
            };

            mouseActions = new Dictionary<string, Action>
            {
                {"TopLeft", () => DisplaySprite(marioStandingStill)},
                {"TopRight", () => DisplaySprite(animatedSprite)},
                {"BottomLeft", () => DisplaySprite(jumpingSprite)},
                {"BottomRight", () => DisplaySprite(runningSprite)},
                {"RightClick", Exit}
            };
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textFont = Content.Load<SpriteFont>("TextFonts/File");
            authorPosition = new Vector2(100, 250);
            creditPosition = new Vector2(100, 220);
            urlPosition = new Vector2(100, 280);

      
            Texture2D staticMario = Content.Load<Texture2D>("marioStandingStill");

            marioStandingStill = new Animations(staticMario, 1, 1);


            Texture2D runningInPlace = Content.Load<Texture2D>("staticAnimated");
            animatedSprite = new Animations(runningInPlace, 1, 4);

            Texture2D runningMario = Content.Load<Texture2D>("MovingAnimatedSheet");
            runningSprite = new Animations(runningMario, 2, 5);

            Texture2D jumpingTexture = Content.Load<Texture2D>("StaticNonanimated");
            jumpingSprite = new Animations(jumpingTexture, 1, 1);

            // Initialize TextSprite instances
            creditTextSprite = new TextSprite(textFont, creditStr, creditPosition, Color.White);
            authorTextSprite = new TextSprite(textFont, authorString, authorPosition, Color.White);
            urlTextSprite = new TextSprite(textFont, urlString, urlPosition, Color.White);
        }

        private void DisplaySprite(ISprite sprite)
        {
            HideAllSprites();
            sprite.Visible = true;
        }

        private void HideAllSprites()
        {
            marioStandingStill.Visible = false;
            animatedSprite.Visible = false;
            runningSprite.Visible = false;
            jumpingSprite.Visible = false;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);

            keyboardController.HandleInput(this);
            mouseController.HandleInput(this);

            marioStandingStill.Update();
            animatedSprite.Update();
            runningSprite.Update();
            jumpingSprite.Update();

            // Makes running move left & right
            if (!reverseX)
            {
                runningXValue += 5;
                if (runningXValue == 720)
                {
                    reverseX = true;
                }
            }
            else
            {
                runningXValue -= 5;
                if (runningXValue == 600)
                {
                    reverseX = false;
                }
            }

            //makes jumping move up & down
            if (!reverseY)
            {
                jumpingYValue -= 5;
                if (jumpingYValue == 300)
                {
                    reverseY = true;
                }
            }
            else if (reverseY)
            {
                jumpingYValue += 5;
                if (jumpingYValue == 400)
                {
                    reverseY = false;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Draw the text sprites
            creditTextSprite.Draw(spriteBatch, creditPosition);
            authorTextSprite.Draw(spriteBatch, authorPosition);
            urlTextSprite.Draw(spriteBatch, urlPosition);

            // Draw the visible sprite
            if (marioStandingStill.Visible)
                marioStandingStill.Draw(spriteBatch, new Vector2(0, 0));
            if (animatedSprite.Visible)
                animatedSprite.Draw(spriteBatch, new Vector2(600, 0));
            if (runningSprite.Visible)
                runningSprite.Draw(spriteBatch, new Vector2(runningXValue, 300));
            if (jumpingSprite.Visible)
            {
                jumpingSprite.Draw(spriteBatch, new Vector2(0, jumpingYValue));

            }

            spriteBatch.End();
        }
    }
}

