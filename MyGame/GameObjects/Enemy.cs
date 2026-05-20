using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Enemy
{
    private Vector2 _position;
    private float _speed = 2f;
    private float _bulletSpawnTimer = 0f;
    private List<Bullet> _enemyBullets = new List<Bullet>();
    public List<Bullet> GetEnemyBullets() => _enemyBullets;

    /// <summary>
    /// コンストラクタで初期位置を設定
    /// </summary>
    /// <param name="position">敵の初期位置</param>
    public Enemy(Vector2 position)
    {
        _position = position;
    }

    // 敵の移動と弾の発射を管理
    public void Update()
    {
        _position.Y += _speed;
        _bulletSpawnTimer += 1f;
        Vector2 bulletPos = new Vector2(_position.X + 24, _position.Y + 24); // 敵の中心から弾を発射
        if (_bulletSpawnTimer >= 60f) // 1秒ごとに弾を発射
        {
            _enemyBullets.AddRange(BulletPattern.CreateBulletPattern(bulletPos, 5, 90, 60)); // 5方向に弾を発射
            _bulletSpawnTimer = 0f;
        }
        
        for(int i = _enemyBullets.Count - 1; i >= 0; i--)
        {
            _enemyBullets[i].Update();

            if (_enemyBullets[i].Position.Y > 720) // 画面下端に到達したら削除
            {
                _enemyBullets.RemoveAt(i);
            }
        }
    }

    // 敵の当たり判定用の矩形を取得
    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X + 16, (int)_position.Y + 16, 32, 32);
    }

    // 敵の描画
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, _position, Color.Red);
    }

}