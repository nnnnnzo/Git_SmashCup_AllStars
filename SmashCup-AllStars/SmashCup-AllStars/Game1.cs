using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
/*
ATTENTION: Valider, Tirer, Resoudre conflits, Envoyer
*/

namespace SmashCup_AllStars
{
    public class Game1 : Game
    {
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
        /*blabla*/
        /*Test modif Gab*/
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _perso1Position = new Vector2(200, 200);
            _vitessePerso1 = 200;
            animationP1 = "idleD";
            lastDirP1 = "D";

            _perso2Position = new Vector2(600, 200);
            _vitessePerso2 = 200;
            animationP2 = "idleG";
            lastDirP2 = "G";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // spritesheet
            SpriteSheet spriteSheetP1 = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());
            _perso1 = new AnimatedSprite(spriteSheetP1);

            SpriteSheet spriteSheetP2 = Content.Load<SpriteSheet>("animBlue.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheetP2);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeed = deltaSeconds * _vitessePerso1;
            if (lastDirP1 == "D")
                animationP1 = "idleD";
            else
                animationP1 = "idleG";

            if (lastDirP2 == "D")
                animationP2 = "idleD";
            else
                animationP2 = "idleG";

            KeyboardState keyboardState = Keyboard.GetState();
            
            if (keyboardState.IsKeyDown(Keys.D))
            {
                animationP1 = "runD";
                _perso1Position.X += walkSpeed;
                lastDirP1 = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                animationP1 = "runG";
                _perso1Position.X -= walkSpeed;
                lastDirP1 = "G";
            }


            if (keyboardState.IsKeyDown(Keys.Right))
            {
                animationP2 = "runD";
                _perso2Position.X += walkSpeed;
                lastDirP2 = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                animationP2 = "runG";
                _perso2Position.X -= walkSpeed;
                lastDirP2 = "G";
            }
            // TODO: Add your update logic here
            _perso1.Play(animationP1);
            _perso2.Play(animationP2);
            _perso1.Update(deltaSeconds);
            _perso2.Update(deltaSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso1, _perso1Position);
            _spriteBatch.Draw(_perso2, _perso2Position);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
