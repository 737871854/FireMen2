using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputRoot : MonoBehaviour {
	public delegate void FingerDownMethod(Vector2 vec2, GameObject go);
	public delegate void FingerHoverMethod(Vector2 vec2, GameObject go, HoverEventPhase phase);
	public delegate void FingerMoveMethod(Vector2 vec2, GameObject go, MotionEventPhase phase);
	public delegate void FingerStationaryMethod(Vector2 vec2, GameObject go, MotionEventPhase phase);
	public delegate void FingerUpMethod(Vector2 vec2, GameObject go);
	/// deltaMove: Distance dragged since last frame
	public delegate void FirstFingerDragMethod(Vector2 vec2, GameObject go, Vector2 deltaMove, ContinuousGestureEventPhase phase);
	public delegate void LongPressMethod(Vector2 vec2, GameObject go);
	public delegate void TapMethod(Vector2 vec2, GameObject go);
	//move: Total swipe vector
	//velocity: Instant gesture velocity in pixels per second
	public delegate void SwipeMethod(Vector2 vec2, GameObject go, GameObject startGO, float velocity, Vector2 move);
	//delta: Gap difference since last frame, in pixels
	//gap: Current gap distance between the two pinching fingers, in pixels
	public delegate void PinchMethod(Vector2 vec2, GameObject go, float delta, float gap, ContinuousGestureEventPhase phase);
	public delegate void TwistMethod(Vector2 vec2, GameObject go, float deltaRotation, float totalRotation);

	FingerDownMethod fingerDownMethods = null;
	FingerHoverMethod fingerHoverMethods = null;
	FingerMoveMethod fingerMoveMethods = null;
	FingerStationaryMethod fingerStationaryMethods = null;
	FingerUpMethod fingerUpMethods = null;

	FirstFingerDragMethod firstFingerDragMethods = null;
	LongPressMethod longPressMethods = null;
	TapMethod tapMethods = null;
	TapMethod doubleTapMethods = null;
	SwipeMethod swipeMethods = null;
	PinchMethod pinchMethods = null;
	TwistMethod twistMethods = null;

	FingerDownDetector fingerDownDetector;
	FingerHoverDetector fingerHoverDetector;
	FingerMotionDetector fingerMotionDetector;
	FingerUpDetector fingerUpDetector;
	DragRecognizer dragRecognizer;
	LongPressRecognizer longPressRecognizer;
	TapRecognizer tapRecognizer;
	TapRecognizer doubleTapRecognizer;
	SwipeRecognizer swipeRecognizer;
	PinchRecognizer pinchRecognizer;
	TwistRecognizer twistRecognizer;
	
	int dragFingerIndex = -1;

	void Awake(){
		//下面使用delegate 的方法触发回调比 SendMessage 效率更高。把下面注释掉就是使用SendMessage触发。
		//使用delegate方法，回调方法名不能写成跟SendMessage目标方法名一样，否则点一下回调会触发两次
 		
		gameObject.AddComponent<ScreenRaycaster>();

		fingerDownDetector = gameObject.AddComponent<FingerDownDetector>();
		fingerDownDetector.enabled = false;
		fingerDownDetector.OnFingerDown += FingerDownEventHandler;

		fingerHoverDetector = gameObject.AddComponent<FingerHoverDetector>();
		fingerHoverDetector.enabled = false;
		fingerHoverDetector.OnFingerHover += FingerHoverEventHandler;

		fingerMotionDetector = gameObject.AddComponent<FingerMotionDetector>();
		fingerMotionDetector.enabled = false;
		fingerMotionDetector.OnFingerMove += FingerMoveEventHandler;
		fingerMotionDetector.OnFingerStationary += FingerStationaryEventHandler;

		fingerUpDetector = gameObject.AddComponent<FingerUpDetector>();
		fingerUpDetector.enabled = false;
		fingerUpDetector.OnFingerUp += FingerUpEventHandler;

		dragRecognizer = gameObject.AddComponent<DragRecognizer>();
		dragRecognizer.enabled = false;
		//只有在固定finger数的情况下才会触发。
		dragRecognizer.IsExclusive = true;
        dragRecognizer.MoveTolerance = 0;
		dragRecognizer.OnGesture += FirstFingerDragEventHandler;

		longPressRecognizer = gameObject.AddComponent<LongPressRecognizer>();
		longPressRecognizer.enabled = false;
		longPressRecognizer.OnGesture += LongPressEventHandler;

		tapRecognizer = gameObject.AddComponent<TapRecognizer>();
		tapRecognizer.enabled = false;
		tapRecognizer.IsExclusive = true;
		tapRecognizer.OnGesture += TapEventHandler;

		doubleTapRecognizer = gameObject.AddComponent<TapRecognizer>();
		doubleTapRecognizer.enabled = false;
		doubleTapRecognizer.RequiredTaps = 2;
		doubleTapRecognizer.OnGesture += DoubleTapEventHandler;

		swipeRecognizer = gameObject.AddComponent<SwipeRecognizer>();
		swipeRecognizer.enabled = false;
		swipeRecognizer.OnGesture += SwipeEventHandler;

		pinchRecognizer = gameObject.AddComponent<PinchRecognizer>();
		pinchRecognizer.enabled = false;
        pinchRecognizer.MinDistance = 0;
        pinchRecognizer.MinDOT = 0;
		pinchRecognizer.OnGesture += PinchEventHandler;
        
		twistRecognizer = gameObject.AddComponent<TwistRecognizer>();
		twistRecognizer.enabled = false;
		twistRecognizer.OnGesture += TwistEventHandler;

	}

	public void OnLevelWasLoaded(int level){
		ScreenRaycaster raycaster = gameObject.GetComponent<ScreenRaycaster>();
		raycaster.Cameras = new Camera[]{
			Camera.main,
		};
	}	

	//to do 添加的时候，检查有没有激活对应的组件，没有就激活。 
	//删除的时候，检查有没有需要取消激活，有就取消激活。
	public void AddFingerDownMethod(FingerDownMethod fingerDownMethod){
		CheckEnable(fingerDownDetector);
		fingerDownMethods += fingerDownMethod;
	}

	public void AddFingerHoverMethod(FingerHoverMethod fingerHoverMethod){
		CheckEnable(fingerHoverDetector);
		fingerHoverMethods += fingerHoverMethod;
	}

	public void AddFingerMoveMethod(FingerMoveMethod fingerMoveMethod){
		CheckEnable(fingerMotionDetector);
		fingerMoveMethods += fingerMoveMethod;
	}

	public void AddFingerStationaryMethod(FingerStationaryMethod fingerStationaryMethod){
		CheckEnable(fingerMotionDetector);
		fingerStationaryMethods += fingerStationaryMethod;
	}

	public void AddFingerUpMethod(FingerUpMethod fingerUpMethod){
		CheckEnable(fingerUpDetector);
		fingerUpMethods += fingerUpMethod;
	}
	
	public void AddFirstFingerDragMethod(FirstFingerDragMethod dragMethod){
		CheckEnable(dragRecognizer);
		firstFingerDragMethods += dragMethod;
	}

	public void AddLongPressMethod(LongPressMethod longPressMethod){
		CheckEnable(longPressRecognizer);
		longPressMethods += longPressMethod;
	}

	public void AddTapMethod(TapMethod tapMethod){
		CheckEnable(tapRecognizer);
		tapMethods += tapMethod;
	}

	public void AddDoubleTapMethod(TapMethod tapMethod){
		CheckEnable(doubleTapRecognizer);
		doubleTapMethods += tapMethod;
	}

	public void AddSwipeMethod(SwipeMethod swipeMethod){
		CheckEnable(swipeRecognizer);
		swipeMethods += swipeMethod;
	}

	public void AddPinchMethod(PinchMethod pinchMethod){
		CheckEnable(pinchRecognizer);
		pinchMethods += pinchMethod;
	}

	public void AddTwistMethod(TwistMethod twistMethod){
		CheckEnable(twistRecognizer);
		twistMethods += twistMethod;
	}


	public void DelFingerDownMethod(FingerDownMethod fingerMethod){
		fingerDownMethods -= fingerMethod;
		if(fingerDownMethods == null){
			fingerDownDetector.enabled = false;
		}
 	}

	public void DelFingerHoverMethod(FingerHoverMethod fingerMethod){
		fingerHoverMethods -= fingerMethod;
		if(fingerHoverMethods == null){
			fingerHoverDetector.enabled = false;
		}
	}

	void CheckMotionDetectorDisable(){
		if(fingerMoveMethods == null && fingerStationaryMethods == null){
			fingerMotionDetector.enabled = false;
		}
	}

	public void DelFingerMoveMethod(FingerMoveMethod fingerMethod){
		fingerMoveMethods -= fingerMethod;
		CheckMotionDetectorDisable();
	}

	public void DelFingerStationaryMethod(FingerStationaryMethod fingerMethod){
		fingerStationaryMethods -= fingerMethod;
		CheckMotionDetectorDisable();
	}

	public void DelFingerUpMethod(FingerUpMethod fingerMethod){
		fingerUpMethods -= fingerMethod;
		if(fingerUpMethods == null){
			fingerUpDetector.enabled = false;
		}
	}

	public void DelFirstFingerDragMethod(FirstFingerDragMethod dragMethod){
		firstFingerDragMethods -= dragMethod;
		if(firstFingerDragMethods == null){
			dragRecognizer.enabled = false;
		}
	}

	public void DelLongPressMethod(LongPressMethod longPressMethod){
		longPressMethods -= longPressMethod;
		if(longPressMethods == null){
			longPressRecognizer.enabled = false;
		}
	}

	public void DelTapMethod(TapMethod tapMethod){
		tapMethods -= tapMethod;
		if(tapMethods == null){
			tapRecognizer.enabled = false;
		}
	}

	public void DelDoubleTapMethod(TapMethod tapMethod){
		doubleTapMethods -= tapMethod;
		if(doubleTapMethods == null){
			doubleTapRecognizer.enabled = false;
		}
	}

	public void DelSwipeMethod(SwipeMethod swipeMethod){
		swipeMethods -= swipeMethod;
		if(swipeMethods == null){
			swipeRecognizer.enabled = false;
		}
	}

	public void DelPinchMethod(PinchMethod pinchMethod){
		pinchMethods -= pinchMethod;
		if(pinchMethods == null){
			pinchRecognizer.enabled = false;
		}
	}

	public void DelTwistMethod(TwistMethod twistMethod){
		twistMethods -= twistMethod;
		if(twistMethods == null){
			twistRecognizer.enabled = false;
		}
	}


	//如果这个回调名字写成OnFingerDown，点一下就会触发两次，一次是delegate触发，一次是SendMessage触发
	void FingerDownEventHandler(FingerDownEvent e) {
		if(fingerDownMethods != null){
			fingerDownMethods(e.Position, e.Selection);
		}
	}

	void FingerHoverEventHandler(FingerHoverEvent e) {
//		Debug.Log("hover event handler()");
		if(fingerHoverMethods != null){
			fingerHoverMethods(e.Position, e.Selection, HoverPhaseConvertor(e.Phase));
		}
	}

	void FingerMoveEventHandler(FingerMotionEvent e){
		if(fingerMoveMethods != null){
			fingerMoveMethods(e.Position, e.Selection, MotionPhaseConvertor(e.Phase));
		}
	}

	void FingerStationaryEventHandler(FingerMotionEvent e) {
		if(fingerStationaryMethods != null){
			fingerStationaryMethods(e.Position, e.Selection, MotionPhaseConvertor(e.Phase));
		}
	}
	
	void FingerUpEventHandler(FingerUpEvent e) {
		if(fingerUpMethods != null){
			fingerUpMethods(e.Position, e.Selection);
		}
	}

	//只对第一只手指的拖动动作有效，这里不暴露finger给用户是因为不想封装比较复杂的finger类，
	//以后要用两三只finger的操作 就再另外封装方法。
	void FirstFingerDragEventHandler(DragGesture gesture) {
		//没有用触屏机器测试，但估计Fingers[0]并不是代表第一只点击屏幕的的手指，
		//finger.Index才是，记录第几只开始触发的手指。两三只手指同时触发时
		//fingers[0].index是不同的。
		FingerGestures.Finger finger = gesture.Fingers[0];
		if(gesture.Phase == ContinuousGesturePhase.Started){
			dragFingerIndex = finger.Index;
		}

		if(dragFingerIndex != finger.Index){
			return;
		}

		//下面的内容保证是第一只手指触发的。


		if(firstFingerDragMethods != null){
			firstFingerDragMethods(gesture.Position, gesture.Selection, gesture.DeltaMove, ContinuousGesturePhaseConvertor(gesture.Phase));
		}

		//第一只手指收起，重置第一只手指index。
		if(gesture.Phase == ContinuousGesturePhase.Ended
		   || gesture.Phase == ContinuousGesturePhase.None){
			dragFingerIndex = -1;
		}
	}

	void LongPressEventHandler(LongPressGesture gesture) {
		if(longPressMethods != null){
			longPressMethods(gesture.Position, gesture.Selection);
		}
	}

	void TapEventHandler(TapGesture gesture) {
		if(tapMethods != null){
			tapMethods(gesture.Position, gesture.Selection);
		}
	}

	void DoubleTapEventHandler(TapGesture gesture){
		if(doubleTapMethods != null){
			doubleTapMethods(gesture.Position, gesture.Selection);
		}
	}

	void SwipeEventHandler(SwipeGesture gesture) {
		if(swipeMethods != null){
			swipeMethods(gesture.Position, gesture.Selection, gesture.StartSelection, gesture.Velocity, gesture.Move);
		}
	}

	void PinchEventHandler(PinchGesture gesture) {
		if(pinchMethods != null){
			pinchMethods(gesture.Position, gesture.Selection, gesture.Delta, gesture.Gap, ContinuousGesturePhaseConvertor(gesture.Phase));
		}
	}

	void TwistEventHandler(TwistGesture gesture) {
		if(twistMethods != null){
			twistMethods(gesture.Position, gesture.Selection, gesture.DeltaRotation, gesture.TotalRotation);
		}
	}

	void CheckEnable(MonoBehaviour com){
		if(com.enabled == false){
			com.enabled = true;
		}
	}

	
	HoverEventPhase HoverPhaseConvertor(FingerHoverPhase phase){
		if(phase == FingerHoverPhase.None){
			return HoverEventPhase.None;
		}else if(phase == FingerHoverPhase.Enter){
			return HoverEventPhase.Enter;
		}else{
			return HoverEventPhase.Exit;
		}
 
	}

	MotionEventPhase MotionPhaseConvertor(FingerMotionPhase phase){
		if(phase == FingerMotionPhase.None){
			return MotionEventPhase.None;
		}else if(phase == FingerMotionPhase.Started){
			return MotionEventPhase.Started;
		}else if(phase == FingerMotionPhase.Updated){
			return MotionEventPhase.Updated;
		}else{
			return MotionEventPhase.Ended;
		}
	}

	ContinuousGestureEventPhase ContinuousGesturePhaseConvertor(ContinuousGesturePhase phase){
		if(phase == ContinuousGesturePhase.None){
			return ContinuousGestureEventPhase.None;
		}else if(phase == ContinuousGesturePhase.Started){
			return ContinuousGestureEventPhase.Started;
		}else if(phase == ContinuousGesturePhase.Updated){
			return ContinuousGestureEventPhase.Updated;
		}else{
			return ContinuousGestureEventPhase.Ended;
		}
	}

	public void Release(){
		//这里不取消注册应该也行，这个go销毁的时候，注册的handler方法也会跟着销毁。
		fingerDownDetector.OnFingerDown -= FingerDownEventHandler;
		fingerHoverDetector.OnFingerHover -= FingerHoverEventHandler;
		fingerMotionDetector.OnFingerMove -= FingerMoveEventHandler;
		fingerMotionDetector.OnFingerStationary -= FingerStationaryEventHandler;
		fingerUpDetector.OnFingerUp -= FingerUpEventHandler;
		dragRecognizer.OnGesture -= FirstFingerDragEventHandler;
		longPressRecognizer.OnGesture -= LongPressEventHandler;
		tapRecognizer.OnGesture -= TapEventHandler;
		doubleTapRecognizer.OnGesture -= DoubleTapEventHandler;
		swipeRecognizer.OnGesture -= SwipeEventHandler;
		pinchRecognizer.OnGesture -= PinchEventHandler;
		twistRecognizer.OnGesture -= TwistEventHandler;
	}
}


public enum HoverEventPhase
{
	None = 0,
	Enter,
	Exit,
}
public enum MotionEventPhase
{
	None = 0,
	Started,
	Updated,
	Ended,
}
public enum ContinuousGestureEventPhase
{
	None = 0,
	Started,
	Updated,
	Ended,
}
 