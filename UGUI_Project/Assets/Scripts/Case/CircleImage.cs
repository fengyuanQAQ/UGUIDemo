using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class CircleImage :Image
{
    [SerializeField]
    /// <summary>
    /// 三角形的数量
    /// </summary>
    private int segments=100;

    [SerializeField]
    private float showPercent=1;

    private List<Vector2> vertexList=new List<Vector2>();
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //清除原来的信息
        vh.Clear();

        int relSegments=(int)(segments*showPercent);

        /*算出position 与 UV的转化因子*/
        //获取当前宽高
        float width=rectTransform.rect.width;
        float height=rectTransform.rect.height;
        //获取当前的UV坐标
        Vector4 uv=(overrideSprite==null)?Vector4.zero:DataUtility.GetOuterUV(overrideSprite);
        Vector2 centerUV=new Vector2((uv.z-uv.x)/2,(uv.w-uv.y)/2);
        //算出转换因子
        Vector2 convertRatio=new Vector2((uv.z-uv.x)/width,(uv.w-uv.y)/height);

        /*准备参数*/
        float radian=2*Mathf.PI/segments;//Horizontal Center
        float radius=width/2;
        //中心点
        UIVertex origin=new UIVertex();
        byte tempValue=(byte)(255*showPercent);
        origin.color=new Color32(tempValue,tempValue,tempValue,255);
        origin.position=new Vector2(-(rectTransform.pivot.x-0.5f)*width,
        -(rectTransform.pivot.y-0.5f)*height);
        origin.uv0=new Vector2(centerUV.x,centerUV.y);
        //添加进顶点表
        vh.AddVert(origin);

        /*计算顶点的坐标位置*/
        //顶点的数量为segments+1
        float currentRadian=Mathf.PI/2;
        for (var i = 0; i <segments+1; i++)
        {
            //计算顶点
            float x=radius*Mathf.Cos(currentRadian);
            float y=radius*Mathf.Sin(currentRadian);
            currentRadian-=radian;
            UIVertex temp=new UIVertex();
            temp.position=new Vector2(origin.position.x+x,origin.position.y+y);
            temp.uv0=new Vector2(origin.uv0.x+x*convertRatio.x,origin.uv0.y+y*convertRatio.y);
            //判断颜色
            if(i<relSegments+1&&relSegments!=0)
                temp.color=color;
            else 
                temp.color=new Color32(66,66,66,255);

            vh.AddVert(temp);
            //将位置坐标添加进缓存
            vertexList.Add(temp.position);
        }

        /*绘制三角形*/
        for (int i = 0; i < segments ;i++)
        {   
            vh.AddTriangle(i+1,0,i+1+1);
        }
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        Vector2 localPoint;
        //将屏幕坐标转换为本地坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,screenPoint,eventCamera,out localPoint);
        return IsValid(localPoint,vertexList);
    }

    private bool IsValid(Vector2 localPoint,List<Vector2> vertexList)
    {
        int crossCount=0;
        //开始连线
        for (int i = 0; i <vertexList.Count ; i++)
        {
            var v1=vertexList[i];            
            var v2=vertexList[(i+1)%vertexList.Count];//这里注意首尾相连
            //首先判断该点的y坐标是否在这条边之间
            if(IsRange(localPoint,v1,v2))
            {
                //判断交点的数量
                crossCount+=GetCrossNum(localPoint,v1,v2);
            }           
        }
        return crossCount%2==1;
    }

    private bool IsRange(Vector2 localPoint,Vector2 v1,Vector2 v2)
    {
        if(v1.y<v2.y)
        {
            return localPoint.y>=v1.y&&localPoint.y<=v2.y;
        }
        else
        {
            return localPoint.y<=v1.y&&localPoint.y>=v2.y;
        }
    }

    private int GetCrossNum(Vector2 localPoint,Vector2 v1,Vector2 v2)
    {
        float k=(v2.y-v1.y)/(v2.x-v1.x);
        float x=(localPoint.y-v1.y)/k+v1.x;
        return x>localPoint.x?1:0;
    }
}
