using Microsoft.Xna.Framework;
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
    public enum FinGame { BleuWon,RougeWon}
    public class ScreenFin : GameScreen
    {
        private Game1 _game1;
        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;

        private Texture2D _backgroundImageEnd;
        private Texture2D _blueWon;
        private Texture2D _redWon;
        private Vector2 _positionBlueWon;
        private Vector2 _positionRedWon;
        private Texture2D _playAgain;
        private Vector2 _positionPlayAgain;

        private FinGame _fin;

        public FinGame Fin { get => _fin; set => _fin = value; }

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

            _positionBlueWon = new Vector2(100, 100);
            _positionRedWon = new Vector2(100, 100);
            _positionPlayAgain= new Vector2(100, 500);






        }

        public override void LoadContent()
        {

            _backgroundImageEnd = Content.Load<Texture2D>("imageEnd");
            _blueWon = Content.Load<Texture2D>("blueWon");
            _redWon = Content.Load<Texture2D>("redWon");
            _playAgain = Content.Load<Texture2D>("rejouer");





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



            if (_fin == FinGame.BleuWon)
            {
                _game1.SpriteBatch.Draw(_blueWon, _positionBlueWon, Color.White);
                _game1.SpriteBatch.Draw(_playAgain, _positionPlayAgain, Color.White);
            }
            if (_fin == FinGame.RougeWon)
            {
                _game1.SpriteBatch.Draw(_redWon, _positionRedWon, Color.White);
                _game1.SpriteBatch.Draw(_playAgain, _positionPlayAgain, Color.White);
            }

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
