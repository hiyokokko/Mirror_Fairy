public class EndChecker
{
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
