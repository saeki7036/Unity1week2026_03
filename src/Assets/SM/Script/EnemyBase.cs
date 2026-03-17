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
    [SerializeField] int hp = 7;

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
    void Update()
    {
        Act();
    }

    protected virtual void Act()
    {
        if (hp <= 0)
            return;

        // do
    } 

    void TakeDamege(int damege)
    {
        hp -= damege;

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
