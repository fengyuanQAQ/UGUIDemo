using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IClickTest : MonoBehaviour,IPointerClickHandler
{
    private bool _isChanged;
    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        var mr=GetComponent<MeshRenderer>();
        _isChanged=!_isChanged;
        if(_isChanged)
            mr.material.color=Color.red;
        else 
            mr.material.color=Color.white;
    }
}
