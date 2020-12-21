using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 拖动的简单用例
/// </summary>
public class IDragTest : MonoBehaviour,IInitializePotentialDragHandler,IBeginDragHandler,
IDragHandler,IEndDragHandler
{
    private Vector3 _offset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 clickPos;
        var rect=GetComponent<RectTransform>();
        //获取偏移位置
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect,eventData.position,eventData.pressEventCamera,out clickPos);
        _offset=clickPos-rect.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //出现问题 如果点击的位置不在物体的中心点，最后仍然将鼠标的位置赋值给中心点，会照成偏移
        #region 
        var rect=GetComponent<RectTransform>();
        //将屏幕上的坐标转换为世界坐标
        // Debug.Log(eventData.position);
        Vector3 targetPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect,eventData.position,
        eventData.pressEventCamera,out targetPos);
        rect.position=targetPos-_offset; 
        #endregion  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
         Debug.Log("准备好了吗");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
