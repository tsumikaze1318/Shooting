using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class BulletPattern
{
    /// <summary>
    /// 弾の生成パターンを作成するメソッド
    /// </summary>
    /// <param name="position">弾の生成位置</param>
    /// <param name="bulletCount">生成する弾の数</param>
    /// <param name="centerAngle">弾の中心角度</param>
    /// <param name="diffusionAngle">弾の拡散角度</param>
    /// <returns>生成された弾のリスト</returns>
    public static List<Bullet> CreateBulletPattern(Vector2 position, int bulletCount, float centerAngle, float diffusionAngle)
    {
        List<Bullet> bullets = new List<Bullet>();
        if(bulletCount == 1)     // 単一の弾の場合は中心角度のみで生成
        {
            bullets.Add(new Bullet(position, AngleToDirection(centerAngle)));
            return bullets;
        }

        float startAngle = centerAngle - diffusionAngle / 2;    // 開始角度を計算
        float angleStep = diffusionAngle / (bulletCount - 1);   // 各弾の角度の間隔を計算
        for (int i = 0; i < bulletCount; i++)   // 指定された数の弾を生成
        {
            float angle = startAngle + i * angleStep;   // 各弾の角度を計算
            Vector2 direction = AngleToDirection(angle);   // 角度から方向ベクトルを計算
            bullets.Add(new Bullet(position, direction));
        }
        return bullets;
    }

    /// <summary>
    /// 角度から方向ベクトルを計算するヘルパーメソッド
    /// </summary>
    /// <param name="angle">角度（度単位）</param>
    /// <returns>方向ベクトル</returns>
    private static Vector2 AngleToDirection(float angle)
    {
        float radians = MathHelper.ToRadians(angle);
        return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }
}

