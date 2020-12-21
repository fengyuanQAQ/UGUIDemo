##### 1、UI遮挡3d物体问题

+ UI响应事件，UI遮挡的3d物体不会响应事件
  + 主摄像机添加Physic Raycaster
  + 3D物体可调用UI的事件响应接口
+ 3D物体想使用UI的事件响应接口，同时不会UI不会遮挡3d物体响应事件

       //所有Raycaster均响应
       List<RaycastResult> results=new List<RaycastResult>();
            //找到所有接收射线的物体
            EventSystem.current.RaycastAll(eventData,results);
        //遍历找到的物体，执行相应的事件
        foreach (var item in results)
        {
            //如果不是自己的事件才执行
            Debug.Log(item.gameObject.name);
            if(gameObject!=item.gameObject)
            {       ExecuteEvents.Execute(item.gameObject,eventData,ExecuteEvents.pointerClickHandler);
            }
        }
+ 过滤掉点击UI的情况（菜单打开，点击非菜单位置，人物仍然响应）

  ```c#
  //仅Graphic Raycaster下的子物体响应
  //找到Graphic Raycaster
  _graphic=GameObject.FindObjectOfType<GraphicRaycaster>();
  //发出射线
  _graphic.Raycast(eventData,results);
  //根据results的数量判断是否点击到UI
  ```

  

##### 2、自己实现圆形图片组件

+ Unity实现圆形图片

  + 实现方式
    + 父物体是圆形图片
    + 加一个Mask遮罩
  + 缺陷：增加drawcall,有锯齿感

+ 实现

  + 图片的渲染

    + 算出Position 与 Uv之间的转化因子
    + 将中心点添加进入顶点
    + 计算顶点坐标的位置，添加顶点
    + 添加三角形

    ```C#
    protected override void OnPopulateMesh(VertexHelper vh)
    DataUtility.GetOuterUV(overrideSprite);//获取转换因子
    vh.AddVert(origin);
    vh.AddTriangle(i+1,0,i+1+1);
    ```

    

  + 技能冷却效果实现

    + 将不显示的部分颜色置为灰色

  + 点击位置响应

    + 只针对圆形：判断点击位置到圆心的距离 与 半径的关系
    + 针对任意图形：从点击点击位置做一条线，判断交点的数量

    ```C#
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    //将屏幕坐标转换为世界坐标
    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,screenPoint,eventCamera,out localPoint);
    
    //判断该座标的x是否<延长线相交点的x坐标
    
    ```

##### 3、不规则图像的点击

+ 内置方法

  + 改变透明度:Image.alphaHitTestMinmumThreshold
  + 影响性能：要开Read/Write Enable
+ 实现
+ 添加PolygonCollider
  + Polygon.OverlapPoint

##### 4、2DImage仿制3D轮转



