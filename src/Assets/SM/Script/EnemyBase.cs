using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] int hp = 7;

    static readonly string ShellTag = "Shell";

    // Start is called before the first frame update
    void Start()
    {
        
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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag(ShellTag))
        {
            //if(TryGetComponent<>())
            TakeDamege(1);
            
        }
    }
}
