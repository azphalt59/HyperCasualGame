using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isInfiny;

    // Start is called before the first frame update
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
        if (isInfiny)
            SetScore(LevelSave.Instance.infinyLevelScore);
    }
    // Update is called once per frame
    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = "Score " + score.ToString();
    }
    
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void SetScore(int precScore)
    {
        score = precScore;
    }
    public void SetScore()
    {
        LevelSave.Instance.infinyLevelScore = score;
    }

}
