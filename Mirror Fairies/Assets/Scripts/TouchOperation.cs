using UnityEngine;
/// <summary>
/// タッチ操作の管理
/// </summary>
public class TouchOperation
{
	//マウスで操作するか
	static bool mouseUse = true;
	/// <summary>
	/// タッチ情報を取得(エディタと実機を考慮)
	/// </summary>
	/// <returns>タッチ情報。タッチされていない場合は TouchInfo.None</returns>
	public static TouchInfo GetTouch(int touchNum)
	{
		if (mouseUse)
		{
			if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
			if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
			if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
		}
		else
		{
			if (Input.touchCount > 0)
			{
				return (TouchInfo)((int)Input.GetTouch(touchNum).phase);
			}
		}
		return TouchInfo.None;
	}
	/// <summary>
	/// タッチポジションを取得(エディタと実機を考慮)
	/// </summary>
	/// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
	public static Vector2 GetTouchPosition(int touchNum)
	{
		if (mouseUse)
		{
			TouchInfo touch = GetTouch(0);
			if (touch != TouchInfo.None) { return Input.mousePosition; }
		}
		else
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(touchNum);
				Vector2 TouchPosition = new Vector2(touch.position.x, touch.position.y);
				return TouchPosition;
			}
		}
		return Vector2.zero;
	}
	/// <summary>
	/// タッチワールドポジションを取得(エディタと実機を考慮)
	/// </summary>
	/// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>
	public static Vector2 GetTouchWorldPosition(Camera cam, int touchNum)
	{
		return cam.ScreenToWorldPoint(GetTouchPosition(touchNum));
	}
}
/// <summary>
/// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
/// </summary>
public enum TouchInfo
{
	//タッチなし
	None = 99,
	//タッチ開始
	Began = 0,
	//タッチ移動
	Moved = 1,
	//タッチ静止
	Stationary = 2,
	//タッチ終了
	Ended = 3,
	//タッチキャンセル
	Canceled = 4,
}
