using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShell : MonoBehaviour
{
    [SerializeField] ShellController[] controllers;
    
    public void StopShells()
    {
        foreach (var item in controllers)
        {
            item.Stop();
        }
    }
}
