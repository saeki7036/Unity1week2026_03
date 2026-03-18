using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageLog : MonoBehaviour
{
    [SerializeField] Text text;

    [SerializeField] float DestroyWaitTime = 2f;

    [SerializeField] Vector2 StartOffset = new(0, -0.7f);
    [SerializeField] Vector2 Targetoffset = new(0,-0.5f);

    Vector2 offset;
    Transform targetPos;

    public void TextUpdate(int Value, Transform target)
    {
        text.text = Value.ToString();

        targetPos = target;

        offset = StartOffset;

        Destroy(gameObject, DestroyWaitTime);
    }

    float timecount = 0;

    // Update is called once per frame
    void Update()
    {
        timecount = Mathf.Min(timecount + Time.deltaTime, 1f);

        offset = StartOffset + (Targetoffset - StartOffset) * timecount;

        if (targetPos != null)
        {
            Vector2 point = (Vector2)targetPos.position + offset;

            Vector2 screenPos = (Vector2)Camera.main.WorldToScreenPoint(point);

            transform.position = screenPos;
        }
    }
}
