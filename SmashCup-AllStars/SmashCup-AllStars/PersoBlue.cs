using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
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
    class PersoBlue
    {
        private AnimatedSprite _textureBleu;
        private Vector2 _positionBleu;
        private int _vitesseBleu;

        
        public Vector2 PositionBleu { get => _positionBleu; set => _positionBleu = value; }
        public int VitesseBleu { get => _vitesseBleu; set => _vitesseBleu = value; }
        public AnimatedSprite TextureBleu { get => _textureBleu; set => _textureBleu = value; }
    }
}
