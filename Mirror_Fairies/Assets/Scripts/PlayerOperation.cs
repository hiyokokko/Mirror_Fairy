using UnityEngine;
public class PlayerOperation
{
	public static Camera cam;
	static float touchBouder = 12.0f;
	public static void PlayerTouch(ref PlayerTouchState playerTouchState, Vector2 pos)
	{
		int touchMax = TouchOperation.windows ? 1 : Input.touchCount <= 2 ? Input.touchCount : 2;
		for (int touchNum = 0; touchNum < touchMax; touchNum++)
		{
			if (TouchOperation.windows)
			{
				if (Input.GetKeyDown(KeyCode.Space)) { playerTouchState.attack = 0; }
				else if (Input.GetKeyUp(KeyCode.Space)) { playerTouchState.attack = -1; }
				if (TouchOperation.GetTouch(0) == TouchInfo.Start)
				{
					playerTouchState.beforePos = pos;
					playerTouchState.beforeTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					playerTouchState.move = 0;
				}
				else if (TouchOperation.GetTouch(0) == TouchInfo.Now)
				{
					playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
				}
				else if (TouchOperation.GetTouch(0) == TouchInfo.End)
				{
					playerTouchState.move = -1;
				}
			}
			else
			{
				int fingerId = Input.GetTouch(touchNum).fingerId;
				if (TouchOperation.GetTouch(touchNum) == TouchInfo.Start)
				{
					if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x >= touchBouder && playerTouchState.attack == -1)
					{
						playerTouchState.attack = fingerId;
					}
					if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x < touchBouder && playerTouchState.move == -1)
					{
						playerTouchState.beforePos = pos;
						playerTouchState.beforeTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
						playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
						playerTouchState.move = fingerId;
					}
				}
				else if (playerTouchState.move == fingerId && TouchOperation.GetTouch(touchNum) == TouchInfo.Now)
				{
					playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
				}
				else if (TouchOperation.GetTouch(touchNum) == TouchInfo.End)
				{
					if (playerTouchState.attack == fingerId) { playerTouchState.attack = -1; }
					else if (playerTouchState.move == fingerId) { playerTouchState.move = -1; }
				}
			}
		}
	}
}