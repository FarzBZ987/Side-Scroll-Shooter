using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This code is using Animator Events, needed to unleash attack in time

public class Player : EntityBeing
{
    // Declare events needed
    public static event Action<int> onUnleashEvent;

    public static event Action<int> onHealthChangeEvent;

    public static event Action<float> onSkillChangeEvent;

    public static event Action<float> onUltimateChangeEvent;

    public static event Action onUltimateClicked;

    public static event Action onDie;

    // Attaching every needed references
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Transform ShootLocation;
    [SerializeField] private Transform SkillLocation;
    [SerializeField] private Magz attackMagz;
    [SerializeField] private Magz skillMagz;
    [SerializeField] private Joystick joystick;

    // Basic variables
    private float horizontal;

    private float vertical;
    private Vector3 Movement = Vector3.zero;
    [Range(0, 5)] private float _manaPoints;
    [Range(0, 20)] private float _burstPoints;
    private float requiredMana = 1;
    private float requiredBurstPoints = 10;
    private bool attackPrepared;
    private bool canMove;

    // For scale modification. Flipping scale is easier than moving where the shooting location is
    // Won't recommend for anything with Collider or turnables. This one flipping the inner sprite of collider object
    private Vector3 originalScale;

    private Vector3 flippedScale;

    // Variables with running event when they changed any value
    private float manaPoints
    {
        get { return _manaPoints; }
        set
        {
            if (value != manaPoints)
            {
                onSkillChangeEvent?.Invoke(value);
            }
            _manaPoints = value;
        }
    }

    private float burstPoints
    {
        get { return _burstPoints; }
        set
        {
            if (value != burstPoints)
            {
                onUltimateChangeEvent?.Invoke(value);
            }
            _burstPoints = value;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        burstPoints = 0;
        manaPoints = 5;
        PrepareAttack();
        originalScale = playerSprite.transform.localScale;
        flippedScale.Set(-originalScale.x, originalScale.y, originalScale.z);
        ActionButtons.onAttackClicked += Attack;
        ActionButtons.onSkillClicked += Skill;
        ActionButtons.onUltimateClicked += Ultimate;
    }

    // Update is called once per frame
    private void Update()
    {
        SetMovement();
        if ((horizontal != 0 || vertical != 0) && canMove)
        {
            Move();
        }
        else Idle();

        manaPoints += 0.2f * Time.deltaTime;
    }

    private void SetMovement()
    {
        if (joystick != null)
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }
        Movement.Set(horizontal, vertical, 0);
        Movement.Normalize();
    }

    protected override void Move()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        animator.SetFloat("Moving", 1);
        rb.velocity = Movement;
        playerSprite.transform.localScale = horizontal < 0 ? flippedScale : originalScale;
    }

    private void Idle()
    {
        animator.SetFloat("Moving", 0);
        rb.velocity = Vector3.zero;
    }

    public void PrepareAttack()
    {
        canMove = true;
        attackPrepared = true;
    }

    public void Attack()
    {
        if (attackPrepared)
        {
            AudioControl.instance.PlaySlash();
            attackPrepared = false;
            animator.SetTrigger("Attack");

            StartCoroutine(Cooldown());
        }
        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(0.125f);
            PrepareAttack();
        }
    }

    private void Shoot(Magz mag, Transform setTransform)
    {
        var bullet = mag.GetAvailableBullet();
        if (!bullet) return;
        bullet.transform.position = setTransform.position;
        bullet.gameObject.SetActive(true);
        bullet.Shoot(playerSprite.transform.localScale.x);
    }

    // Used in animator event
    public void AttackShoot()
    {
        Shoot(attackMagz, ShootLocation);
    }

    private void Skill()
    {
        if (attackPrepared && manaPoints >= requiredMana)
        {
            AudioControl.instance.PlaySkillRelease();
            attackPrepared = false;
            animator.SetTrigger("Skill");
            manaPoints -= requiredMana;
            canMove = false;
            burstPoints = burstPoints + 1;
        }
    }

    // Used in animator event
    public void SkillShoot()
    {
        Shoot(skillMagz, SkillLocation);
    }

    private void Ultimate()
    {
        if (attackPrepared && burstPoints >= requiredBurstPoints)
        {
            onUltimateClicked?.Invoke();
            AudioControl.instance.PlayUltimateRelease();
            attackPrepared = false;
            animator.SetTrigger("Ultimate");
            burstPoints -= requiredBurstPoints;
        }
    }

    public void PlayShiningSword()
    {
        AudioControl.instance.PlayUltimateShine();
    }

    // Used in animator event
    private void Unleash()
    {
        onUnleashEvent?.Invoke(30);
    }

    public override bool TakeDamage(int damage)
    {
        var damageTaken = base.TakeDamage(damage);
        if (damageTaken)
        {
            // Broadcasting the damage is taken from player. Needed for UI
            onHealthChangeEvent?.Invoke(hitPoints);
            if (hitPoints <= 0) onDie?.Invoke();
        }
        return damageTaken;
    }

    protected override void Die()
    {
        joystick = null;
        ActionButtons.onAttackClicked -= Attack;
        ActionButtons.onSkillClicked -= Skill;
        ActionButtons.onUltimateClicked -= Ultimate;
        animator.SetTrigger("Die");
        canMove = false;
    }
}