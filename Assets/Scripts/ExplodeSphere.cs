using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSphere : MonoBehaviour
{
    [HideInInspector] public Material sphereColor;
    float explosionRadius;
    float explosionForce = 50.0f;
    float explosionUpward = 0.4f;

    private void Awake()
    {



    }
    // Start is called before the first frame update
    void Start()
    {
/*        MeshRenderer[] spheres = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mr in spheres)
        {
            mr.material = sphereColor;
        }*/

        explosionRadius = GetComponent<SphereCollider>().radius;

        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
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
        StartCoroutine(Destroy());
    }

/*    private void OnEnable()
    {
        explosionRadius = GetComponent<SphereCollider>().radius;

        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
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
        StartCoroutine(Destroy());
    }*/

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
