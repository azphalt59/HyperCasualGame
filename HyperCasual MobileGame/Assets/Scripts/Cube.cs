using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody rb;
    public float verticalForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EatCube()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.GetComponent<CubeType>()!= null)
        {
            if (collision.gameObject.GetComponent<CubeType>().GetCubeValue() == GetComponent<CubeType>().GetCubeValue())
            {
                GetComponent<CubeType>().CubeLevelUp();
                EatCube();
                Destroy(collision.gameObject);
            }
        }
        
    }
}
