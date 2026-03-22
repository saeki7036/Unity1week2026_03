using System;
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

    [SerializeField] float WaveTime = 15f;

    [SerializeField] Vector2 SpawnRange_min, SpawnRange_max;

    [SerializeField] StopShell addShell;

    [SerializeField] PhaseTextAnimationController phase;

    int CurrentActCount;
    float CurrentWaveTime;

    float TargetWaveTime = 10f;

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
        int level = EnemyBase.GetLevel();
        //Debug.Log(EnemyBase.GetLevel());
        if (level > 1)
            phase.PhaseStart(EnemyBase.GetLevel());

        CurrentActCount = StartActCount + level;

        TargetWaveTime = WaveTime + Mathf.Min(level * 2,15);
        CurrentWaveTime = 0;

        EnemyList.Clear();
        EnemySpawnTime.Clear();

        EnemyList.Push(Instantiate(Enemy_L, RamdomPos, Quaternion.identity));

        for (int i = 0; i < CurrentActCount; i++)
        {
            EnemyList.Push(Instantiate(Enemy_S, RamdomPos, Quaternion.identity));
            EnemySpawnTime.Add(UnityEngine.Random.Range(0, TargetWaveTime));
        }

        EnemySpawnTime.Sort();
    }


    // Update is called once per frame
    void Update()
    {
        if(checkFlag)
            return;

        CurrentWaveTime = Mathf.Min(TargetWaveTime, CurrentWaveTime + Time.deltaTime);

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
