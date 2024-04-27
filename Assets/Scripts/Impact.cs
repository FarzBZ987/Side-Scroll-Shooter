using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    private void Start()
    {
        Bullet.onImpactActions += InitializeHitStop;
    }

    private void InitializeHitStop()
    {
        StopCoroutine(HitStop());
        Time.timeScale = 1.0f;

        StartCoroutine(HitStop());
    }

    private IEnumerator HitStop()
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(0.12f);
        Time.timeScale = 1;
    }
}