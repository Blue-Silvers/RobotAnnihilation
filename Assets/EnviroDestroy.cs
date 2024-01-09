using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroDestroy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(gameObject);
        }
    }
}
