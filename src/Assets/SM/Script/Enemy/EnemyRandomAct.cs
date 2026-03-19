using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomAct : EnemyBase
{
    [Header("EnemyRandomAct")]
    [SerializeField] ShotPatarnBase[] patarns;//発射パターンのクラス
    protected override void EnemyUpDate()
    {
        int random = Random.Range(0, patarns.Length);

        patarns[random].PatarnPlay(this.transform);
    }

    protected override void DestroyPrehub()
    {
        //AddLevel();
        base.DestroyPrehub();
    }
}
