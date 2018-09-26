/// <summary>
/// (https://qiita.com/tempura/items/4a5482ff6247ec8873df)を参考にしました。
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOperation : MonoBehaviour
{
	private static Vector3 TouchPosition = Vector3.zero;
	static bool IsEditor = false;

	/// <summary>
	/// タッチ情報を取得(エディタと実機を考慮)
	/// </summary>
	/// <returns>タッチ情報。タッチされていない場合は null</returns>
	public static TouchInfo GetTouch(int touchNum)
	{
		if (IsEditor)
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
	public static Vector3 GetTouchPosition(int touchNum)
	{
		if (IsEditor)
		{
			TouchInfo touch = GetTouch(0);
			if (touch != TouchInfo.None) { return Input.mousePosition; }
		}
		else
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(touchNum);
				TouchPosition.x = touch.position.x;
				TouchPosition.y = touch.position.y;
				return TouchPosition;
			}
		}
		return Vector3.zero;
	}

	/// <summary>
	/// タッチワールドポジションを取得(エディタと実機を考慮)
	/// </summary>
	/// <param name='camera'>カメラ</param>
	/// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>
	public static Vector3 GetTouchWorldPosition(Camera camera, int touchNum)
	{
		return camera.ScreenToWorldPoint(GetTouchPosition(touchNum));
	}
}

/// <summary>
/// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
/// </summary>
public enum TouchInfo
{
	/// <summary>
	/// タッチなし
	/// </summary>
	None = 99,

	// 以下は UnityEngine.TouchPhase の値に対応
	/// <summary>
	/// タッチ開始
	/// </summary>
	Began = 0,
	/// <summary>
	/// タッチ移動
	/// </summary>
	Moved = 1,
	/// <summary>
	/// タッチ静止
	/// </summary>
	Stationary = 2,
	/// <summary>
	/// タッチ終了
	/// </summary>
	Ended = 3,
	/// <summary>
	/// タッチキャンセル
	/// </summary>
	Canceled = 4,
}