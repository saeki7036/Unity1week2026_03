using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventShell : MonoBehaviour
{
    [SerializeField] bool eventCheck = false;

    [SerializeField] UnityEvent ShellEvent; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (eventCheck)
            return;

        if(true)//collision.CompareTag("Attack"))
        {
            eventCheck = true;
            ShellEvent.Invoke();
        }
    }
}
