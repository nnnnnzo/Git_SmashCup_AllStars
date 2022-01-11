using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace SmashCup_AllStars
{
    class ScreenMapPrincipale: GameScreen
    {

        private Game1 _game1;
        private TiledMap _mapPrincipale;
        private TiledMapRenderer _renduMapPrincipale;
        private TiledMapLayer _mapLayerSol;

        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;



        public ScreenMapPrincipale(Game1 game): base(game)
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
            _mapPrincipale = Content.Load<TiledMap>("Ice");
            _renduMapPrincipale = new TiledMapRenderer(GraphicsDevice, _mapPrincipale);
            _mapLayerSol= _mapPrincipale.GetLayer<TiledMapTileLayer>("Terrain");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            



        }


        public override void Draw(GameTime gameTime)
        {

            _game1.GraphicsDevice.Clear(new Color(99, 160, 166));
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 2800;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 1400;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: matrix);
            _renduMapPrincipale.Draw();
            _game1.SpriteBatch.End();
           

            /*
            _game1.GraphicsDevice.Clear(Color.Red);
            _game1.SpriteBatch.Begin();
            _renduMapPrincipale.Draw();
            _game1.SpriteBatch.End();
            */
        }



    }
}
