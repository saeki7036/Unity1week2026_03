using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShotPatarnBase : MonoBehaviour
{
    [SerializeField]
    int shotInterval = 200;//発射間隔パラメータ

    /// <summary>
    /// 発射の間隔の条件判定
    /// </summary>
    /// <param name="time">カウントパラメータ</param>
    /// <returns>カウントがインターバル以上ならtrue</returns>
    public bool PatarnCeangeLimit(int time) => time >= shotInterval;
    
    //発射パターン
    public virtual void PatarnPlay(Transform enemyTransform)
    {
        return;//基底クラス
    }
}
