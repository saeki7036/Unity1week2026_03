using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Vector3 MousePos;
    public Vector3 MouseScreenPos;

    public Vector2 InputTarget = Vector2.zero;
    [SerializeField] float ControllerTarget_Range = 2;
    [SerializeField] RectTransform rectTransform;

    Vector2 stickInput;

    PlayerController playerController => PlayerController.Instance;
    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        //Debug.Log("コントローラーが接続されていません");
        MouseScreenPos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MouseScreenPos);
        MousePos.z = 0;
        transform.position = MousePos;

        rectTransform.position = MouseScreenPos;


    }
}
