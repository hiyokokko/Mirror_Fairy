public class EndChecker
{
	public static float endRight = 16.0f;
	public static float endLeft = -16.0f;
	public static float endTop = 9.0f;
	public static float endBottom = -9.0f;
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
