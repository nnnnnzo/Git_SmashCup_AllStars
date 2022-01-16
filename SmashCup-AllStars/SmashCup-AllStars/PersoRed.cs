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
        private Vector2 _positionPersoRed;
        private AnimatedSprite _perso1;
        private int _vitessePerso1;
        private string _animationP1;
        private string _lastDirP1;
        
        private bool _jumpingP1;
        private float _jumpspeedP1 = 0;

        
       
        private float _startYP1;
       


 
        private TiledMapTileLayer _mapLayerSol;





        public AnimatedSprite Perso1 { get => _perso1; set => _perso1 = value; }
        public string AnimationP1 { get => _animationP1; set => _animationP1 = value; }
        public Vector2 PositionPersoRed { get => _positionPersoRed; set => _positionPersoRed = value; }
        public TiledMapTileLayer MapLayerSol { get => _mapLayerSol; set => _mapLayerSol = value; }

        public  void Initialize()
        {
           

            // joueur 1
            _positionPersoRed = new Vector2(ScreenMapPrincipale.WIDTH_WINDOW/2, ScreenMapPrincipale.HEIGHT_WINDOW / 2);
            _vitessePerso1 = 200;
            _animationP1 = "idleD";
            _lastDirP1 = "D";
            _startYP1 = _positionPersoRed.Y;//Starting position



        }

        public  void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);

           

        }

        public  void Update(GameTime gameTime)
        {
         
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
            if (keyboardState.IsKeyDown(Keys.J))
            {
                _animationP1 = "runD";
                PositionPersoRed += new Vector2(walkSpeedPerso1, 0);
                _lastDirP1 = "D";
            }

            if (keyboardState.IsKeyDown(Keys.H))
            {
                _animationP1 = "runG";
                PositionPersoRed-= new Vector2(walkSpeedPerso1,0);
                _lastDirP1 = "G";
            }
            

            if (_jumpingP1)
            {
                _positionPersoRed.Y += _jumpspeedP1;//Making it go up
                _jumpspeedP1 += 1;//Some math (explained later)
                if (_positionPersoRed.Y >= _startYP1)
                //If it's farther than ground
                {
                    _positionPersoRed.Y = _startYP1;//Then set it on
                    _jumpingP1 = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.U))
                {
                    _jumpingP1 = true;
                    _jumpspeedP1 = -44;//Give it upward thrust
                }
            }

            ushort x2 = (ushort)(_positionPersoRed.X / 70 + 0.5);
            ushort y2 = (ushort)(_positionPersoRed.Y / 70 + 2);

            TiledMapTile? tile2;
            _mapLayerSol.TryGetTile(x2, y2, out tile2);
            if (tile2 == null)
            {
                _positionPersoRed.Y += 14;
            }
            else
                _startYP1 = _positionPersoRed.Y;
            
            

            _perso1.Play(_animationP1);
           _perso1.Update(deltaSeconds);
            

        }

        public   void Draw(SpriteBatch sp)
        {



            sp.Draw(_perso1, PositionPersoRed);
        }
    }


}
