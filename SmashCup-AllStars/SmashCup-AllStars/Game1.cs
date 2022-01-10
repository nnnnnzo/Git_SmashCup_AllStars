﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
/*
ATTENTION: Valider, Tirer, Resoudre conflits, Envoyer
*/

namespace SmashCup_AllStars
{
    public class Game1 : Game
    {

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

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

        //Test nouveau perso avec une classe sprite
        private Sprite _persoTest;

        //animation boule de feu
        private AnimatedSprite _bdf;
        private Vector2 _bdfPosition;
        private string _bdfPositionDepart;
        private string animationBdf;
        private int _vitesseBdf;
        private bool deplacementBDF;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1750;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 950;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            //var joueur 1
            _perso1Position = new Vector2(200, 200);
            _vitessePerso1 = 200;
            animationP1 = "idleD";
            lastDirP1 = "D";
            startYP1 = _perso1Position.Y;//Starting position
            jumpingP1 = false;//Init jumping to false
            jumpspeedP1 = 0;


            //var joueur 2
            _perso2Position = new Vector2(600, 200);
            _vitessePerso2 = 200;
            animationP2 = "idleG";
            lastDirP2 = "G";
            startYP2 = _perso2Position.Y;//Starting position
            jumpingP2 = false;//Init jumping to false
            jumpspeedP2 = 0;

            animationBdf = "bouleDeFeuG";
            _bdfPosition = new Vector2(200, 200);
            _vitesseBdf = 200;
            deplacementBDF = false;

            // Test nouveaux perso avec la classe sprite

            _persoTest = new Sprite();
            _persoTest.Initialize(new Vector2(500, 200), 200);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // spritesheet
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());

            _perso1 = new AnimatedSprite(spriteSheetP1);

            SpriteSheet spriteSheetP2 = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheetP2);

            SpriteSheet spriteSheetBDF = Content.Load<SpriteSheet>("bdf.sf", new JsonContentLoader());
            _bdf = new AnimatedSprite(spriteSheetBDF);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeedPerso1 = deltaSeconds * _vitessePerso1;
            float walkSpeedPerso2 = deltaSeconds * _vitessePerso2;
            KeyboardState keyboardState = Keyboard.GetState();

            float walkSpeedBdf = deltaSeconds * _vitesseBdf;
            
            if (deplacementBDF)
            {
                if (_bdfPositionDepart == "D")
                {
                    if (_bdfPosition.X > 1600)
                    {
                        _bdfPosition = new Vector2(200, 200);
                        deplacementBDF = false;
                    }
                    else
                    {
                        animationBdf = "bouleDeFeuD";
                        _bdfPosition.X += walkSpeedBdf;
                    }
                }
                else
                {
                    if (_bdfPosition.X < 0)
                    {
                        _bdfPosition = new Vector2(200, 200);
                        deplacementBDF = false;
                    }
                    else
                    {
                        animationBdf = "bouleDeFeuG";
                        _bdfPosition.X -= walkSpeedBdf;
                    }
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.X))
                {
                    deplacementBDF = true;
                    _bdfPositionDepart = lastDirP2;
                }
            }


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
                    jumpspeedP1 = -14;//Give it upward thrust
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
                    jumpspeedP1 = -14;//Give it upward thrust
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
                    jumpspeedP2 = -14;//Give it upward thrust
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
            _perso1.Update(deltaSeconds);
            _perso2.Update(deltaSeconds);
            _bdf.Play(animationBdf);
            _bdf.Update(deltaSeconds);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            _tiledMapRenderer.Draw();
            _spriteBatch.Draw(_perso1, _perso1Position);
            _spriteBatch.Draw(_perso2, _perso2Position);
            _spriteBatch.Draw(_bdf, _bdfPosition);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

