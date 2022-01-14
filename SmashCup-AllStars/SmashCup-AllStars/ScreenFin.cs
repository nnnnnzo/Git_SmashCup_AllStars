﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
namespace SmashCup_AllStars
{
    public class ScreenFin : GameScreen
    {
        private Game1 _game1;
        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;

        private Texture2D _backgroundImageEnd;
       




        public ScreenFin(Game1 game): base(game)
        {

            _game1 = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _game1.Graphics.PreferredBackBufferWidth = WIDTH_WINDOW;
            _game1.Graphics.PreferredBackBufferHeight = HEIGHT_WINDOW;
            _game1.Graphics.IsFullScreen = false;
            _game1.Graphics.ApplyChanges();


            

        }

        public override void LoadContent()
        {

            _backgroundImageEnd = Content.Load<Texture2D>("imageEnd");






            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {



        }


        public override void Draw(GameTime gameTime)
        {


            _game1.GraphicsDevice.Clear(Color.Black);
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 1200;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 650;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: matrix);
            _game1.SpriteBatch.Draw(_backgroundImageEnd, new Vector2(scaleX, scaleY), Color.White);
           
            _game1.SpriteBatch.End();


            /*
            _game1.GraphicsDevice.Clear(Color.Red);
            _game1.SpriteBatch.Begin();
            _game1.SpriteBatch.Draw(_backgroundImageMenu, new Vector2(0, 0), Color.White);
            _game1.SpriteBatch.End();
            */



        }










    }
}