using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitsionHandler : MonoBehaviour
{
    [SerializeField] private GameObject go;
    [SerializeField] private float batteryCharge;
    [SerializeField] private LaserScript laserScript1;
    [SerializeField] private LaserScript laserScript2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            laserScript1.Reload(batteryCharge);
            laserScript2.Reload(batteryCharge);

            Destroy(go);
        }

    }
}
