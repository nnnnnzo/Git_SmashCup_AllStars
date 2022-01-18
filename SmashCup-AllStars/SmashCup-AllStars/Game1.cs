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
using System;
/*
ATTENTION: Valider, Tirer, Resoudre conflits, Envoyer
*/

namespace SmashCup_AllStars
{

    public enum Ecran { Principal,Menu,End};
   
    public class Game1 : Game
    {
  

        //Test class screenMapPrincipale

        
        private ScreenMapPrincipale _screenMapPrincipale;
        private ScreenMenu _screenMapMenu;
        private ScreenFin _screenFin;
        private readonly ScreenManager _screenManager;
        private Ecran _ecranEnCours;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;




       

        

        // private Texture2D _imageFondMenu;

    

        // J'encapsule Graphic et spritebatch pour pouvoir les réutiliser dans les autres classes.
        public GraphicsDeviceManager Graphics { get => _graphics; set => _graphics = value; }
        public SpriteBatch SpriteBatch { get => _spriteBatch; set => _spriteBatch = value; }
      

        /*blabla*/
        /*Test modif Gab*/

     


        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

        }

        protected override void Initialize()
        {

            

            base.Initialize();
        }

      


        protected override void LoadContent()
        {
            
            SpriteBatch = new SpriteBatch(GraphicsDevice);




            //ScreenManager:
            _screenMapPrincipale = new ScreenMapPrincipale(this);
            _screenMapMenu = new ScreenMenu(this);
            _screenFin = new ScreenFin(this);
            _screenManager.LoadScreen(_screenMapMenu, new FadeTransition(GraphicsDevice, Color.Black));
            _ecranEnCours = Ecran.Menu;




            base.LoadContent();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           
            KeyboardState keyboardState = Keyboard.GetState();



            if (_ecranEnCours == Ecran.Menu && keyboardState.IsKeyDown(Keys.Enter))
                {
                        
                        
                            _ecranEnCours = Ecran.Principal;
                            _screenManager.LoadScreen(_screenMapPrincipale, new FadeTransition(GraphicsDevice, Color.Black));
                        


                }
           

            else if (_ecranEnCours == Ecran.Principal && keyboardState.IsKeyDown(Keys.K))
            {
                _ecranEnCours = Ecran.Menu;
                _screenManager.LoadScreen(_screenMapMenu, new FadeTransition(GraphicsDevice, Color.Black));

            }

            else if (_ecranEnCours == Ecran.Principal && _screenMapPrincipale.Timer == -1 && _screenMapPrincipale.DamagePersoBlue<_screenMapPrincipale.DamagePersoRed)
            {
                _ecranEnCours = Ecran.End;
                _screenManager.LoadScreen(_screenFin, new FadeTransition(GraphicsDevice, Color.Black));
                _screenFin.Fin = FinGame.BleuWon;
            }
            else if (_ecranEnCours == Ecran.Principal && _screenMapPrincipale.Timer == -1 && _screenMapPrincipale.DamagePersoBlue > _screenMapPrincipale.DamagePersoRed)
            {
                _ecranEnCours = Ecran.End;
                _screenManager.LoadScreen(_screenFin, new FadeTransition(GraphicsDevice, Color.Black));
                _screenFin.Fin = FinGame.RougeWon;
            }

            else if (_ecranEnCours == Ecran.End && keyboardState.IsKeyDown(Keys.R))
            {
                _ecranEnCours = Ecran.Menu;
                _screenManager.LoadScreen(_screenMapMenu, new FadeTransition(GraphicsDevice, Color.Black));

            }


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}

