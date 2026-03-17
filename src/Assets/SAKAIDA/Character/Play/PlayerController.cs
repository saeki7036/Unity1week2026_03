using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    [SerializeField] float DefaltSpeed = 3;
    [SerializeField] float ChargeSpeed = 1.5f;
    [SerializeField] int DefaltAddLevel = 1;
    [SerializeField] int ChargeAddLevel = 2;
    [SerializeField] float ChargeTime = 1;
    [SerializeField] GameObject ChargeEffect;
    [SerializeField] GameObject ChargeMaxEffect;
    float chargeCount = 0;
    [SerializeField] int HP;
    [SerializeField] int MAXHP;

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
        CursorDirection = CursorMathf();
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
                //isNoAction();
                break;
            case MoveType.Wait:
                
                break;



        }


    }
    public void isMove(float value)
    {        
        rb.velocity = MoveInput.normalized * culcarateSpeed(pressing);           
    }
    float culcarateSpeed(bool press) 
    {
        float speed = 0;
        if (press)
        {
            speed = ChargeSpeed;
            ChargeCoumt += Time.fixedDeltaTime;
            if (ChargeCoumt > ChargeTime/2) Camera.Shake(0.1f,0.04f);
            ChargeEffect.SetActive(true);
            if (ChargeCoumt > ChargeTime) ChargeMaxEffect.SetActive(true);
        }
        else 
        { 
            speed = DefaltSpeed;
            ChargeCoumt = 0;
            ChargeEffect.SetActive(false);
            ChargeMaxEffect.SetActive(false);
        }
        return speed;
    }
    void isAttack() 
    {

        
        GameObject CL_AttackArea = Instantiate(AttackArea,transform.position,Quaternion.identity);
        PlayerAttack playerAttack = CL_AttackArea.GetComponent<PlayerAttack>();
        CL_AttackArea.transform.up = CursorDirection;
        playerAttack.Direction = CursorDirection;
        playerAttack.player = this;
        if (ChargeCoumt > ChargeTime)
        {
            playerAttack.AddLevel = ChargeAddLevel;
            playerAttack.transform.localScale = playerAttack.transform.localScale * 1.5f;
            
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

        if(HP <= 0)
        {
            movetype = MoveType.NoAction;
            gameover.Invoke();
        }
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
