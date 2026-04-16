public static class Remapper
{
    
    public static int Remap(this int value, int fromMin, int fromMax, int toMin, int toMax)
    {
        float ratio = (value - fromMin) / (fromMax - fromMin);
        return (int)(toMin + ratio * (toMax - toMin));
    }
}