### 一、Canvas

### 1、RectTransform

##### 1)Anchor

+ 全部锚点在一起：此时改变屏幕的大小，UI组件中心点与锚点的相对位置不会改变，并且大小也不会改变
+ 锚点分开：除了相对位置不会改变之外，大小会改变对应边与对应锚点成比例。
+ 几个重要的点
  + 轴心点：代表的是图形的位置
  + Anchor Position:锚点的几何中心位置
  + Local Position：该物体的轴心点到父物体轴心点的距离
+ 改变轴点的朝向可以使得物体向某一方向延伸

##### 2)蓝图模式

使得物体的可响应区域不随物体的变化而变化

### 2、Canvas

UI的载体

##### 1)Camera

+ 三种模式

  + OverLay:UI显示在最上层
  + Camera:与具体的Camera相关联
  + WorldSpace:画布作为3D物体显示

+ Camera Scalar

  + 设置对应比例

    ```c#
    float wScale=rect.rect.width/1920f;
    transform.GetComponent<CameraScalar>().ScaleFactor=wScale;
    ```

### 3、Graphic Raycaster

管理Canvas下元素的射线响应

+ Ignore Reversed Graphics:忽略反转元素的射线响应
+ Blocking Object :阻挡射线的类型（判断的依据是碰撞体）
+ Blocking Mask:配合上面的阻挡类型使用

### 4、CanvasGroup

统一管理子物体下面的元素

+ Alpha:透明度
+ Interactable:是否可以交互
+ Blocks Raycasts:是否阻挡射线
+ Ignore Parent Groups:忽略父物体的Group

### 5、渲染层级

+ 相机深度
+ 层级
+ 自然层级，越往下越后渲染，先显示

### 6、2D项目显示3D模型

RawImage

+ 通过UV播放帧动画
+ 2d显示3d模型
  + 新建一个RT(Render Texture)
  + 让相机照射这个3d物体,将RT拖入相机Target Texture
  + 将RT拖入RawImage
  + 设置相机模式为Solid Color



### 二、组件

##### 1、Text

富文本

+ b:粗体的   <b>Bold</b>
+ i:斜体的  <i>Italic</i>
+ size:大小 <size=50>size</size>
+ color:颜色 <color=green>color</color>

##### 2、Mask

遮罩孩子的大小，限制在自己的大小范围内

+ Mask:比较耗性能，DrawCall增多
+ Rect Mask 2D:消耗性能少

##### 3、交互组件

+ Button:onClick
+ Toggle:onValueChanged
  + Toggle Group控制
+ Slider:  onValueChanged
  + 进度条
  + 音量控制、画质选择等
+ ScrollBar:onValueChanged
+ InputFiled:onValueChanged
+ ScrollView
+ DropDown
  + 模板中Item项添加图片 ->ItemImage
  + 标题图片需要放在Dropdown下面 ->Caption Image
  + Item里面还可以调整CheckMark
+ InputField
  + onValueChanged
  + onEndEdit

##### 4、自动排列组件

LayOutxxx

+ Hor/Ver Layout Group:Control Child Size 和 Child Force Expand要同时勾选，同时取消
  + LayoutElement:配置使用，Group下面的子物体
    + 尽量不要在使用LayoutElment的时候同时勾选Expand width.height
+ Grid Layout Group(推荐)
+ Aspect Ratio Fitter：纵横比率适配
+ Content Size Fitter:内容大小适配
  + 可以使用LayoutElement
  + 配合Text使用比较Nice



### 三、EventSystem

场景唯一，管理事件响应，Canvas Racaster	

##### 1、组成部分

+ EventSystem:事件的处理和分发
+ Standalone Input Module:处理输入数据

##### 2、事件添加

+ 接口实现

  + 拖动

    + IInitializePotentialDragHandler,IBeginDragHandler,IDragHandler(基本接口，其他都是基于他的),IEndDragHandler
    + IDropHandler
      + 执行在EndDrag之后
      + 如果上面有层级，最后落在另一个物体之上，则最后会在层级优先级高的物体上显示
    + 出现问题 如果点击的位置不在物体的中心点，最后仍然将鼠标的位置赋值给中心点，会照成偏移
      + 解决方法:通过减去这个偏移即可

  + 点击

    + IPointerEnterHandler,IPointerDownHandler,IPointerUpHandler,

      IPointerClickHandler,IPointerExitHandler

    + 祖传bug:子物体只有IPointerClickHandler,父物体有IPointerDownHandler,IPointerUpHandler,IPointerClickHandler，此时子物体点击没效果

      + 给子物体添加PointerDownHandler即可

  + 选中

    + ISelectHandler,IUpdateSelectedHandler,IDeselectHandler
    + 需要Selectable组件

  + PointerEventDeta

    + clickTime:项目运行后多少秒的时间点的上一次的点击时间
    + delta

+ 组件实现:EventTrigger

+ 代码添加组件





















