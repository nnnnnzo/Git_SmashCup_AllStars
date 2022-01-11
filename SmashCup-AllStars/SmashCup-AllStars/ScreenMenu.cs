using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;


namespace SmashCup_AllStars
{
    public enum Bouton { Play,Quit, Option};
    class ScreenMenu: GameScreen
    {
        private Bouton _boutonCliquer;

        private Game1 _game1;
        private Texture2D _backgroundImageMenu;

        private Texture2D _buttonPlay;
        private Texture2D _buttonOption;
        private Texture2D _buttonQuit;

        private Vector2 _positionButtonPlay;
        private Vector2 _positionButtonOption;
        private Vector2 _positionButtonQuit;

        private Rectangle _buttonPlayRectangle;
        private MouseState _mouseState;



        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;

        public Rectangle ButtonPlayRectangle { get => _buttonPlayRectangle; set => _buttonPlayRectangle = value; }
       

        public Vector2 PositionButtonPlay { get => _positionButtonPlay; set => _positionButtonPlay = value; }
        public Vector2 PositionButtonOption { get => _positionButtonOption; set => _positionButtonOption = value; }
        public Vector2 PositionButtonQuit { get => _positionButtonQuit; set => _positionButtonQuit = value; }

        public Texture2D ButtonPlay { get => _buttonPlay; set => _buttonPlay = value; }
        public Texture2D ButtonOption { get => _buttonOption; set => _buttonOption = value; }
        public Texture2D ButtonQuit { get => _buttonQuit; set => _buttonQuit = value; }
        public MouseState MouseState { get => _mouseState; set => _mouseState = value; }

        public ScreenMenu(Game1 game) : base(game)
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

            //_positionButtonPlay = new Vector2((WIDTH_WINDOW - _buttonPlay.Width)/2, (HEIGHT_WINDOW - _buttonPlay.Height) / 2);
            _positionButtonOption = new Vector2(950, 425);
            _positionButtonQuit = new Vector2(950, 525);

        }

        public override void LoadContent()
        {
            _backgroundImageMenu = Content.Load<Texture2D>("bgMenu");
            _buttonPlay = Content.Load<Texture2D>("play");
            _buttonOption = Content.Load<Texture2D>("option");
            _buttonQuit = Content.Load<Texture2D>("quit");
            _positionButtonPlay = new Vector2(((WIDTH_WINDOW/2  - _buttonPlay.Width/2)), (HEIGHT_WINDOW - _buttonPlay.Height) );

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle rPlay = new Rectangle((int)PositionButtonPlay.X, (int)PositionButtonPlay.Y, ButtonPlay.Width, ButtonPlay.Height);
                if (rPlay.Contains(_mouseState.X, _mouseState.Y))
                    _boutonCliquer = Bouton.Play;

            }



        }


        public override void Draw(GameTime gameTime)
        {

           
            _game1.GraphicsDevice.Clear(Color.Black);
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 1900;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 1050;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: matrix);
            _game1.SpriteBatch.Draw(_backgroundImageMenu, new Vector2(scaleX, scaleY), Color.White);
            _game1.SpriteBatch.Draw(ButtonPlay, PositionButtonPlay, Color.White);
            _game1.SpriteBatch.Draw(ButtonOption, PositionButtonOption, Color.White);
            _game1.SpriteBatch.Draw(ButtonQuit, PositionButtonQuit, Color.White);
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
