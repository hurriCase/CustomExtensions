# Unity Custom Extensions Package

Utility extensions for Unity to handle Tasks, strings, and JSON serialization.

## Quick Start

1. **Convert Tasks to Coroutines**
```csharp
// Wait for async task in coroutine
async Task LoadDataAsync() => await Task.Delay(1000);

IEnumerator LoadDataCoroutine()
{
    yield return LoadDataAsync().WaitForTask();
    Debug.Log("Data loaded!");
}
```

2. **Get Backing Fields**
```csharp
string backingField = "MyProperty".ConvertToBackingField(); 
// Returns "<MyProperty>k__BackingField"
```

3. **Resolve JSON Properties**
```csharp
internal enum ResponseType
{
    [JsonProperty("text/plain")] 
    TextPlain
}

var mime = ResponseType.TextPlain.GetJsonPropertyName(); 
// Returns "text/plain"
```

## Core Features

- Task to Coroutine conversion
- String manipulation utilities
- JSON property resolution
- Exception propagation
- Editor property access

## Extensions

### ConvertExtension
Bridges Unity's Coroutine system with async/await:

```csharp
public static class ConvertExtension 
{
    public static IEnumerator WaitForTask(this Task task);
}

// Usage
IEnumerator MyCoroutine()
{
    yield return SomeAsyncMethod().WaitForTask();
    // Continues after task completes
}
```

Features:
- Seamless Task integration
- Proper exception handling
- Per-frame yielding
- Task completion monitoring

### StringExtensions
Converts property names to backing field format:

```csharp
public static class StringExtensions 
{
    public static string ConvertToBackingField(this string propertyName);
}
```

### JsonExtension
Gets JSON property names from enum values:

```csharp
public static class JsonExtension 
{
    public static string GetJsonPropertyName(this Enum enumValue);
}
```

## Best Practices

1. Task Conversion:
   - Use try-catch for exception handling
   - Consider cancellation tokens
   - Monitor task completion status

2. String Extensions:
   - Validate property names
   - Handle null cases
   - Use in editor scripts

3. JSON Properties:
   - Always define JsonProperty attributes
   - Use consistent naming conventions
   - Consider serialization context

## Common Issues & Solutions

1. **Task Exceptions**
   - Always wrap WaitForTask in try-catch
   - Handle AggregateException properly
   - Log exceptions appropriately

2. **Property Access**
   - Verify property exists
   - Check for null values
   - Use correct casing

3. **JSON Resolution**
   - Missing attributes default to enum names
   - Handle null enum values
   - Consider case sensitivity

## Technical Details

### Task Conversion Process
1. Monitor task completion
2. Yield each frame
3. Propagate exceptions
4. Resume execution

### Property Field Format
- Uses C# backing field convention
- Handles serialization properly
- Compatible with Unity inspector

## Usage Examples

### Async Loading With Coroutines
```csharp
public class Loader : MonoBehaviour
{
    IEnumerator LoadAssetCoroutine()
    {
        try 
        {
            yield return LoadAssetAsync().WaitForTask();
            Debug.Log("Load complete!");
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}");
        }
    }

    async Task LoadAssetAsync()
    {
        await Task.Delay(1000); // Simulated load
    }
}
```

### Editor Property Access
```csharp
public class CustomEditor : Editor 
{
    void OnInspectorGUI()
    {
        var prop = serializedObject.FindProperty(
            "myProperty".ConvertToBackingField()
        );
    }
}
```

### Serialization
```csharp
internal enum FileType
{
    [JsonProperty("txt")] Text,
    [JsonProperty("img")] Image
}

var type = FileType.Text;
var jsonName = type.GetJsonPropertyName(); // "txt"
```