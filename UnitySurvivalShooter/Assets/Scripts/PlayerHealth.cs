using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public AudioClip hurtClip;
    public AudioClip deathClip;
    public UIManager uiManager;

    private AudioSource audioSource;
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!IsDead)
        {
            audioSource.PlayOneShot(hurtClip);
        }
        StartCoroutine(uiManager.HitEffect());
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die()
    {
        base.Die();
        animator.SetTrigger("Dead");
        audioSource.PlayOneShot(deathClip);

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
}
