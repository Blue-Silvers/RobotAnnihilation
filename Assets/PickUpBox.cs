using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{

    [SerializeField] CapsuleCollider capsule;
    private Animator animator;
    [SerializeField] private LaserScript laserScript;

    float actualLife;
    [SerializeField] float maxLife;
    [SerializeField] float heal;
    [SerializeField] int enemyDamage;
    [SerializeField] int money;
    [SerializeField] float batteryCharge;

    private void Start()
    {
        money = 0;
        actualLife = maxLife;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            actualLife -= enemyDamage;

            if (actualLife <= 0)
            {
                animator.SetBool("Dead", true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money"))
        {

            animator.SetTrigger("Pickup");

            if (other.gameObject.tag == "Ammo")
            {
                laserScript.Reload(batteryCharge);
            }
            else if (other.gameObject.tag == "Heal")
            {
                actualLife += heal;

                if (actualLife > maxLife)
                {
                    actualLife = maxLife;
                }
            }
            else
            {
                money += 1;
            }

            Destroy(other.gameObject);
        }
        else if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Upgrade")
        {
            animator.SetTrigger("Use");
            //arrêter le temps
            //afficher ATH
        }
    }
}
