using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitsionHandler : MonoBehaviour
{
    [SerializeField] private GameObject go;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(go);
    }
}
