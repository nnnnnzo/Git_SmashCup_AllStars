using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Microsoft.Xna.Framework.Media;


namespace SmashCup_AllStars
{
   
    public class ScreenMenu: GameScreen
    {

        private Game1 _game1;
        private Texture2D _enterToStart;
        private Vector2 _positionEnterToStart;

        private Texture2D _backgroundImageMenu;

        private Texture2D _reglesImage;
        private Texture2D _controlImage;

        private Texture2D _reglesTexte;
        private Vector2 _positionReglesTexte;

        private Texture2D _controlKey;
        private Vector2 _positionControKey;
        


       

        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;

        private Song _musicMenu;

       

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

    
            _positionEnterToStart = new Vector2(450, 625);
            _positionControKey = new Vector2(525, 700);
            _positionReglesTexte = new Vector2(650, 760);



        }

        public override void LoadContent()
        {
            _backgroundImageMenu = Content.Load<Texture2D>("bgMenu");
            _enterToStart = Content.Load<Texture2D>("startEnter");
            _reglesImage = Content.Load<Texture2D>("Règles");
            _reglesTexte = Content.Load<Texture2D>("regleCorrection");
            _controlImage = Content.Load<Texture2D>("Touches");

            _controlKey = Content.Load<Texture2D>("holdControlKey");

            _musicMenu = Content.Load<Song>("musicMenuCupHead");
                MediaPlayer.Play(_musicMenu);
            MediaPlayer.Volume =1f;
           


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            
        }


        public override void Draw(GameTime gameTime)
        {

           
            _game1.GraphicsDevice.Clear(Color.Black);
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 1900;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 1050;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: matrix);
            _game1.SpriteBatch.Draw(_backgroundImageMenu, new Vector2(scaleX, scaleY), Color.White);
            _game1.SpriteBatch.Draw(_enterToStart, _positionEnterToStart, Color.White);
            _game1.SpriteBatch.Draw(_reglesTexte,_positionReglesTexte,Color.White);
            _game1.SpriteBatch.Draw(_controlKey, _positionControKey, Color.White);

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                _game1.SpriteBatch.Draw(_reglesImage, new Vector2(scaleX, scaleY), Color.White);

            }

            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _game1.SpriteBatch.Draw(_controlImage, new Vector2(scaleX, scaleY), Color.White);


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
