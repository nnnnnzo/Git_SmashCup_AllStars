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

    public enum Ecran { Principal,Menu};
    public class Game1 : Game
    {
       
        private TiledMapTileLayer _mapLayerSol;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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


        //Test class screenMapPrincipale

        private ScreenMapPrincipale _screenMapPrincipale;
        private ScreenMenu _screenMapMenu;
        private readonly ScreenManager _screenManager;
        private Ecran _ecranEnCours;
        

        private MouseState _mouseState;

        

        // private Texture2D _imageFondMenu;

        private Effect effect;

        // J'encapsule Graphic et spritebatch pour pouvoir les réutiliser dans les autres classes.
        public GraphicsDeviceManager Graphics { get => _graphics; set => _graphics = value; }
        public SpriteBatch SpriteBatch { get => _spriteBatch; set => _spriteBatch = value; }

        /*blabla*/
        /*Test modif Gab*/

        //Test nouveau perso avec une classe sprite
        private Sprite _persoTest;

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


        private SpriteFont _police;
        //vie perso 1
        private int _vieperso1;
        private Vector2 _positionVie1;
        //vie perso 2
        private int _vieperso2;
        private Vector2 _positionVie2;

        public Vector2 _finPosition;
        public Vector2 _tailleTexteFin;



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

            //WIdh: 1200
            //Height:700
            
            /*Graphics.PreferredBackBufferWidth = 1200;  // set this value to the desired width of your window
            Graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
            Graphics.IsFullScreen = false; //activer plein ecran pour build final
            Graphics.ApplyChanges();
            */



            //var joueur 1
            _perso1Position = new Vector2(900, 200);
            _vitessePerso1 = 200;
            animationP1 = "idleD";
            lastDirP1 = "D";
            startYP1 = _perso1Position.Y;//Starting position
            jumpingP1 = false;//Init jumping to false
            jumpspeedP1 = 0;

            //var joueur 2
            _perso2Position = new Vector2(1900, 200);
            _vitessePerso2 = 200;
            animationP2 = "idleG";
            lastDirP2 = "G";
            startYP2 = _perso2Position.Y;//Starting position
            jumpingP2 = false;//Init jumping to false
            jumpspeedP2 = 0;

            animationBdf1 = "bouleDeFeuG";
            animationBdf2 = "bouleDeFeuD";
            _bdfPosition1 = new Vector2(800, -100);
            _bdfPosition2 = new Vector2(800, -100);
            _vitesseBdf = 300;
            deplacementBDF1 = false;
            deplacementBDF2 = false;


            // Test nouveaux perso avec la classe sprite
            _persoTest = new Sprite();
            _persoTest.Initialize(new Vector2(500, 200), 200);

            _positionVie1= new Vector2(0, 0);
            _vieperso1 = 3;
            _positionVie2 = new Vector2(0, 20);
            _vieperso2 = 3;

            base.Initialize();
        }

        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (_mapLayerSol.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }


        protected override void LoadContent()
        {
            /*
            _tiledMap = Content.Load<TiledMap>("Ice");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayerSol = _tiledMap.GetLayer<TiledMapTileLayer>("Terrain");
            */
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            


            //ScreenManager:
            _screenMapPrincipale = new ScreenMapPrincipale(this);
            _screenMapMenu = new ScreenMenu(this);
            _screenManager.LoadScreen(_screenMapMenu, new FadeTransition(GraphicsDevice, Color.Black));
            _ecranEnCours = Ecran.Menu;

            // _imageFondMenu = Content.Load<Texture2D>("MenuImageSmashCup");

           


            //effect = Content.Load<Effect>("crt-lottes-mg");
            // spritesheet
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);

            SpriteSheet spriteSheetP2 = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheetP2);

            SpriteSheet spriteSheetBDF1 = Content.Load<SpriteSheet>("bdf.sf", new JsonContentLoader());
            _bdf1 = new AnimatedSprite(spriteSheetBDF1);
            SpriteSheet spriteSheetBDF2 = Content.Load<SpriteSheet>("bdf.sf", new JsonContentLoader());
            _bdf2 = new AnimatedSprite(spriteSheetBDF2);

            _police = Content.Load<SpriteFont>("Font");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPerso1 = deltaSeconds * _vitessePerso1;
            float walkSpeedPerso2 = deltaSeconds * _vitessePerso2;
            float walkSpeedBdf = deltaSeconds * _vitesseBdf;
            KeyboardState keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            //Gestion des screen avec touche:

            // Rectangle test = _screenMapMenu.Rectangle((int)_screenMapMenu.PositionButtonPlay.X, (int)_screenMapMenu.PositionButtonPlay.Y, _screenMapMenu.ButtonPlay.Width, _screenMapMenu.ButtonPlay.Height);

            //Rectangle r1 = new Rectangle((int)_screenMapMenu.PositionButtonPlay.X, (int)_screenMapMenu.PositionButtonPlay.Y, _screenMapMenu.ButtonPlay.Width, _screenMapMenu.ButtonPlay.Height);
            //r1.Contains(_screenMapMenu.MouseState.X, _screenMapMenu.MouseState.Y);
           
            
                


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


            //_screenMapMenu.ButtonPlayRectangle.Contains(_screenMapMenu.MouseState.X, _screenMapMenu.MouseState.Y);

            //colisions
            Rectangle perso1 = new Rectangle((int)_perso1Position.X - 98 / 2, (int)_perso1Position.Y - 5, 98, 150);
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
                    _bdfPosition1 = _perso1Position;
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
                if (_perso1Position.Y >= startYP1)
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

            

            //Direction dans laquelles regarder
            if (lastDirP1 == "D")
                animationP1 = "idleD";
            else
                animationP1 = "idleG";
            if (lastDirP2 == "D")
                animationP2 = "idleD";
            else
                animationP2 = "idleG";

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
            ushort x1 = (ushort)(_perso1Position.X / 70 + 0.5);
            ushort y1 = (ushort)(_perso1Position.Y / 70 + 2.12);


            int tile1 = _mapLayerSol.GetTile(x1, y1).GlobalIdentifier;
            if (tile1 == 0)
            {
                _perso1Position.Y += 14;
            }
            else
                startYP1 = _perso1Position.Y;

            ushort x2 = (ushort)(_perso2Position.X / 70 + 0.5);
            ushort y2 = (ushort)(_perso2Position.Y / 70 + 2);


            int tile2 = _mapLayerSol.GetTile(x2, y2).GlobalIdentifier;
            if (tile2 == 0)
            {
                _perso2Position.Y += 14;
            }
            else
                startYP2 = _perso2Position.Y;


            /*
            ushort x = (ushort)(_perso1Position.X + 150);
            ushort y = (ushort)(_perso1Position.Y +300);
            TiledMapTile? tile = null;

            int tile2 = mapLayerSol.GetTile(x2, y2).GlobalIdentifier;
            if (tile2 == 0)
            {
                _perso2Position.Y += 14;
            }
            else
                startYP2 = _perso2Position.Y;


            /*
            if (tile.HasValue)
            {
                startYP1 = _perso1Position.Y;// collided!
                // you can also compute the tile's position using the X, Y and tileWidth if needed.
                Console.WriteLine(tile);
            }
            else
            {
                _perso1Position.Y += 14;
            }
            */


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
            _perso1.Play(animationP1);
            _perso2.Play(animationP2);
            _bdf1.Play(animationBdf1);
            _bdf2.Play(animationBdf2);
            _perso1.Update(deltaSeconds);
            _perso2.Update(deltaSeconds);
            _bdf1.Update(deltaSeconds);
            _bdf2.Update(deltaSeconds);
            // GraphicsDevice.BlendState = BlendState.AlphaBlend;
            //_tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            /*GraphicsDevice.Clear(new Color(99,160,166));
            var scaleX = (float)Graphics.PreferredBackBufferWidth / 2800;
            var scaleY = (float)Graphics.PreferredBackBufferHeight / 1400;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            */


            //SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,  transformMatrix: matrix);
            //effect.CurrentTechnique.Passes[0].Apply();
            // SpriteBatch.Draw(_imageFondMenu, new Vector2(scaleX, scaleY),Color.White);

            //_tiledMapRenderer.Draw(matrix);
            
          

            //SpriteBatch.Draw(_perso1, _perso1Position);
            //SpriteBatch.Draw(_perso2, _perso2Position);
            //SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

