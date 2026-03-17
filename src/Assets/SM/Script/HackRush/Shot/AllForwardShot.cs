using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AllForwardShot : ShotPatarnBase
{
    [SerializeField]
    GameObject bulletPrehab;//Bulletのオブジェクト

    [SerializeField] 
    float aimValue = 560f;//方向計算強度

    [SerializeField]
    float patarnReportTimeSpan = 0.3f;//発射間隔の遅延時間

    [SerializeField]
    int patarnReportValue = 3;//発射パターンの起動回数

    //10度ずつ離して36回動かして10度ずつ全方向を指定する
    const int AllForwardValue = 36;

    //発射処理
    public override void PatarnPlay(Transform enemyTransform)
    {
        //発射対象の位置を取得
        Vector3 target = PlayerController.Instance.GetTransform.position;

        //UniTask非同期処理起動
        //発射処理の遅延処理の起動
        StartCoroutine(PatarnDelay(target, enemyTransform));
    }

    //発射処理の遅延処理
    IEnumerator PatarnDelay(Vector3 target, Transform enemyTransform)
    {
        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0; i < patarnReportValue; i++)
        {
            // インターバル遅延
            yield return new WaitForSeconds(patarnReportTimeSpan);
            
            //複数回の発射処理起動
            RepeatPatarnPlay(target, enemyTransform);
        }
    }

    //複数回の発射処理
    void RepeatPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //nullチェック
        if (enemyTransform == null)
            return;

        //10度ずつずらす
        for (int i = 0; i <= AllForwardValue; i++)
        {
            //基準となる方向を計算
            Vector2 dirTarget = target - enemyTransform.position;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //発射方向計算
            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;

            //発射方向計算
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
