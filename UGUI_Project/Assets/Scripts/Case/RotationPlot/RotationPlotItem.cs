using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class RotationPlotItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private float _aniTime=0.5f;//动画播放时间
    private int _posId;
    public int PosId { get => _posId; set => _posId = value; }
    private float _offset;//偏移量
    private Action<float> _moveAction;//移动事件

    private Image _img;
    private Image Img
    {
        get
        {
            if (_img == null)
                _img = GetComponent<Image>();
            return _img;
        }

    }

    private RectTransform _rect;
    private RectTransform Rect
    {
        get
        {
            if (_rect == null)
                _rect = GetComponent<RectTransform>();
            return _rect;
        }
    }


    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetSprite(Sprite sprite)
    {
        Img.sprite = sprite;
    }

    public void SetSize(Vector2 size)
    {
        Rect.sizeDelta = size;
    }

    public void SetPosData(ItemData data)
    {
        Rect.DOAnchorPos(Vector2.right * data.x,_aniTime);
        Rect.DOScale(data.ScaleTimes * Vector3.one, _aniTime);
        transform.SetSiblingIndex(data.order);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _offset += eventData.delta.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //执行移动移动事件
        if (_moveAction != null)
        {
            _moveAction(_offset);
            _offset=0;
        }
        else
        {
            Debug.Log("没有绑定移动事件");
        }
    }

    public void AddMoveListener(Action<float> onMove)
    {
        _moveAction=onMove;
    }

    /// <summary>
    /// 改变当前的id值
    /// </summary>
    public void ChangeId(int indexDelta,int count)
    {
        //预防最小值
        PosId=(PosId+indexDelta)<0?count-1:PosId+indexDelta;
        //预防最大值
        PosId=PosId%count;
    }
}
