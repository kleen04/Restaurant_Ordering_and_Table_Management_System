namespace Restaurant_Ordering_and_Management_System.Helper
{
    
    public static class ValidationHelper
    {
        public static bool IsRequired(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsPositiveInteger(string value, out int result)
        {
            return int.TryParse(value, out result) && result > 0;
        }

        public static bool IsNonNegativeDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, out result) && result >= 0;
        }
    }
}
