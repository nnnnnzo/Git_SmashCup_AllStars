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
        private Vector2 _persoPosition;
        private AnimatedSprite _perso;
        private int _vitessePerso;
        private string animation;
        private string lastDir;
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
            _persoPosition = new Vector2(400, 200);
            _vitessePerso = 200;
            animation = "idleD";
            lastDir = "D";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // spritesheet
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("animRed.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            float walkSpeed = deltaSeconds * _vitessePerso;
            if (lastDir == "D")
                animation = "idleD";
            else
                animation = "idleG";

            KeyboardState keyboardState = Keyboard.GetState();
            
            if (keyboardState.IsKeyDown(Keys.D))
            {
                animation = "runD";
                _persoPosition.X += walkSpeed;
                lastDir = "D";
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                animation = "runG";
                _persoPosition.X -= walkSpeed;
                lastDir = "G";
            }
            // TODO: Add your update logic here
            _perso.Play(animation);
            _perso.Update(deltaSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _persoPosition);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
