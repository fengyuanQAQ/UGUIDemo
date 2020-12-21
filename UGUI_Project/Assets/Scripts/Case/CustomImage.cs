using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomImage : Image
{
    private PolygonCollider2D _polygon;

    public PolygonCollider2D Polygon
    {
        get
        {
            if(_polygon==null)
                _polygon=GetComponent<PolygonCollider2D>();
            return _polygon;
        }
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        //获取屏幕点的世界坐标
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,screenPoint,eventCamera,out worldPoint);
        return Polygon.OverlapPoint(worldPoint);
    }
}
