using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObjectControl : MonoBehaviour
{
    private float positionDelta;
    private float lastPosition;

    [SerializeField] private List<ParallaxObject> parallaxes;

    // Update is called once per frame
    private void Update()
    {
        var currentPosition = transform.position.x;
        positionDelta = lastPosition - currentPosition;
        if (positionDelta == 0) return;
        lastPosition = currentPosition;
        foreach (var parallax in parallaxes)
        {
            parallax.Move(positionDelta);
        }
    }
}