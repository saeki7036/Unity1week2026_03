using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot3Ways : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;//Bulletのオブジェクト

    [SerializeField] 
    float aimValue = 1000f;//方向計算強度

    [SerializeField]
    float ToDistance = 3f;

    //3Wayのため、補正を、-1,0,1の数値でかける
    //-1～1の間を指定して3方向を指定するための数値
    const int MinForwardValue = -1,
              MaxForwardValue = 1;

    //発射処理
    public override void PatarnPlay(Transform enemyTransform)
    {
        //nullチェック
        if (enemyTransform == null)
            return;

        //発射対象の位置を取得
        Vector3 target = PlayerController.Instance.GetTransform.position;

        Vector2 SpawnPos = enemyTransform.position;

        //3Wayのため、補正を、-1,0,1の数値でかける
        for (int i = MinForwardValue; i <= MaxForwardValue; i++)
        {
            //プレイヤーに飛ばす方向を計算
            Vector2 dirTarget = (target - enemyTransform.position).normalized;

            //発射方向計算
            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;

            //発射方向計算
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletPrehab, SpawnPos + ToDistance * rotate, Quaternion.identity);

            StartCoroutine(ShotStay(bullet, rotate, 3));

            //ShellController shell = bullet.GetComponent<ShellController>();          

            //shell.Shot(3, rotate);

            continue;

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //発射方向計算
            angleRadians = (aimValue * i)* Mathf.Deg2Rad;

            //発射方向計算
            rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
