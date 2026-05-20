using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Bullet
{
    public Vector2 Position;
    private Vector2 _velocity;
    private float _speed = 5f;

    /// <summary>
    /// コンストラクタで初期位置と移動方向を設定
    /// </summary>
    /// <param name="startPos">弾の初期位置</param>
    /// <param name="direction">弾の移動方向</param>
    public Bullet(Vector2 startPos, Vector2 direction)
    {
        Position = startPos;
        _velocity = direction * _speed;
    }

    // 弾の移動を管理
    public void Update()
    {
        Position += _velocity;
    }

    // 弾の当たり判定用の矩形を取得
    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X + 2, (int)Position.Y + 2, 12, 12);
    }

    // 弾の描画
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, Position, Color.White);
    }
}