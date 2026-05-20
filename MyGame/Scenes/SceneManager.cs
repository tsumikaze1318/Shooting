using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public enum SceneType
{
    Title,
    Game,
    GameOver
}

public class SceneManager
{
    private SceneType _currentScene = SceneType.Title;
    private GameManager _gameManager;
    public SceneManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Update()
    {
        switch (_currentScene)
        {
            case SceneType.Title:
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _currentScene = SceneType.Game;
                }
                break;
            case SceneType.Game:
                _gameManager.Update();
                if (_gameManager.IsGameOver)
                {
                    _currentScene = SceneType.GameOver;
                }
                break;
            case SceneType.GameOver:
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    _gameManager.Retry();
                    _currentScene = SceneType.Game;
                }
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch,SpriteFont font)
    {
        spriteBatch.Begin();
        switch (_currentScene)
        {
            case SceneType.Title:
                // タイトル画面の描画
                spriteBatch.DrawString(
                    font, "Press Enter to Start", 
                    new Vector2(1280 / 2 - textSize(font, "Press Enter to Start").X / 2, 600), Color.Black);
                break;
            case SceneType.Game:
                _gameManager.Draw();
                break;
            case SceneType.GameOver:
                // ゲームオーバー画面の描画
                _gameManager.Draw();
                break;
        }
        spriteBatch.End();
    }

    private static Vector2 textSize(SpriteFont font, string str)
    {
        return font.MeasureString(str);
    }
    
}
