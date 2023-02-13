using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody rb;
    public float verticalForce = 10f;
    public float tumble = 5f;
    bool bumped = false;
    bool hasBump = false;
    bool canBump = false;
    private Vector3 eatenCube;

    public enum ShapeType { Cube, Triangle, Prism};
    public ShapeType shapeType;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (hasBump == false && canBump == true)
        //    Bump();
    }
    public void Bump()
    {
        if (eatenCube.z > transform.position.z && bumped == false || rb.velocity.magnitude < 0.5f && bumped == false)
        {
           
        }
        Debug.Log("Bumped cube");
        bumped = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.forward * 0.25f, ForceMode.Impulse);
        rb.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
       
    public void EatCube(Vector3 eatenCubePos)
    {
        Debug.Log("Eat a cube");
        eatenCube = eatenCubePos;
        canBump = true;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CubeType>() != null)
        {
            if (other.gameObject.GetComponent<CubeType>().GetCubeValue() == gameObject.GetComponent<CubeType>().GetCubeValue() || gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow)
            {
                if (other.gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow) return;
                other.gameObject.GetComponent<CubeType>().CubeLevelUp();
                other.gameObject.GetComponent<Cube>().EatCube(other.transform.position);
                Destroy(this.gameObject);
            }
        }
        if (GetComponent<CubeProjectile>() != null) return;
        if (other.gameObject.GetComponent<GameOver>() != null)
        {
            GameManager.Instance.GameOver();
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.GetComponent<CubeType>()!= null)
        {

        }
    }
}
