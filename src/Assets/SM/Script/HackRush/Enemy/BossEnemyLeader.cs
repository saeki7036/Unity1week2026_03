using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyLeader : EnemyBaseRush
{
    [SerializeField] 
    ShotPatarnBase[] patarns;//行動パターンクラス群

    [SerializeField] 
    int patarnChengeInterval = 600;//行動パターン切り替えインターバル

    [SerializeField] 
    SpriteRenderer bossSpriteRenderer;//色変更に使うレンダラー

    int patarnChengeCount = 0;//行動パターン切り替えカウンター

    bool superMode = false;//行動パターン切り替えフラグ

    //行動パターン切り替えフラグ更新セッター
    public void SetSuperMode() => superMode = true;

    //現在の発射パターン
    ShotPatarnBase currentPatarn;

    //発射パターンをランダムに選択
    ShotPatarnBase PatarnChenge() => patarns[Random.Range(0, patarns.Length)];

     float maxHP;//最大体力

    //パラメータ設定
    protected override void EnemySetUp()
    {
        maxHP = HP;//最大体力取得
    }

    //アップデート
    protected override void EnemyUpDate()
    {
        //パターンの切り替えカウント加算
        patarnChengeCount++;

        //パターン未設定なら設定する
        if (currentPatarn == null)
            currentPatarn = PatarnChenge();

        //パターン変更チェック
        //パターンの切り替えカウントがInterval以上で切り替え
        if (patarnChengeCount >= patarnChengeInterval)
        {
            //パターンカウント初期化
            patarnChengeCount = 0;

            //パターン変更
            currentPatarn = PatarnChenge();
        }

        //発射条件チェック
        //発射パターンの設定が問題ないかも調べる
        if (currentPatarn != null && currentPatarn.PatarnCeangeLimit(shotTimeCount))     
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

        //全パターン同時に使う
        if (superMode)  
            foreach (ShotPatarnBase Patarn in patarns)
                Patarn.PatarnPlay(this.transform);  
        
        //現在のパターンのみ使う
        else
            currentPatarn.PatarnPlay(this.transform);
    }

    //残HPから色変更(減るほど赤)
    void DamageColor()
    {
        //割合計算
        float value = HP / maxHP;

        //GとBがHPが減るにつれて数値が下がり赤くなる
        bossSpriteRenderer.color = new Color(1, value, value, 1);
    }

    //死亡処理
    protected override void EnemyDead()
    {
        //スコア加算
        //GameManager.AddScore(score);

        //オブジェクト自体を非表示に
        this.gameObject.SetActive(false);
    }
}
