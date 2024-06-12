namespace WebApplication1.Models

{
    public static class TemperatureUtility
    {
        public static bool HasFever(float temperature)
        {
            
            return temperature > 37.5; 
        }

    }
}
