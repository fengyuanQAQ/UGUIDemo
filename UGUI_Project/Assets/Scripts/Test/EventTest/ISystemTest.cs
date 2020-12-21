using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ISystemTest : MonoBehaviour,IMoveHandler,ISubmitHandler,ICancelHandler,IScrollHandler
{
    public void OnCancel(BaseEventData eventData)
    {
        Debug.Log("那我取消了o");
    }

    public void OnMove(AxisEventData eventData)
    {
        Debug.Log("那我移动了哦");
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("那我开始滚了哦");
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("那我提交了哦");
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
