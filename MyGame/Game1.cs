using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;
    private Player _player;
    private SceneManager _sceneManager;
    private SpriteFont _gameFont;
    private Texture2D _entity;
    private Texture2D _playerHitDetection;
    private Texture2D _bulletTexture;
    public SpriteFont GameFont => _gameFont;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gameFont = Content.Load<SpriteFont>("GameFont"); // GameFont.spritefont
        _entity = Content.Load<Texture2D>("entity"); // entity.png
        _playerHitDetection = Content.Load<Texture2D>("player_hit_detection"); // player_hit_detection.png
        _bulletTexture = Content.Load<Texture2D>("bullet"); // bullet.png
        _player = new Player(_entity, _playerHitDetection);
        _gameManager = new GameManager(GraphicsDevice, _gameFont,_player, _entity,_bulletTexture);
        _sceneManager = new SceneManager(_gameManager);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _sceneManager.Update();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _sceneManager.Draw(_spriteBatch, _gameFont);
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
