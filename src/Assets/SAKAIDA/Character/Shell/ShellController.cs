using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    
    public int DefaltAttack = 0;
    public int CulcarateAttack = 0;
    public int Level = 0;

    public bool BrakeShell = false;//壁に接触したら壊れるかを判断

    public float DefaltSpeed = 1;
    //１レベルごとの攻撃倍率
    public float UP_SPEED_DOUBLE = 0.5f;
    public float CulcarateSpeed = 0;
    [SerializeField] ParticleSystem HeighLevelEffect;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int MAX_LEVEL = 5;
    [SerializeField] Color SlowSpeedColor;
    [SerializeField] Color HightSpeedColor;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        CulcarateSpeed = DefaltSpeed * (1 + Level * UP_SPEED_DOUBLE) ;
        CulcarateAttack = DefaltAttack * Level;
    }

    public bool IsNoVerocity ()=> rb.velocity == Vector2.zero;

    public void Shot(int UP_LEVEL,Vector2 direction) 
    {
        animator.speed = (float)Level / 3;
        animator.Play("移動");
        
        if (Level <= MAX_LEVEL)
        {
            Level += UP_LEVEL;
            CulcarateSpeed = DefaltSpeed * (1 + Level * UP_SPEED_DOUBLE);
            CulcarateAttack = DefaltAttack * Level;
        }
            
        spriteRenderer.color = ChangeColer();
        rb.velocity = direction * CulcarateSpeed;
    }

    public void Stop ()=> rb.constraints = RigidbodyConstraints2D.FreezeAll;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall")) 
        {
            if (BrakeShell) Destroy(gameObject);

            if (Level <= MAX_LEVEL)
            {
                if (Level > 0) 
                {
                    Level--; 
                }
            }
            else
            {
                Level = MAX_LEVEL;
                
            }
        }
        if (Level > 10)
        {
            HeighLevelEffect.emissionRate = 100;
        }
        else 
        {
            HeighLevelEffect.emissionRate = 0;
        }
        animator.speed = (float)Level / 3;
        CulcarateSpeed = DefaltSpeed * (1 + Level * UP_SPEED_DOUBLE);
        CulcarateAttack = DefaltAttack * Level;
        rb.velocity = rb.velocity.normalized * CulcarateSpeed;
        spriteRenderer.color = ChangeColer();

    }

    Color ChangeColer() 
    {
        float t = Mathf.InverseLerp(0, MAX_LEVEL, Level);

        // RGB �� HSV
        Color.RGBToHSV(SlowSpeedColor, out float h1, out float s1, out float v1);
        Color.RGBToHSV(HightSpeedColor, out float h2, out float s2, out float v2);

        // HSV�ŕ��
        float h = Mathf.Lerp(h1, h2, t);
        float s = Mathf.Lerp(s1, s2, t);
        float v = Mathf.Lerp(v1, v2, t);

        // HSV �� RGB
        return Color.HSVToRGB(h, s, v);
    }
}
