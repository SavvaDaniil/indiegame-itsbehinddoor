using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameChoiseTakePencil : GameChoiseAbstract
{
    [XmlElement("pr_choise")]
    public int choise = 0;

    public GameChoiseTakePencil()
    {
        
    } 
    public GameChoiseTakePencil(int choise)
    {
        this.choise = choise;
    }

    public bool IsTook(){
        return this.choise == 1;
    }
}
