using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleAct : EnemyBase
{

    private void Start()
    {
        GameObject CL_DieEffect = Instantiate(SpawnEffect, transform.position, Quaternion.identity);
        Destroy(CL_DieEffect, 2);
    }

    [Header("EnemySimpleAct")]
    

    [SerializeField] ShotPatarnBase patarn;//発射パターンのクラス
    protected override void EnemyUpDate()
    {
        patarn.PatarnPlay(this.transform);
    }
}
