using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Material> CubeColors;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] public GameObject currentCubeProjectile;
    [SerializeField] public GameObject nextCubeProjectile;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform boardSpawnPosition;
    


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
        if (nextCubeProjectile == null)
        {
            SpawnNextCubeProjectile();
        }
        if (currentCubeProjectile == null)
        {
            nextCubeProjectile.transform.position = boardSpawnPosition.position;
            currentCubeProjectile = nextCubeProjectile;
            currentCubeProjectile.GetComponent<CubeProjectile>().enabled = true;
            nextCubeProjectile = null;
        }
    }
   
    void SpawnNextCubeProjectile()
    {
        if (nextCubeProjectile == null)
        {
            GameObject cube = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
            nextCubeProjectile = cube;
        }
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
