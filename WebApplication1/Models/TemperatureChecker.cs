namespace WebApplication1.Models
{
    public static class TemperatureChecker
    {
        public static string CheckTemperature(float temperature, string scale)
        {
            if (scale == "Fahrenheit")
            {
                temperature = (temperature - 32) * 5 / 9;
            }

            if (temperature >= 37)
            {
                return "You have a fever.";
            }
            else if (temperature <= 34)
            {
                return "You have hypothermia.";
            }
            else
            {
                return "Your temperature is normal.";
            }
        }
    }
}


