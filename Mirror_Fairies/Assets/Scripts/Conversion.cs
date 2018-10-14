public class Conversion
{
	public static float FloatConversion(double d)
	{
		return float.Parse(d.ToString());
	}
	public static double DoubleConversion(float f)
	{
		return double.Parse(f.ToString());
	}
}
