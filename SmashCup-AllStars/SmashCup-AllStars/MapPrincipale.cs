using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace SmashCup-AllStars
{
    public class MapPrincipale : GameScreen
    {
    private Game _game1;
    private TiledMap _tiledMap;
    private TiledMapRenderer _tiledMapRenderer;

    public TiledMap TiledMap { get => _tiledMap; set => _tiledMap = value; }
    public TiledMapRenderer TiledMapRenderer { get => _tiledMapRenderer; set => _tiledMapRenderer = value; }




    public MapPrincipale(Game game) : base(game)
    {
        _game1 = game;
    }



    public override void LoadContent()
    {
        _tiledMap = Content.Load<TiledMap>("map");
        TiledMapRenderer = new TiledMapRenderer(GraphicsDevice, TiledMap);
        




    }

    public override void Update(GameTime gametime)
    {




    }


    public override void Draw(GameTime gameTime)
    {

        TiledMapRenderer.Draw();


    }

    }
}
