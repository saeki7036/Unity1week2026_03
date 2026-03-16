using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int AddLevel = 1;
    public Vector2 Direction = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ê⁄êG");
        ShellController shell = other.GetComponent<ShellController>();
        if (!shell) return;
        shell.Shot(AddLevel,Direction);

        Debug.Log("çUåÇ");
    }
}
