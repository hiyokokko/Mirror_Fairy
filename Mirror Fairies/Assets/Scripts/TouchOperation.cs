using UnityEngine;
/// <summary>
/// タッチ操作の管理
/// </summary>
public class TouchOperation
{
	//マウスで操作するか
	public static bool mouseUse = true;
	/// <summary>
	/// タッチ情報を取得(エディタと実機を考慮)
	/// </summary>
	/// <returns>タッチ情報。タッチされていない場合は TouchInfo.None</returns>
	public static TouchInfo GetTouch(int touchNum)
	{
		if (mouseUse)
		{
			if (Input.GetMouseButtonDown(0)) { return TouchInfo.Start; }
			if (Input.GetMouseButton(0)) { return TouchInfo.Now; }
			if (Input.GetMouseButtonUp(0)) { return TouchInfo.End; }
		}
		else
		{
			if (Input.touchCount > 0)
			{
				switch (Input.GetTouch(touchNum).phase)
				{
					case TouchPhase.Began: return TouchInfo.Start;
					case TouchPhase.Moved: return TouchInfo.Now;
					case TouchPhase.Stationary: return TouchInfo.Now;
					case TouchPhase.Ended: return TouchInfo.End;
					case TouchPhase.Canceled: return TouchInfo.End;
				}
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
			return Input.mousePosition;
		}
		else
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(touchNum);
				Vector2 touchPosition = new Vector2(touch.position.x, touch.position.y);
				return touchPosition;
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
	None = 0,
	//タッチ開始
	Start = 1,
	//タッチ中
	Now = 2,
	//タッチ終了
	End = 3,
}
