using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class RotationPlot2D : MonoBehaviour
{
    const int InvalideRatio=-10000;

    [SerializeField]
    /// <summary>
    /// 图片精灵的个数
    /// </summary>
    private List<Sprite> Sprites;

    /// <summary>
    /// 图片的大小
    /// </summary>
    [SerializeField]
    private Vector2 ItemSize=new Vector2(400,400);

    /// <summary>
    /// 图片的偏移
    /// </summary>
    [SerializeField]
    private float Offset=10;

    /// <summary>
    /// 保存每一个物体的信息
    /// </summary>
    /// <typeparam name="ItemData"></typeparam>
    /// <returns></returns>
    private List<ItemData> _datas=new List<ItemData>();

    /// <summary>
    /// 保存脚本对象
    /// </summary>
    private List<RotationPlotItem> _items=new List<RotationPlotItem>();

    /// <summary>
    /// 保存对象的位置id值
    /// </summary>
    private List<int> _posIdDatas=new List<int>();

    [SerializeField]
    private float MaxScale=1,MinScale=0.5f;

    void Start()
    {
        CreatItem();
        CalculateItemData();
        SetItemData();
    }

    /// <summary>
    /// 生成图片
    /// </summary>
    private void CreatItem()
    {   
        //创建一个模板物体
        var temp=new GameObject("template");
        foreach (var sprite in Sprites)
        {
            var go=Instantiate(temp);
            go.AddComponent<Image>();
            var rotationItem = go.AddComponent<RotationPlotItem>();
            rotationItem.SetParent(transform);
            rotationItem.SetSprite(sprite);
            rotationItem.SetSize(ItemSize);
            rotationItem.AddMoveListener(Change);
            _items.Add(rotationItem);
        }
        //删除这个模板
        Destroy(temp);
    }

    private void Change(float offset)
    {
        Debug.Log(offset);
        int indexDelta=offset>0?1:-1;
        //改变id值
        foreach (var item in _items)
        {
            item.ChangeId(indexDelta,_items.Count);
        }

        //设置位置信息
        for (var i = 0; i < _items.Count; i++)
        {
            _items[i].SetPosData(_datas[_items[i].PosId]);
        }
    }

    /// <summary>
    /// 计算图片的数据
    /// </summary>
    private void CalculateItemData()
    {
        float curRatio=0;//当前的比率
        float ratio=1/(float)Sprites.Count;
        
        float length=(ItemSize.x+Offset)*Sprites.Count;//x方向的长度
        // Debug.Log(length);
        
        for (var i = 0; i < Sprites.Count; i++)
        {
            ItemData dataTemp=new ItemData();
            dataTemp.x=GetItemX(curRatio,length);
            dataTemp.ScaleTimes=GetItemScaleTimes(curRatio,length);
            // Debug.Log("tims:"+dataTemp.ScaleTimes);
            _datas.Add(dataTemp);

            //保留一个id值
            _items[i].PosId=i;
            _posIdDatas.Add(i);

            curRatio+=ratio;
        }

        //按照从小到大排列id,然后将这个值赋值给层级
        _posIdDatas = _posIdDatas.OrderBy((id)=>_datas[id].ScaleTimes).ToList();
        for (var i = 0; i < _datas.Count; i++)
        {
            _datas[_posIdDatas[i]].order=i;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    private float GetItemScaleTimes(float ratio, float length)
    {
        if(ratio<0||ratio>1){
            Debug.LogError("你这比率不合理");
            return InvalideRatio;
        }
        
        float dif=(MaxScale-MinScale)/0.5f;

        if(ratio>=0&&ratio<0.5f)
            return MaxScale-ratio*dif;
        else
            return MinScale+(ratio-0.5f)*dif;
    }
    /// <summary>
    /// 获取x方向上的偏移
    /// </summary>
    private float GetItemX(float ratio,float length)
    {
        if(ratio<0||ratio>1){
            Debug.LogError("你这比率不合理");
            return InvalideRatio;
        }

        if(ratio>=0&ratio<0.25)
            return ratio*length;
        else if(ratio>=0.25&ratio<0.75)
            return (0.5f-ratio)*length;
        else
            return (ratio-1)*length;
    }

    /// <summary>
    /// 设置图片的信息
    /// </summary>
    private void SetItemData()
    {
       for (var i = 0; i < _items.Count; i++)
       {
           _items[i].SetPosData(_datas[i]);
       }
    }
}

    /// <summary>
    /// 保存物体的位置信息，和放大系数
    /// </summary>
    public class ItemData
    {
        public float x;
        public float ScaleTimes;
        /// <summary>
        /// 层级的顺序
        /// </summary>
        public int order;
    }
