using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : EnemyBaseRush
{
    [SerializeField] ShotPatarnBase patarn;//発射パターンのクラス

    //単体テストOK
    protected override void EnemyUpDate()
    {
        //移動
        transform.Translate(new Vector3(0f, -Speed, 0f));

        //発射チェック
        if(patarn.PatarnCeangeLimit(shotTimeCount))
            BulletShot();
    }

    //発射処理
    void BulletShot()
    {
        //カウント初期化
        shotTimeCount = 0;

        //発射パターン起動
        patarn.PatarnPlay(this.transform);
    }
}
