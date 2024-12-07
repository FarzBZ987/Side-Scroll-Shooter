using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isDestroyOnCollision;

    [SerializeField] private float maxTime = 2;
    private float projectileTime;
    [SerializeField] private GameObject projectileSprite;

    public static Action onImpactActions;

    private void OnEnable()
    {
        projectileTime = 0;
        rb.velocity = Vector3.zero;
    }

    public void Shoot(float direction)
    {
        rb.velocity = speed * (direction < 0 ? Vector3.left : Vector3.right);

        if (projectileSprite != null)
        {
            var projectileX = projectileSprite.transform.localScale.x;
            projectileSprite.transform.localScale =
                new Vector3(Mathf.Abs(projectileX) * direction < 0 ? -1 : 1,
                projectileSprite.transform.localScale.y,
                projectileSprite.transform.localScale.z);
        }
    }

    private void Update()
    {
        projectileTime += Time.deltaTime;
        if (projectileTime >= maxTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (!collision.CompareTag("Enemy")) return;

        var enemyStat = collision.gameObject.GetComponent<EntityBeing>();
        if (!enemyStat) return;

        if (enemyStat.TakeDamage(damage))
        {
            onImpactActions?.Invoke();
            if (isDestroyOnCollision)
            {
                AudioControl.instance.PlaySlashHit();
                gameObject.SetActive(false);
            }
            else
            {
                AudioControl.instance.PlayFireHit();
            }
        }
    }
}