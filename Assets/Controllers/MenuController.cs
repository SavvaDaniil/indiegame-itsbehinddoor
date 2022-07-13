using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private readonly GameFacade gameFacade = new GameFacade();
    private Button _btnStart;
    private Button _btnContinue;
    private Button _btnExit;
    private bool _isAnyActualSave = false;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _btnStart = GameObject.Find("_btnStart").GetComponent<Button>();
        _btnStart.onClick.AddListener(startNewGame);
        _btnContinue = GameObject.Find("_btnContinue").GetComponent<Button>();
        _btnContinue.onClick.AddListener(continueGame);

        _btnExit = GameObject.Find("_btnExit").GetComponent<Button>();
        _btnExit.onClick.AddListener(exitGame);
        _isAnyActualSave = gameFacade.IsAnyActualSave();

        if(!_isAnyActualSave){
            ButtonState buttonState = new ButtonState();
            buttonState.setDisabled(ref _btnContinue);
        }
        
    }
    
    void Update()
    {

    }

    private void startNewGame()
    {
        GameStateCurrentSingleton.getInstance().IsNewGameOrContinueAndFalse = true;
        SceneManager.LoadScene("GameScene");
    }

    private void continueGame()
    {
        if(!_isAnyActualSave)return;
        GameStateCurrentSingleton.getInstance().IsNewGameOrContinueAndFalse = false;
        SceneManager.LoadScene("GameScene");
    }

    private void exitGame(){
        Application.Quit();
    }
}
