using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBall : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float radius;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Explosion()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach(Collider shape in colliders)
        {
            Rigidbody shapeRb = shape.GetComponent<Rigidbody>();
            if (shapeRb != null)
            {
                Vector3 dir = shape.gameObject.transform.position - transform.position;
                
                shapeRb.AddForce(dir*force, ForceMode.Impulse);
            }   
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CubeType>() != null) Explosion();

    }
    private void Update()
    {
        if(rb.velocity == Vector3.zero && GameManager.Instance.currentShape == this.gameObject) GameManager.Instance.currentShape = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius); 
    }
}