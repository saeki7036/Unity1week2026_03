using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HormingBullet : BulletBase
{
    [SerializeField]
    float Interval = 3f;//遅延時間

    protected override void BulletSetUp()
    {
        //velocityを取得
        Vector2 velocity = rb2D.velocity;

        //velocityにspeedを反映
        rb2D.velocity = velocity.normalized * speed;

        //非同期処理起動
        StartCoroutine(Horming());
    }

    IEnumerator Horming()
    {
        // インターバル遅延
        yield return new WaitForSeconds(Interval);

        // プレイヤー方向を計算
        Vector2 velocityFromPlayer = PlayerController.Instance.GetTransform.position - transform.position;

        // プレイヤー方向に変更
        rb2D.velocity = velocityFromPlayer.normalized * speed;
    }
}
