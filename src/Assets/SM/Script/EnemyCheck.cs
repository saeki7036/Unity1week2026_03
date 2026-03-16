using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] List<EnemyBase> EnemyList;

    [SerializeField] UnityEvent ClearEvent;

    [SerializeField] int MaxActCount = 4;

    [SerializeField] bool checkFlag = true;

    public int EnemyCrrentCount;
    int ActCrrentCount;

    public void CheckStart()
    {
        checkFlag = false;
    }

    private void Start()
    {
        EnemyCrrentCount = EnemyList.Count;
        ActCrrentCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkFlag)
            return;

        bool check = false;

        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i] == null)
            {
                continue;
            }

            switch (EnemyList[i].eStatus)
            {
                case enemyStatus.Stay:
                    {
                        if (ActCrrentCount < MaxActCount)
                        {
                            EnemyList[i].EnemySpwan();
                            EnemyList[i].SetStatus(enemyStatus.Act);
                            ActCrrentCount++;
                        }
                        check = true;
                    }
                    break;
                case enemyStatus.Act:
                    {
                        check = true;
                    }
                    break;
                case enemyStatus.Stop:
                    {
                        EnemyList[i].EnemyDeath();
                        EnemyList[i].SetStatus(enemyStatus.Death);
                        EnemyCrrentCount--;
                        ActCrrentCount--;
                    }
                    break;
            }
        }

        //Debug.Log(check+ "" + EnemyCrrentCount +""+ ActCrrentCount);
        if (check || EnemyCrrentCount != 0 || ActCrrentCount != 0)
        {
            return;
        }
        else
        {
            ClearEvent.Invoke();

            checkFlag = true;
        } 
    }
}
