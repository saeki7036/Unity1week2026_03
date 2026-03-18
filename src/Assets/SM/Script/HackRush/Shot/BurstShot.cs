using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurstShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;//Bulletのオブジェクト

    [SerializeField]
    float patarnReportTimeSpan = 0.2f;//発射間隔の遅延時間

    [SerializeField]
    int patarnReportValue = 3;//発射パターンの起動回数

    [SerializeField]
    float ToDistance = 3f;

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
            //インターバル遅延
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

        Vector2 SpawnPos = enemyTransform.position;

        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = (target - enemyTransform.position).normalized;

        //オブジェクト生成
        GameObject bullet = Instantiate(bulletPrehab, SpawnPos + ToDistance * dirTarget, Quaternion.identity);

        StartCoroutine(ShotStay(bullet, dirTarget.normalized, 4));

        //ShellController shell = bullet.GetComponent<ShellController>();

        //shell.Shot(5, dirTarget.normalized);

        return;

        //Rigidbody2D取得
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //ノーマライズ
        Vector2 rotate = dirTarget.normalized;

        //発射方向代入
        bulletRB.velocity = rotate;
    }
}
