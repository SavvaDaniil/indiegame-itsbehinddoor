using System;
using System.Threading;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class GameService
{

    private List<PlotDefaultContentScriptableObject> _plotContentsDefault;
    private readonly string _userGameDataPathXML = System.IO.Path.Combine(Application.persistentDataPath, "UserGameData.xml");
    private static Mutex _userGameDataMutex = new Mutex();

    public GameService(){}

    public GameService(List<PlotDefaultContentScriptableObject> plotContents){
        _plotContentsDefault = plotContents;
    }

    public void Save(PlotDefaultContentScriptableObject plotContent){
        //Debug.Log("GameService Save");
        UserGameData userGameData = this.LoadLastUserGameData();
        if(userGameData == null)userGameData = new UserGameData();
        userGameData.lastPlotContentId = plotContent.Identificator;
        userGameData.dateOfSave = DateTime.Now;

        //Thread threadSave = new Thread(() => saveInFile(userGameData));
        //threadSave.Start();
        saveInFile(userGameData);
    }

    public void Save(GameChoiseTakePhone gameChoiseTakePhone){
        ...
        saveInFile(userGameData);
    }

    public void Save(GameChoiseTakePencil gameChoiseTakePencil){
        ...
        
        saveInFile(userGameData);
    }
    
    public void ClearSave(){
        UserGameData userGameData = new UserGameData();
        userGameData.lastPlotContentId = null;
        userGameData.dateOfSave = DateTime.Now;
        
        saveInFile(userGameData);
    }


    private void saveInFile(UserGameData userGameData)
    {
        Debug.Log("GameService try saveInFile id: " + userGameData.lastPlotContentId);
        
        try {
            XmlSerializer serializer = new XmlSerializer(typeof(UserGameData)); // создаем сериализатор и сообщаем ему о том, какой тип сериализуем
            using (TextWriter writer = new StreamWriter(_userGameDataPathXML)) // если вкратце, то здесь мы создаем модуль, позволяющий записывать символы по указанной директории
            {
                serializer.Serialize(writer, userGameData); // сериализуем данные
            }
        } catch(Exception ex) {
            Debug.Log("GameService save failed ex: " + ex);
            return;
        }
        
        Debug.Log("GameService successfullSave");
    }

    public PlotDefaultContentScriptableObject LoadLastPlotContent(){
        string lastPlotContentId = this.LoadLastPlotContentId();
        if(lastPlotContentId == null){
            //Debug.Log("GameService сохранение не найдено");
            return null;
        }
        //Debug.Log("GameService последняя сохраненный id сцены: " + lastPlotContentId);

        foreach(PlotDefaultContentScriptableObject plotContent in _plotContentsDefault){
            if(plotContent.Identificator.Equals(lastPlotContentId)){
                //Debug.Log("GameService последняя сохраненная сцена: " + plotContent.Identificator);
                return plotContent;
            }
        }
        return null;
    }

    public string LoadLastPlotContentId(){
        UserGameData userGameData = this.LoadLastUserGameData();
        if(userGameData == null)return null;
        return userGameData.lastPlotContentId;
    }

    public UserGameData LoadLastUserGameData(){
        UserGameData userGameData;

        //_userGameDataMutex.WaitOne();
        try {
            if(!System.IO.File.Exists(_userGameDataPathXML)){
                Debug.Log("Файл с сохранением не найден");
                //_userGameDataMutex.ReleaseMutex();
                return null;
            }
            
            XmlSerializer deserializer = new XmlSerializer(typeof(UserGameData));
            using (TextReader reader = new StreamReader(_userGameDataPathXML))
            {
                userGameData = (UserGameData) deserializer.Deserialize(reader);
                //if(userGameData.gameChoiseTakePhone != null){
                    //Debug.Log("userGameData.gameChoiseTakePhone choise: " + userGameData.gameChoiseTakePhone.choise);
                //}
            }

        } catch(Exception ex){
            Debug.Log("GameService LoadLastUserGameData failed Exception: " + ex);
            //_userGameDataMutex.ReleaseMutex();
            return null;
        }
        //_userGameDataMutex.ReleaseMutex();

        return userGameData;
    }

}
