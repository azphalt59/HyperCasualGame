using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelSave : MonoBehaviour
{
    public static LevelSave Instance;
    public List<bool> LevelEnd;
    public List<SpriteRenderer> levels;
    public int infinyLevelScore = 0;

    public Sprite greenBox;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(transform.gameObject);
        Screen.SetResolution(Screen.height * 9 / 16, Screen.height, true);

    }
    private void Start()
    {
        Screen.SetResolution(Screen.height *9/16, Screen.height, true);
    }

    public void GetData()
    {
        levels.Clear();
        foreach (Level item in FindObjectsOfType<Level>())
        {
            levels.Add(item.gameObject.GetComponent<SpriteRenderer>());
        }
        levels = levels.OrderBy(o => o.name).ToList();
    }
    public void GetData(SpriteRenderer spriteRenderer)
    {
        levels.Add(spriteRenderer);
        
    }
    private void Update()
    {
       if(SceneManager.GetActiveScene().buildIndex == 0)
       {
            for (int i = 0; i < levels.Count; i++)
            {
                if(LevelEnd[i])
                {
                    levels[i].sprite = greenBox;
                }
            }
       }
    }
}
