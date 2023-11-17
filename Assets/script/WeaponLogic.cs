using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [Header("Weapon SFX")]
    public AudioClip AttackConnectAudio;
    AudioSource WeaponAudio;

    [Header("Weapon VFX")]
    public ParticleSystem SlashEffect;
    public MovementLogic logic;
    // public GameObject hit;

    public void SlashEffectToggleOn()
    {
        SlashEffect.Play();
    }

    //public void AttackConnect()
    //{
    //    WeaponAudio.clip = AttackConnectAudio;
    //    WeaponAudio.Play();
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Witch")
        {
            //WeaponAudio.clip = AttackConnectAudio;
            //WeaponAudio.Play();

            other.GetComponent<Animator>().SetTrigger("GetHit");
            Witch target = other.transform.GetComponent<Witch>();

            MovementLogic logic = this.GetComponentInParent<MovementLogic>();
            if (logic.GetComponent<Animator>().GetBool("Attack") == true)
            {
                target.TakeDamage(50);
                Debug.Log(other.name + "-50");
            }
        }
        // else
        // Debug.Log("salah");
    }


}
