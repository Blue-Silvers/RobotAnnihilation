using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private int actualLife;
    MeshRenderer meshRenderer;
    bool hit = false;
    bool canHit = true;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        actualLife = maxLife;
        meshRenderer.material.SetColor("_Color", Color.cyan);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && canHit == true)
        {
            actualLife--;
            meshRenderer.material.SetColor("_Color", Color.red);
            hit = true;
            canHit = false;
            Invoke("blueTic", 0.2f);
        }
    }
    private void Update()
    {
        if (actualLife < 1)
        {
            Destroy(gameObject);
        }
    }

    public void blueTic()
    {
        meshRenderer.material.SetColor("_Color", Color.cyan);
        if (hit == true)
        {
            hit = false;
            Invoke("redTic", 0.2f);
        }
        else
        {
            canHit = true;
        }
    }

    public void redTic()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
        Invoke("blueTic", 0.2f);
    }

}
