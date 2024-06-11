namespace WebApplication1.Models

{
    public static class TemperatureUtility
    {
        public static bool HasFever(float temperature)
        {
            // Implement logic to determine if temperature indicates fever
            // For example:
            return temperature > 37.5; // Assuming fever threshold is 37.5 Celsius
        }
    }
}

