using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlotContentMusic", menuName = "ScriptableObjects/PlotContentMusic", order = 2)]
public class PlotContentMusic : ScriptableObject
{
    [SerializeField]
    public string NameOfFile;
    
    [SerializeField]
    public float volume = 0.7f;
}
