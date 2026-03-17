using System;
using UnityEngine;

public class Big1Shot : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;//Bulletのオブジェクト

    //単体テストOK
    //発射処理
    public override void PatarnPlay(Transform enemyTransform)
    {
        //nullチェック
        if (enemyTransform == null)
            return;

        //発射対象の位置を取得
        Vector3 target = PlayerController.Instance.GetTransform.position;

        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = target - enemyTransform.position;

        //オブジェクト生成
        GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

        //Rigidbody2D取得
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //ノーマライズ
        Vector2 rotate = dirTarget.normalized;

        //発射方向代入
        bulletRB.velocity = rotate;
    }
}
