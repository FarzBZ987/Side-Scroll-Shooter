using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    public float speed;
    private Vector3 pos = Vector3.zero;

    public void Move(float positionDelta)
    {
        pos.Set(positionDelta, 0, 0);
        transform.position += pos * speed;
    }
}