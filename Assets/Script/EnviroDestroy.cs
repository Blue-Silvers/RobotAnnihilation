using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroDestroy : MonoBehaviour
{
    [SerializeField] private float maxLife;
    private float actualLife;
    private void Start()
    {
        actualLife = maxLife;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(gameObject);
        }
    }

    public void LaserDamage(float damage)
    {
        actualLife -= damage;
        if (actualLife < 0)
        {
            Destroy(gameObject) ;
        }
    }
}
