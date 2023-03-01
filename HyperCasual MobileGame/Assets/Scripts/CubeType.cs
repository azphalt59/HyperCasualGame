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
        TwoHundredFiftySix, FiveHundredTwelve, OneThousandTwentyFour,
        Rainbow,
        ExplosionShape
    }
    public TypeOfCube typeOfCube;

    private void Start()
    {
        gameManager = GameManager.Instance;
        RandomCubeType();
        int rand = Random.Range(0, 101);
        if (rand < gameManager.GetRainbowFrequency())
        {
            typeOfCube = TypeOfCube.Rainbow;
        }
        if(rand < gameManager.GetExplosionFrequency())
        {
            if (typeOfCube != TypeOfCube.Rainbow)
                typeOfCube = TypeOfCube.ExplosionShape;
        }
        switch (typeOfCube)
        {
            case TypeOfCube.One:
                cubeRenderer.material = gameManager.ShapeColor[0];
                cubeValue = 1;
                levelIndex = 0;
                break;
            case TypeOfCube.Two:
                cubeRenderer.material = gameManager.ShapeColor[1];
                cubeValue = 2;
                levelIndex = 1;
                break;
            case TypeOfCube.Four:
                cubeRenderer.material = gameManager.ShapeColor[2];
                cubeValue = 4;
                levelIndex = 2;
                break;
            case TypeOfCube.Eight:
                cubeRenderer.material = gameManager.ShapeColor[3];
                cubeValue = 8;
                levelIndex = 3;
                break;
            case TypeOfCube.Sixteen:
                cubeRenderer.material = gameManager.ShapeColor[4];
                cubeValue = 16;
                levelIndex = 4;
                break;
            case TypeOfCube.ThirtyTwo:
                cubeRenderer.material = gameManager.ShapeColor[5];
                cubeValue = 32;
                levelIndex = 5;
                break;
            case TypeOfCube.SixtyFour:
                cubeRenderer.material = gameManager.ShapeColor[6];
                cubeValue = 64;
                levelIndex = 6;
                break;
            case TypeOfCube.OneHundredTwentyEight:
                cubeRenderer.material = gameManager.ShapeColor[7];
                cubeValue = 128;
                levelIndex = 7;
                break;
            case TypeOfCube.TwoHundredFiftySix:
                cubeRenderer.material = gameManager.ShapeColor[8];
                cubeValue = 256;
                levelIndex = 8;
                break;
            case TypeOfCube.FiveHundredTwelve:
                cubeRenderer.material = gameManager.ShapeColor[9];
                cubeValue = 512;
                levelIndex = 9;
                break;
            case TypeOfCube.OneThousandTwentyFour:
                cubeRenderer.material = gameManager.ShapeColor[10];
                cubeValue = 1024;
                levelIndex = 10;
                break;
            case TypeOfCube.ExplosionShape:
                gameObject.name += " Explosion";
                cubeRenderer.material = SpecialTypeManager.Instance.GetExploMaterial();
                cubeValue = 0f;
                break;
            case TypeOfCube.Rainbow:
                gameObject.name += " Rainbow";
                cubeRenderer.material = SpecialTypeManager.Instance.GetRainbowMaterial();
                cubeValue = 0;
                //levelIndex = 15;
                break;
           
        }
    }
    private void Update()
    {
        switch(typeOfCube)
        {
            case TypeOfCube.Rainbow:
                RainbowMaterial();
                break;
        }
    }
    public void RainbowMaterial()
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
        cubeRenderer.material = gameManager.ShapeColor[levelIndex];
        GetComponent<Rigidbody>().mass -= GameManager.Instance.MassOnLevelUp;
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
