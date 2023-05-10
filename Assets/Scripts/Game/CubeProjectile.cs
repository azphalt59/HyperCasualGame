using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeProjectile : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] private float force;
    [SerializeField] private bool minValue = true;
   
    Rigidbody rb;
    bool isThrow = false;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        force = gm.MinForce;   
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isThrow == false)
        {
            MoveCubePositionArrow();
            if (Input.GetMouseButton(0))
            {
                MoveCubePosition();
            }
            if(Input.GetKey(KeyCode.Space))
            {
                ControlForce();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ThrowCube();
                //force = gm.MinForce;
            }
            
        }
       
        if(isThrow)
        {
            if(rb.velocity.magnitude == 0)
            {
                if(GetComponent<ExplosionBall>() == null)
                    GameManager.Instance.currentShape = null;
                Destroy(this);
            }
        }
    }
    public bool GetProjectileState()
    {
        return isThrow;
    }
    public void ControlForce()
    {
        GameManager.Instance.SliderValue = force;

        if (force >= gm.MaxForce)
            minValue = false;
        if (force <= gm.MinForce)
            minValue = true;

        if (minValue)
        {
            force += gm.IncrementForce * Time.deltaTime;
        }
        else
        {
            force -= gm.IncrementForce * Time.deltaTime;
        } 
    }
    
    public void ThrowCube()
    {
        foreach (Collider item in gameObject.GetComponents<Collider>())
        {
            item.enabled = true;
        }
        if(GetComponent<CubeType>() != null)
        {
            if(GetComponent<CubeType>().typeOfCube == CubeType.TypeOfCube.BlackCube)
            {
                gameObject.AddComponent<ExplosionBall>();
            }
        }
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        isThrow = true;
    }
    public void MoveCubePositionArrow()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(xMovement, 0, 0) * Time.deltaTime * GameManager.Instance.GetShapeSpeed();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
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
            GameManager.Instance.currentShape = null;
            Destroy(this);
        }
            
        
    }


}
