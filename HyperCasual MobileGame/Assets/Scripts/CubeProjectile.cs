using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProjectile : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] private float force;
    Rigidbody rb;
    bool isThrow = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isThrow == false)
        {
            if (Input.GetMouseButton(0))
            {
                MoveCubePosition();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowCube();
            }
        }
       
        if(isThrow)
        {
            if(rb.velocity.magnitude == 0)
            {
                Destroy(this);
            }
        }
    }

    public void ThrowCube()
    {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        isThrow = true;
    }
    public void MoveCubePosition()
    {
        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Camera.main.transform.position.z, Input.mousePosition.z - Camera.main.transform.position.z));
        transform.position = new Vector3(MouseWorldPos.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.GetComponent<CubeType>() != null && isThrow)
        {
            Destroy(this);
        }
            
        
    }


}
