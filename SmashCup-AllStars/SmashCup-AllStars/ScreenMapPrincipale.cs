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
using System.Collections.Generic;

namespace SmashCup_AllStars
{
    public enum TimerFin { Fin };
    public class ScreenMapPrincipale : GameScreen
    {

        private Game1 _game1;
        private TiledMap _mapPrincipale;
        private TiledMapRenderer _renduMapPrincipale;
        private TiledMapTileLayer _mapLayerSol;
        private float _definedCooldown = 0.5f;
        private float _currentCooldownP1;
        private float _currentCooldownP2;

        //perso rouge
        private Vector2 _perso1Position;
        private AnimatedSprite _perso1;
        private int _vitessePerso1;
        private string animationP1;
        private string _lastDirP1;

        //perso bleu
        private Vector2 _perso2Position;
        private AnimatedSprite _perso2;
        private int _vitessePerso2;
        private string animationP2;
        private string lastDirP2;
        private bool jumpingP1, jumpingP2; //Is the character jumping?
        private float startYP1, jumpspeedP1 = 0, startYP2, jumpspeedP2 = 0; //startY to tell us //where it lands, jumpspeed to see how fast it jumps

      


        //animation boule de feu 
        private AnimatedSprite _bullet1;
        private AnimatedSprite _bullet2;
        private Vector2 _bulletPosition1;
        private Vector2 _bulletPosition2;
        private string _bulletPositionDepart1;
        private string _bulletPositionDepart2;
        private string animationBullet1;
        private string animationBullet2;
        private int _vitesseBullet;
        private bool deplacementB1;
        private bool deplacementB2;

        // Animation bullets

        private AnimatedSprite _bulletD1;
        private Vector2 _positionBulletD1;
        private string _annimationBulletD1;
        List<Vector2> bulletsD1;

        private AnimatedSprite _bulletG1;
        private Vector2 _positionBulletG1;
        private string _annimationBulletG1;
        List<Vector2> bulletsG1;

        private AnimatedSprite _bulletD2;
        private Vector2 _positionBulletD2;
        private string _annimationBulletD2;
        List<Vector2> bulletsD2;

        private AnimatedSprite _bulletG2;
        private Vector2 _positionBulletG2;
        private string _annimationBulletG2;
        List<Vector2> bulletsG2;


        private SpriteFont _police;
        //vie perso 1
        private int _vieperso1;
        private Vector2 _positionVie1;
        //vie perso 2
        private int _vieperso2;
        private Vector2 _positionVie2;
        //timer
        private float _timer;
        private Vector2 _positionTimer;

        public Vector2 _finPosition;
        public Vector2 _tailleTexteFin;

        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;

        //perso test red
        private PersoRed _persoRed;
        private Vector2 _startYPtest;
        
        //Perso test Blue
        private PersoBlue _persoBlue;





        public TiledMapTileLayer MapLayerSol { get => _mapLayerSol; set => _mapLayerSol = value; }
        public float Timer { get => _timer; set => _timer = value; }
        public Vector2 StartYPtest { get => _startYPtest; set => _startYPtest = value; }

        //public Vector2 Perso1Position { get => _perso1Position; set => _perso1Position = value; }

        public ScreenMapPrincipale(Game1 game) : base(game)
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

           
           
            _currentCooldownP1 = _definedCooldown;
            _currentCooldownP2 = _definedCooldown;

            //Perso Class red
            _persoRed = new PersoRed();
            _persoRed.Initialize();

            //Perso Class blue
            _persoBlue = new PersoBlue();
            _persoBlue.Initialize();


