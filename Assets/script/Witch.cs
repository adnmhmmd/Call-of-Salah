using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Witch : MonoBehaviour
{

    [Header("Witch Setting")]
    public float hitPoints = 100f;
    public float turnSpeed = 15f;
    public Transform target;
    public float ChaseRange;
    private NavMeshAgent agent;
    private float DistancetoTarget;
    private float DistancetoDefault;
    private Animator anim;
    Vector3 DefaultPosition;

    [Header("Witch SFX")]
    public AudioClip GetHitAudio;
    public AudioClip StepAudio;
    public AudioClip AttackSwingAudio;
    public AudioClip AttackConnectAudio;
    public AudioClip DeathAudio;
    AudioSource WitchAudio;

    [Header("Witch VFX")]
    public ParticleSystem SlashEffect;

    void Start()
    {
        target = FindAnyObjectByType<MovementLogic>().transform;
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        anim.SetFloat("HitPoint", hitPoints);
        WitchAudio = this.GetComponent<AudioSource>();
        DefaultPosition = this.transform.position;
        // DefaultPosition = Enemy.transform.position;
    }

    void Update()
    {
        DistancetoTarget = Vector3.Distance(target.position, transform.position);
        DistancetoDefault = Vector3.Distance(DefaultPosition, transform.position);



        if (DistancetoTarget <= ChaseRange && hitPoints != 0)
        {
            FaceTarget(target.position);
            if (DistancetoTarget > agent.stoppingDistance + 2f)
            {
                ChaseTarget();
                SlashEffect.Stop();
            }
            else if (DistancetoTarget <= agent.stoppingDistance)
            {
                Attack();
            }
        }
        else if (DistancetoTarget >= ChaseRange)
        {
            agent.SetDestination(DefaultPosition);
            FaceTarget(DefaultPosition);
            if (DistancetoDefault <= agent.stoppingDistance)
            {
                // Debug.Log("Time to stop");
                anim.SetBool("Walk", false);
                anim.SetBool("Attack", false);
            }
        }
    }

    public void SlashEffectToggleOn()
    {
        SlashEffect.Play();
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void Attack()
    {
        // WitchAudio.clip = AttackSwingAudio;
        // WitchAudio.Play();
        // Debug.Log("attack");
        anim.SetBool("Attack", true);
        anim.SetBool("Walk", false);
    }

    public void ChaseTarget()
    {
        agent.SetDestination(target.position);
        // Debug.Log("walk");
        anim.SetBool("Walk", true);
        anim.SetBool("Attack", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

    }

    public void TakeDamage(float damage)
    {
        WitchAudio.clip = GetHitAudio;
        WitchAudio.Play();
        hitPoints -= damage;
        // anim.SetTrigger("GetHit");
        anim.SetFloat("HitPoint", hitPoints);
        Debug.Log("hp witch = " + hitPoints);
        if (hitPoints <= 0)
        {
            WitchAudio.clip = DeathAudio;
            WitchAudio.Play();
            anim.SetBool("Death", true);
            Destroy(gameObject, 5f);
        }
    }


    public void HitConnect()
    {
        WitchAudio.clip = AttackConnectAudio;
        WitchAudio.Play();
        if (DistancetoTarget <= agent.stoppingDistance)
        {
            // Debug.Log("kena hit");
            target.GetComponent<MovementLogic>().PlayerGetHit(40f);
        }
    }

    public void step()
    {
        WitchAudio.clip = StepAudio;
        WitchAudio.Play();
    }

    //public void WitchGetHit(float damage)
    //{
    //     Debug.Log("Witch Receive Damage - " + damage);
    //     hitPoints = hitPoints - damage;
    //     if (hitPoints == 0f)
    //     {
    //       WitchAudio.clip = GetHitAudio;
    //       WitchAudio.Play();
    //        anim.SetBool("GetHit", false);
    //     }
    //}

}
