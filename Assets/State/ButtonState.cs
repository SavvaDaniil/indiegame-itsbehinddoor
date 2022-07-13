using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState
{
    
    public void setDisabled(ref Button btn){
        /*
        Material mat = btn.GetComponent<Renderer>().material;

        Color oldColor = mat.color;
        Color newColor = new Color(mat.color.r, mat.color.g, mat.color.b, 1/3);
        mat.SetColor("disabledColor", newColor);
        */
        btn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }
}