          /*

            //var joueur 1
            _perso1Position = new Vector2(900, 200);
            _vitessePerso1 = 200;
            animationP1 = "idleD";
            _lastDirP1 = "D";
            startYP1 = _perso1Position.Y;//Starting position
            jumpingP1 = false;//Init jumping to false
            jumpspeedP1 = 0;

            //var joueur 2
            _perso2Position = new Vector2(WIDTH_WINDOW / 2, HEIGHT_WINDOW / 2);
            _vitessePerso2 = 200;
            animationP2 = "idleG";
            lastDirP2 = "G";
            startYP2 = _perso2Position.Y;//Starting position
            jumpingP2 = false;//Init jumping to false
            jumpspeedP2 = 0;

            //Boule de feu
            animationBullet1 = "dirG";
            animationBullet2 = "dirD";
            _bulletPosition1 = new Vector2(800, -100);
            _bulletPosition2 = new Vector2(800, -100);
            _vitesseBullet = 500;
            deplacementB1 = false;
            deplacementB2 = false;
          */

            // Vie perso
            _positionVie1 = new Vector2(0, 0);
            _vieperso1 = 3;
            _positionVie2 = new Vector2(0, 60);
            _vieperso2 = 3;


            //Timer
            _positionTimer = new Vector2(0, 120);
            _timer = 500;

            //Bullets
            _positionBulletD1 = new Vector2(800, 200);
            _annimationBulletD1 = "dirD";
            _annimationBulletG1 = "dirG";

            _annimationBulletD2 = "dirD";
            _annimationBulletG2 = "dirG";

        




            //base.Initialize();

        }

        public override void LoadContent()
        {
            _mapPrincipale = Content.Load<TiledMap>("IceMap");
            _renduMapPrincipale = new TiledMapRenderer(GraphicsDevice, _mapPrincipale);
            _mapLayerSol = _mapPrincipale.GetLayer<TiledMapTileLayer>("Terrain");
            _persoRed.MapLayerSolPersoRed = _mapLayerSol;
            _persoBlue.MapLayerSolPersoBlue = _mapLayerSol;



            // spritesheet personnages

            /*
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);

            SpriteSheet spriteSheetP2 = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheetP2);
            */
           

            bulletsD1 = new List<Vector2>();
            SpriteSheet bulletD1Image = Content.Load<SpriteSheet>("bulletRedV2.sf", new JsonContentLoader());
            _bulletD1 = new AnimatedSprite(bulletD1Image);

            bulletsG1 = new List<Vector2>();
            SpriteSheet bulletG1Image = Content.Load<SpriteSheet>("bulletRedV2.sf", new JsonContentLoader());
            _bulletG1 = new AnimatedSprite(bulletG1Image);

           
            bulletsD2 = new List<Vector2>();
            SpriteSheet bulletD2Image = Content.Load<SpriteSheet>("bulletBlueV2.sf", new JsonContentLoader());
            _bulletD2 = new AnimatedSprite(bulletD2Image);

            bulletsG2 = new List<Vector2>();
            SpriteSheet bulletG2Image = Content.Load<SpriteSheet>("bulletBlueV2.sf", new JsonContentLoader());
            _bulletG2 = new AnimatedSprite(bulletG2Image);

            // police de vie
            _police = Content.Load<SpriteFont>("Font");

            
            _persoRed.LoadContent(Content);
            _persoBlue.LoadContent(Content);
            //base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            if (_timer > 0)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    _game1.Exit();

                float walkSpeedPerso1 = deltaSeconds * _vitessePerso1;
                float walkSpeedPerso2 = deltaSeconds * _vitessePerso2;
               
                float walkSpeedBdf = deltaSeconds * _vitesseBullet;
                _timer = _timer - (deltaSeconds / 3);



                KeyboardState keyboardState = Keyboard.GetState();

                _currentCooldownP1 += deltaSeconds;
                _currentCooldownP2 += deltaSeconds;
                //colisions
                Rectangle _boxPerso1 = new Rectangle((int)_perso1Position.X - 98 / 2, (int)_perso1Position.Y - 5, 98, 150);
                Rectangle _boxPerso2 = new Rectangle((int)_perso2Position.X - 98 / 2, (int)_perso2Position.Y - 5, 98, 150);

               
                

                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (_currentCooldownP1 >= _definedCooldown)
                    {
                        if (_lastDirP1 == "D")
                        {
                            bulletsD1.Add(new Vector2(_persoRed.PositionPersoRed.X + 50, _persoRed.PositionPersoRed.Y + 60));
                            _currentCooldownP1 = 0;
                        }
                        else
                        {
                            bulletsG1.Add(new Vector2(_persoRed.PositionPersoRed.X - 50, _persoRed.PositionPersoRed.Y + 60));
                            _currentCooldownP1 = 0;
                        }

                    }
                }

                

                
                if (keyboardState.IsKeyDown(Keys.RightControl))
                {
                    if (_currentCooldownP2 >= _definedCooldown)
                    {
                        if (lastDirP2 == "D")
                        {
                            bulletsD2.Add(new Vector2(_perso2Position.X + 50, _perso2Position.Y + 60));
                            _currentCooldownP2 = 0;
                        }
                        else
                        {
                            bulletsG2.Add(new Vector2(_perso2Position.X - 50, _perso2Position.Y + 60));
                            _currentCooldownP2 = 0;
                        }

                    }
                }

