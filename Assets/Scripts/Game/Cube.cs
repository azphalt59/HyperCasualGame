using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody rb;
    public float verticalForce = 10f;
    public float tumble = 5f;
    bool isTrigger = false;
    private float timer;
    
    private Vector3 eatenCube;
    private float soundTimer = 0.5f;
    private float soundlastTimer = 0;
    private bool hasSounded = false;

    public enum ShapeType { Cube, Triangle, Prism};
    public ShapeType shapeType;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(isTrigger)
        {
            timer += Time.deltaTime;
        }
        if(timer > 0.2f)
        {
            isTrigger = false;
            timer = 0f;
        }

        if(hasSounded)
        {
            soundlastTimer += Time.deltaTime;
        }
        if(soundTimer < soundlastTimer)
        {
            hasSounded = false;
            soundlastTimer = 0f;
        }
    }
    public void Bump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.forward * GameManager.Instance.HorizontalForce, ForceMode.Impulse);
        rb.AddForce(Vector3.up * GameManager.Instance.VerticalForce, ForceMode.Impulse);
        rb.angularVelocity = Random.insideUnitSphere * GameManager.Instance.Tumble;
    }
       
    public void EatCube(Vector3 eatenCubePos)
    {
        eatenCube = eatenCubePos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OutOfLevel>() != null)
        {
            GameManager.Instance.OutOfLevel();
            Destroy(gameObject);
        }
           

        if (other.gameObject.GetComponent<CubeType>() != null)
        {
            if(other.gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.BlackCube)
            {
                    GameManager.Instance.OnDeathCube(gameObject.transform.position);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    return;
            }
            if (gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.BlackCube)
            {
                GameManager.Instance.OnDeathCube(other.gameObject.transform.position);
                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }

            if (other.gameObject.GetComponent<CubeType>().GetCubeValue() == gameObject.GetComponent<CubeType>().GetCubeValue() || gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow)
            {
                GameObject cubeThrew;
                if (other.gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow && GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow)
                {
                    return;
                }
                else if(other.gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow)
                {
                    if (isTrigger)
                        return;

                    isTrigger = true;
                    GetComponent<CubeType>().CubeLevelUp();
                    GetComponent<Cube>().EatCube(other.transform.position);
                    GameManager.Instance.OnDeathCube(other.gameObject.transform.position);
                    Destroy(other.gameObject);
                    return;
                }
                else if(gameObject.GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.Rainbow)
                {
                    if (isTrigger)
                        return;

                    isTrigger = true;
                    GameManager.Instance.OnDeathCube(gameObject.transform.position);
                    Destroy(gameObject);
                    other.gameObject.GetComponent<CubeType>().CubeLevelUp();
                    other.gameObject.GetComponent<Cube>().EatCube(other.transform.position);
                    
                    return;
                }
                else if(other.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= gameObject.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    if (isTrigger)
                        return;

                    isTrigger = true;
                    cubeThrew = other.gameObject;
                    cubeThrew.GetComponent<CubeType>().CubeLevelUp();
                    cubeThrew.GetComponent<Cube>().EatCube(other.transform.position);
                    GameManager.Instance.OnDeathCube(gameObject.transform.position);
                    Destroy(this.gameObject);
                    return;
                }
                else if(other.gameObject.GetComponent<Rigidbody>().velocity.magnitude <= gameObject.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    if (isTrigger)
                        return;

                    isTrigger = true;
                    cubeThrew = gameObject;
                    cubeThrew.GetComponent<CubeType>().CubeLevelUp();
                    cubeThrew.GetComponent<Cube>().EatCube(other.transform.position);
                    GameManager.Instance.OnDeathCube(other.gameObject.transform.position);
                    Destroy(other.gameObject);
                    return;
                }

                
            }
            if(other.gameObject.GetComponent<CubeType>().GetCubeValue() != gameObject.GetComponent<CubeType>().GetCubeValue())
            {
                if (hasSounded)
                    return;

                hasSounded = true;
                GameManager.Instance.OnCubeCol(transform.position, gameObject);
            }
        }
        if (GetComponent<CubeProjectile>() != null) return;
        if (other.gameObject.GetComponent<GameOver>() != null)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
                GameManager.Instance.gameObject.GetComponent<WinCondition>().Lose();
        }

    }
}
