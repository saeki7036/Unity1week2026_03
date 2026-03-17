using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    
    public int DefaltAttack = 0;
    public int CulcarateAttack = 0;
    public int Level = 0;

    public float DefaltSpeed = 1;
    //ÇPÉåÉxÉãÇ≤Ç∆ÇÃçUåÇî{ó¶
    public float UP_SPEED_DOUBLE = 0.5f;
    public float CulcarateSpeed = 0;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] int MAX_LEVEL = 5;

    private void Start()
    {
        CulcarateSpeed = DefaltSpeed * (1 + Level * UP_SPEED_DOUBLE) ;
        CulcarateAttack = DefaltAttack * Level;
    }
    public void Shot(int UP_LEVEL,Vector2 direction) 
    {
        if (Level <= MAX_LEVEL)
        {
            Level += UP_LEVEL;
            CulcarateSpeed = DefaltSpeed * (1 + Level * UP_SPEED_DOUBLE);
            CulcarateAttack = DefaltAttack * Level;
        }
        

        rb.velocity = direction * CulcarateSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall")) 
        {
            if (Level <= MAX_LEVEL) return; 
                Level = MAX_LEVEL;
            CulcarateSpeed = DefaltSpeed * (1 + Level * UP_SPEED_DOUBLE);
            CulcarateAttack = DefaltAttack * Level;
            rb.velocity = rb.velocity.normalized * CulcarateSpeed;
        }
    }
}
