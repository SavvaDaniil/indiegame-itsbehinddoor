using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    private PlotContentFacade _plotContentFacade;

    private Button _btnBack;
    private Button _btnChoise1;
    private Button _btnChoise2;
    private GameObject _btnChoise1GameObject;
    private GameObject _btnChoise2GameObject;
    private Text _btnChoise1Text;

    private VideoPlayer _bgdVideo;
    private Canvas _blockTextBgd;
    private GameObject _canvasTextBgd;
    private Text _hitText;
    private AudioSource _voiceSource;
    private AudioSource _soundSource;
    private AudioSource _musicSource;

    private string _plotTextContent;
    private bool _isContentUploadedToPlayers = false;
    private bool _isVideoLaunched = false;
    private bool _isExitLaunched = false;

    [SerializeField]
    public PlotDefaultContentScriptableObject plotContentCurrent;
    private bool _isAlreadyPauseForChoise = false;

    [SerializeField]
    public List<PlotDefaultContentScriptableObject> plotContents;

    private PlotContentMusic _plotContentMusicCurrent;
    private bool _musicIsPlaying = false;
    
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        this.initObjectsInScene();

        if(!GameStateCurrentSingleton.getInstance().IsNewGameOrContinueAndFalse){
            //загружаем последнюю сцену
            Debug.Log("Грузим сохранение");
            PlotDefaultContentScriptableObject plotContentLast = _plotContentFacade.LoadLastSave();
            if(plotContentLast != null){
                if(plotContentLast.Identificator != null){
                    plotContentCurrent = plotContentLast;
                    Debug.Log("Наденное сохранение id: " + plotContentCurrent.Identificator);
                } else {
                    Debug.Log("Нет идентификатора в сохранении");
                }
            } else {
                Debug.Log("Провал загрузки сохранения");
            }
        }

        if(plotContentCurrent == null){
            Debug.Log("Start content not found");
            return;
        }

        //_plotContentFacade.Run(plotContentCurrent, ref _bgdVideo, ref _isVideoLaunched, ref _voiceSource, ref _soundSource);
        _plotContentFacade.UploadContentToPlayers(plotContentCurrent, ref _bgdVideo, ref _isVideoLaunched, ref _voiceSource, ref _soundSource, ref _musicSource, ref _plotContentMusicCurrent, ref _musicIsPlaying);
        _plotTextContent = plotContentCurrent.Description;
        
        //setupContentTextPrinting();
    }

    void Update()
    {
        if(_isExitLaunched)return;

        try{
            //Debug.Log("GameController CHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECKCHECK");
            if(!_isContentUploadedToPlayers){
                //check for ready content
                ...
            }
        } catch(Exception ex){
            Debug.Log("GameController update bgdVideo.isPrepared failed ex: " + ex);
        }

        try{
            //Debug.Log("IsVideoPrepared: " + _bgdVideo.isPrepared);
            if(_isVideoLaunched){
                if(_plotContentFacade.IsVideoFinished(ref _bgdVideo)){
                    Debug.Log("Видео сцены закончилась");
                    _isVideoLaunched = false;
                    _hitText.text = "";
                    this.getDesicionAfterFinishOfPlotContent();
                }
            }
        } catch(Exception ex){
            Debug.Log("GameController update _isVideoLaunched failed ex: " + ex);
        }

        try{
            
            if(plotContentCurrent.IsMakeChoiseContent && !_isAlreadyPauseForChoise && _isVideoLaunched){
                if(_bgdVideo.time >= plotContentCurrent.SecondsWhenPause){

                    //pause content
                    _plotContentFacade.Pause(plotContentCurrent, ref _bgdVideo, ref _isVideoLaunched, ref _voiceSource, ref _soundSource, ref _musicSource, ref _plotContentMusicCurrent, ref _musicIsPlaying);
                    _isAlreadyPauseForChoise = true;
                    _plotContentFacade.ChangeShowingChoise(true, plotContentCurrent, ref _btnChoise1GameObject, ref _btnChoise2GameObject);
                }
            }
        } catch(Exception ex){
            Debug.Log("GameController update IsMakeChoiseContent failed ex: " + ex);
        }
    }


    private void makeChoise(int choise){
        _plotContentFacade.MakeChoise(plotContentCurrent, ref _btnChoise1GameObject, ref _btnChoise2GameObject, choise);

        ...
    }



    private void setupContentTextPrinting()
    {
        if(_plotTextContent == null || _plotTextContent.Equals("")){
            _canvasTextBgd.SetActive(false);
            return;
        }
        _canvasTextBgd.SetActive(true);
        StartCoroutine(enterTextSlowly(0.03f));
    }


    private IEnumerator enterTextSlowly(float time)
    {
        ...

        StopCoroutine(enterTextSlowly(0.0f));
    }


    private void getDesicionAfterFinishOfPlotContent(){
        _isAlreadyPauseForChoise = false;
        //StopCoroutine(enterTextSlowly(0.005f));

        try{
            if(plotContentCurrent.IsStrategy){
                plotContentCurrent = _plotContentFacade.GetNextPlotContentByChoises(plotContentCurrent);
                if(plotContentCurrent == null){
                    return;
                }
                this.preparePlotContentCurrent();
            } else if(plotContentCurrent.NextPlot != null){
                plotContentCurrent = plotContentCurrent.NextPlot;
                this.preparePlotContentCurrent();
            } else if(plotContentCurrent.IsFinish){
                _plotContentFacade.ClearSave();
                this.toMainMenu();
            } else {
                Debug.Log("Ошибка, нет следущей сцены после id: " + plotContentCurrent.Identificator);
            }
        } catch(Exception ex){
            Debug.Log("GameController getDesicionAfterFinishOfPlotContent failed ex: " + ex);
        }
    }

    private void preparePlotContentCurrent(){
        _isContentUploadedToPlayers = false;
        try {
            _plotContentFacade.UploadContentToPlayers(plotContentCurrent, ref _bgdVideo, ref _isVideoLaunched, ref _voiceSource, ref _soundSource, ref _musicSource, ref _plotContentMusicCurrent, ref _musicIsPlaying);
            _plotTextContent = plotContentCurrent.Description;
        } catch (Exception ex){
            Debug.Log("GameController preparePlotContentCurrent failed ex: " + ex);
        }
    }

    private void playPlotContentCurrent(){
        try {
            _plotContentFacade.Run(plotContentCurrent, ref _bgdVideo, ref _isVideoLaunched, ref _voiceSource, ref _soundSource, ref _musicSource, ref _plotContentMusicCurrent, ref _musicIsPlaying);
            //_plotTextContent = plotContentCurrent.Description;
            setupContentTextPrinting();
        } catch(Exception ex){
            Debug.Log("GameController playPlotContentCurrent failed ex: " + ex);
        }
    }

    private void toMainMenu(){
        //if(_bgdVideo.isPlaying)_bgdVideo.Stop();
        _isExitLaunched = true;
        _isVideoLaunched = false;
        _bgdVideo.Stop();
        _bgdVideo.clip = null;
        Resources.UnloadUnusedAssets(); 
        Debug.Log("GameController toMainMenu");
        try {
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        } catch(Exception ex){
            Debug.Log("GameController toMainMenu Exception: " + ex);
        }
    }


    private void initObjectsInScene(){
        _btnBack = GameObject.Find("btnBack").GetComponent<Button>();

        _btnChoise1 = GameObject.Find("btnChoise1").GetComponent<Button>();
        _btnChoise1.onClick.AddListener(() => makeChoise(1));
        _btnChoise1GameObject = GameObject.FindGameObjectWithTag("btnChoise1");
        _btnChoise1GameObject.SetActive(false);

        _btnChoise2 = GameObject.Find("btnChoise2").GetComponent<Button>();
        _btnChoise2.onClick.AddListener(() => makeChoise(0));
        _btnChoise2GameObject = GameObject.FindGameObjectWithTag("btnChoise2");
        _btnChoise2GameObject.SetActive(false);


        _bgdVideo = GameObject.Find("bgdVideo").GetComponent<VideoPlayer>();
        //_bgdVideo.audioOutputMode = VideoAudioOutputMode.Direct;

        _blockTextBgd = GameObject.Find("blockTextBgd").GetComponent<Canvas>();
        _canvasTextBgd = GameObject.Find("canvasTextBgd");
        _hitText = GameObject.Find("hitText").GetComponent<Text>();

        _voiceSource = GetComponent<AudioSource>();
        _soundSource = GetComponent<AudioSource>();
        _musicSource = GetComponent<AudioSource>();

        _btnBack.onClick.AddListener(toMainMenu);
        _bgdVideo.isLooping = false;
        _plotContentFacade = new PlotContentFacade(plotContents);
    }
}
