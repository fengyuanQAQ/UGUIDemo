using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IClickUI : MonoBehaviour, IPointerClickHandler
{
    private bool _isChanged;
    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeColor();
        // ExcuteAll(eventData);
    }

    private void ChangeColor()
    {
        var img = GetComponent<Image>();
        _isChanged = !_isChanged;
        if (_isChanged)
            img.color = Color.red;
        else
            img.color = Color.yellow;
    }

    /// <summary>
    /// 执行所有事件
    /// </summary>
    private void ExcuteAll(PointerEventData eventData)
    {
        List<RaycastResult> results=new List<RaycastResult>();
        //找到所有接收射线的物体
        EventSystem.current.RaycastAll(eventData,results);

        //遍历找到的物体，执行相应的事件
        foreach (var item in results)
        {
            //如果不是自己的事件才执行
            // Debug.Log(item.gameObject.name);
            if(gameObject!=item.gameObject)
            {
                ExecuteEvents.Execute(item.gameObject,eventData,ExecuteEvents.pointerClickHandler);
            }
        }
    }

}
