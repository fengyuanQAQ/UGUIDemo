using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 简单的模拟进度条的操作
/// 要平滑、要稳
/// </summary>
public class SliderCase : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ProgressBar());
    }

    private IEnumerator ProgressBar()
    {
        float target=0;
        Slider slider=GetComponent<Slider>();
        while(target<100)
        {
            target+=1f;
            yield return new WaitUntil(()=>
            {
                slider.value=Mathf.SmoothStep(slider.value,target,0.5f);
                return Mathf.Abs(slider.value-target)<=0.01f;
            });
        }
    }
}
