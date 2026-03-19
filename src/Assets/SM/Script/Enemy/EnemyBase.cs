using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyStatus
{
    Stay,
    Act,
    Stop,
    Death,
}


public class EnemyBase : MonoBehaviour
{
    public int DefaltHP = 7;
    public int MaxHp = 7;
    public int Level = 1;
    [SerializeField] float PU_HP_DOUBLE = 0.5f;
    [SerializeField] int hp = 7;

    [SerializeField] float ActTimeSpawn = 3f;

    float ActTimeCount;

    static readonly string ShellTag = "Shell";

    public enemyStatus eStatus = enemyStatus.Stay;

    public void SetStatus(enemyStatus s) => eStatus = s;

    public void EnemySpwan() => gameObject.SetActive(true);

    public void EnemyDeath() => gameObject.SetActive(false);

    // Start is called before the first frame update
    void Start()
    {
        //eStatus = enemyStatus.Stay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //一定範囲内(画面内)の時に加算
        if (eStatus == enemyStatus.Act)
            ActTimeCount += Time.fixedDeltaTime;
        else
            return;

        if(ActTimeCount >= ActTimeSpawn)
        {
            ActTimeCount = 0;

            //派生先の処理
            EnemyUpDate();
        }
    }

    protected virtual void EnemyUpDate()
    {
        return;//基底クラス
    }

    public void Spawn(int AddLevel) 
    {

        eStatus = enemyStatus.Act;

       

    }
    
    protected virtual void Act()
    {
        if (hp <= 0)
            return;

        // do
    } 

    void TakeDamege(int damage)
    {
        if(damage <= 0)
            return;

        hp -= damage;

        DamageOutPut.ShowDamage(damage, this.transform);

        if(hp <= 0)
        {
            IsDeath();
        }
    }

    void IsDeath()
    {
        eStatus = enemyStatus.Stop;
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (collision.CompareTag(ShellTag))
        {
            if(collision.gameObject.TryGetComponent<ShellController>(out var shell))
                TakeDamege(shell.CulcarateAttack); 
        }
    }
}
