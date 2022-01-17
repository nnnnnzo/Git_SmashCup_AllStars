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
    public class PersoBlue
    {


        //Perso rouge
        private Vector2 _positionPersoBlue;
        private AnimatedSprite _persoBlueSprite;
        private int _vitessePersoBlue;
        private string _animationPersoBlue;
        private string _lastDirPersoBlue;
        private bool _jumpingPersoBlue;
        private float _jumpspeedPersoBlue = 0;
        private float _startYPersoBlue;

 







        // Map perso Red
        private TiledMapTileLayer _mapLayerSolPersoBlue;







        public Vector2 PositionPersoBlue { get => _positionPersoBlue; set => _positionPersoBlue = value; }
        public TiledMapTileLayer MapLayerSolPersoBlue { get => _mapLayerSolPersoBlue; set => _mapLayerSolPersoBlue = value; }
        public string AnimationPersoBlue{ get => _animationPersoBlue; set => _animationPersoBlue = value; }
        public AnimatedSprite PersoBlueSprite { get => _persoBlueSprite; set => _persoBlueSprite = value; }
        public string LastDirPersoBlue { get => _lastDirPersoBlue; set => _lastDirPersoBlue = value; }

      

        public void Initialize()
        {


            // joueur 1
            _positionPersoBlue = new Vector2(ScreenMapPrincipale.WIDTH_WINDOW / 2, ScreenMapPrincipale.HEIGHT_WINDOW / 2);
            _vitessePersoBlue = 200;
            _animationPersoBlue = "idleG";
            _lastDirPersoBlue = "G";
            _startYPersoBlue = _positionPersoBlue.Y;//Starting position






        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            SpriteSheet spriteSheetPersoBlue = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _persoBlueSprite = new AnimatedSprite(spriteSheetPersoBlue);




        }

        public void Update(GameTime gameTime)
        {

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPersoBlue = deltaSeconds * _vitessePersoBlue;

            KeyboardState keyboardState = Keyboard.GetState();


            //Direction dans laquelles regarder
            if (_lastDirPersoBlue == "D")
                _animationPersoBlue = "idleD";
            else if (_lastDirPersoBlue == "G")
                _animationPersoBlue = "idleG";

            Vector2 deplacement = new Vector2(0, 0);
            //Deplacement Joueur 1
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _animationPersoBlue = "runD";
                _positionPersoBlue += new Vector2(walkSpeedPersoBlue, 0);
                _lastDirPersoBlue = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _animationPersoBlue = "runG";
                _positionPersoBlue -= new Vector2(walkSpeedPersoBlue, 0);
                _lastDirPersoBlue = "G";
            }


            if (_jumpingPersoBlue)
            {
                _positionPersoBlue.Y += _jumpspeedPersoBlue;//Making it go up
                _jumpspeedPersoBlue += 1;//Some math (explained later)
                if (_positionPersoBlue.Y >= _startYPersoBlue)
                //If it's farther than ground
                {
                    _positionPersoBlue.Y = _startYPersoBlue;//Then set it on
                    _jumpingPersoBlue = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    _jumpingPersoBlue = true;
                    _jumpspeedPersoBlue = -44;//Give it upward thrust
                }
            }


            ushort x2 = (ushort)(_positionPersoBlue.X / 70 + 0.5);
            ushort y2 = (ushort)(_positionPersoBlue.Y / 70 + 2);

            TiledMapTile? tilePersoRed;
            _mapLayerSolPersoBlue.TryGetTile(x2, y2, out tilePersoRed);
            if (tilePersoRed == null)
            {
                _positionPersoBlue.Y += 14;
            }
            else
                _startYPersoBlue = _positionPersoBlue.Y;





            _persoBlueSprite.Play(_animationPersoBlue);
            _persoBlueSprite.Update(deltaSeconds);


        }

        public void Draw(SpriteBatch sp)
        {




            sp.Draw(_persoBlueSprite, _positionPersoBlue);
        }
    }


}