                for (int i = 0; i < bulletsD1.Count; i++)
                {

                    float x = bulletsD1[i].X;
                    x += walkSpeedBdf;
                    bulletsD1[i] = new Vector2(x, bulletsD1[i].Y);
                    Rectangle _colBoxD1 = new Rectangle((int)bulletsD1[i].X - 286 / 4, (int)bulletsD1[i].Y - 146 / 4, 143, 30);
                    if (_colBoxD1.Intersects(_boxPerso2))
                    {
                        _vieperso2--;
                        bulletsD1.RemoveAt(i);
                    }

                }

                for (int i = 0; i < bulletsG1.Count; i++)
                {
                    float x = bulletsG1[i].X;
                    x -= walkSpeedBdf;
                    bulletsG1[i] = new Vector2(x, bulletsG1[i].Y);
                    Rectangle _colBoxG1 = new Rectangle((int)bulletsG1[i].X - 286 / 4, (int)bulletsG1[i].Y - 146 / 4, 143, 30);
                    if (_colBoxG1.Intersects(_boxPerso2))
                    {
                        _vieperso2--;
                        bulletsG1.RemoveAt(i);
                    }

                }

                

                

                for (int i = 0; i < bulletsD2.Count; i++)
                {
                    float x = bulletsD2[i].X;
                    x += walkSpeedBdf;
                    bulletsD2[i] = new Vector2(x, bulletsD2[i].Y);
                    Rectangle _colBoxD2 = new Rectangle((int)bulletsD2[i].X - 286 / 4, (int)bulletsD2[i].Y - 146 / 4, 143, 30);
                    if (_colBoxD2.Intersects(_boxPerso1))
                    {
                        _vieperso1--;
                        bulletsD2.RemoveAt(i);
                    }


                }
                for (int i = 0; i < bulletsG2.Count; i++)
                {
                    float x = bulletsG2[i].X;
                    x -= walkSpeedBdf;
                    bulletsG2[i] = new Vector2(x, bulletsG2[i].Y);
                    Rectangle _colBoxG2 = new Rectangle((int)bulletsG2[i].X - 286 / 4, (int)bulletsG2[i].Y - 146 / 4, 143, 30);
                    if (_colBoxG2.Intersects(_boxPerso1))
                    {
                        _vieperso1--;
                        bulletsG2.RemoveAt(i);
                    }
                }

                


