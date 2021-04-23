namespace BlazorFomantic
{
    public static class StringExtensions
    {
        public static string StringIf(this bool condition, string output)
        {
            return condition ? output : ""; 
        }

        public static string Spaciate(this string input)
        {
            return $" {input} ";
        }
    }
}