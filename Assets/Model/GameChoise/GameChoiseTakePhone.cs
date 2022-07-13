using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameChoiseTakePhone// : GameChoiseAbstract
{
    [XmlElement("pr_choise")]
    public int choise = 0;

    public GameChoiseTakePhone()
    {
        
    } 
    public GameChoiseTakePhone(int choise)
    {
        this.choise = choise;
    }
    

    public bool IsTook(){
        return this.choise == 1;
    }
}
