namespace CoreLayer.Utilities;

public static class CheckNumber
{
    public static bool CheckIsNumber(this string value)
    {
        return Int64.TryParse(value,out var a );
    }
}