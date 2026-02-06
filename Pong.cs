using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private const int _width = 750, _height = 450;
    private const int _playAreaEdgeLineWidth = 12, _BallWidthAndHeight = 30;
    private const int _PaddleWidth = 10, _PaddleHeight = 60;

    private float _ballSpeed, _paddleSpeed;
    private Vector2 _ballPosition, _ballDirection;
    private Vector2 _paddlePosition, _paddleDirection;
    private Texture2D _backgroundTexture, _ballTexture, _paddleTexture;
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

        _paddlePosition = new Vector2(690, 198);
        _paddleSpeed = 500;
        _paddleDirection = Vector2.Zero;

        _playAreaBoundingBox = new (0,0,_width,_height);
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexture = Content.Load<Texture2D>("Paddle");
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
        
        KeyboardState key = Keyboard.GetState();
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(key.IsKeyDown(Keys.Up))
        {
            if(_paddlePosition.Y<_playAreaBoundingBox.Top)
            {
                _paddleDirection = Vector2.Zero;
            }
            else
            {
                _paddleDirection = new Vector2(0,-1);
            } 
        }
        else if(key.IsKeyDown(Keys.Down))
        {
            if(_paddlePosition.Y>_playAreaBoundingBox.Bottom-_PaddleHeight)
            {
                _paddleDirection = Vector2.Zero;
            }
            else
            {
                _paddleDirection = new Vector2(0,1);
            } 
        }
        else
        {
            _paddleDirection = Vector2.Zero;
        }

        _paddlePosition += _paddleDirection * _paddleSpeed * dt;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, _playAreaBoundingBox, Color.Blue);

        var _ballRect = new Rectangle((int)_ballPosition.X,(int)_ballPosition.Y,_BallWidthAndHeight, _BallWidthAndHeight);
        var _paddleRect = new Rectangle((int)_paddlePosition.X,(int)_paddlePosition.Y,_PaddleWidth, _PaddleHeight);

        _spriteBatch.Draw(_ballTexture, _ballRect, Color.White);
        _spriteBatch.Draw(_paddleTexture, _paddleRect, Color.White);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
