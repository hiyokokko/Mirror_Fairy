public class Conversion
{
	/// <summary>
	/// double変数を誤差なしでfloat変数へ変換。
	/// </summary>
	public static float FloatConversion(double d)
	{
		return float.Parse(d.ToString());
	}
	/// <summary>
	/// float変数を誤差なしでdouble変数へ変換。
	/// </summary>
	public static double DoubleConversion(float f)
	{
		return double.Parse(f.ToString());
	}
}
