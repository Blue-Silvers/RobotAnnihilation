using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    private LineRenderer line;
    private Transform laserSpawnPoint;
    [SerializeField] private float maxLenght;

    [SerializeField] private float damagePerTick;
    [SerializeField] private float maxCharge;
    [SerializeField] private float actualCharge;
    [SerializeField] private float surcharge;


    [SerializeField] private ParticleSystem startParticle;
    [SerializeField] private ParticleSystem endParticle;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        laserSpawnPoint = GetComponent<Transform>();
        line.enabled = false;

        startParticle.Stop();
        endParticle.Stop();

        actualCharge = maxCharge;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {


            line.enabled = true;
            startParticle.Play();
            endParticle.Play();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1)) 
        { 
            line.enabled = false;
            startParticle.Stop();
            endParticle.Stop();
            line.SetPosition(0, laserSpawnPoint.position);
            line.SetPosition(1, laserSpawnPoint.position);
        }
    }

    private void FixedUpdate()
    {
        if (line.enabled)
        {
            if (actualCharge <= 0)
            {
                Surcharge();
            }
            else
            {
                actualCharge -= surcharge;
            }
        }
        else 
        { 
            return; 
        }

        Ray ray = new Ray(laserSpawnPoint.position, laserSpawnPoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxLenght);
        Vector3 hitPosition = cast ? hit.point : laserSpawnPoint.position + laserSpawnPoint.forward * maxLenght;
        line.SetPosition(0, laserSpawnPoint.position);
        line.SetPosition(1, hitPosition);
        endParticle.transform.position = hitPosition;

        if(line.enabled == true && cast && hit.collider.TryGetComponent(out EnviroDestroy enviroDestroy))
        {
            enviroDestroy.LaserDamage(damagePerTick);
        }
    }

    private void Surcharge()
    {
        line.enabled = false;
        actualCharge = 0;
        startParticle.Stop();
        endParticle.Stop();
        line.SetPosition(0, laserSpawnPoint.position);
        line.SetPosition(1, laserSpawnPoint.position);
    }


    public void Reload(float reloading)
    {
        actualCharge += reloading;

        if(actualCharge > maxCharge)
        {
            actualCharge = maxCharge;
        }
    }
}
