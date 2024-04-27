using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// This code is using Animator Events, needed to playing script in time

public class Enemy : EntityBeing
{
    private Rigidbody2D rb;

    // Stats, serialized for adjustments
    [SerializeField] private float movementSpeed = 2;

    [SerializeField] private float attackCooldown = 0.2f;

    // Used for cooldown for each attack
    private float currentCooldown = 0;

    // Player, needed to move, determining which direction this enemy walks
    private EntityBeing target;

    // Used to determine wheter this enemy could move or not
    private bool isAbleToMove = false;

    public static System.Action onEnemyKill;

    private void OnEnable()
    {
        // Determine how much HP this enemy will have
        hitPoints = Random.Range(35, 45);

        // Initializing player if it doesn't have any
        if (target == null) target = FindObjectOfType<Player>();

        // Subscribing to player when it want to release the ultimate
        Player.onUnleashEvent += TakeUltimate;
        Player.onUltimateClicked += InUltimate;

        ReadyToMove();
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        currentCooldown -= Time.deltaTime;
        Move();
    }

    private void OnDisable()
    {
        Player.onUnleashEvent -= TakeUltimate;
    }

    // Attacking player here
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!isAbleToMove) return;
        if (currentCooldown >= 0) return;
        if (target.TakeDamage(2))
        {
            AudioControl.instance.PlayEnemyHit();
            currentCooldown = attackCooldown;
        }
    }

    public void ReadyToMove()
    {
        isAbleToMove = true;
    }

    protected override void AttackedActions()
    {
        animator.SetTrigger("hurt");
        isAbleToMove = false;
    }

    private void InUltimate()
    {
        isAbleToMove = false;
    }

    private void TakeUltimate(int i)
    {
        ReadyToMove();
        TakeDamage(i);
    }

    // Only move when it's permitted
    protected override void Move()
    {
        if (!isAbleToMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = Vector3.Normalize(target.transform.position - transform.position) * movementSpeed;
    }

    // Seting the movability of this enemy+
    protected override void Die()
    {
        animator.SetTrigger("die");
        isAbleToMove = false;
        onEnemyKill?.Invoke();
    }

    private void DisableOnDie()
    {
        gameObject.SetActive(false);
    }
}