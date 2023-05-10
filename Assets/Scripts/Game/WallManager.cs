using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [Header("Walls settings")]
    public static WallManager Instance;
    public List<Material> WallColor;
    public float WallHitCD = 0.5f;
    public List<Wall> Walls;

    [Header("Ground settings")]
    public List<Ground> Grounds;
    public List<Material> GroundsDamaged;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void HealWalls()
    {
        foreach (Wall wall in Walls)
        {
            wall.Heal();
        }
    }

    public void SetStartWall()
    {

        foreach (Wall wall in Walls)
        {
            wall.GetComponent<MeshRenderer>().material = WallColor[0];
        }
    }
    public void GroundDamage(int damageIndex)
    {
        foreach (Ground ground in Grounds)
        {
            int rand = Random.Range(0, 101);
            if (rand > 85)
            {
                ground.gameObject.GetComponent<MeshRenderer>().material = GroundsDamaged[damageIndex];
                GameManager.Instance.OnLose(ground.gameObject.transform.position);
            }
                
        }
    }
    public void FallingGround()
    {
        foreach (Ground ground in Grounds)
        {
            ground.AddRb();
        }
    }

    
}
