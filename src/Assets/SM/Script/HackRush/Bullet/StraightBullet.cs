using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    //単体テスト済み

    //テスト用変数
    //Vector3 forcas = Vector3.zero;

    protected override void BulletSetUp()
    {
        //直線弾のみ
        Vector2 velocity = rb2D.velocity;

        //speedを反映
        rb2D.velocity = velocity.normalized * speed;

        //テスト用処理
        //forcas = (transform.position - vector).normalized;
    }

    /*
    void FixedUpdate()
    {
        //テスト用動作
        //transform.Translate(forcas * speed);
    }
    */
}
