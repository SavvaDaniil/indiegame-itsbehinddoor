using System;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameChoiseAbstract
{
    [XmlElement("choise")]
    public int _choise;

    protected GameChoiseAbstract(){}

    protected GameChoiseAbstract(int choise){
        _choise = choise;
    }
}
