using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySub : EnemyBaseRush
{
    [SerializeField] 
    ShotPatarnBase patarn;//行動パターン

    [SerializeField] 
    Transform shottingTransform;//発射位置の座標

    [SerializeField] 
    SpriteRenderer bossArmSpriteRenderer;//色変更に使うレンダラー

    float maxHP;//最大体力

    //パラメータ設定
    protected override void EnemySetUp()
    {
        maxHP = HP;//最大体力取得
    }

    //アップデート
    protected override void EnemyUpDate()
    {
        //発射条件チェック
        if (patarn.PatarnCeangeLimit(shotTimeCount))
            BulletShot();//発射処理

        //オブジェクトが表示されているなら色変更
        if (this.gameObject.activeSelf)
        {
            //色変更
            DamageColor();
        }
    }

    //発射処理
    void BulletShot()
    {
        //カウント初期化
        shotTimeCount = 0;

        //発射パターン起動
        patarn.PatarnPlay(shottingTransform);
    }

    //残HPから色変更(減るほど赤)
    void DamageColor()
    {
        //割合計算
        float value = HP / maxHP;

        //GとBがHPが減るにつれて数値が下がり赤くなる
        bossArmSpriteRenderer.color = new Color(1, value, value, 1);
    }

    //死亡処理
    protected override void EnemyDead()
    {
        //スコア加算
        //GameManager.AddScore(score);

        //オブジェクト自体を非表示に
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 破壊判定
    /// </summary>
    /// <returns>破壊していたらtrue</returns>
    public bool IsDestroyed() => HP <= 0;
}
