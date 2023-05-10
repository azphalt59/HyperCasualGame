using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinCondition : MonoBehaviour
{
    [SerializeField] private int winValue;
    [SerializeField] private int RealWinValue;

    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private GameObject outOfLevel;
   
    private void Update()
    {
        RealWinValue = (int)Mathf.Pow(2, winValue);
    }
    public void CheckWin(int cubeValue)
    {
        if (cubeValue >= Mathf.Pow(2, winValue))
        {
            Win();
        }
    }

    private void Win()
    {
        winCanvas.SetActive(true);
        GameManager.Instance.SetWinToTrue();
        LevelSave.Instance.LevelEnd[SceneManager.GetActiveScene().buildIndex - 1] = true;
    }

    public void Lose()
    {
        WallManager.Instance.FallingGround();
        Destroy(outOfLevel);
        Invoke("ShowLoseCanvas", 2f);
    }
    public void ShowLoseCanvas()
    {
        loseCanvas.SetActive(true);
    }
    public void RetryLevel()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene);
    }

    public void SetWinCondition(int newValue)
    {
        winValue = newValue;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ContinueLevel()
    {
        
        int currScene = SceneManager.GetActiveScene().buildIndex;
       
        if (currScene+1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currScene + 1);
        }
        
    }
}
