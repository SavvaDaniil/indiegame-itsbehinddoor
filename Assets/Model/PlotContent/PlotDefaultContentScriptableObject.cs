using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlotDefaultContent", menuName = "ScriptableObjects/PlotDefaultContent", order = 1)]
public class PlotDefaultContentScriptableObject : ScriptableObject
{
    [SerializeField]
    public string Identificator;

    [SerializeField]
    public int waitSecsForText = 0;

    [TextArea(10, 100)]
    public string Description;

    [SerializeField]
    public bool IsPauseBefore16SecsToEnd = false;

    [SerializeField]
    public bool IsFinish = false;

    [SerializeField]
    public bool IsStrategy = false;

    [SerializeField]
    public PlotDefaultContentScriptableObject NextPlot;
    [SerializeField]
    public PlotDefaultContentScriptableObject DynamicSubPlot;


    [SerializeField]
    public bool IsMakeChoiseContent = false;
    [SerializeField]
    public float SecondsWhenPause = 0.0f;
    [SerializeField]
    public string Choise1;
    [SerializeField]
    public string Choise2;
    [TextArea(10, 20)]
    public string TextPlotAfterChoise;

    [SerializeField]
    public PlotContentMusic plotContentMusic;

}