                /*

                //Missile test PersoRed Gauche Marche droite non

                if (keyboardState.IsKeyDown(Keys.I))
                {
                    if (_currentCooldownP1 >= _definedCooldown)
                    {
                        if (_persoRed.LastDirPersoRed == "D")
                        {
                            bulletsD1.Add(new Vector2(_persoRed.PositionPersoRed.X/ +50, _persoRed.PositionPersoRed.Y + 60));
                            _currentCooldownP1 = 0;
                        }
                        else if (_persoRed.LastDirPersoRed == "G")
                        {
                            bulletsG1.Add(new Vector2(_persoRed.PositionPersoRed.X - 50, _persoRed.PositionPersoRed.Y + 60));
                            _currentCooldownP1 = 0;
                        }
                     
                    }
                }

                for (int i = 0; i < bulletsD1.Count; i++)
                {

                    float x = bulletsD1[i].X;
                    x += walkSpeedBdf;
                    bulletsD1[i] = new Vector2(x, bulletsD1[i].Y);
                    Rectangle _colBoxD1 = new Rectangle((int)bulletsD1[i].X - 286 / 4, (int)bulletsD1[i].Y - 146 / 4, 143, 30);
                    if (_colBoxD1.Intersects(_boxPerso2))
                    {
                        _vieperso2--;
                        bulletsD1.RemoveAt(i);
                    }

                }

                for (int i = 0; i < bulletsG1.Count; i++)
                {
                    float x = bulletsG1[i].X;
                    x -= walkSpeedBdf;
                    bulletsG1[i] = new Vector2(x, bulletsG1[i].Y);
                    Rectangle _colBoxG1 = new Rectangle((int)bulletsG1[i].X - 286 / 4, (int)bulletsG1[i].Y - 146 / 4, 143, 30);
                    if (_colBoxG1.Intersects(_boxPerso2))
                    {
                        _vieperso2--;
                        bulletsG1.RemoveAt(i);
                    }

                }

                */




                

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
                if (_lastDirP1 == "D")
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
                    _lastDirP1 = "D";
                }



                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    animationP1 = "runG";
                    _perso1Position.X -= walkSpeedPerso1;
                    _lastDirP1 = "G";
                }

           
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

                /*
                
                ushort x1 = (ushort)(_perso1Position.X / 70 + 0.5);
                ushort y1 = (ushort)(_perso1Position.Y / 70 + 2.12);


                int tile1 = MapLayerSol.GetTile(x1, y1).GlobalIdentifier;
                if (tile1 == 0)
                {
                    _perso1Position.Y += 14;
                }
                else
                    startYP1 = _perso1Position.Y;

                ushort x2 = (ushort)(_perso2Position.X / 70 + 0.5);
                ushort y2 = (ushort)(_perso2Position.Y / 70 + 2);


                int tile2 = MapLayerSol.GetTile(x2, y2).GlobalIdentifier;
                if (tile2 == 0)
                {
                    _perso2Position.Y += 14;
                }
                else
                    startYP2 = _perso2Position.Y;

                */

                // Gravité classe perso Red test si fonctionne ou pas

               
                ushort xPersoRed = (ushort)(_persoRed.PositionPersoRed.X / 70 + 0.5);
                ushort yPersoRed = (ushort)(_persoRed.PositionPersoRed.Y / 70 + 2.12);


                int tilePersoRed = MapLayerSol.GetTile(xPersoRed, yPersoRed).GlobalIdentifier;
                if (tilePersoRed == 0)
                {
                    _persoRed.PositionPersoRed += new Vector2(0, 14);
                }
                else
                {
                    _startYPtest = _persoRed.PositionPersoRed;

                }


                // Gravité classe perso Bluetest si fonctionne ou pas


                ushort xPersoBlue = (ushort)(_persoBlue.PositionPersoBlue.X / 70 + 0.5);
                ushort yPersoBlue = (ushort)(_persoBlue.PositionPersoBlue.Y / 70 + 2.12);


                int tilePersoBlue= MapLayerSol.GetTile(xPersoBlue, yPersoBlue).GlobalIdentifier;
                if (tilePersoBlue == 0)
                {
                    _persoBlue.PositionPersoBlue += new Vector2(0, 14);
                }
                else
                {
                    _startYPtest = _persoBlue.PositionPersoBlue;

                }





            }

            else
                _timer = -1;

            /*
            for (int i = 0; i < bulletsD2.Count; i++)
            {
                float x = bulletsD2[i].X;
                x += walkSpeedBdf;
                bulletsD2[i] = new Vector2(x, bulletsD2[i].Y);
                Rectangle _colBoxD2 = new Rectangle((int)bulletsD2[i].X - 286 / 4, (int)bulletsD2[i].Y - 146 / 4, 143, 30);
                if (_colBoxD2.Intersects(_boxPerso1))
                {
                    _vieperso1--;
                    bulletsD2.RemoveAt(i);
                }


            }
            for (int i = 0; i < bulletsG2.Count; i++)
            {
                float x = bulletsG2[i].X;
                x -= walkSpeedBdf;
                bulletsG2[i] = new Vector2(x, bulletsG2[i].Y);
                Rectangle _colBoxG2 = new Rectangle((int)bulletsG2[i].X - 286 / 4, (int)bulletsG2[i].Y - 146 / 4, 143, 30);
                if (_colBoxG2.Intersects(_boxPerso1))
                {
                    _vieperso1--;
                    bulletsG2.RemoveAt(i);
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


            int tile1 = MapLayerSol.GetTile(x1, y1).GlobalIdentifier;
            if (tile1 == 0)
            {
                _perso1Position.Y += 14;
            }
            else
                startYP1 = _perso1Position.Y;

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
            */


            // TODO: Add your update logic here



            // _perso1.Play(animationP1);
            // _perso2.Play(animationP2);
           
            _bulletD1.Play(_annimationBulletD1);
            _bulletG1.Play(_annimationBulletG1);

            _bulletD2.Play(_annimationBulletD2);
            _bulletG2.Play(_annimationBulletG2);



           // _perso1.Update(deltaSeconds);
            //_perso2.Update(deltaSeconds);
            

            _bulletD1.Update(deltaSeconds);
            _bulletG1.Update(deltaSeconds);

            _bulletD2.Update(deltaSeconds);
            _bulletG2.Update(deltaSeconds);
          
           

          

            _persoRed.Update(gameTime);
            _persoBlue.Update(gameTime);
            //base.Update(gameTime);
        }
    

           

        
           

        public override void Draw(GameTime gameTime)
        {

            _game1.GraphicsDevice.Clear(new Color(99, 160, 166));
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 2800;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 1400;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            var matrixMin = Matrix.CreateScale(scaleX, scaleY, 0.5f);


            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _renduMapPrincipale.Draw(matrix);

            //_game1.SpriteBatch.Draw(_persoRed.Perso1, _persoRed.Perso1Position);


           
            
                _game1.SpriteBatch.DrawString(_police, $"Vie RED : {_vieperso1}", _positionVie1, Color.White);
                _game1.SpriteBatch.DrawString(_police, $"Vie BLUE : {_vieperso2} ", _positionVie2, Color.White);
            if (_timer > 0)
            {
                _game1.SpriteBatch.DrawString(_police, $"Chrono : {Math.Round(Timer)} ", _positionTimer, Color.White);
            }

            Vector2 scalem = new Vector2((float)scaleX * 1.65f, (float)scaleY * 1.35f);
          //  _game1.SpriteBatch.Draw(_perso1, _perso1Position);
          //  _game1.SpriteBatch.Draw(_perso2, _perso2Position);

         

            for (int i = 0; i < bulletsD1.Count; i++)
                _game1.SpriteBatch.Draw(_bulletD1, bulletsD1[i], 0, scalem);

            for (int i = 0; i < bulletsG1.Count; i++)
                _game1.SpriteBatch.Draw(_bulletG1, bulletsG1[i], 0, scalem);

            for (int i = 0; i < bulletsD2.Count; i++)
                _game1.SpriteBatch.Draw(_bulletD2, bulletsD2[i], 0, scalem);

            for (int i = 0; i < bulletsG2.Count; i++)
                _game1.SpriteBatch.Draw(_bulletG2, bulletsG2[i], 0, scalem);


           
            _persoRed.Draw(_game1.SpriteBatch);
            _persoBlue.Draw(_game1.SpriteBatch);
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
