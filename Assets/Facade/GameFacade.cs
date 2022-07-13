using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameFacade
{
    //private float volume = 1.0f;

    public bool IsAnyActualSave(){
        GameService gameService = new GameService();
        return gameService.LoadLastPlotContentId() != null;
    }

    public void launchPlotContent(PlotDefaultContentScriptableObject plotContent, ref VideoPlayer bgdVideo){

        if(plotContent.Identificator != null)this.launchVideo(plotContent.Identificator, ref bgdVideo);
        
    }


    private void launchVideo(string identificator, ref VideoPlayer bgdVideo){
        VideoClip videoClip = Resources.Load<VideoClip>("Video/" + identificator);
        bgdVideo.clip = videoClip;
        bgdVideo.Prepare();
        bgdVideo.Play();
    }

    public bool isVideoFinished(ref VideoPlayer bgdVideo, bool isDebug = false){
        if(isDebug)Debug.Log("bgdVideo.frame: " + bgdVideo.frame + " from " + Convert.ToInt64(bgdVideo.frameCount));
        return bgdVideo.frame + 1 >= Convert.ToInt64(bgdVideo.frameCount);
    }


}
