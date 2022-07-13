using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateCurrentSingleton
{
    private static GameStateCurrentSingleton _instance;

    private GameStateCurrentSingleton(){}

    public static GameStateCurrentSingleton getInstance(){
        if(_instance == null)_instance = new GameStateCurrentSingleton();
        return _instance;
    }

    public bool IsNewGameOrContinueAndFalse = true;
}
