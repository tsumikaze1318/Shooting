using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager
{
    #region 変数
    private Player _player;
    private Texture2D _entity;
    private SpriteBatch _spriteBatch;
    private Texture2D _bulletTexture;
    private List<Enemy> _enemies = new List<Enemy>();
    private float _enemySpawnTimer = 0f;
    private Random _random;
    private bool _enemyDestroyed = false;
    private bool _isGameOver = false;
    public bool IsGameOver => _isGameOver;
    private SpriteFont _gameFont;
    private int _score = 0;
    private Texture2D _playerHitDetection;
    private Texture2D _enemyTexture;
    #endregion


    /// <summary>
    /// コンストラクタで必要なリソースを初期化
    /// </summary>
    /// <param name="graphicsDevice">グラフィックスデバイス</param>
    /// <param name="gameFont">ゲームで使用するフォント</param>
    public GameManager(GraphicsDevice graphicsDevice, SpriteFont gameFont, Player player, Texture2D enemyTexture, Texture2D bulletTexture)
    {
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _gameFont = gameFont;
        _player = player;
        _enemyTexture = enemyTexture;
        _bulletTexture = bulletTexture;
        _random = new Random();
    }

    // ゲームの更新ロジックを管理するメソッド
    public void Update()
    {
        if (_isGameOver) return;
        _player.Update();

        _enemySpawnTimer += 1f;
        if (_enemySpawnTimer >= 60f) // 1秒ごとに敵を出現させる
        {
            _enemies.Add(new Enemy(new Vector2(_random.Next(0,1200), 0)));
            _enemySpawnTimer = 0f;
        }

        foreach (var enemy in _enemies)
        {
            enemy.Update();
        }
        // 敵と弾の衝突判定
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            for (int j = _player.GetPlayerBullets().Count - 1; j >= 0; j--)
            {
                // 敵とプレイヤーの弾が衝突した場合、敵と弾を削除し、スコアを加算
                if (_enemies[i].GetBounds().Intersects(_player.GetPlayerBullets()[j].GetBounds()))
                {
                    _enemies.RemoveAt(i);
                    _player.GetPlayerBullets().RemoveAt(j);
                    _score += 100;
                    break;
                }
            }
        }

        _player.InvisibleTime--;
        if (_player.InvisibleTime <= 0)
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                // 敵とプレイヤーが衝突した場合、ライフを減らす
                if (_enemies[i].GetBounds().Intersects(_player.GetBounds()))
                {
                    _player.Life--;
                    _player.InvisibleTime = 120; // プレイヤーがダメージを受けた後、一定時間無敵にする
                }
                for (int j = _enemies[i].GetEnemyBullets().Count - 1; j >= 0; j--)
                {
                    // 敵の弾とプレイヤーが衝突した場合、ライフを減らす
                    if (_enemies[i].GetEnemyBullets()[j].GetBounds().Intersects(_player.GetBounds()))
                    {
                        _enemies[i].GetEnemyBullets().RemoveAt(j);
                        _player.Life--;
                        _player.InvisibleTime = 120; // プレイヤーがダメージを受けた後、一定時間無敵にする
                    }
                }
            }
        }

        // プレイヤーのライフが0以下になった場合、ゲームオーバー
        if (_player.Life <= 0)
        {
            _isGameOver = true;
        }
    }

    public void Retry()
    {
        _player.Position = new Vector2(1280 / 2, 540);
        _player.Life = 3;
        _player.InvisibleTime = 0f;
        _player._playerBullets.Clear();
        _enemies.Clear();
        _score = 0;
        _isGameOver = false;
    }

    // ゲームの描画ロジックを管理するメソッド
    public void Draw()
    {
        _spriteBatch.Begin();
        // プレイヤーの描画
        _player.Draw(_spriteBatch);
        // プレイヤーの弾の描画
        foreach (var bullet in _player.GetPlayerBullets())
        {
            bullet.Draw(_spriteBatch, _bulletTexture);
        }
        // 敵の弾の描画
        foreach (var enemy in _enemies)
        {
            foreach (var bullet in enemy.GetEnemyBullets())
            {
                bullet.Draw(_spriteBatch, _bulletTexture);
            }
        }
        // 敵の描画
        foreach (var enemy in _enemies)
        {
            enemy.Draw(_spriteBatch, _enemyTexture);
        }
        // ゲームオーバーの表示
        if (_isGameOver)
        {
            // ゲームオーバーのテキストとスコアを画面中央に表示
            Vector2 textSize = _gameFont.MeasureString("GAME OVER");
            Vector2 scoreTextSize = _gameFont.MeasureString($"FinelScore:{_score}");
            _spriteBatch.DrawString(
                _gameFont, "GAME OVER",
                new Vector2(1280 / 2 - textSize.X / 2, 200),
                Color.Red);
            _spriteBatch.DrawString(
            _gameFont, $"FinelScore:{_score}",
            new Vector2(1280 / 2 - scoreTextSize.X / 2, 300),
            Color.White);
        }
        // ゲームオーバーでない場合はスコアを表示
        else
        {
            // スコアの表示
            _spriteBatch.DrawString(
                _gameFont, $"Score:{_score}",
                new Vector2(20, 20),
                Color.White);
            _spriteBatch.DrawString(
                _gameFont,$"Life:{_player.Life}",
                new Vector2(20, 70),
                Color.White);
        }
        _spriteBatch.End();
        
    }
}