/// <summary>
/// ポジションの確認。
/// </summary>
public class EndChecker
{
	public static float endRight = 16.0f;
	public static float endLeft = -16.0f;
	public static float endTop = 9.0f;
	public static float endBottom = -9.0f;
	/// <summary>
	/// 右側のポジションの確認。
	/// </summary>
	/// <param name="x">確認したいX座標</param>
	/// <returns>超えているなら true 超えてないなら false</returns>
	public static bool EndRight (float x)
	{
		if (x >= endRight)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// 左側のポジションの確認。
	/// </summary>
	/// <param name="x">確認したいX座標</param>
	/// <returns>超えているなら true 超えてないなら false</returns>
	public static bool EndLeft(float x)
	{
		if (x <= endLeft)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// 上側のポジションの確認。
	/// </summary>
	/// <param name="y">確認したいY座標</param>
	/// <returns>超えているなら true 超えてないなら false</returns>
	public static bool EndTop(float y)
	{
		if (y >= endTop)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// 下側のポジションの確認。
	/// </summary>
	/// <param name="y">確認したいY座標</param>
	/// <returns>超えているなら true 超えてないなら false</returns>
	public static bool EndBottom(float y)
	{
		if (y <= endBottom)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
