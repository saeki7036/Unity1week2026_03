using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //場外に接触したオブジェクトを削除
        Destroy(other.gameObject);
    }
}
