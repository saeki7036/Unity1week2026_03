using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    [SerializeField]
    protected Rigidbody2D rb2D;//派生先で使う Rigidbody2D

    [SerializeField]
    protected float speed = 0.01f;//派生先で使う弾の速度

    [SerializeField]
    int score = 10;//スコアの基本数値
    
    [SerializeField]
    GameObject BulletDieEffect;//bulletの破壊時に再生するエフェクトのオブジェクト

    [SerializeField]
    AudioClip DieClip; //音声クリップ

    enum Scale//スケールパラメータのenum
    {
        large,
        normal,
        small,
        zero,
    }

    [SerializeField]
    Scale scale = Scale.normal;//弾の大きさのパラメータ

    [SerializeField]
    int addSuperAttackPoint = 5;//プレイヤー側が攻撃した時に得られるポイント

    public int GetAddPoint => addSuperAttackPoint;//ゲッター

    AudioManager Audio => AudioManager.instance;//インスタンスのラムダ式

    //transformに代入するスケールのfloatパラメータの積に使用
    const float largeScaleValue = 1.5f,
                normalScaleValue = 0.75f,
                smallScaleValue = 0.3f;

    Vector3 ScaleSetting()
    {
        //transformに代入するスケールの実際の数値を返す
        switch (scale)
        {
            //Vector3.oneにfloatパラメータの積で計算
            case Scale.large:
                return Vector3.one * largeScaleValue;

            case Scale.normal:
                return Vector3.one * normalScaleValue;

            case Scale.small:
                return Vector3.one * smallScaleValue;
        }

        //scaleがzero及び範囲外は0を返す
        return Vector3.zero;
    }

    //スコア計算に使用する倍率パラメータ
    const int largeScoreValue = 4,
              normalScoreValue = 3,
              smallScoreValue = 2,
              zeroScoreValue = 1;
         
    int ScaleScore()
    {
        //enumパラメータからスコア倍率を返す
        switch (scale)
        {
            case Scale.large:
                return largeScoreValue;

            case Scale.normal:
                return normalScoreValue;

            case Scale.small:
                return smallScoreValue;
        }

        //scale = zeroは基本的に例外
        Debug.LogWarning("scale が zeroに処理が実行された");
        return zeroScoreValue;
    }

    void ScaleDown()
    {
        //scaleを一段階変更する
        switch (scale)
        {
            //large -> normal
            case Scale.large:
                scale = Scale.normal;
                break;

            //normal -> small
            case Scale.normal:
                scale = Scale.small;
                break;

            //small -> zero
            case Scale.small:
                scale = Scale.zero;
                break;
        }
        //zeroは変化しない
    }

    void Start()
    {
        //スケールを設定
        //this.transform.localScale = ScaleSetting();

        //派生先のクラスの呼び出し
        BulletSetUp();
    }

    protected virtual void BulletSetUp()
    {
        return;//基底クラス
    }

    public void KillBullet()
    {
        //スコア加算
        //GameManager.AddScore(score * ScaleScore());

        //破壊処理
        DestroyBullet();
    }

    public void SetScale()
    {
        //スコア加算
        //GameManager.AddScore(score * ScaleScore());

        //Enum変数
        ScaleDown();

        //スケール取得
        Vector3 nextScale = ScaleSetting();
       
        //スケールが0なら破壊処理
        if (nextScale == Vector3.zero)
            DestroyBullet();

        //スケールをセット
        this.transform.localScale = nextScale;
    }

    void DestroyBullet()
    {
        //音声再生
        if (DieClip != null)
        {
            Audio.PlaySE(DieClip);
        }
        else 
        {
            Debug.Log("音がセットされてないよ");
        }

        if (BulletDieEffect != null)
        {
            //エフェクト生成
            Instantiate(BulletDieEffect, transform.position, Quaternion.identity);
        }
        

        //オブジェクト削除
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーの当たり判定処理クラスを取得
        if(other.TryGetComponent<PlayerHitBox>(out PlayerHitBox player))
        {
            //プレイヤーの被弾処理
            player.Hit();

            //ダメージログ
            Debug.Log("Damage");

            //一度被弾した弾は削除
            Destroy(this.gameObject);
        }
    }
}
