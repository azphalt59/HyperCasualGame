using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    [Header("Special shapes frequencies")]
    [SerializeField] private float rainbowFrequency = 0;
    [SerializeField] private float explosionFrequency = 0;

    [Header("Shapes settings")]
    public List<Material> ShapeColor;
    [SerializeField] private List<GameObject> shapesPrefab;
    public float MassOnLevelUp = 0.1f;

    [Header("Shapes spawn")]
    [SerializeField] private float respawnDelay = 0.5f;
    private float spawnerTimer = 0f;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform boardSpawnPosition;

    [SerializeField] public GameObject currentShape;
    [SerializeField] public GameObject nextShape;

    public GameObject empty;

    [Header("Bounce Settings")]
    public float VerticalForce = 1f;
    public float HorizontalForce = 1f;
    public float Tumble = 5f;



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
        SpawnShape();
    }




    void SpawnShape()
    {
        if (nextShape == null)
        {
            SpawnNextShapeProjectile();
        }
        if (currentShape == null)
        {
            MoveCurrentShapeOnBoard();
        }
    }
    void SpawnNextShapeProjectile()
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

    void MoveCurrentShapeOnBoard()
    {
        spawnerTimer += Time.deltaTime;
        if (spawnerTimer >= respawnDelay)
        {
            Debug.Log(" Move shape ");
            nextShape.transform.position = boardSpawnPosition.position;
            if (nextShape.GetComponent<Cube>() != null)
            {
                if (nextShape.GetComponent<Cube>().shapeType == Cube.ShapeType.Prism) nextShape.transform.position -= new Vector3(0, 0, nextShape.transform.localScale.z * 0.75f);
            }

            currentShape = nextShape;
            currentShape.GetComponent<CubeProjectile>().enabled = true;
            nextShape = null;
            spawnerTimer = 0f;
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public float GetRainbowFrequency()
    {
        return rainbowFrequency;
    }
    public float GetExplosionFrequency()
    {
        return explosionFrequency;
    }
}
