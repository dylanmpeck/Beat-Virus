using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDestroy : MonoBehaviour
{
    [SerializeField] GameObject explodeSphere;
    [SerializeField] GameObject graphics;
    RhythmMove mover;

    private void Start()
    {
        mover = GetComponent<RhythmMove>();
    }

    public void EnableMovement()
    {
        mover.enabled = true;
    }

    public void DisableMovement()
    {
        mover.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != this && collision.collider.tag == "DragSphere")
        {
            //GameObject explode = Instantiate(explodeSphere, transform.position, transform.rotation);

            // Debug.Log(this.gameObject.GetComponent<MeshRenderer>().materials[0].ToString());
            explodeSphere.GetComponent<ExplodeSphere>().sphereColor = graphics.GetComponent<MeshRenderer>().material;
            explodeSphere.SetActive(true);
            GetComponent<Collider>().enabled = false;
            graphics.SetActive(false);
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
