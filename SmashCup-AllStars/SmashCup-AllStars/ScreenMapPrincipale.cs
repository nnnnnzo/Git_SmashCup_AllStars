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
        


      


       

        // Animation bullets

        private AnimatedSprite _bulletD1;
        private string _annimationBulletD1;
        List<Vector2> bulletsD1;

        private AnimatedSprite _bulletG1;
        private string _annimationBulletG1;
        List<Vector2> bulletsG1;

        private AnimatedSprite _bulletD2;
        private string _annimationBulletD2;
        List<Vector2> bulletsD2;

        private AnimatedSprite _bulletG2;
        private string _annimationBulletG2;
        List<Vector2> bulletsG2;
        private int _vitesseBullet;


        private SpriteFont _police;
      
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
        private string animationBulletMob;
        private int _vitesseBulletMob;
        private bool deplacementBMob;
        private string lastDirMob;




        public TiledMapTileLayer MapLayerSol { get => _mapLayerSol; set => _mapLayerSol = value; }
        public float Timer { get => _timer; set => _timer = value; }
        public Vector2 StartYPtest { get => _startYPtest; set => _startYPtest = value; }


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
            _positionDamagePersoBlue = new Vector2(0, 60);
            _damagePersoBlue = 0;

            // Vie perso Blue
            _positionDamagePersoBlue = new Vector2(0, 60);
            _damagePersoBlue = 0;

            //Timer
            _positionTimer = new Vector2(0, 120);
            _timer = 500;

            //Bullets
            
            _annimationBulletD1 = "dirD";
            _annimationBulletG1 = "dirG";
            _vitesseBullet = 500;
            _annimationBulletD2 = "dirD";
            _annimationBulletG2 = "dirG";


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
            _bulletPositionMob = new Vector2(100, 100);
            _vitesseBulletMob = 200;
            deplacementBMob = false;






            //base.Initialize();

        }

        public override void LoadContent()
        {
            _mapPrincipale = Content.Load<TiledMap>("IceMap");
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
            SpriteSheet spriteSheetB3 = Content.Load<SpriteSheet>("bullet.sf", new JsonContentLoader());
            _bulletMob = new AnimatedSprite(spriteSheetB3);

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

                _currentPositionDPersoBlue = new Vector2(_persoBlue.PositionPersoBlue.X + 50, _persoBlue.PositionPersoBlue.Y + 60);
                _currentPositionGPersoBlue = new Vector2(_persoBlue.PositionPersoBlue.X - 50, _persoBlue.PositionPersoBlue.Y + 60);

                _currentPositionDPersoRed = new Vector2(_persoRed.PositionPersoRed.X + 50, _persoRed.PositionPersoRed.Y + 60);
                _currentPositionGPersoRed = new Vector2(_persoRed.PositionPersoRed.X - 50, _persoRed.PositionPersoRed.Y + 60);

                
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    _game1.Exit();

             
                float walkSpeedBdf = deltaSeconds * _vitesseBullet;
                _timer = _timer - (deltaSeconds / 3);



                KeyboardState keyboardState = Keyboard.GetState();

                _currentCooldownPersoRed += deltaSeconds;
                _currentCooldownPersoBlue += deltaSeconds;
                //colisions
                Rectangle _boxPersoRed = new Rectangle((int)_persoRed.PositionPersoRed.X - 98 / 2, (int)_persoRed.PositionPersoRed.Y - 5, 98, 150);
                Rectangle _boxPersoBlue = new Rectangle((int)_persoBlue.PositionPersoBlue.X - 98 / 2, (int)_persoBlue.PositionPersoBlue.Y - 5, 98, 150);
                Rectangle _boxB3 = new Rectangle((int)_bulletPositionMob.X - 286 / 4, (int)_bulletPositionMob.Y - 146 / 4, 143, 73);
                Rectangle _boxMob = new Rectangle((int)_mobPosition.X - 400 / 2, (int)_mobPosition.Y - 200, 400, 600);





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
                        _damagePersoBlue= _damagePersoBlue+3;
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
                        _damagePersoBlue = _damagePersoBlue + 3;
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
                        _damagePersoRed = _damagePersoRed + 3;
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
                        _damagePersoRed = _damagePersoRed + 3;
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





            }

            else
                _timer = -1;

           


            // TODO: Add your update logic here



            _bulletD1.Play(_annimationBulletD1);
            _bulletG1.Play(_annimationBulletG1);
            _bulletD2.Play(_annimationBulletD2);
            _bulletG2.Play(_annimationBulletG2);
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


           
            
                _game1.SpriteBatch.DrawString(_police, $"Damage Counter PersoRed : {_damagePersoRed}", _positionDamagePersoRed, Color.White);
                _game1.SpriteBatch.DrawString(_police, $"Damage Counter PersoBlue : {_damagePersoBlue} ", _positionDamagePersoBlue, Color.White);
            if (_timer > 0)
            {
                _game1.SpriteBatch.DrawString(_police, $"Chrono : {Math.Round(Timer)} ", _positionTimer, Color.White);
            }

            Vector2 scalem = new Vector2((float)scaleX * 1.65f, (float)scaleY * 1.35f);
        

         

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
