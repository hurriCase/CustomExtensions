# Unity Custom Extensions Package

Utility extensions for common Unity operations and JSON handling.

## Extensions

### ConvertExtension
Bridges the gap between Tasks and Unity's Coroutine system:

```csharp
public class DataManager : MonoBehaviour
{
    private async void LoadData()
    {
        // Regular async/await usage
        await LoadDataAsync();
    }

    private IEnumerator LoadDataCoroutine()
    {
        // Convert Task to Coroutine
        var task = LoadDataAsync();
        yield return task.WaitForTask();
        
        // Continue after task completes
    }

    private Task LoadDataAsync()
    {
        return Task.Delay(1000); // Simulated load
    }
}
```

Features:
- Wraps Tasks in Coroutines
- Propagates exceptions properly
- Yields per frame for responsiveness
- Works with any Task type

### JsonExtension
Gets JSON property names from enum values using attributes:

```csharp
internal enum ResponseType
{
    [JsonProperty("text/plain")]
    TextPlain,
    
    [JsonProperty("application/json")]
    Json
}

// Usage
var mime = ResponseType.TextPlain.GetJsonPropertyName(); // Returns "text/plain"
```

Features:
- Reads JsonPropertyAttribute values
- Falls back to enum name if no attribute
- Handles null cases safely

## Usage Notes

### Task to Coroutine Conversion
Use when you need to:
- Mix async/await with Coroutines
- Work with legacy Coroutine code
- Control execution in MonoBehaviours

```csharp
// Example with error handling
IEnumerator LoadWithRetry()
{
    try
    {
        yield return LoadDataAsync().WaitForTask();
        Debug.Log("Load complete");
    }
    catch (Exception e)
    {
        Debug.LogError($"Load failed: {e.Message}");
    }
}
```

### JSON Property Name Resolution
Use when working with:
- API responses
- JSON serialization
- Enum-based configuration

```csharp
internal enum FileType
{
    [JsonProperty("txt")]
    Text,
    
    [JsonProperty("img")]
    Image
}

var type = FileType.Text;
var jsonName = type.GetJsonPropertyName(); // Gets "txt"
```

## Best Practices

1. Task Conversion:
   - Always handle exceptions
   - Use for long-running operations
   - Consider task cancellation if needed

2. JSON Property Names:
   - Always provide JsonProperty attributes
   - Use consistent naming conventions
   - Consider case sensitivity in APIs

## Common Issues

- Unhandled task exceptions will crash Unity
- Missing JsonProperty attributes fall back to enum names
- Null enum fields return null for property names