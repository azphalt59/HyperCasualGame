using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeType : MonoBehaviour
{
    [SerializeField] private float cubeValue = 0;
    [SerializeField] private MeshRenderer cubeRenderer;
    GameManager gameManager;
    int levelIndex = 0;
    public enum TypeOfCube
    {
        One, Two, Four, Eight, Sixteen, 
        ThirtyTwo, SixtyFour, OneHundredTwentyEight,
        TwoHundredFiftySix, FiveHundredTwelve, OneThousandTwentyFour
    }
    public TypeOfCube typeOfCube;

    private void Start()
    {
        gameManager = GameManager.Instance;
        RandomCubeType();
        switch (typeOfCube)
        {
            case TypeOfCube.One:
                cubeRenderer.material = gameManager.CubeColors[0];
                cubeValue = 1;
                levelIndex = 0;
                break;
            case TypeOfCube.Two:
                cubeRenderer.material = gameManager.CubeColors[1];
                cubeValue = 2;
                levelIndex = 1;
                break;
            case TypeOfCube.Four:
                cubeRenderer.material = gameManager.CubeColors[2];
                cubeValue = 4;
                levelIndex = 2;
                break;
            case TypeOfCube.Eight:
                cubeRenderer.material = gameManager.CubeColors[3];
                cubeValue = 8;
                levelIndex = 3;
                break;
            case TypeOfCube.Sixteen:
                cubeRenderer.material = gameManager.CubeColors[4];
                cubeValue = 16;
                levelIndex = 4;
                break;
            case TypeOfCube.ThirtyTwo:
                cubeRenderer.material = gameManager.CubeColors[5];
                cubeValue = 32;
                levelIndex = 5;
                break;
            case TypeOfCube.SixtyFour:
                cubeRenderer.material = gameManager.CubeColors[6];
                cubeValue = 64;
                levelIndex = 6;
                break;
            case TypeOfCube.OneHundredTwentyEight:
                cubeRenderer.material = gameManager.CubeColors[7];
                cubeValue = 128;
                levelIndex = 7;
                break;
            case TypeOfCube.TwoHundredFiftySix:
                cubeRenderer.material = gameManager.CubeColors[8];
                cubeValue = 256;
                levelIndex = 8;
                break;
            case TypeOfCube.FiveHundredTwelve:
                cubeRenderer.material = gameManager.CubeColors[9];
                cubeValue = 512;
                levelIndex = 9;
                break;
            case TypeOfCube.OneThousandTwentyFour:
                cubeRenderer.material = gameManager.CubeColors[10];
                cubeValue = 1024;
                levelIndex = 10;
                break;
        }
    }
    private void Update()
    {
        
        
    }

    public float GetCubeValue()
    {
        return cubeValue;
    }

    public void CubeLevelUp()
    {
        levelIndex++;
        cubeValue = Mathf.Pow(2, levelIndex);
        ScoreManager.Instance.AddScore((int)cubeValue);
        cubeRenderer.material = gameManager.CubeColors[levelIndex];
        GetComponent<Cube>().Bump();
    }
    public void RandomCubeType()
    {
        typeOfCube = GetRandomEnum<TypeOfCube>(2);
    }
    public static T GetRandomEnum<T>(int maxIndex)
    {

        if(maxIndex != 0)
        {
            System.Array A = System.Enum.GetValues(typeof(T));
            T V = (T)A.GetValue(UnityEngine.Random.Range(0, maxIndex));
            return V;
        }
        else
        {
            System.Array A = System.Enum.GetValues(typeof(T));
            T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
            return V;
        }
        
    }
    

    
}
