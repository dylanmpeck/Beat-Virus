using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDestroy : MonoBehaviour
{
    [SerializeField] GameObject explodeSphere;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != this && collision.collider.tag == "DragSphere")
        {
            GameObject explode = Instantiate(explodeSphere, transform.position, transform.rotation);

            // Debug.Log(this.gameObject.GetComponent<MeshRenderer>().materials[0].ToString());
            //explodeSphere.SetActive(true);
            explode.GetComponent<ExplodeSphere>().sphereColor = GetComponentInChildren<MeshRenderer>().material;
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
