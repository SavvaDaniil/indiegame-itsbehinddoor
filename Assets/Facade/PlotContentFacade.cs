using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlotContentFacade
{
    private AudioClip _voiceContent;
    private float _voiceLastTime;

    private AudioClip _soundContent;
    private AudioClip _musicContent;
    private List<PlotDefaultContentScriptableObject> _plotContentsDefault;
    private GameService _gameService;

    public PlotContentFacade(List<PlotDefaultContentScriptableObject> plotContents){
        _plotContentsDefault = plotContents;
        _gameService = new GameService(plotContents);
    }


    public PlotDefaultContentScriptableObject LoadLastSave(){
        return _gameService.LoadLastPlotContent();
    }

    public void ClearSave(){
        _gameService.ClearSave();
    }


    public void UploadContentToPlayers(PlotDefaultContentScriptableObject plotContent, ref VideoPlayer bgdVideo, ref bool isVideoLaunched, ref AudioSource voiceSource, ref AudioSource soundSource, ref AudioSource musicSource, ref PlotContentMusic plotContentMusicCurrent, ref bool musicIsPlaying){
        _gameService.Save(plotContent);
        bgdVideo.Stop();
        bgdVideo.clip = null;
        this.uploadVideoToPlayer(plotContent.Identificator, ref bgdVideo, ref isVideoLaunched);

        ...

    }

    public void Run(PlotDefaultContentScriptableObject plotContent, ref VideoPlayer bgdVideo, ref bool isVideoLaunched, ref AudioSource voiceSource, ref AudioSource soundSource, ref AudioSource musicSource, ref PlotContentMusic plotContentMusicCurrent, ref bool musicIsPlaying){
        this.runVideo(plotContent.Identificator, ref bgdVideo, ref isVideoLaunched);

        if(plotContent.plotContentMusic != null && !musicIsPlaying){
            this.runMusic(plotContent.plotContentMusic.volume, ref musicSource);
            musicIsPlaying = true;
        } else {
            Debug.Log("Denied to play music. | musicIsPlaying: " + musicIsPlaying + " | plotContentMusicCurrent.Name: " + (plotContentMusicCurrent != null ? plotContentMusicCurrent.NameOfFile : null));
        }
        
    }

    public void Pause(PlotDefaultContentScriptableObject plotContent, ref VideoPlayer bgdVideo, ref bool isVideoLaunched, ref AudioSource voiceSource, ref AudioSource soundSource, ref AudioSource musicSource, ref PlotContentMusic plotContentMusicCurrent, ref bool musicIsPlaying){
        bgdVideo.Pause();
        isVideoLaunched = false;
    }

    public void Continue(PlotDefaultContentScriptableObject plotContent, ref VideoPlayer bgdVideo, ref bool isVideoLaunched, ref AudioSource voiceSource, ref AudioSource soundSource, ref AudioSource musicSource, ref PlotContentMusic plotContentMusicCurrent, ref bool musicIsPlaying){
        bgdVideo.Play();
        isVideoLaunched = true;
    }

    public void ChangeShowingChoise(bool isShow, PlotDefaultContentScriptableObject plotContent, ref GameObject btnChoise1GameObject, ref GameObject btnChoise2GameObject){
        if(isShow){
            if(!plotContent.IsMakeChoiseContent)return;
            btnChoise1GameObject.GetComponentInChildren<Text>().text = plotContent.Choise1;
            btnChoise2GameObject.GetComponentInChildren<Text>().text = plotContent.Choise2;
            btnChoise1GameObject.SetActive(true);
            btnChoise2GameObject.SetActive(true);
        } else {
            btnChoise1GameObject.SetActive(false);
            btnChoise2GameObject.SetActive(false);
        }
    }

    public void MakeChoise(PlotDefaultContentScriptableObject plotContent, ref GameObject btnChoise1GameObject, ref GameObject btnChoise2GameObject, int choise){
        switch(plotContent.Identificator){
            case "010":
                GameChoiseTakePhone gameChoiseTakePhone = new GameChoiseTakePhone(choise);
                _gameService.Save(gameChoiseTakePhone);
                break;
            case "018":
                GameChoiseTakePencil gameChoiseTakePencil = new GameChoiseTakePencil(choise);
                _gameService.Save(gameChoiseTakePencil);
                break;
            default:
                break;
        }
        this.ChangeShowingChoise(false, plotContent, ref btnChoise1GameObject, ref btnChoise2GameObject);
    }

    public PlotDefaultContentScriptableObject GetNextPlotContentByChoises(PlotDefaultContentScriptableObject plotContent){
        UserGameData userGameData = _gameService.LoadLastUserGameData();
        if(userGameData == null){
            Debug.LogError("Нет сохранений");
            return null;
        }

        switch(plotContent.Identificator){
            ...
        }
        return null;
    }


    private bool uploadVideoToPlayer(string identificator, ref VideoPlayer bgdVideo, ref bool isVideoLaunched){
        if(identificator == null)return false;
        isVideoLaunched = false;
        VideoClip videoClip = Resources.Load<VideoClip>("Video/" + identificator);
        if(videoClip == null){
            Debug.Log("Видео файл к сцене не найден");
            return false;
        }
        bgdVideo.Stop();
        bgdVideo.clip = null;
        bgdVideo.Prepare();
        bgdVideo.clip = videoClip;
        Resources.UnloadUnusedAssets(); //manual garbage collection
        bgdVideo.Prepare();
        return true;
    }

    private bool runVideo(string identificator, ref VideoPlayer bgdVideo, ref bool isVideoLaunched){
        bgdVideo.Play();

        isVideoLaunched = true;

        return true;
    }

    public bool IsVideoFinished(ref VideoPlayer bgdVideo, bool isDebug = false){
        if(isDebug)Debug.Log("bgdVideo.frame: " + bgdVideo.frame + " from " + Convert.ToInt64(bgdVideo.frameCount));
        return bgdVideo.frame + 1 >= Convert.ToInt64(bgdVideo.frameCount);
    }



    private void uploadVoiceToSource(string identificator, ref AudioSource voiceSource){
        //voiceSource.Stop();
        _voiceContent = Resources.Load<AudioClip>("Voice/" + identificator);
        if(_voiceContent == null)
        {
            Debug.Log("Голос не найден");
            return;
        }
        //Debug.Log("PlotContentFacade uploadVoiceToSource: " + _voiceContent);
        voiceSource.clip = _voiceContent;
        voiceSource.volume = 0.5f;
    }

    private void runVoice(string identificator, ref AudioSource voiceSource){
        if(_voiceContent == null)return;
        voiceSource.loop = false;
        voiceSource.Play();
        //voiceSource.PlayOneShot(_voiceContent, 0.7f);
    }


    private void uploadSoundToSource(string identificator, ref AudioSource soundSource){
        _soundContent = Resources.Load<AudioClip>("Sound/" + identificator);
        if(_soundContent != null){
            //soundSource.clip = _soundContent;
            //soundSource.Play(0);
        }
    }

    private void runSound(string identificator, ref AudioSource soundSource){
        if(_soundContent != null){
            //soundSource.clip = _soundContent;
            //soundSource.Play(0);
            soundSource.PlayOneShot(_soundContent, 1.0f);
        }
    }



    private void uploadMusicToSource(string nameOfFile){
        _musicContent = Resources.Load<AudioClip>("Music/" + nameOfFile);
        Debug.Log("PlotContentFacade uploadMusicToSource: " + _musicContent);
    }

    private void runMusic(float volume, ref AudioSource musicSource){
        if(_musicContent != null){
            musicSource.time = 0;
            musicSource.loop = true;
            musicSource.PlayOneShot(_musicContent, volume);
            Debug.Log("PlotContentFacade runMusic: " + _musicContent);
        }
    }
}
