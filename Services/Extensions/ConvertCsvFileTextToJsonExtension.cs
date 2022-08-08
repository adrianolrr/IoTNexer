namespace Services.Extensions
{
    public static class ConvertCsvFileTextToJsonExtension<T> where T : class
    {
        public static Task<List<T>> ConvertCsvFileToJson(string text)
        {
            var list = new List<T>();
            if (!string.IsNullOrEmpty(text))
                foreach (var line in text.Contains("\\") ? text.Split("\\r\\n") : text.Split("\r\n"))
                {
                
                }
            return Task.FromResult(list);
        }
    }
}
