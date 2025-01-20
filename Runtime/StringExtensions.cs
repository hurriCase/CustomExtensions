namespace CustomExtensions.Runtime
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a property name to its backing field representation
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>The backing field name format</returns>
        public static string ConvertToBackingField(this string propertyName)
            => $"<{propertyName}>k__BackingField";
    }
}