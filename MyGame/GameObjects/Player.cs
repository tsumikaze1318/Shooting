using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Player
{
    public Vector2 Position = new Vector2(1280/2, 540);
    public List<Bullet> _playerBullets = new List<Bullet>();
    private float _shootCooldown = 0f;
    public int Life = 3;
    public float InvisibleTime = 120f;
    private Texture2D _player;
    private Texture2D _playerHitDetection;

    public Player(Texture2D player, Texture2D playerHitDetection)
    {
        _player = player;
        _playerHitDetection = playerHitDetection;
    }

    public List<Bullet> GetPlayerBullets() => _playerBullets;

    // プレイヤーの移動と弾の発射を管理
    public void Update()
    {
        // プレイヤーの移動
        var keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Right))
            Position.X += 3;
        if (keyboard.IsKeyDown(Keys.Left))
            Position.X -= 3;
        if (keyboard.IsKeyDown(Keys.Up))
            Position.Y -= 3;
        if (keyboard.IsKeyDown(Keys.Down))
            Position.Y += 3;

        // 弾の発射
        Vector2 bulletPos = new Vector2(Position.X + 24, Position.Y);
        if (keyboard.IsKeyDown(Keys.Space) && _shootCooldown <= 0)
        {
            _playerBullets.AddRange(BulletPattern.CreateBulletPattern(bulletPos, 1, -90, 0));
            _shootCooldown = 10; // 連射制限
        }

        if (_shootCooldown > 0)
            _shootCooldown--;

        // 弾の更新と画面外に出た弾の削除
        for (int i = _playerBullets.Count - 1; i >= 0; i--)
        {
            _playerBullets[i].Update();

            if (_playerBullets[i].Position.Y < 0)
            {
                _playerBullets.RemoveAt(i);
            }
        }

    }

    // プレイヤーの当たり判定用の矩形を取得
    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X + +24, (int)Position.Y + 24, 16, 16);
    }

    // プレイヤーの描画
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_player, Position, Color.Blue);
        spriteBatch.Draw(_playerHitDetection, GetBounds(), Color.Gray);
    }
}