using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Material> CubeColors;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] public CubeProjectile cubeProjectile;
    [SerializeField] private Transform spawnPosition;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        if(cubeProjectile == null)
        {
            SpawnCubeProjectile();
        }
       
        
       
    }

    void SpawnCubeProjectile()
    {
        if(cubeProjectile == null)
        {
            GameObject cube = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
            cubeProjectile = cube.GetComponent<CubeProjectile>();
        }
       
    }
}
