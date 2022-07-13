using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState2 : MonoBehaviour
{
    private GameObject _currentGameObject;
    Renderer _m_ObjectRenderer;

    [SerializeField]
    public float alpha = 0.5f;

    void Start()
    {
        _currentGameObject = gameObject;
        if(_currentGameObject.GetComponent<Renderer>() == null){
            Debug.Log(_currentGameObject.name + ": Нет Renderer");
        }

        if(_currentGameObject.GetComponent<Image>() != null){
            Debug.Log(_currentGameObject.name + ": Image найден");

            Material mat = _currentGameObject.GetComponent<Image>().GetComponent<Renderer>().material;
            if(mat == null){
                Debug.Log("mat is null");
            }
            Color oldColor = mat.color;
            //Color newColor = new Color(mat.color.r, mat.color.g, mat.color.b, (isActive ? 1 : this.alpha));
            Color newColor = new Color(0, 0, 0, this.alpha);
            mat.SetColor("disabledColor", newColor);
        }


        //print(_currentGameObject.name + ": Materials " + _currentGameObject.GetComponent<Renderer>().material);
        //setEnabled(false);
    }

    void Update(){
        //setEnabled(false);
    }

    public void setEnabled(GameObject gameObject, bool isActive){
        Material mat = gameObject.GetComponent<Renderer>().material;
        if(mat == null){
            Debug.Log("mat is null");
        }
        Color oldColor = mat.color;
        //Color newColor = new Color(mat.color.r, mat.color.g, mat.color.b, (isActive ? 1 : this.alpha));
        Color newColor = new Color(0, 0, 0, (isActive ? 1 : this.alpha));
        mat.SetColor("disabledColor", newColor);
    }

}
