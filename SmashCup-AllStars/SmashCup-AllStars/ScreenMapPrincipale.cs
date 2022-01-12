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
    public class ScreenMapPrincipale: GameScreen 
    {

        private Game1 _game1;
        private TiledMap _mapPrincipale;
        private TiledMapRenderer _renduMapPrincipale;
        private TiledMapTileLayer _mapLayerSol;

        private Vector2 _perso1Position;
        private AnimatedSprite _perso1;
        private int _vitessePerso1;
        private string animationP1;
        private string lastDirP1;

        private Vector2 _perso2Position;
        private AnimatedSprite _perso2;
        private int _vitessePerso2;
        private string animationP2;
        private string lastDirP2;
        private bool jumpingP1, jumpingP2; //Is the character jumping?
        private float startYP1, jumpspeedP1 = 0, startYP2, jumpspeedP2 = 0; //startY to tell us //where it lands, jumpspeed to see how fast it jumps


        //animation boule de feu 
        private AnimatedSprite _bdf1;
        private AnimatedSprite _bdf2;
        private Vector2 _bdfPosition1;
        private Vector2 _bdfPosition2;
        private string _bdfPositionDepart1;
        private string _bdfPositionDepart2;
        private string animationBdf1;
        private string animationBdf2;
        private int _vitesseBdf;
        private bool deplacementBDF1;
        private bool deplacementBDF2;

        // Animation bullets

        private AnimatedSprite _bullet;
        private Vector2 _positionBullet;
        private string _annimationBullet;

        private SpriteFont _police;
        //vie perso 1
        private int _vieperso1;
        private Vector2 _positionVie1;
        //vie perso 2
        private int _vieperso2;
        private Vector2 _positionVie2;

        public Vector2 _finPosition;
        public Vector2 _tailleTexteFin;

        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;

        private ScreenMenu _screenMenuMusic;
        private PersoRed _persoRed;

        public TiledMapTileLayer MapLayerSol { get => _mapLayerSol; set => _mapLayerSol = value; }
        public Vector2 Perso1Position { get => _perso1Position; set => _perso1Position = value; }

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


            // joueur 1
            _persoRed = new PersoRed();
            
            

            //var joueur 1
            _perso1Position = new Vector2(900, 200);

            /*
            _vitessePerso1 = 200;
            animationP1 = "idleD";
            lastDirP1 = "D";
            startYP1 = _perso1Position.Y;//Starting position
            jumpingP1 = false;//Init jumping to false
            jumpspeedP1 = 0;

            */

            //var joueur 2
            _perso2Position = new Vector2(WIDTH_WINDOW/2, HEIGHT_WINDOW/2);
            _vitessePerso2 = 200;
            animationP2 = "idleG";
            lastDirP2 = "G";
            startYP2 = _perso2Position.Y;//Starting position
            jumpingP2 = false;//Init jumping to false
            jumpspeedP2 = 0;

            //Boule de feu
            animationBdf1 = "bouleDeFeuG";
            animationBdf2 = "bouleDeFeuD";
            _bdfPosition1 = new Vector2(800, -100);
            _bdfPosition2 = new Vector2(800, -100);
            _vitesseBdf = 300;
            deplacementBDF1 = false;
            deplacementBDF2 = false;

            // Vie perso
            _positionVie1 = new Vector2(0, 0);
            _vieperso1 = 3;
            _positionVie2 = new Vector2(0, 20);
            _vieperso2 = 3;


            //Bullets
            _positionBullet = new Vector2(800, 200);
            _annimationBullet = "dirG";
            


            //base.Initialize();

        }

        public override void LoadContent()
        {
            _mapPrincipale = Content.Load<TiledMap>("IceMap");
            _renduMapPrincipale = new TiledMapRenderer(GraphicsDevice, _mapPrincipale);
            MapLayerSol = _mapPrincipale.GetLayer<TiledMapTileLayer>("Terrain");

           
            
            /*// spritesheet personnages
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);
            */

            SpriteSheet spriteSheetP2 = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheetP2);

            // spritesheet boule de feu
            SpriteSheet spriteSheetBDF1 = Content.Load<SpriteSheet>("bdf.sf", new JsonContentLoader());
            _bdf1 = new AnimatedSprite(spriteSheetBDF1);
            SpriteSheet spriteSheetBDF2 = Content.Load<SpriteSheet>("bdf.sf", new JsonContentLoader());
            _bdf2 = new AnimatedSprite(spriteSheetBDF2);

            // police de vie
            _police = Content.Load<SpriteFont>("Font");

            //spritesheet bullet
            SpriteSheet spriteSheetBullet = Content.Load<SpriteSheet>("bullet.sf", new JsonContentLoader());

            _bullet = new AnimatedSprite(spriteSheetBullet);

            _persoRed.LoadContent(Content);

            //base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game1.Exit();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPerso1 = deltaSeconds * _vitessePerso1;
            float walkSpeedPerso2 = deltaSeconds * _vitessePerso2;
            float walkSpeedBdf = deltaSeconds * _vitesseBdf;
            KeyboardState keyboardState = Keyboard.GetState();

            //colisions
            Rectangle perso1 = new Rectangle((int)Perso1Position.X - 98 / 2, (int)Perso1Position.Y - 5, 98, 150);
            Rectangle perso2 = new Rectangle((int)_perso2Position.X - 98 / 2, (int)_perso2Position.Y - 5, 98, 150);
            Rectangle bdf1 = new Rectangle((int)_bdfPosition1.X - 286 / 2, (int)_bdfPosition1.Y - 146 / 2, 286, 146);
            Rectangle bdf2 = new Rectangle((int)_bdfPosition2.X - 286 / 2, (int)_bdfPosition2.Y - 146 / 2, 286, 146);
            if (bdf2.Intersects(perso1))
            {
                _vieperso1--;
            }
            if (bdf1.Intersects(perso2))
            {
                _vieperso2--;
            }

            //bdf perso rouge (1)
            if (deplacementBDF1)
            {
                if (_bdfPositionDepart1 == "D")
                {
                    if (_bdfPosition1.X > 2800 || bdf2.Intersects(perso1) || bdf1.Intersects(perso2))
                    {
                        _bdfPosition1 = new Vector2(800, -100);
                        deplacementBDF1 = false;
                    }
                    else
                    {
                        animationBdf1 = "bouleDeFeuD";
                        _bdfPosition1.X += walkSpeedBdf;
                    }
                }
                else
                {
                    if (_bdfPosition1.X < 0 || bdf2.Intersects(perso1) || bdf1.Intersects(perso2))
                    {
                        _bdfPosition1 = new Vector2(800, -100);
                        deplacementBDF1 = false;
                    }
                    else
                    {
                        animationBdf1 = "bouleDeFeuG";
                        _bdfPosition1.X -= walkSpeedBdf;
                    }
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    deplacementBDF1 = true;
                    _bdfPositionDepart1 = lastDirP1;
                    _bdfPosition1 = Perso1Position;
                    _bdfPosition1.Y = _bdfPosition1.Y + 75;

                }
            }
            //bdf perso bleu (2)
            if (deplacementBDF2)
            {
                if (_bdfPositionDepart2 == "D")
                {
                    if (_bdfPosition2.X > 2800 || bdf2.Intersects(perso1) || bdf1.Intersects(perso2))
                    {
                        _bdfPosition2 = new Vector2(800, -100);
                        deplacementBDF2 = false;
                    }
                    else
                    {
                        animationBdf2 = "bouleDeFeuD";
                        _bdfPosition2.X += walkSpeedBdf;
                    }
                }
                else
                {
                    if (_bdfPosition2.X < 0 || bdf2.Intersects(perso1) || bdf1.Intersects(perso2))
                    {
                        _bdfPosition2 = new Vector2(800, -100);
                        deplacementBDF2 = false;
                    }
                    else
                    {
                        animationBdf2 = "bouleDeFeuG";
                        _bdfPosition2.X -= walkSpeedBdf;
                    }
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.RightControl))
                {
                    deplacementBDF2 = true;
                    _bdfPositionDepart2 = lastDirP2;
                    _bdfPosition2 = _perso2Position;
                    _bdfPosition2.Y = _bdfPosition2.Y + 75;
                }
            }


            //Jump Joueur 1
            if (jumpingP1)
            {
                _perso1Position.Y += jumpspeedP1;//Making it go up
                jumpspeedP1 += 1;//Some math (explained later)
                if (Perso1Position.Y >= startYP1)
                //If it's farther than ground
                {
                    _perso1Position.Y = startYP1;//Then set it on
                    jumpingP1 = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    jumpingP1 = true;
                    jumpspeedP1 = -44;//Give it upward thrust
                }
            }


            //Jump Joueur 2
            if (jumpingP2)
            {
                _perso2Position.Y += jumpspeedP2;//Making it go up
                jumpspeedP2 += 1;//Some math (explained later)
                if (_perso2Position.Y >= startYP2)
                //If it's farther than ground
                {
                    _perso2Position.Y = startYP2;//Then set it on
                    jumpingP2 = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    jumpingP2 = true;
                    jumpspeedP2 = -44;//Give it upward thrust
                }
            }

            /*

            //Direction dans laquelles regarder
            if (lastDirP1 == "D")
                animationP1 = "idleD";
            else
                animationP1 = "idleG";
            */


            if (lastDirP2 == "D")
                animationP2 = "idleD";
            else
                animationP2 = "idleG";

            /*

            //Deplacement Joueur 1
            if (keyboardState.IsKeyDown(Keys.D))
            {
                animationP1 = "runD";
                _perso1Position.X += walkSpeedPerso1;
                lastDirP1 = "D";
            }

            

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                animationP1 = "runG";
                _perso1Position.X -= walkSpeedPerso1;
                lastDirP1 = "G";
            }

            */

           /* ushort x1 = (ushort)(_perso1Position.X / 70 + 0.5);
            ushort y1 = (ushort)(_perso1Position.Y / 70 + 2.12);


             int tile1 = MapLayerSol.GetTile(x1, y1).GlobalIdentifier;
             if (tile1 == 0)
             {
                 _perso1Position.Y += 14;
             }
             else
                 startYP1 = _perso1Position.Y;
           */

             ushort x2 = (ushort)(_perso2Position.X / 70 + 0.5);
             ushort y2 = (ushort)(_perso2Position.Y / 70 + 2);


             int tile2 = MapLayerSol.GetTile(x2, y2).GlobalIdentifier;
             if (tile2 == 0)
             {
                 _perso2Position.Y += 14;
             }
             else
                 startYP2 = _perso2Position.Y;
            

            //Deplacement Joueur 2
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                animationP2 = "runD";
                _perso2Position.X += walkSpeedPerso2;
                lastDirP2 = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                animationP2 = "runG";
                _perso2Position.X -= walkSpeedPerso2;
                lastDirP2 = "G";
            }


            // TODO: Add your update logic here

          
            //_perso1.Play(animationP1);
            _perso2.Play(animationP2);
            _bdf1.Play(animationBdf1);
            _bdf2.Play(animationBdf2);
            _bullet.Play(_annimationBullet);
            //_perso1.Update(deltaSeconds);
            //_persoRed.Perso1.Update(deltaSeconds);
            _perso2.Update(deltaSeconds);
            _bdf1.Update(deltaSeconds);
            _bdf2.Update(deltaSeconds);
            _bullet.Update(deltaSeconds);
            _persoRed.Update(gameTime);

            //base.Update(gameTime);





        }


        public override void Draw(GameTime gameTime)
        {

            _game1.GraphicsDevice.Clear(new Color(99, 160, 166));
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 2800;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 1400;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: matrix);
            _renduMapPrincipale.Draw(matrix);

            //_game1.SpriteBatch.Draw(_persoRed.Perso1, _persoRed.Perso1Position);
           


            _game1.SpriteBatch.DrawString(_police, $"Vie RED : {_vieperso1}", _positionVie1, Color.White);
          _game1.SpriteBatch.DrawString(_police, $"Vie BLUE : {_vieperso2} ", _positionVie2, Color.White);

        

          //_game1.SpriteBatch.Draw(_perso1, _perso1Position);
          _game1.SpriteBatch.Draw(_perso2, _perso2Position);
          _game1.SpriteBatch.Draw(_bdf1, _bdfPosition1);
          _game1.SpriteBatch.Draw(_bdf2, _bdfPosition2);
            _game1.SpriteBatch.Draw(_bullet, _positionBullet);
            _persoRed.Draw(_game1.SpriteBatch);
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
