using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDestroy : MonoBehaviour
{
    [SerializeField] GameObject explodeSphere;
    [SerializeField] GameObject graphics;
    RhythmMove mover;
    Rigidbody rb;
    bool collided;

    private void Start()
    {
        mover = GetComponent<RhythmMove>();
        rb = GetComponent<Rigidbody>();
    }

    public void EnableMovement()
    {
        if (!collided) mover.enabled = true;
    }

    public void DisableMovement()
    {
        mover.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != this && collision.collider.tag == "DragSphere")
        {
            if (collided) return;
            collided = true;

            GameManager.Scored(100);
            explodeSphere.GetComponent<ExplodeSphere>().sphereColor = graphics.GetComponent<MeshRenderer>().material;
            explodeSphere.SetActive(true);
            GetComponent<Collider>().enabled = false;
            graphics.SetActive(false);
            StartCoroutine(Deactivate());
            SFX.PlayBurstSound();
        }
    }

    IEnumerator Deactivate()
    {
        mover.enabled = false;
        yield return new WaitForSeconds(1.2f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        graphics.SetActive(true);
        explodeSphere.SetActive(false);
        collided = false;
        this.gameObject.SetActive(false);
    }
}
