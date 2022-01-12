using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
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
    public class PersoRed 
    {
       

        //Perso rouge
        private Vector2 _perso1Position;
        private AnimatedSprite _perso1;
        private int _vitessePerso1;
        private string _animationP1;
        private string _lastDirP1;
        private float _startYP1;
        private bool _jumpingP1;
        private float _jumpspeedP1 = 0;

        private ScreenMapPrincipale _screenMapPrincipal;
        private Game1 _game1; 




        
        public AnimatedSprite Perso1 { get => _perso1; set => _perso1 = value; }
        public string AnimationP1 { get => _animationP1; set => _animationP1 = value; }



        public  void Initialize()
        {

            // joueur 1
            //_perso1Position = new Vector2(ScreenMapPrincipale.WIDTH_WINDOW/2, ScreenMapPrincipale.HEIGHT_WINDOW / 2);
            _vitessePerso1 = 200;
            _animationP1 = "idleD";
            _lastDirP1 = "D";
            //_startYP1 = _perso1Position.Y;//Starting position




        }

        public  void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);


        }

        public  void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game1.Exit();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPerso1 = deltaSeconds * _vitessePerso1;
            KeyboardState keyboardState = Keyboard.GetState();

            //Direction dans laquelles regarder
            if (_lastDirP1 == "D")
                _animationP1 = "idleD";
            else
                _animationP1 = "idleG";

            Vector2 deplacement= new Vector2(0,0);
            //Deplacement Joueur 1
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _animationP1 = "runD";
                //_game1.Perso1PositionGame1.X += walkSpeedPerso1;
                _lastDirP1 = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                _animationP1 = "runG";
                _perso1Position.X -= walkSpeedPerso1;
                _lastDirP1 = "G";
            }


            //Jump Joueur 1
            if (_jumpingP1)
            {
                _perso1Position.Y += _jumpspeedP1;//Making it go up
                _jumpspeedP1 += 1;//Some math (explained later)
                if (_perso1Position.Y >= _startYP1)
                //If it's farther than ground
                {
                    _perso1Position.Y = _startYP1;//Then set it on
                    _jumpingP1 = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    _jumpingP1 = true;
                    _jumpspeedP1 = -44;//Give it upward thrust
                }
            }


            /*ushort x1 = (ushort)(_perso1Position.X / 70 + 0.5);
            ushort y1 = (ushort)(_perso1Position.Y / 70 + 2.12);


            int tile1 = _screenMapPrincipal.MapLayerSol.GetTile(x1, y1).GlobalIdentifier;
            if (tile1 == 0)
            {
                _perso1Position.Y += 14;
            }
            else
                startYP1 = _perso1Position.Y;
            */



            _perso1.Play(_animationP1);
           _perso1.Update(deltaSeconds);

        }

        public   void Draw(SpriteBatch sp)
        {



            sp.Draw(_perso1, _perso1Position);
        }
    }


}
