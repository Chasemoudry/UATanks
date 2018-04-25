# UATanks
## Project Summary
Research project aimed towards testing concepts such as procedural generation, delegates/events, and data management via scriptable objects. Additionally, this project features modern Unity tools such as Cinemachine, ProBuilder, and NavMesh building components.

## Systems Summary
### Entity Events
Each entity which inherits IVehicle holds multiple events:  
1. PrimarySkill
    * When called, triggers the vehicle's primary skill
    * Example Usage: Fire a projectile
2. SecondarySkill
    * When called, triggers the vehicle's secondary skill 
    * Example Usage: Gain a temporary speed boost
3. Death
    * Is called when the vehicle's durability reaches zero
    * Example Usage: Increment score, drop a pickup
4. DurabilityChanged
    * Is called when the vehicle's durability changes
    * Example Usage: Update UI, play particle effect

_Note: All events are of delegate type System.Action_
```csharp
namespace System
{
    public delegate void Action();
}
```

#### __Editing Events__  
Example Handler:
```csharp
event System.Action BasicEvent;
```

Example Method:
```csharp
void LogEvent()
{
    Debug.Log("Event has occurred!");
}
```

1. Adding an event handler  
    * Via method reference
        * When adding by method reference, the method _must_ match the event's delegate type.
        ```csharp
        // Add handler
        BasicEvent += LogEvent;
        ```  
    * Via anonymous delegate
        * Adding handlers via anonymous delegate allows the creation of "inline" delegates meaning explicit method creation is not required.
        ```csharp
        // Add handler
        BasicEvent += delegate() { Debug.Log("Event has ccurred!"); };
        ```
    * Via lambda expression
        * Lambda expressions are a more convenient way to write an anonymous delegate.
        ```csharp
        // Add handler
        BasicEvent += () => Debug.Log("Event has occurred!");
		```

2. Removing an event handler  
    * Note: You can only remove handlers that were added via method reference, anonymous handlers cannot be explicitly removed.
    ```csharp
    // Remove handler
    BasicEvent -= LogEvent;
    ```

#### __Calling Events__  
Each event can be invoked by calling its respective "Raise" method.

```csharp
public event System.Action BasicEvent;

protected virtual void OnSampleEvent()
{
	System.Action handler = BasicEvent;

	if (handler != null)
	{
		handler();
	}
}
``` 