using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseRush : MonoBehaviour
{
    [SerializeField]
    protected int score = 50;//派生先でも使うスコア数値
    [SerializeField]
    protected float HP = 1;//派生先でも使う
    [SerializeField]
    protected float Speed = 0.1f;//派生先でも使うEnemyの移動速度

    [SerializeField]
    GameObject DieEffect;//死亡時のエフェクトのオブジェクト

    [SerializeField]
    AudioClip DieClip;//死亡時の音声

    [SerializeField]
    int addSuperAttackPoint = 10;//プレイヤー側が攻撃した時に得られるポイント

    [SerializeField]
    int firstShotDalayValue = -200;//初撃タイミングの遅延

    public int GetAddPoint => addSuperAttackPoint;//得られるポイントのゲッター

    AudioManager Audio => AudioManager.instance;

    protected int shotTimeCount;//派生先でも使う

    const float shotCountLimitY = -5.5f;//画面内での位置

    private void Start()
    {
        shotTimeCount = firstShotDalayValue;//ある程度初撃タイミングを遅延

        //パラメータ設定
        EnemySetUp();
    }

    protected virtual void EnemySetUp()
    {
        return;//基底クラス
    }

    //ダメージ処理
    public void TakeDamage(float damage)
    {
        //ダメージ減算
        HP -= damage;

        //HPなくなったら死亡処理
        if(HP <= 0) 
            EnemyDead();
    }

    //死亡処理
    protected virtual void EnemyDead()
    {
        //音声再生
        if (DieClip != null)
        {
            Audio.PlaySE(DieClip);
        }
        else 
        {
            Debug.Log("死亡時効果音が入っていません");
        }

        //エフェクト生成
        Instantiate(DieEffect,this.transform.position,Quaternion.identity);

        //スコア加算
        //GameManager.AddScore(score);

        //敵を削除
        Destroy(this.gameObject);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        //一定範囲内(画面内)の時に加算
        if(transform.position.y > shotCountLimitY)
        shotTimeCount++;

        //派生先の処理
        EnemyUpDate();
    }

    protected virtual void EnemyUpDate()
    {
        return;//基底クラス
    }
}
