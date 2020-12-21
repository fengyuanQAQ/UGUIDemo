using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通过设置UV Rect的坐标 实现帧动画
/// </summary>
public class RawImageUV : MonoBehaviour
{
    private RawImage _rawImage;

    private float _offsetX,_offsetY;
    // Start is called before the first frame update
    void Start()
    {
        _rawImage=GetComponent<RawImage>();
        _offsetX=1/3f;
        _offsetY=1/4f;
        StartCoroutine(PlayAni());
    }

    private IEnumerator PlayAni()
    {
        float x=0f;
        float y=0f;

        while(true)
        {
            _rawImage.uvRect=new Rect(x,y,_rawImage.uvRect.width,_rawImage.uvRect.height);
            
            if(x<2/3.0)
            {
                x+=_offsetX;
            }else{
                x=0;
                y=(y+_offsetY)>=1?0:y+_offsetY;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }



}
