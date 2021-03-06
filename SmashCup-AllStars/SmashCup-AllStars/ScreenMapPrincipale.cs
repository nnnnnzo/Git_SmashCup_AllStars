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
   
    public class ScreenMapPrincipale : GameScreen
    {

        private Game1 _game1;
        private TiledMap _mapPrincipale;
        private TiledMapRenderer _renduMapPrincipale;
        private TiledMapTileLayer _mapLayerSol;
        private float _definedCooldown = 0.6f;
        


        // Animation bullets

        private AnimatedSprite _bulletD1;
        private string _animationBulletD1;
        List<Vector2> bulletsD1;

        private AnimatedSprite _bulletG1;
        private string _animationBulletG1;
        List<Vector2> bulletsG1;

        private AnimatedSprite _bulletD2;
        private string _animationBulletD2;
        List<Vector2> bulletsD2;

        private AnimatedSprite _bulletG2;
        private string _animationBulletG2;
        List<Vector2> bulletsG2;
        private int _vitesseBullet;


        private SpriteFont _policePersoRed;
        private SpriteFont _policePersoBlue;
        private SpriteFont _policeMob;
        private SpriteFont _policeTimer;

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
        private Vector2 _currentPositionDPersoRed;
        private Vector2 _currentPositionGPersoRed;
        private int _damagePersoRed;
        private Vector2 _positionDamagePersoRed;
        private float _currentCooldownPersoRed;

        //Perso test Blue
        private PersoBlue _persoBlue;
        private Vector2 _currentPositionDPersoBlue;
        private Vector2 _currentPositionGPersoBlue;
        private int _damagePersoBlue;
        private Vector2 _positionDamagePersoBlue;
        private float _currentCooldownPersoBlue;

        // Mob
        private int _vieMob;
        private Vector2 _positionVieMob;
        private Vector2 _mobPosition;
        private int _vitesseMob;
        private AnimatedSprite _mob;
        private string animationMob;
        private bool spawnMob;
        private float _timerMort;
        private AnimatedSprite _bulletMob;
        private Vector2 _bulletPositionMob;
        private string _bulletPositionDepartMob;
        private int _bulletPosDepartY;
        private string animationBulletMob;
        private int _vitesseBulletMob;
        private bool deplacementBMob;
        private string lastDirMob;
        private int Yneed;

       

        // Condition de victoire



        public TiledMapTileLayer MapLayerSol { get => _mapLayerSol; set => _mapLayerSol = value; }
        public float Timer { get => _timer; set => _timer = value; }
        public Vector2 StartYPtest { get => _startYPtest; set => _startYPtest = value; }
       
        public int DamagePersoRed { get => _damagePersoRed; set => _damagePersoRed = value; }
        public int DamagePersoBlue { get => _damagePersoBlue; set => _damagePersoBlue = value; }

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



            _currentCooldownPersoRed = _definedCooldown;
            _currentCooldownPersoBlue = _definedCooldown;

            //Perso Class red
            _persoRed = new PersoRed();
          
            _persoRed.Initialize();

            //Perso Class blue
            _persoBlue = new PersoBlue();
            _persoBlue.Initialize();




            // Vie perso Red
            _positionDamagePersoRed = new Vector2(0, 0);
            _damagePersoRed = 0;
           

            // Vie perso Blue
            _positionDamagePersoBlue = new Vector2(2500, 0);
            _damagePersoBlue = 0;

            //Timer
            _positionTimer = new Vector2(1350, 60);
            _timer = 120;

            //Bullets
            
            _animationBulletD1 = "dirD";
            _animationBulletG1 = "dirG";
            _vitesseBullet = 500;
            _animationBulletD2 = "dirD";
            _animationBulletG2 = "dirG";



            // Vie IA / Mob
            _positionVieMob = new Vector2(1200, 0);
            _vieMob = 12;
            //var Mob
            _mobPosition = new Vector2(-500, -500);
            animationMob = "idleD";
            _vitesseMob = 50;
            //var bullet Mob
            animationBulletMob = "dirD";
            lastDirMob = "D";
            Yneed = (int)_persoRed.PositionPersoRed.Y + 80;
            _bulletPositionMob = new Vector2(100, 100);
            _vitesseBulletMob = 200;
            deplacementBMob = false;
            spawnMob = false;
            //timer mort mob
            _timerMort = 2;





            //base.Initialize();

        }

        public override void LoadContent()
        {
            _mapPrincipale = Content.Load<TiledMap>("IceMapFix");
            _renduMapPrincipale = new TiledMapRenderer(GraphicsDevice, _mapPrincipale);
            _mapLayerSol = _mapPrincipale.GetLayer<TiledMapTileLayer>("Terrain");
            _persoRed.MapLayerSolPersoRed = _mapLayerSol;
            _persoBlue.MapLayerSolPersoBlue = _mapLayerSol;
            






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

            // spritesheet mob
            SpriteSheet mobSheet = Content.Load<SpriteSheet>("spriteMob.sf", new JsonContentLoader());
            _mob = new AnimatedSprite(mobSheet);
            SpriteSheet spriteSheetB3 = Content.Load<SpriteSheet>("bulletMob.sf", new JsonContentLoader());
            _bulletMob = new AnimatedSprite(spriteSheetB3);

            // police 
            _policePersoRed = Content.Load<SpriteFont>("font2");
            _policePersoBlue = Content.Load<SpriteFont>("font2");
            _policeMob = Content.Load<SpriteFont>("font2");
            _policeTimer = Content.Load<SpriteFont>("font2");


            _persoRed.LoadContent(Content);
            _persoBlue.LoadContent(Content);
            //base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedBdf = deltaSeconds * _vitesseBullet;
            float walkSpeedBulletMob = deltaSeconds * _vitesseBulletMob;
            float walkSpeedMob = deltaSeconds * _vitesseMob;
            

            _timer = _timer - (deltaSeconds / 3);
            if (_timer > 0)
            {
                _currentPositionDPersoRed = new Vector2(_persoRed.PositionPersoRed.X + 50, _persoRed.PositionPersoRed.Y + 60);
                _currentPositionGPersoRed = new Vector2(_persoRed.PositionPersoRed.X - 50, _persoRed.PositionPersoRed.Y + 60);

                _currentPositionDPersoBlue = new Vector2(_persoBlue.PositionPersoBlue.X + 50, _persoBlue.PositionPersoBlue.Y + 60);
                _currentPositionGPersoBlue = new Vector2(_persoBlue.PositionPersoBlue.X - 50, _persoBlue.PositionPersoBlue.Y + 60);

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    _game1.Exit();

                KeyboardState keyboardState = Keyboard.GetState();

                _currentCooldownPersoRed += deltaSeconds;
                _currentCooldownPersoBlue += deltaSeconds;
                //colisions
                Rectangle _boxPersoRed = new Rectangle((int)_persoRed.PositionPersoRed.X - 98 / 2, (int)_persoRed.PositionPersoRed.Y - 5, 98, 150);
                Rectangle _boxPersoBlue = new Rectangle((int)_persoBlue.PositionPersoBlue.X - 98 / 2, (int)_persoBlue.PositionPersoBlue.Y - 5, 98, 150);
                Rectangle _boxB3 = new Rectangle((int)_bulletPositionMob.X - 286 / 4, (int)_bulletPositionMob.Y - 146 / 4, 143, 73);
                Rectangle _boxMob = new Rectangle((int)_mobPosition.X - 400 / 2, (int)_mobPosition.Y - 200, 400, 600);
                

                Console.WriteLine(_persoRed.PositionPersoRed.X+"  "+ _persoRed.PositionPersoRed.Y);
                //Missile test PersoRed Gauche ,droite
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (_currentCooldownPersoRed >= _definedCooldown)
                    {
                        if (_persoRed.LastDirPersoRed == "D")
                        {
                            bulletsD1.Add(_currentPositionDPersoRed);
                            _currentCooldownPersoRed = 0;
                        }
                        else
                        {
                            bulletsG1.Add(_currentPositionGPersoRed);
                            _currentCooldownPersoRed = 0;
                        }
                    }
                }

                for (int i = 0; i < bulletsD1.Count; i++)
                {

                    float x = bulletsD1[i].X;
                    x += walkSpeedBdf;
                    bulletsD1[i] = new Vector2(x, bulletsD1[i].Y);
                    Rectangle _colBoxD1 = new Rectangle((int)bulletsD1[i].X - 286 / 4, (int)bulletsD1[i].Y - 146 / 4, 143, 30);
                    if (_colBoxD1.Intersects(_boxPersoBlue))
                    {
                        DamagePersoBlue= DamagePersoBlue+3;
                        bulletsD1.RemoveAt(i);
                    }

                    if (_colBoxD1.Intersects(_boxMob))
                    {
                        _vieMob--;
                        bulletsD1.RemoveAt(i);
                    }
                }

                for (int i = 0; i < bulletsG1.Count; i++)
                {
                    float x = bulletsG1[i].X;
                    x -= walkSpeedBdf;
                    bulletsG1[i] = new Vector2(x, bulletsG1[i].Y);
                    Rectangle _colBoxG1 = new Rectangle((int)bulletsG1[i].X - 286 / 4, (int)bulletsG1[i].Y - 146 / 4, 143, 30);
                    if (_colBoxG1.Intersects(_boxPersoBlue))
                    {
                        DamagePersoBlue = DamagePersoBlue + 3;
                        bulletsG1.RemoveAt(i);
                    }
                    if (_colBoxG1.Intersects(_boxMob))
                    {
                        _vieMob --;
                        bulletsG1.RemoveAt(i);
                    }
                }

                //Missile test PersoBlue Gauche ,droite
                if (keyboardState.IsKeyDown(Keys.RightControl))
                {
                    if (_currentCooldownPersoBlue >= _definedCooldown)
                    {
                        if (_persoBlue.LastDirPersoBlue == "D")
                        {
                            bulletsD2.Add(_currentPositionDPersoBlue);
                            _currentCooldownPersoBlue = 0;
                        }
                        else 
                        {
                            bulletsG2.Add(_currentPositionGPersoBlue);
                            _currentCooldownPersoBlue = 0;
                        }
                    }
                }


                for (int i = 0; i < bulletsD2.Count; i++)
                {
                    float x = bulletsD2[i].X;
                    x += walkSpeedBdf;
                    bulletsD2[i] = new Vector2(x, bulletsD2[i].Y);
                    Rectangle _colBoxD2 = new Rectangle((int)bulletsD2[i].X - 286 / 4, (int)bulletsD2[i].Y - 146 / 4, 143, 30);
                    if (_colBoxD2.Intersects(_boxPersoRed))
                    {
                        DamagePersoRed = DamagePersoRed + 3;
                        bulletsD2.RemoveAt(i);
                    }
                    if (_colBoxD2.Intersects(_boxMob))
                    {
                        _vieMob--;
                        bulletsD2.RemoveAt(i);
                    }
                }

                for (int i = 0; i < bulletsG2.Count; i++)
                {
                    float x = bulletsG2[i].X;
                    x -= walkSpeedBdf;
                    bulletsG2[i] = new Vector2(x, bulletsG2[i].Y);
                    Rectangle _colBoxG2 = new Rectangle((int)bulletsG2[i].X - 286 / 4, (int)bulletsG2[i].Y - 146 / 4, 143, 30);
                    if (_colBoxG2.Intersects(_boxPersoRed))
                    {
                        DamagePersoRed = DamagePersoRed + 3;
                        bulletsG2.RemoveAt(i);
                    }
                    if (_colBoxG2.Intersects(_boxMob))
                    {
                        _vieMob--;
                        bulletsG2.RemoveAt(i);
                    }
                }             

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

                //----------------------------------------------------------------------------------------
                if (_timer >= 50 && _timer <=110) // temps où l'IA va apparaitre dans le jeu
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
                if (_timer >= 100 && _timer <= 115 && _mobPosition.X <= 700 && spawnMob == true)
                {
                    _mobPosition = new Vector2(1050, 650);
                }
                // si les perso tombent de la platforme, ils perdent une vie et respawn. 
                if (_persoRed.PositionPersoRed.X >= 2800 || _persoRed.PositionPersoRed.Y >= 1250 || _persoRed.PositionPersoRed.X <= 0)
                {
                    DamagePersoRed = DamagePersoRed + 3;
                    _persoRed.PositionPersoRed = new Vector2(800, 650);
                }

                if (_persoBlue.PositionPersoBlue.X >= 2700 || _persoBlue.PositionPersoBlue.Y >= 1250 || _persoBlue.PositionPersoBlue.X <= 0)
                {
                    DamagePersoBlue = DamagePersoBlue + 3;
                    _persoBlue.PositionPersoBlue = new Vector2(800, 650);
                }
                if (deplacementBMob) // apparition de la bullet du mob
                {
                    if (_bulletPositionDepartMob == "D")
                    {
                        animationBulletMob = "dirD";
                    }
                    if(_bulletPositionDepartMob == "G")
                    {
                        animationBulletMob = "dirG";
                    }
                    if (_bulletPositionMob.X > 2800 || _bulletPositionMob.X < 0)
                    {
                        deplacementBMob = false;
                    }
                    if (_boxB3.Intersects(_boxPersoRed))
                    {
                        DamagePersoRed = DamagePersoRed + 3;
                        deplacementBMob = false;
                    }
                    if (_boxB3.Intersects(_boxPersoBlue))
                    {
                        DamagePersoBlue = DamagePersoBlue + 3;
                        deplacementBMob = false;
                    }
                    else
                    {
                        if (_vieMob <= 6 && _vieMob > 0) // vitesse de la bullet suivant si le boss est en mode rage ou non.
                        {
                            if (_bulletPositionDepartMob == "D")
                            {
                                _bulletPositionMob.X += walkSpeedBulletMob * 2;
                                _bulletPositionMob.Y = _bulletPosDepartY;
                            }
                            if (_bulletPositionDepartMob == "G")
                            {
                                _bulletPositionMob.X -= walkSpeedBulletMob * 2;
                                _bulletPositionMob.Y = _bulletPosDepartY;
                            }
                        }
                        else
                        {
                            if (_bulletPositionDepartMob == "D")
                            {
                                _bulletPositionMob.X += walkSpeedBulletMob;
                                _bulletPositionMob.Y = _bulletPosDepartY;
                            }
                            if (_bulletPositionDepartMob == "G")
                            {
                                _bulletPositionMob.X -= walkSpeedBulletMob;
                                _bulletPositionMob.Y = _bulletPosDepartY;
                            }
                        }
                    }
                }
                else
                {
                    if (spawnMob == true)
                    {
                        deplacementBMob = true;
                        _bulletPositionDepartMob = lastDirMob;
                        _bulletPosDepartY = Yneed+80;
                        _bulletPositionMob = _mobPosition;
                        _bulletPositionMob.Y = _persoRed.PositionPersoRed.Y + 80;
                    }
                }
                if (_vieMob <= 0) // si la vie de l'IA est <= à 0, elle reste à 0
                {
                    _vieMob = 0;
                }
                // idleD = qui regarde à droite et idleG = qui regarde à gauche
                int distRed = (int)_mobPosition.X - (int)_persoRed.PositionPersoRed.X;
                int distBlue = (int)_mobPosition.X - (int)_persoBlue.PositionPersoBlue.X;
                // idleD = qui regarde à droite et idleG = qui regarde à gauche
                if (spawnMob)
                {
                    if (Math.Abs(distRed) < Math.Abs(distBlue)) // orientation de l'IA suivant si le personnage est à gauche ou à droite 
                    {
                        if (distRed > 0)
                        {
                            animationMob = "idleG";
                            lastDirMob = "G";
                            Yneed = (int)_persoRed.PositionPersoRed.Y;
                            _mobPosition.X -= walkSpeedMob;
                        }
                        if (distRed < 0)
                        {
                            animationMob = "idleD";
                            lastDirMob = "D";
                            Yneed = (int)_persoRed.PositionPersoRed.Y;
                            _mobPosition.X += walkSpeedMob;
                        }
                    }
                    else
                    {
                        if (distBlue > 0)
                        {
                            animationMob = "idleG";
                            lastDirMob = "G";
                            Yneed = (int)_persoBlue.PositionPersoBlue.Y;
                            _mobPosition.X -= walkSpeedMob;
                        }
                        if (distBlue < 0)
                        {
                            animationMob = "idleD";
                            lastDirMob = "D";
                            Yneed = (int)_persoBlue.PositionPersoBlue.Y;
                            _mobPosition.X += walkSpeedMob;
                        }
                    }
                }
                //----------------------------------------------------------------------------------------
            }

            else
                _timer = -1;


            _bulletD1.Play(_animationBulletD1);
            _bulletG1.Play(_animationBulletG1);
            _bulletD2.Play(_animationBulletD2);
            _bulletG2.Play(_animationBulletG2);
            _bulletMob.Play(animationBulletMob);
            _mob.Play(animationMob);
            _bulletD1.Update(deltaSeconds);
            _bulletG1.Update(deltaSeconds);
            _bulletD2.Update(deltaSeconds);
            _bulletG2.Update(deltaSeconds);
            _bulletMob.Update(deltaSeconds);
            _mob.Update(deltaSeconds);
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
            _game1.SpriteBatch.DrawString(_policePersoRed, $"Damage Red : {DamagePersoRed} %", _positionDamagePersoRed, Color.DarkRed);
            _game1.SpriteBatch.DrawString(_policePersoBlue, $"Damage Blue : {DamagePersoBlue} %", _positionDamagePersoBlue, Color.DarkBlue);
            
            if (_timer > 0)
            {
                _game1.SpriteBatch.DrawString(_policeTimer, $"{Math.Round(_timer)}", _positionTimer, Color.Black);
            }

            Vector2 scalem = new Vector2((float)scaleX * 1.65f, (float)scaleY * 1.35f);
            Vector2 scalem2 = new Vector2((float)scaleX * 2f, (float)scaleY * 1.6f);

            if (spawnMob == true)
            {
               
                
                    _game1.SpriteBatch.Draw(_bulletMob, _bulletPositionMob, 0, scalem);
                    _game1.SpriteBatch.Draw(_mob, _mobPosition, 0, scalem2);
                    _game1.SpriteBatch.DrawString(_policeMob, $"Vie BOSS [ {_vieMob} ]", _positionVieMob, Color.DarkGreen);
                
            }

            
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
        }
    }
}
