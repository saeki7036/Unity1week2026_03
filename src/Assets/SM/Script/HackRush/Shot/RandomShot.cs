using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomShot : ShotPatarnBase
{
    [SerializeField]
    GameObject bulletPrehab;//Bulletのオブジェクト

    [SerializeField] 
    float aimValue = 560f;//方向計算強度

    [SerializeField] 
    float patarnReportTimeSpan = 0.1f;//発射間隔の遅延時間

    [SerializeField]
    int patarnReportValue = 50;//発射パターンの起動回数

    [SerializeField]
    int oneSetBulletCount = 3;//パターン毎の発射回数

    //ランダムパラメータのための上限、下限。
    //数値1ごとに10度を想定
    const int RandomMinValue = 0, 
              RandomMaxValue = 37;

    //ランダムな方向を取得(0～36の間)
    int RandomAngle => UnityEngine.Random.Range(RandomMinValue, RandomMaxValue);

    //発射処理
    public override void PatarnPlay(Transform enemyTransform)
    {
        //UniTask非同期処理起動
        //発射処理の遅延処理の起動
        StartCoroutine(PatarnDelay(enemyTransform));
    }

    //発射処理の遅延処理
    IEnumerator PatarnDelay(Transform enemyTransform)
    {
        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0; i < patarnReportValue; i++)
        {
            //インターバル遅延
            yield return new WaitForSeconds(patarnReportTimeSpan);

            //複数回の発射処理起動
            RepeatPatarnPlay(enemyTransform);
        }
    }

    //複数回の発射処理
    void RepeatPatarnPlay(Transform enemyTransform)
    {
        //プレイヤーのnullチェック
        if (PlayerController.Instance.GetTransform == null) 
            return;

        //nullチェック
        if (enemyTransform == null)
            return;

        //発射対象の位置を取得
        Vector3 target = PlayerController.Instance.GetTransform.position;

        //発射回数分ループ
        for (int i = 0; i < oneSetBulletCount; i++)
        {
            //基準となる方向を計算
            Vector2 dirTarget = target - enemyTransform.position;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //ランダムに方向を指定
            float angleRadians = (aimValue * RandomAngle) * Mathf.Deg2Rad;

            //発射方向計算
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
