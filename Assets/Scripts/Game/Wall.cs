using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    MeshRenderer wallMesh;
    public int colorIndex = 0;
    
    private float timer;
    public int ddd;
    
    // Start is called before the first frame update
    void Start()
    {
        WallManager.Instance.Walls.Add(this);
        wallMesh = GetComponent<MeshRenderer>();
        wallMesh.material = WallManager.Instance.WallColor[0];
        ddd = WallManager.Instance.WallColor.Count;
    }
    private void Hitted(Vector3 colPos)
    {
        if (timer < WallManager.Instance.WallHitCD)
            return;
        if (colorIndex >= WallManager.Instance.WallColor.Count-1)
            return;

        if (colorIndex < WallManager.Instance.WallColor.Count-1)
        {
            GameManager.Instance.OnWallHit(colPos);
            colorIndex++;
            wallMesh.material = WallManager.Instance.WallColor[colorIndex];
        }
           
        if (colorIndex == WallManager.Instance.WallColor.Count-1)
        {
            GetComponent<Collider>().enabled = false;
        }
        timer = 0;
    }

    public void Heal()
    {
        if (colorIndex == 0)
            return;

        GetComponent<Collider>().enabled = true;
        wallMesh.material = WallManager.Instance.WallColor[colorIndex];
        colorIndex--;
        SetMaterial();
 
    }

    public void SetMaterial()
    {
        wallMesh.material = WallManager.Instance.WallColor[colorIndex];
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Cube>() != null)
        {
            if(collision.gameObject.GetComponent<CubeProjectile>() != null)
            {
                if(collision.gameObject.GetComponent<CubeProjectile>().GetProjectileState() == false)
                {
                    return;
                }
            }
            if (timer < WallManager.Instance.WallHitCD)
                return;
            Hitted(collision.gameObject.transform.position);
        }
    }
}
