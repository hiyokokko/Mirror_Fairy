/// <summary>
/// ポジションの確認。
/// </summary>
public class EndChecker
{
	/// <summary>
	/// 右側のポジションの確認。
	/// </summary>
	/// <param name="x">確認したいX座標</param>
	/// <returns>超えているなら true 超えてないなら false</returns>
	public static bool EndRight (float x)
	{
		if (x >= 16.0f)
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
		if (x <= -16.0f)
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
		if (y >= 9.0f)
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
		if (y <= -9.0f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
