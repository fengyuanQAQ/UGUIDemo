using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ISelectTest : MonoBehaviour,ISelectHandler,IDeselectHandler,IUpdateSelectedHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnDeselect:"+gameObject.name);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnSelect:"+gameObject.name);
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        Debug.Log("OnUpdateSelected:"+gameObject.name);
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
