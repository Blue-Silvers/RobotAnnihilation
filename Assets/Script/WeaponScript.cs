using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider swordCollid;

    private void Start()
    {
        swordCollid.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("Attack", true);
            Invoke("SwordCollid", 0.2f);
        }
    }

    public void SwordCollid()
    {
        swordCollid.enabled = true;
    }

}
