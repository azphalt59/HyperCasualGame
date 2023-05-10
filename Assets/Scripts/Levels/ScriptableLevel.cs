using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelInput")]
public class ScriptableLevel : ScriptableObject
{
    public List<Material> WallColor;
    public int WinValue;
    public int Health;
    public float RainbowFrequency;
}
