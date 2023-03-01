using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody rb;
    public float verticalForce = 10f;
    public float tumble = 5f;
    bool bumped = false;
    
    private Vector3 eatenCube;

    public enum ShapeType { Cube, Triangle, Prism};
    public ShapeType shapeType;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    public void Bump()
    {
        //if(bumped)
        //{
        //    Debug.Log("Déjà bump");
        //    return;
        //}

        bumped = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.forward * GameManager.Instance.HorizontalForce, ForceMode.Impulse);
        rb.AddForce(Vector3.up * GameManager.Instance.VerticalForce, ForceMode.Impulse);
        rb.angularVelocity = Random.insideUnitSphere * GameManager.Instance.Tumble;
        
    }
       
    public void EatCube(Vector3 eatenCubePos)
    {
        //Debug.Log("Eat a cube");
        eatenCube = eatenCubePos;
       
       
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
}
