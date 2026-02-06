using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private const int _width = 750, _height = 450;
    private const int _playAreaEdgeLineWidth = 12, _BallWidthAndHeight = 100;

    private float _ballSpeed = 10;
    private Vector2 _ballPosition, _ballDirection;
    private Texture2D _backgroundTexture, _ballTexture;
    private Rectangle _playAreaBoundingBox;
    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _width;
        _graphics.PreferredBackBufferHeight = _height;

        _ballPosition.X = 150;
        _ballPosition.Y = 195; 
        _ballSpeed = 300;

        _ballDirection.X = 1;
        _ballDirection.Y = -1;

        _playAreaBoundingBox = new (0,0,_width,_height);
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _ballPosition+= _ballDirection * _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_ballPosition.X<=_playAreaBoundingBox.Left ||
           _ballPosition.X>=_playAreaBoundingBox.Right-_BallWidthAndHeight)
        {
            _ballDirection.X *= -1;
        }
        if(_ballPosition.Y<=_playAreaBoundingBox.Top ||
           _ballPosition.Y>=_playAreaBoundingBox.Bottom-_BallWidthAndHeight)
        {
            _ballDirection.Y *= -1;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, _playAreaBoundingBox, Color.Blue);

        var _ballRect = new Rectangle((int)_ballPosition.X,(int)_ballPosition.Y,_BallWidthAndHeight, _BallWidthAndHeight);


        _spriteBatch.Draw(_ballTexture, _ballRect, Color.White);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
