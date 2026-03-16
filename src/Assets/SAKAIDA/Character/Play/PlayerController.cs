using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    [SerializeField] float DefaltSpeed = 3;
    [SerializeField] float ChargeSpeed = 1.5f;
    [SerializeField] int HP;
    [SerializeField] int MAXHP;

    [SerializeField] float ChargeCoumt = 0;

    [SerializeField]bool pressing;

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

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("ˆÚ“®‚µ‚½");
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
        if (context.phase == InputActionPhase.Started)
        {
            pressing = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            pressing = false;

        }
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
        }
        else 
        { 
            speed = DefaltSpeed;
        }
        return speed;
    }
    void isAttack() 
    {

        

    }

    public Vector2 CursorMathf() 
    {
        Vector2 cursordirection = cursorController.MousePos - transform.position;
        return cursordirection.normalized;

    }
}
