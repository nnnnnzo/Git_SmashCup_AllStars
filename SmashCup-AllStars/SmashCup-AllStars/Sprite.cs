using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace SmashCup_AllStars
{
    public class Sprite 
    {
        
        private Vector2 _position;
        private int _vitesse;
        private AnimatedSprite _perso;

       
        public Vector2 Position { get => _position; set => _position = value; }
        public int Vitesse { get => _vitesse; set => _vitesse = value; }
        public AnimatedSprite Perso { get => _perso; set => _perso = value; }

        public void Initialize(Vector2 position, int vitesse)
        {
            _position = position;
            _vitesse = vitesse;

           

        }


        public void LoadContent(SpriteSheet sprite,ContentManager content,string assetName)
        {

           


            sprite = content.Load<SpriteSheet>(assetName, new JsonContentLoader());
            _perso = new AnimatedSprite(sprite);

        }
        public void Update(GameTime gameTime)
        {
          
        }

        public void Draw (SpriteBatch spritebacth,GameTime gameTime)
        {

            spritebacth.Draw(_perso, _position);

        }











    }
}
