using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDownClick : MonoBehaviour
{
    private GraphicRaycaster _graphic;
    private bool _isChanged;
    // Start is called before the first frame update
    void Start()
    {
        _graphic=GameObject.FindObjectOfType<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&!IsUi())
        {
            ChangeColor();
        }
    }

    private bool IsUi()
    {
        #region 
        // PointerEventData eventData=new PointerEventData(EventSystem.current);
        // eventData.position=Input.mousePosition;
        // eventData.pressPosition=Input.mousePosition;
        // List<RaycastResult> results=new List<RaycastResult>();
        // _graphic.Raycast(eventData,results);
        
        // //打印信息
        // foreach (var result in results)
        // {
        //     Debug.Log(result.gameObject.name);
        // }
        // return results.Count>0;
        #endregion
        
        //所有的RayCaster均响应
        PointerEventData eventData=new PointerEventData(EventSystem.current);
        eventData.pressPosition=Input.mousePosition;
        eventData.position=Input.mousePosition;
        List<RaycastResult> results=new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,results);
        
        return results.Count>0;
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
