using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    Coroutine slowCoroutine;


    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    [SerializeField]
    SceneChanger changer;

    [SerializeField] float DefaltSpeed = 3;
    [SerializeField] float ChargeSpeed = 1.5f;
    [SerializeField] int DefaltAddLevel = 1;
    [SerializeField] int ChargeAddLevel = 2;
    [SerializeField] float ChargeTime = 1;
    [SerializeField] GameObject ChargeShotEffect;
    [SerializeField] GameObject ChargeEffect;
    [SerializeField] GameObject ChargeMaxEffect;
    [SerializeField] GameObject DieEffect;
    [SerializeField] GameObject Animbody;
    [SerializeField] Animator Damageanimator;
    float chargeCount = 0;
    [SerializeField] int HP;
    [SerializeField] int MAXHP;
    bool ChargeFlag = false;
    public int GetHP() => HP;

    [SerializeField] float ChargeCoumt = 0;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();

    [SerializeField] float AttackDestroyTime = 0.1f;
    [SerializeField] float AttackFowerd = 1;
    [SerializeField] GameObject AttackArea;

    [SerializeField]bool pressing;

    [SerializeField] UnityEvent gameover;
    Camera_Controller Camera =>Camera_Controller.instance;

    public Vector2 CursorDirection = Vector2.zero;
    public float CursorDistance = 0;

    public Vector2 MoveInput = Vector2.zero;
    public Vector2 MoveInputSave = Vector2.zero;
    public Vector2 LastMoveInput = Vector2.zero;

    public bool Die = false;
    float DieResetCount = 0;

    [SerializeField] CursorController cursorController;

    public enum MoveType
    {
        Guard,
        Attack,
        Nomal,
        Damage,
        Dodge,
        Skill,
        NoAction,
        Wait
    }
    public MoveType movetype = MoveType.Nomal;
    void Start()
    {
        
    }

    public void SetNoAction ()=> movetype = MoveType.NoAction;

    public Transform GetTransform => this.transform;
    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("遘ｻ蜍輔＠縺");
        MoveInput = context.ReadValue<Vector2>();

        if (MoveInput != Vector2.zero)
        {
            LastMoveInput = MoveInput;
        }

        if (movetype != MoveType.Dodge && movetype != MoveType.NoAction && movetype != MoveType.Wait)
        {
            MoveInputSave = MoveInput;

        }
        
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (movetype == MoveType.NoAction)
            return;

        if (context.phase == InputActionPhase.Started)
        {
            pressing = true;
            
            
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            movetype = MoveType.Attack;
            pressing = false;
        }

        animator.SetBool("AttackWait", pressing);
        
    }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void FixedUpdate()
    {

        switch (movetype)
        {

            case MoveType.Nomal:
                isMove(1);
                //animator.SetBool("Dash", false);
                animator.SetInteger("Attack", 0);
                break;
            case MoveType.Guard:
                
                break;
            case MoveType.Attack:
                isAttack();
                break;
            case MoveType.Damage:

                break;
            case MoveType.Skill:
                
                break;
            case MoveType.Dodge:
                
                break;
            case MoveType.NoAction:
                isNoAction();
                break;
            case MoveType.Wait:
                
                break;



        }
        if (Die) 
        {
            DieResetCount += Time.deltaTime;
            if (DieResetCount > 2) 
            {
                changer.SceneChangeMainGame();
                Die = false;
            }
        }

    }
    void isNoAction() 
    { 
    rb.velocity = Vector3.zero;
    }
    void isDamage() 
    {
        animator.Play("被弾");
       
    }

    public void Damage() 
    {
        animator.Play("被弾");
    }
    public void isMove(float value)
    {        
        rb.velocity = MoveInput.normalized * culcarateSpeed(pressing);
        animator.SetBool("Attack", false);

        if (pressing)
        {
            if (CursorDirection.x >= 0.3)
            {
                Animbody.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (CursorDirection.x <= -0.3)
            {
                Animbody.transform.localScale = new Vector3(1, 1, 1);
            }
            CursorDirection = CursorMathf();
        }
        else 
        { 
            if (MoveInput.x >= 0.3)
            {
                Animbody.transform.localScale = new Vector3(-1,1,1);
            }
            else if (MoveInput.x <= -0.3) 
            {
                Animbody.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
        if (MoveInput == Vector2.zero) 
        {
            animator.SetInteger("Anim", 0);
        }
        else 
        {
            animator.SetInteger("Anim", 1);
        }
        
    }
    float culcarateSpeed(bool press) 
    {
        float speed = 0;
        if (press)
        {
            speed = ChargeSpeed;
            ChargeCoumt += Time.fixedDeltaTime;
           
            ChargeEffect.SetActive(true);
            if (ChargeCoumt > ChargeTime)
            {
                ChargeMaxEffect.SetActive(true);
                Camera.Shake(0.1f, 0.04f);
                animator.SetBool("Charge", true);
                if (!ChargeFlag) 
                { 
                    ChargeFlag = true;
                    AudioManager.instance.PlaySE(audioClips[2]);
                }
            }
            else 
            {
                animator.SetBool("Charge", false);
                ChargeFlag = false;
            }
        }
        else 
        { 
            speed = DefaltSpeed;
            ChargeCoumt = 0;
            ChargeEffect.SetActive(false);
            ChargeMaxEffect.SetActive(false);
            animator.SetBool("Charge", false);
        }
        return speed;
    }
    void isAttack() 
    {

        animator.SetBool("Attack", true);
        GameObject CL_AttackArea = Instantiate(AttackArea,transform.position,Quaternion.identity);
        PlayerAttack playerAttack = CL_AttackArea.GetComponent<PlayerAttack>();
        CL_AttackArea.transform.up = CursorDirection;
        playerAttack.Direction = CursorDirection;
        playerAttack.player = this;
        if (ChargeCoumt > ChargeTime)
        {
            playerAttack.AddLevel = ChargeAddLevel;
            playerAttack.transform.localScale = playerAttack.transform.localScale * 2;
            
        }
        else 
        {
            
            playerAttack.AddLevel = DefaltAddLevel;
        }
        

        CL_AttackArea.transform.position =
        transform.position + (Vector3)CursorDirection * AttackFowerd;

        AudioManager.instance.PlaySE(audioClips[0]);
        Destroy(CL_AttackArea, AttackDestroyTime);

        movetype = MoveType.Nomal;

    }

    public Vector2 CursorMathf() 
    {
        Vector2 cursordirection = cursorController.MousePos - transform.position;
        return cursordirection.normalized;

    }

    public void TakeDamege()
    {
        if(HP <= 0)
            return;

        HP = Mathf.Max(HP -1,0);
        Damage();
        Damageanimator.Play("被弾",0,0);
        AudioManager.instance.PlaySE(audioClips[3]);
        if(HP <= 0)
        {
            movetype = MoveType.NoAction;
            gameover.Invoke();
            animator.Play("死亡");
            Die = true;
            AudioManager.instance.BgmSource.Stop();
            StartCoroutine(SlowMotion(0.5f,0.1f));
            AudioManager.instance.PlaySE(audioClips[4]);
            GameObject CL_Effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
            Destroy(CL_Effect,2);
        }
    }
    public void StartSlow(float duration, float scale)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            StopCoroutine(ChargeShotEffectAnimation(1));
        }
        slowCoroutine = StartCoroutine(SlowMotion(duration, scale));
        StartCoroutine(ChargeShotEffectAnimation(1));
    }
    public IEnumerator ChargeShotEffectAnimation(float duration) 
    {
        ChargeShotEffect.transform.up = CursorDirection;
        ChargeShotEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(duration);
        ChargeShotEffect.SetActive(false);
    }
    public IEnumerator SlowMotion(float duration, float scale)
    {
        float originalTimeScale = Time.timeScale;

        Time.timeScale = scale;

        // リアル時間で待つ（←ここ重要）
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1;
    }
}
