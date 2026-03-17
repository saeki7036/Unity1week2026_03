using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBigHorming : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;//Bulletのオブジェクト(小さい弾)

    [SerializeField] 
    GameObject bulletLarge;//Bulletのオブジェクト(大きい弾)

    [SerializeField] 
    float aimValue = 560f;//方向計算強度

    [SerializeField] 
    float patarnReportTimeSpan = 0.2f;//発射間隔の遅延時間

    [SerializeField] 
    int patarnReportValue = 5;//発射パターンの起動回数

    [SerializeField]
    float ToDistance = 3f;

    //プレイヤーの方向以外の方向にばら撒くため3～33を指定
    //O～2及び34～36の間はプレイヤーの方向になるため発射させない
    const int MinBackValue = 3,
              MaxBackValue = 33;

    //全体の発射処理
    public override void PatarnPlay(Transform enemyTransform)
    {
        //発射対象の位置を取得
        Vector3 target = PlayerController.Instance.GetTransform.position;

        //相手に向かって直線に発射するパターン起動
        MainPatarnPlay(target, enemyTransform);

        //UniTask非同期処理起動
        //相手以外の方向に扇形に発射するパターン起動
        StartCoroutine(OtherPatarnDelay(target, enemyTransform));
    }

    //発射処理(プレイヤーに1方向)
    void MainPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        Vector2 SpawnPos = enemyTransform.position;

        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = (target - enemyTransform.position).normalized;

        //オブジェクト生成
        GameObject bullet = Instantiate(bulletLarge, SpawnPos + ToDistance * dirTarget, Quaternion.identity);

        //Rigidbody2D取得
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //ノーマライズ
        Vector2 rotate = Quaternion.Euler(Vector3.forward) * dirTarget.normalized;

        //発射方向代入
        bulletRB.velocity = rotate;
    }

    //発射処理の遅延処理
    IEnumerator OtherPatarnDelay(Vector3 target, Transform enemyTransform) 
    {
        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0;i < patarnReportValue; i++)
        {
            //インターバル遅延
            yield return new WaitForSeconds(patarnReportTimeSpan);

            //発射処理起動
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    //発射処理(プレイヤーじゃない方向に扇状に発射)
    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //nullチェック
        if (enemyTransform == null)
            return;

        Vector2 SpawnPos = enemyTransform.position;

        //プレイヤーの方向以外の方向にばら撒くため3～33を指定
        for (int i = MinBackValue; i <= MaxBackValue; i++)
        {
            //基準となる方向を計算
            Vector2 dirTarget = (target - enemyTransform.position).normalized;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletSmall, SpawnPos + ToDistance * dirTarget, Quaternion.identity);

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //発射方向計算
            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;

            //発射方向計算
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
