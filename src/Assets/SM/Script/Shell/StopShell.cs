using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShell : MonoBehaviour
{
    [SerializeField] ShellController[] controllers;
    
    public void StopShells()
    {
        GameObject[] shells = GameObject.FindGameObjectsWithTag("Shell");

        foreach (var shell in shells)
        {
            if(shell.TryGetComponent<ShellController>(out var con))
                con.Stop();
        }
    }
}
