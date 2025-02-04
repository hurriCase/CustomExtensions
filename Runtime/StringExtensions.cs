using UnityEditor;

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

        public static void CreateFolder(this string path)
        {
            var folders = path.Split('/');
            var currentPath = folders[0];

            for (var i = 1; i < folders.Length; i++)
            {
                var parentPath = currentPath;
                currentPath = $"{currentPath}/{folders[i]}";

                if (AssetDatabase.IsValidFolder(currentPath) is false)
                    AssetDatabase.CreateFolder(parentPath, folders[i]);
            }

            AssetDatabase.Refresh();
        }
    }
}