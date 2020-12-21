using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyChangeColor : MonoBehaviour
{
    private bool isColorChanged;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeColor1()
    {
        isColorChanged=!isColorChanged;
        if(isColorChanged)
            transform.GetComponent<Image>().color=Color.red;
        else 
            transform.GetComponent<Image>().color=Color.white;
    }
}
