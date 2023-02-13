using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Material> ShapeColor;

    [SerializeField] private float rainbowFrequency = 0;
    [SerializeField] private List<GameObject> shapesPrefab;
    [SerializeField] public GameObject currentShape;
    [SerializeField] public GameObject nextShape;
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
        if (nextShape == null)
        {
            SpawnNextCubeProjectile();
        }
        if (currentShape == null)
        {
            Debug.Log(" Move shape ");
            nextShape.transform.position = boardSpawnPosition.position;
            if(nextShape.GetComponent<Cube>() != null)
            {
                if (nextShape.GetComponent<Cube>().shapeType == Cube.ShapeType.Prism) nextShape.transform.position -= new Vector3(0, 0, nextShape.transform.localScale.z * 0.75f);
            }
         
            currentShape = nextShape;
            currentShape.GetComponent<CubeProjectile>().enabled = true;
            nextShape = null;
        }
    }
    public float GetRainbowFrequency()
    {
        return rainbowFrequency;
    }
    void SpawnNextCubeProjectile()
    {
        if (nextShape == null)
        {
            Debug.Log(" Spawn shape ");
            int rand = Random.Range(0, shapesPrefab.Count);
            GameObject shape = Instantiate(shapesPrefab[rand], spawnPosition.position, Quaternion.identity);
            nextShape = shape;
            if (nextShape.GetComponent<Cube>() != null)
            {
                if (shape.GetComponent<Cube>().shapeType == Cube.ShapeType.Triangle) shape.transform.eulerAngles = new Vector3(0, 45, 0);
            }
            if (nextShape.GetComponent<Cube>() != null) shape.name = shape.GetComponent<Cube>().shapeType.ToString();
        }
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
