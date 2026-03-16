using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] List<GameObject> EnemyList;

    [SerializeField] UnityEvent ClearEvent;

    [SerializeField] bool checkFlag = true;

    public void CheckStart()
    {
        checkFlag = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(checkFlag)
            return;

        //bool check = false;

        for (int i = EnemyList.Count - 1; i >= 0; i--)
        {
            if (EnemyList[i] == null)
            {
                EnemyList.RemoveAt(i);
            }
            if (!EnemyList[i].activeSelf)
            {
                //check = true;
                //break;
                return;
            }
            else
            {
                continue;
            }
        }

        ClearEvent.Invoke();

        checkFlag = true;
    }
}
