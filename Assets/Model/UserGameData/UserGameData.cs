using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UserGameData
{
    [XmlElement("lastPlotContentId")]
    public string lastPlotContentId;
    [XmlElement("dateOfSave")]
    public DateTime dateOfSave;

    [XmlElement("gameChoiseTakePhone")]
    public GameChoiseTakePhone gameChoiseTakePhone;

    [XmlElement("gameChoiseTakePencil")]
    public GameChoiseTakePencil gameChoiseTakePencil;
}
