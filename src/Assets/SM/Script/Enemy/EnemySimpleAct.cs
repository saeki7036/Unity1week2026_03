using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleAct : EnemyBase
{
    [Header("EnemySimpleAct")]
    [SerializeField] ShotPatarnBase patarn;//発射パターンのクラス
    protected override void EnemyUpDate()
    {
        patarn.PatarnPlay(this.transform);
    }
}
