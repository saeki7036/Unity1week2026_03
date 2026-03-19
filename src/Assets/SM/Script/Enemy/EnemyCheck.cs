using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] GameObject Enemy_S, Enemy_L;

    [SerializeField] UnityEvent ClearEvent;

    [SerializeField] bool checkFlag = true;

    [SerializeField] int StartActCount = 4;

    [SerializeField] float WaveTime = 30f;

    [SerializeField] Vector2 SpawnRange_min, SpawnRange_max;

    [SerializeField] StopShell addShell;

    int CurrentActCount;
    float CurrentWaveTime;

    Stack<GameObject> EnemyList;
    List<float> EnemySpawnTime;

    public void CheckStart()
    {
        checkFlag = false;
    }

    public void CheckStop()
    {
        checkFlag = true;
    }

    private void Start()
    {
        EnemyList = new();
        EnemySpawnTime = new();

        EnemyBase.ResetLevel();

        NextLevelSetting();
    }

    Vector2 RamdomPos => new Vector2(UnityEngine.Random.Range(SpawnRange_min.x, SpawnRange_max.x),
                                     UnityEngine.Random.Range(SpawnRange_min.y, SpawnRange_max.y));

    void NextLevelSetting()
    {
        Debug.Log(EnemyBase.GetLevel());
        CurrentActCount = StartActCount + EnemyBase.GetLevel();

        CurrentWaveTime = 0;

        EnemyList.Clear();
        EnemySpawnTime.Clear();

        EnemyList.Push(Instantiate(Enemy_L, RamdomPos, Quaternion.identity));

        for (int i = 0; i < CurrentActCount; i++)
        {
            EnemyList.Push(Instantiate(Enemy_S, RamdomPos, Quaternion.identity));
            EnemySpawnTime.Add(UnityEngine.Random.Range(0, WaveTime));
        }

        EnemySpawnTime.Sort();
    }


    // Update is called once per frame
    void Update()
    {
        if(checkFlag)
            return;

        CurrentWaveTime = Mathf.Min(WaveTime, CurrentWaveTime + Time.deltaTime);

        if(EnemySpawnTime.Count == 0)
        {
            if(EnemyList.Count == 1) 
            { 
                if(EnemyList.Peek() == null)
                {
                    EnemyBase.AddLevel();
                    addShell.SpawnShell();
                    NextLevelSetting();
                }
                else if(!EnemyList.Peek().activeSelf)
                {
                    EnemyList.Peek().SetActive(true);
                } 
            }
        }
        else if (EnemySpawnTime[0] <= CurrentWaveTime)
        {
            Debug.Log(EnemySpawnTime[0]);
            EnemySpawnTime.RemoveAt(0);

            if(EnemyList.Count > 1)
            {

                
                EnemyList.Peek().SetActive(true);

                EnemyList.Pop();
            }           
        }
    }
}
