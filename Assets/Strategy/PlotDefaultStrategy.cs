using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotDefaultStrategy : MonoBehaviour
{
    [SerializeField]
    public PlotDefaultContentScriptableObject content;


    // Start is called before the first frame update
    void Start()
    {
        if(content == null){
            Debug.Log("content is null | PlotDefaultStrategy start failed");
            return;
        }
        Debug.Log("PlotDefaultStrategy №"+ content.Identificator +" Start");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
