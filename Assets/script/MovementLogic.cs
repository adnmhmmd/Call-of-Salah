using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementLogic : MonoBehaviour
{
    private Rigidbody rb;
    public float walkspeed = 0.5f, runspeed = 1f, jumppower = 10f, fallspeed , HitPoints = 100f, attackRange = 0.5f;
    public Transform PlayerOrientation;
    float horizintalInput;
    float verticalInput;
    float MaxHealth;
    Vector3 moveDirection;
    bool grounded = true;
    public Animator anim;
    bool AimMode = false, TPSMode = true;
    public CameraLogic camLogic;
    public bool isattack;
    public UIGameLogic UIGameplay;

    [Header("Player SFX")]
    public AudioClip AttackSwingAudio;
    public AudioClip StepAudio;
    public AudioClip DeathAudio;
    AudioSource PlayerAudio;

    public bool Attack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //PlayerOrientation = this.GetComponent<Transform>();
        MaxHealth = HitPoints;
        UIGameplay.UpdateHealthBar(HitPoints, MaxHealth);
        PlayerAudio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            Movement();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerGetHit(HitPoints);
        }
        HitLogic();
        Jump();
    }

    public void step()
    {
        // Debug.Log("step");
        PlayerAudio.clip = StepAudio;
        PlayerAudio.Play();
    }

    private void Movement()
    {
        horizintalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizintalInput;

        if (grounded && moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumppower, ForceMode.Impulse);
            grounded = false;
            anim.SetBool("Jump", true);
        }
        else if (!grounded)
        {
            rb.AddForce(Vector3.down * fallspeed * rb.mass, ForceMode.Force);
        }
    }

    public void HitLogic()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerAudio.clip = AttackSwingAudio;
            PlayerAudio.Play();
            // Debug.Log("attack");
            anim.SetBool("Attack", true);
        }
    }

    public void PlayerGetHit(float damage)
    {
        // Debug.Log("Player Receive Damage - " + damage);
        HitPoints = HitPoints - damage;
        UIGameplay.UpdateHealthBar(HitPoints, MaxHealth);
        anim.SetTrigger("GetHit");
        // Debug.Log("hp = " + HitPoints);
        if (HitPoints <= 0f)
        {   
            HitPoints = 0f;
            anim.SetBool("Death", true);
            PlayerAudio.clip = DeathAudio;
            PlayerAudio.Play();
        }
    }

    public void groundedchanger()
    {
        grounded = true;
        anim.SetBool("Jump", false);
    }

}
