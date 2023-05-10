using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;

    private void Start()
    {
        LevelSave.Instance.GetData();
        levelSelection.SetActive(false);
    }
    public void ShowLevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }
    public void HideLevelSelection()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ResetProgression()
    {

    }
    
    public void LaunchLevel(string LevelToSpawn)
    {
        SceneManager.LoadScene(LevelToSpawn);
    }
    public void Quit()
    {
        Application.Quit();
    }


}
