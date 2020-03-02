using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSphere : MonoBehaviour
{
    [HideInInspector] public Material sphereColor;
    float explosionRadius;
    float explosionForce = 50.0f;
    float explosionUpward = 0.4f;

    void OnEnable()
    {
        explosionRadius = GetComponent<SphereCollider>().radius;

        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            // set proper color of spheres while applying force (could be left or right)
            MeshRenderer mr = hit.GetComponent<MeshRenderer>();
            if (mr != null && mr.material != sphereColor)
            {
                mr.material = sphereColor;
            }
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }
}
