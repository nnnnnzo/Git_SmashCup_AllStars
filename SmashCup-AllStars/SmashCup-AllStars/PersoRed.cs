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
using System.Collections.Generic;

namespace SmashCup_AllStars
{
    public class PersoRed 
    {
       

        //Perso rouge
        private Vector2 _positionPersoRed;
        private AnimatedSprite _persoRedSprite;
        private int _vitessePersoRed;
        private string _animationPersoRed;
        private string _lastDirPersoRed;
        private bool _jumpingPersoRed;
        private float _jumpspeedPersoRed = 0;
        private float _startYPersoRed;

        // Map perso Red
        private TiledMapTileLayer _mapLayerSolPersoRed;





       
       
        public Vector2 PositionPersoRed { get => _positionPersoRed; set => _positionPersoRed = value; }
        public TiledMapTileLayer MapLayerSolPersoRed { get => _mapLayerSolPersoRed; set => _mapLayerSolPersoRed = value; }
        public string AnimationPersoRed { get => _animationPersoRed; set => _animationPersoRed = value; }
        public AnimatedSprite PersoRedSprite { get => _persoRedSprite; set => _persoRedSprite = value; }
        public string LastDirPersoRed { get => _lastDirPersoRed; set => _lastDirPersoRed = value; }

  

        public  void Initialize()
        {
           

            // joueur 1
            _positionPersoRed = new Vector2(730,840);
            _vitessePersoRed = 200;
            AnimationPersoRed = "idleD";
            LastDirPersoRed = "D";
            _startYPersoRed = _positionPersoRed.Y;//Starting position

        




        }

        public  void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            SpriteSheet spriteSheetPersoRed = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());
            _persoRedSprite = new AnimatedSprite(spriteSheetPersoRed);

      


        }

        public  void Update(GameTime gameTime)
        {
         
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPersoRed = deltaSeconds * _vitessePersoRed;
           
            KeyboardState keyboardState = Keyboard.GetState();


            //Direction dans laquelles regarder
            if (LastDirPersoRed == "D")
                AnimationPersoRed = "idleD";
            else if (LastDirPersoRed=="G")
                AnimationPersoRed = "idleG";

            Vector2 deplacement= new Vector2(0,0);
            //Deplacement Joueur 1
            if (keyboardState.IsKeyDown(Keys.D))
            {
                AnimationPersoRed = "runD";
                PositionPersoRed += new Vector2(walkSpeedPersoRed, 0);
                LastDirPersoRed = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                AnimationPersoRed = "runG";
                PositionPersoRed-= new Vector2(walkSpeedPersoRed, 0);
                LastDirPersoRed = "G";
            }
            

            if (_jumpingPersoRed)
            {
                _positionPersoRed.Y += _jumpspeedPersoRed;//Making it go up
                _jumpspeedPersoRed += 1;//Some math (explained later)
                if (_positionPersoRed.Y >= _startYPersoRed)
                //If it's farther than ground
                {
                    _positionPersoRed.Y = _startYPersoRed;//Then set it on
                    _jumpingPersoRed = false;
                }
            }
            else
            {
                if(PositionPersoRed.Y >= 0)
                {
                    if (keyboardState.IsKeyDown(Keys.Z))
                    {
                        _jumpingPersoRed = true;
                        _jumpspeedPersoRed = -44;//Give it upward thrust
                    }
                }
                else
                {
                    _positionPersoRed.Y = 10;
                }
            }

            
            ushort x2 = (ushort)(_positionPersoRed.X / 70 + 0.5);
            ushort y2 = (ushort)(_positionPersoRed.Y / 70 + 2);

            TiledMapTile? tilePersoRed;
            MapLayerSolPersoRed.TryGetTile(x2, y2, out tilePersoRed);
            if (tilePersoRed == null)
            {
                _positionPersoRed.Y += 14;
            }
            else
                _startYPersoRed = _positionPersoRed.Y;


         
          

            _persoRedSprite.Play(AnimationPersoRed);
            _persoRedSprite.Update(deltaSeconds);
            

        }

        public   void Draw(SpriteBatch sp)
        {
         
           


            sp.Draw(_persoRedSprite, PositionPersoRed);
        }
    }


}
