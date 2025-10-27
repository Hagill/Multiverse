using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;
    float offsetY;

    public float limitX = 4.5f;
    public float limitY = 3.0f;

    void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        
        pos.x = Mathf.Clamp(target.position.x + offsetX, -limitX, limitX);
        pos.y = Mathf.Clamp(target.position.y + offsetY, -limitY, limitY);
        
        transform.position = pos;
    }
}
