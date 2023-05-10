using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private ScriptableLevel levelInput;
    public int health = 3;
    private int maxHealth;
    private int hit = 0;
    [SerializeField] private int bestValue = 2;
    private int bestColorIndex = 2;
    private bool isWin = false;

    [Header("VFX")]
    public List<GameObject> DeathVfx;
    public List<GameObject> WallHitVfx;
    public List<ParticleSystem> WinVfx;
    public List<GameObject> LoseVfx;
    public GameObject BestShape;
    public Transform bestShapeTransform;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tookDamage;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip newColor;
    [SerializeField] private AudioClip firework;
    [SerializeField] private AudioClip wallHit;
    [SerializeField] private AudioClip cubeBump;
    [SerializeField] private AudioClip cubeCol;



    [Header("Special shapes frequencies")]
    [SerializeField] private float rainbowFrequency = 0;
    [SerializeField] private float explosionFrequency = 0;

    [Header("Shapes settings")]
    public List<Material> ShapeColor;
    [SerializeField] private List<GameObject> shapesPrefab;
    public float MassOnLevelUp = 0.1f;
    [SerializeField] private float shapeSpeed;

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

    [Header("Force Slider")]
    [SerializeField] private Slider forceSlider;
    public float SliderValue;
    public float MinForce = 0.35f;
    public float MaxForce = 1.5f;
    public float IncrementForce = 0.2f;
    [SerializeField] private float minToMaxDuration = 1f;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        forceSlider.minValue = MinForce;
        forceSlider.maxValue = MaxForce;
        IncrementForce = (MaxForce - MinForce)/minToMaxDuration;
        maxHealth = health;

        if(levelInput != null)
        {
            rainbowFrequency = levelInput.RainbowFrequency;
            health = levelInput.Health;
            GetComponent<WinCondition>().SetWinCondition(levelInput.WinValue);
            GetComponent<WallManager>().WallColor = levelInput.WallColor;
            GetComponent<WallManager>().SetStartWall();
        }
    }
    void Update()
    {
        if (isWin)
            return;

        SpawnShape();
        forceSlider.minValue = MinForce;
        forceSlider.maxValue = MaxForce;
        forceSlider.value = SliderValue;
        IncrementForce = (MaxForce - MinForce) / minToMaxDuration;

    }

    public void UpdateSlider(float value)
    {
        SliderValue = value;
    }
    public void NewShapeIsNewStronger(int cubeValue)
    {
        if(cubeValue > bestValue)
        {
            WallManager.Instance.HealWalls();
            OnBestShape(bestShapeTransform.position);
            bestValue = cubeValue;
            bestColorIndex++;
        }

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
    public void OutOfLevel()
    {
        health--;
        if (health <= 0)
            GetComponent<WinCondition>().Lose();
        if (hit > maxHealth - 2)
            return;

        WallManager.Instance.GroundDamage(hit);
        hit++;

        
    }
    public void OnBestShape(Vector3 pos)
    {
        GameObject vfx = Instantiate(BestShape, pos, Quaternion.identity);
        vfx.GetComponent<ParticleSystem>().Play();
        PlaySound(newColor);
        Destroy(vfx, 3f);
    }
    public void OnCubeCol(Vector3 pos, GameObject cube)
    {
        AudioSource a = cube.AddComponent<AudioSource>();
        a.playOnAwake = false;
        a.clip = cubeCol;
        a.volume = 0.075f;
        a.Play();
        Destroy(a, 3f);
    }
    public void OnDeathCube(Vector3 pos)
    {
        int rand = Random.Range(0, DeathVfx.Count);
        GameObject vfx = Instantiate(DeathVfx[rand], pos, Quaternion.identity);
        vfx.GetComponent<ParticleSystem>().Play();
        AudioSource a = vfx.AddComponent<AudioSource>();
        a.playOnAwake = false;
        a.clip = cubeBump;
        a.volume = 0.15f;
        a.Play();
        Destroy(vfx, 3f);
    }
    
    public void OnWallHit(Vector3 pos)
    {
        int rand = Random.Range(0, WallHitVfx.Count);
        GameObject vfx = Instantiate(WallHitVfx[rand], pos, Quaternion.identity);
        vfx.GetComponent<ParticleSystem>().Play();
        AudioSource a = vfx.AddComponent<AudioSource>();
        a.playOnAwake = false;
        a.clip = cubeBump;
        a.volume = 0.15f;
        a.Play();
        Destroy(vfx, 3f);
    }
    public void OnWin(Vector3 pos)
    {
        foreach (ParticleSystem item in WinVfx)
        {
            item.Play();
            int rand = Random.Range(0, 101);
            if(rand > 66)
            {
                AudioSource a = item.gameObject.AddComponent<AudioSource>();
                a.playOnAwake = false;
                a.clip = firework;
                a.Play();
            }
            
        } 
    }
    public void OnLose(Vector3 pos)
    {
        int rand = Random.Range(0, LoseVfx.Count);
        GameObject vfx = Instantiate(LoseVfx[rand], pos, Quaternion.identity);
        PlaySound(explosion);
        vfx.GetComponent<ParticleSystem>().Play();
        Destroy(vfx, 3f);
    }

    public float GetRainbowFrequency()
    {
        return rainbowFrequency;
    }
    public float GetExplosionFrequency()
    {
        return explosionFrequency;
    }
    public float GetShapeSpeed()
    {
        return shapeSpeed;
    }
    public int GetBestShapeValue()
    {
        return bestValue;
    }
    public int GetBestColorIndex()
    {
        return bestColorIndex;
    }
    public void SetWinToTrue()
    {
        isWin = true;
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
