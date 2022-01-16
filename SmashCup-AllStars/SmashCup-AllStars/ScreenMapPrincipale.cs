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
        private float _currentCooldown;

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

        private Vector2 _mobPosition;
        private AnimatedSprite _mob;
        private string animationMob;
        private bool spawnMob;
        private float _timerMort;

        private AnimatedSprite _bulletMob;
        private Vector2 _bulletPositionMob;
        private string _bulletPositionDepartMob;
        private string animationBulletMob;
        private int _vitesseBulletMob;
        private bool deplacementBMob;
        private string lastDirMob;

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

        private AnimatedSprite _bullet;
        private Vector2 _positionBullet;
        private string _annimationBullet;

        List<Vector2> bullets;


        private SpriteFont _police;
        //vie perso 1
        private int _vieperso1;
        private Vector2 _positionVie1;
        //vie perso 2
        private int _vieperso2;
        private Vector2 _positionVie2;
        //vie Mob
        private int _vieMob;
        private Vector2 _positionVieMob;

        //timer
        private float _timer;
        private Vector2 _positionTimer;
        public Vector2 _finPosition;
        public Vector2 _tailleTexteFin;

        public static int WIDTH_WINDOW = 1200;
        public static int HEIGHT_WINDOW = 700;


        //private PersoRed _persoRed;

        public TiledMapTileLayer MapLayerSol { get => _mapLayerSol; set => _mapLayerSol = value; }
        public float Timer { get => _timer; set => _timer = value; }

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


            // joueur 1
            //_persoRed = new PersoRed();
            _currentCooldown = _definedCooldown;


            //var joueur 1
            _perso1Position = new Vector2(WIDTH_WINDOW / 2, HEIGHT_WINDOW / 2);
            _vitessePerso1 = 200;
            animationP1 = "idleD";
            lastDirP1 = "D";
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

            // Vie perso
            _positionVie1 = new Vector2(0, 0);
            _vieperso1 = 3;
            _positionVie2 = new Vector2(0, 60);
            _vieperso2 = 3;

            // Vie IA / Mob
            _positionVieMob = new Vector2(1200, 0);
            _vieMob = 12;
            //var Mob
            _mobPosition = new Vector2(-500, -500);
            animationMob = "idleG";
            //var bullet Mob
            animationBulletMob = "dirG";
            _bulletPositionMob = new Vector2(100, 100);
            _vitesseBulletMob = 100;
            deplacementBMob = false;


            //Timer
            _positionTimer = new Vector2(0, 120);
            _timer = 180;

            _timerMort = 2;

            //Bullets
            _positionBullet = new Vector2(800, 200);
            _annimationBullet = "dirD";



            //base.Initialize();

        }

        public override void LoadContent()
        {
            _mapPrincipale = Content.Load<TiledMap>("IceMap");
            _renduMapPrincipale = new TiledMapRenderer(GraphicsDevice, _mapPrincipale);
            MapLayerSol = _mapPrincipale.GetLayer<TiledMapTileLayer>("Terrain");



            // spritesheet personnages
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);

            SpriteSheet spriteSheetP2 = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheetP2);

            // spritesheet mob
            SpriteSheet mobSheet = Content.Load<SpriteSheet>("spriteMob.sf", new JsonContentLoader());
            _mob = new AnimatedSprite(mobSheet);

            // spritesheet boule de feu
            SpriteSheet spriteSheetB1 = Content.Load<SpriteSheet>("bullet.sf", new JsonContentLoader());
            _bullet1 = new AnimatedSprite(spriteSheetB1);
            SpriteSheet spriteSheetB2 = Content.Load<SpriteSheet>("bullet.sf", new JsonContentLoader());
            _bullet2 = new AnimatedSprite(spriteSheetB2);
            SpriteSheet spriteSheetB3 = Content.Load<SpriteSheet>("bullet.sf", new JsonContentLoader());
            _bulletMob = new AnimatedSprite(spriteSheetB3);

            bullets = new List<Vector2>();
            SpriteSheet bulletImage = Content.Load<SpriteSheet>("bullet.sf", new JsonContentLoader());
            _bullet = new AnimatedSprite(bulletImage);

            

            // police de vie
            _police = Content.Load<SpriteFont>("Font");

            //spritesheet bullet

            //_bullet = new AnimatedSprite(spriteSheetBullet);

            //_persoRed.LoadContent(Content);

            //base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPerso1 = deltaSeconds * _vitessePerso1;
            float walkSpeedPerso2 = deltaSeconds * _vitessePerso2;
            float walkSpeedBdf = deltaSeconds * _vitesseBullet;
            float deltaSecond = deltaSeconds / 2;
            _timer = _timer - (deltaSeconds / 3);
            if (_timer > 0)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    _game1.Exit();
              
                



                KeyboardState keyboardState = Keyboard.GetState();

                _currentCooldown += deltaSeconds;
                //colisions
                Rectangle _boxPerso1 = new Rectangle((int)_perso1Position.X - 98 / 2, (int)_perso1Position.Y - 5, 98, 150);
                Rectangle _boxPerso2 = new Rectangle((int)_perso2Position.X - 98 / 2, (int)_perso2Position.Y - 5, 98, 150);
                Rectangle _boxB1 = new Rectangle((int)_bulletPosition1.X - 286 / 4, (int)_bulletPosition1.Y - 146 / 4, 143, 73);
                Rectangle _boxB2 = new Rectangle((int)_bulletPosition2.X - 286 / 4, (int)_bulletPosition2.Y - 146 / 4, 143, 73);
                Rectangle _boxB3 = new Rectangle((int)_bulletPositionMob.X - 286 / 4, (int)_bulletPositionMob.Y - 146 / 4, 143, 73);
                Rectangle _boxMob = new Rectangle((int)_mobPosition.X - 400 / 2, (int)_mobPosition.Y - 200, 400, 600); // collision pour que les joueurs lui tire dessus
                //Rectangle _boxMobRangeG = new Rectangle((int)_mobPosition.X - 750, (int)_mobPosition.Y, 750, 900); // collision pour déplacer le mob et le faire s'approcher des joueurs
                //Rectangle _boxMobRangeD = new Rectangle((int)_mobPosition.X, (int)_mobPosition.Y, 750, 900); // collision pour déplacer le mob et le faire s'approcher des joueurs
                
                
                if (_boxB2.Intersects(_boxPerso1))
                {
                    _vieperso1--;
                }
                if (_boxB1.Intersects(_boxPerso2))
                {
                    _vieperso2--;
                }
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    if (_currentCooldown >= _definedCooldown)
                    {
                        bullets.Add(_perso1Position);
                        _currentCooldown = 0;
                    }
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    float x = bullets[i].X;
                    x += walkSpeedBdf;
                    bullets[i] = new Vector2(x, bullets[i].Y);

                }


                //bdf perso rouge (1)
                if (deplacementB1)
                {
                    if (_bulletPositionDepart1 == "D")
                    {
                        animationBullet1 = "dirD";
                        if (_bulletPosition1.X > 2800)
                        {
                            deplacementB1 = false;
                        }
                        if(_boxB1.Intersects(_boxPerso2))
                        {
                            _vieperso2--;
                            deplacementB1 = false;
                        }
                        if(_boxB1.Intersects(_boxMob))
                        {
                            _vieMob--;
                            deplacementB1 = false;
                        }
                        else
                        {
                            //animationBullet2 = "dirD";
                            _bulletPosition1.X += walkSpeedBdf;
                        }
                    }
                    else
                    {
                        animationBullet1 = "dirG";
                        if (_bulletPosition1.X < 0)
                        {
                            deplacementB1 = false;
                        }
                        if (_boxB1.Intersects(_boxPerso2))
                        {
                            _vieperso2--;
                            deplacementB1 = false;
                        }
                        if (_boxB1.Intersects(_boxMob))
                        {
                            _vieMob--;
                            deplacementB1 = false;
                        }
                        else
                        {
                            _bulletPosition1.X -= walkSpeedBdf;
                        }
                    }
                }
                else
                {
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        deplacementB1 = true;
                        _bulletPositionDepart1 = lastDirP1;
                        _bulletPosition1 = _perso1Position;
                        _bulletPosition1.Y = _bulletPosition1.Y + 75;
                    }
                }
                //bdf perso bleu (2)
                if (deplacementB2)
                {
                    if (_bulletPositionDepart2 == "D")
                    {
                        animationBullet2 = "dirD";
                        if (_bulletPosition2.X > 2800)
                        {
                            deplacementB2 = false;
                        }
                        if (_boxB2.Intersects(_boxPerso1))
                        {
                            _vieperso2--;
                            deplacementB2 = false;
                        }
                        if (_boxB2.Intersects(_boxMob))
                        {
                            _vieMob--;
                            deplacementB2 = false;
                        }
                        else
                        {
                            //animationBullet2 = "dirD";
                            _bulletPosition2.X += walkSpeedBdf;
                        }
                    }
                    else
                    {
                        animationBullet2 = "dirG";
                        if (_bulletPosition2.X < 0)
                        {
                            deplacementB2 = false;
                        }
                        if (_boxB2.Intersects(_boxPerso1))
                        {
                            _vieperso1--;
                            deplacementB2 = false;
                        }
                        if (_boxB2.Intersects(_boxMob))
                        {
                            _vieMob--;
                            deplacementB2 = false;
                        }
                        else
                        {
                            //animationBullet2 = "dirG";
                            _bulletPosition2.X -= walkSpeedBdf;
                        }
                    }
                }
                else
                {
                    if (keyboardState.IsKeyDown(Keys.RightControl))
                    {
                        deplacementB2 = true;
                        _bulletPositionDepart2 = lastDirP2;
                        _bulletPosition2 = _perso2Position;
                        _bulletPosition2.Y = _bulletPosition2.Y + 75;
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


                //----------------------------------------------------------------------------------------
                if (_timer >= 100 && _timer <= 175 ) // temps où l'IA va apparaitre dans le jeu
                {
                    spawnMob = true;
                    
                }
                else
                {
                    spawnMob = false;
                }

                if (_vieMob == 0) // si la vie de l'IA est égale à 0 on fait apparaitre le sprite de sa mort pendant 2s (PS : ça marche)
                {
                    animationMob = "mort";
                    _timerMort = _timerMort - (deltaSeconds / 3);
                }
                if (_timerMort <= 0)
                {
                    spawnMob = false;
                    _mobPosition = new Vector2(-500, -500);
                }
                if (_timer >= 100 && _timer <= 175  && _mobPosition.X <= 700 && spawnMob == true)
                {
                    _mobPosition = new Vector2(800, 650);
                }
                if (deplacementBMob) // apparition de la bullet du mob
                {
                    if (_bulletPositionDepartMob == "D")
                    {
                        animationBulletMob = "dirD";
                        if (_bulletPositionMob.X > 2800)
                        {
                            deplacementBMob = false;
                        }
                        if (_boxB3.Intersects(_boxPerso1))
                        {
                            _vieperso1--;
                            deplacementBMob = false;
                        }
                        if (_boxB3.Intersects(_boxPerso2))
                        {
                            _vieperso2--;
                            deplacementBMob = false;
                        }
                        else
                        {
                            _bulletPositionMob.X += walkSpeedBdf;
                        }
                    }
                    else
                    {
                        animationBulletMob = "dirG";
                        if (_bulletPositionMob.X < 0)
                        {
                            deplacementBMob = false;
                        }
                        if (_boxB3.Intersects(_boxPerso1))
                        {
                            _vieperso1--;
                            deplacementBMob = false;
                        }
                        if (_boxB3.Intersects(_boxPerso2))
                        {
                            _vieperso2--;
                            deplacementBMob = false;
                        }
                        else
                        {
                            _bulletPositionMob.X -= walkSpeedBdf;
                        }
                    }
                }
                else
                {
                    if (spawnMob == true)
                    {
                        deplacementBMob = true;
                        _bulletPositionDepartMob = lastDirMob;
                        _bulletPositionMob = _mobPosition;
                        _bulletPositionMob.Y = _perso1Position.Y + 80;
                    }
                }

                if (_vieMob <= 0) // si la vie de l'IA est <= à 0, elle reste à 0
                {
                    _vieMob = 0;
                }

                if (spawnMob)
                {
                    //Direction du mob selon le joueur (IA)
                    if (_vieMob > 6) // si la vie est supérieur à 6
                    {
                        if (_perso1Position.X > _mobPosition.X) // orientation de l'IA suivant si le personnage est à gauche ou à droite 
                        {
                            animationMob = "idleG";
                            if(_mobPosition.X <= 1350)
                            {
                                _mobPosition.X++;
                                lastDirMob = "D";
                            }
                        }
                        else if (_perso1Position.X < _mobPosition.X)
                        {
                            animationMob = "idleD";
                            if (_mobPosition.X >= 700)
                            {
                                _mobPosition.X--;
                                lastDirMob = "G";
                            }
                        }
                        else if (_perso1Position.X == _mobPosition.X)
                        {
                            animationMob = "idleD";
                        }
                    }
                    else if (_vieMob <= 6 && _vieMob > 0) // si la vie de l'IA est entre 0 et 6 de vie
                    {
                        if (_perso1Position.X > _mobPosition.X)
                        {
                            animationMob = "rageG";
                            if (_mobPosition.X <= 1350)
                            {
                                _mobPosition.X++;
                                lastDirMob = "D";
                            }
                        }
                        else if (_perso1Position.X < _mobPosition.X)
                        {
                            animationMob = "rageD";
                            if (_mobPosition.X >= 700)
                            {
                                _mobPosition.X--;
                                lastDirMob = "G";
                            }
                        }
                        else if (_perso1Position.X == _mobPosition.X)
                        {
                            animationMob = "rageD";
                        }
                    }
                }

                // si les perso tombent de la platforme, ils perdent une vie et respawn. 
                if(_perso1Position.X >= 2800 || _perso1Position.Y >= 1250 || _perso1Position.X <= 0)
                {
                    _vieperso1--;
                    _perso1Position = new Vector2(800, 650);
                }

                if (_perso2Position.X >= 2700 || _perso2Position.Y >= 1250 || _perso2Position.X <= 0)
                {
                    _vieperso2--;
                    _perso2Position = new Vector2(800, 650);
                }

                    //----------------------------------------------------------------------------------------

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
            }

            else
                _timer = -1;

            /*
            _currentCooldown += deltaSeconds;
            //colisions
            Rectangle _boxPerso1 = new Rectangle((int)_perso1Position.X - 98 / 2, (int)_perso1Position.Y - 5, 98, 150);
            Rectangle _boxPerso2 = new Rectangle((int)_perso2Position.X - 98 / 2, (int)_perso2Position.Y - 5, 98, 150);
            Rectangle _boxB1 = new Rectangle((int)_bulletPosition1.X - 286 / 4, (int)_bulletPosition1.Y - 146 / 4, 143, 73);
            Rectangle _boxB2 = new Rectangle((int)_bulletPosition2.X - 286 / 4, (int)_bulletPosition2.Y - 146 / 4, 143, 73);
            if (_boxB2.Intersects(_boxPerso1))
            {
                _vieperso1--;
            }
            if (_boxB1.Intersects(_boxPerso2))
            {
                _vieperso2--;
            }


            if (keyboardState.IsKeyDown(Keys.R))
            {
                if (_currentCooldown >= _definedCooldown)
                {
                    bullets.Add(_perso1Position);
                    _currentCooldown = 0;
                }
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                  float x = bullets[i].X;
                  x += walkSpeedBdf;
                  bullets[i] = new Vector2(x, bullets[i].Y);
  
            }


            //bdf perso rouge (1)
            if (deplacementB1)
            {
                if (_bulletPositionDepart1 == "D")
                {
                    animationBullet1 = "dirD";
                    if (_bulletPosition1.X > 2800 || _boxB1.Intersects(_boxPerso2))
                    {
                        deplacementB1 = false;
                    }
                    else
                    {
                        //animationBullet2 = "dirD";
                        _bulletPosition1.X += walkSpeedBdf;
                    }
                }
                else
                {
                    animationBullet1 = "dirG";
                    if (_bulletPosition1.X < 0 || _boxB1.Intersects(_boxPerso2))
                    {
                        deplacementB1 = false;
                    }
                    else
                    {
                        _bulletPosition1.X -= walkSpeedBdf;
                    }
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    deplacementB1 = true;
                    _bulletPositionDepart1 = lastDirP1;
                    _bulletPosition1 = _perso1Position;
                    _bulletPosition1.Y = _bulletPosition1.Y + 75;
                }
            }
            //bdf perso bleu (2)
            if (deplacementB2)
            {
                if (_bulletPositionDepart2 == "D")
                {
                    animationBullet2 = "dirD";
                    if (_bulletPosition2.X > 2800 || _boxB2.Intersects(_boxPerso1))
                    {
                        deplacementB2 = false;
                    }
                    else
                    {
                        //animationBullet2 = "dirD";
                        _bulletPosition2.X += walkSpeedBdf;
                    }
                }
                else
                {
                    animationBullet2 = "dirG";
                    if (_bulletPosition2.X < 0 || _boxB2.Intersects(_boxPerso1))
                    {
                        deplacementB2 = false;
                    }
                    else
                    {
                        //animationBullet2 = "dirG";
                        _bulletPosition2.X -= walkSpeedBdf;
                    }
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.RightControl))
                {
                    deplacementB2 = true;
                    _bulletPositionDepart2 = lastDirP2;
                    _bulletPosition2 = _perso2Position;
                    _bulletPosition2.Y = _bulletPosition2.Y + 75;
                }
            }
            */

            /*

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


            _perso1.Play(animationP1);
            _perso2.Play(animationP2);
            _bullet1.Play(animationBullet1);
            _bullet2.Play(animationBullet2);
            _bulletMob.Play(animationBullet2);
            //_bullet.Play(_annimationBullet);
            _perso1.Update(deltaSeconds);
            //_persoRed.Perso1.Update(deltaSeconds);
            _perso2.Update(deltaSeconds);
            _bullet1.Update(deltaSeconds);
            _bullet2.Update(deltaSeconds);
            _bulletMob.Update(deltaSeconds);
            //_bullet.Update(deltaSeconds);
            //_persoRed.Update(gameTime);
            _mob.Play(animationMob);
            _mob.Update(deltaSecond);
            _perso1.Play(animationP1);
            _perso2.Play(animationP2);
            _bullet1.Play(animationBullet1);
            _bullet2.Play(animationBullet2);
            _bullet.Play(_annimationBullet);
            _perso1.Update(deltaSeconds);
            //_persoRed.Perso1.Update(deltaSeconds);
            _perso2.Update(deltaSeconds);
            _bullet1.Update(deltaSeconds);
            _bullet2.Update(deltaSeconds);
            _bullet.Update(deltaSeconds);
            //_persoRed.Update(gameTime);

            //base.Update(gameTime);
        }
           



        


        public override void Draw(GameTime gameTime)
        {

            _game1.GraphicsDevice.Clear(new Color(99, 160, 166));
            var scaleX = (float)_game1.Graphics.PreferredBackBufferWidth / 2800;
            var scaleY = (float)_game1.Graphics.PreferredBackBufferHeight / 1400;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            var matrixMin = Matrix.CreateScale(scaleX, scaleY, 0.5f);

            _game1.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: matrix);
            _renduMapPrincipale.Draw(matrix);

            //_game1.SpriteBatch.Draw(_persoRed.Perso1, _persoRed.Perso1Position);

            _game1.SpriteBatch.DrawString(_police, $"Vie RED : {_vieperso1}", _positionVie1, Color.White);
            _game1.SpriteBatch.DrawString(_police, $"Vie BLUE : {_vieperso2} ", _positionVie2, Color.White);
            _game1.SpriteBatch.DrawString(_police, $"Chrono : {Math.Round(Timer)} ", _positionTimer, Color.White);
           // _game1.SpriteBatch.DrawString(_police, $"Vie BOSS [ {_vieMob} ]", _positionVieMob, Color.White);


            Vector2 scalem = new Vector2((float)scaleX * 1.5f, (float)scaleY * 1.5f);
            Vector2 scalem2 = new Vector2((float)scaleX * 2f, (float)scaleY * 1.6f);
            _game1.SpriteBatch.Draw(_perso1, _perso1Position);
            _game1.SpriteBatch.Draw(_perso2, _perso2Position);
            //_game1.SpriteBatch.DrawRectangle(new RectangleF(0, 0, 2800, 1400), Color.Black, 1f);
            if(spawnMob == true)
            {
                _game1.SpriteBatch.Draw(_mob, _mobPosition, 0, scalem2);
                _game1.SpriteBatch.DrawString(_police, $"Vie BOSS [ {_vieMob} ]", _positionVieMob, Color.White);
                _game1.SpriteBatch.Draw(_bulletMob, _bulletPositionMob, 0, scalem);
            }
            if (deplacementB1 == true)
                _game1.SpriteBatch.Draw(_bullet1, _bulletPosition1, 0, scalem);
            if (deplacementB2 == true)
                _game1.SpriteBatch.Draw(_bullet2, _bulletPosition2, 0, scalem);
            for (int i = 0; i < bullets.Count; i++)
                _game1.SpriteBatch.Draw(_bullet, bullets[i], 0, scalem);

         //_game1.SpriteBatch.Draw(_bullet, _positionBullet);
            //_persoRed.Draw(_game1.SpriteBatch);
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
