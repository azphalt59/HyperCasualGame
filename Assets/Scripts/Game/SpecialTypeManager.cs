using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTypeManager : MonoBehaviour
{
    public static SpecialTypeManager Instance;

    [Header("Explosive")]
    [SerializeField] private float force = 5f;
    [SerializeField] private float radius = 2f;
    [SerializeField] private Material explosifMat;

    [Header("Rainbow")]
    [SerializeField] private Material rainbowMat;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public float GetForce()
    {
        return force;
    }

    public float GetRadius()
    {
        return radius;
    }
    public Material GetExploMaterial()
    {
        return explosifMat;
    }
    public Material GetRainbowMaterial()
    {
        return rainbowMat;
    }
}
