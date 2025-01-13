using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LivingEntity target;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    private Coroutine coUpdatePath;
    public ParticleSystem hitEffect;
    public AudioClip hitClip;
    public AudioClip deathClip;

    public float damage = 30f;
    public float attackTimeInterval = 1f;
    public int score;
    private float lastAttackTime;
    public bool HasTarget
    {
        get
        {
            return target != null && !target.IsDead;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        navMeshAgent.enabled = true;

        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = true;
        }
        coUpdatePath = StartCoroutine(CoUpdatePath());
    }
    protected void OnDisable()
    {
        StopCoroutine(coUpdatePath);
        coUpdatePath = null;
        target = null;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }
    private IEnumerator CoUpdatePath()
    {
        while (true)
        {
            if (!HasTarget)
            {
                navMeshAgent.isStopped = true;
                target = GameObject.FindWithTag("Player").GetComponent<LivingEntity>();
            }
            if (HasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.transform.position);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        hitEffect.Play();
        audioSource.PlayOneShot(hitClip);
    }
    public override void Die()
    {
        base.Die();

        if(gameObject.tag == "ZomBear")
        {
            audioSource.PlayOneShot(deathClip);
            StartCoroutine(StartSinking());
        }
        else
        {
            audioSource.PlayOneShot(deathClip);
            animator.SetTrigger("Dead");
        }
        StopCoroutine(coUpdatePath);
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!IsDead && Time.time >= lastAttackTime + attackTimeInterval)
        {
            var cols = other.GetComponents<Collider>();
            foreach (var col in cols)
            {
                if (col != null && col.gameObject.GetComponent<LivingEntity>() == target)
                {
                    lastAttackTime = Time.time;

                    Vector3 hitPoint = other.ClosestPoint(transform.position);
                    Vector3 hitNormal = (hitPoint - transform.position).normalized;

                    target.OnDamage(damage, hitPoint, hitNormal);
                }
            }
        }
    }
    public IEnumerator StartSinking()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
