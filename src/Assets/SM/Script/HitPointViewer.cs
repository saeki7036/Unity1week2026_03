using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointViewer : MonoBehaviour
{
    [SerializeField] GameObject[] pointImages;

    public void SetHitPoint(int value)
    {
        if(value < 0 || pointImages.Length <= value)
        {
            return;
        }

        pointImages[value].gameObject.SetActive(false);
    }
}
