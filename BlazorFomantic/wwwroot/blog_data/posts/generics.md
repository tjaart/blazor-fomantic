C# Generics
===
## What is it?
Generics allow you to create classes, methods and interfaces that defer the specification of one or more types until the class or method is declared and instantiated [1] Generics a programming language feature called paramterized typing.

## How does it work?

Generics are declared using type Parameters, denoted by `<>` angle brackets. A type parameter specifies which type will be taken in for that particular call.

### Methods

```csharp
public static TItem CreateItem<TItem>() {...}
```

### Classes

```csharp
public class Basket<TBasketItem> {...}
```

### Type Inference

```csharp
public void DoSomethingGeneric<T>(T something) {...}

var p = new Person();

DoSomethingGeneric<Person>(p);
// doesn't require the type parameter, because it is inferred from the paramenter

DoSomethingGeneric(p);
```
When type parameters are used in methods and there is a parameter of the type paramenter type, the type is inferred.



### How it's compiled
The dotnet commom intermediate language supports generics natively. They show up in CIL with a arity number e.g.

`List'1` -> this is how many type parameters there are in a class or method.

### Type Constraints

Type constraints ensure that your generic class or method always only takes in types that is supported by your method/class. It also makes all the interfaces and base classes in the constraints available to you when you write your implementation.

The `where` below can be read as "Where the Type TSortable Inherist from ISortable and INamed

```csharp
public void SortAnything<TSortable>(TSortable sortable) where TSortable : ISortable, INamed 
{
   sortable.Sort();
   Console.WriteLine($"Sorted {sortable.Name}");
}
```

```csharp
public interface ISortable {
   void Sort();
}

public interface INamed {
  string Name {get; set;}
}
```

## Reasons to use Generics
### Container Types
If you need to create a datastructure that holds items of any kind, generics allow you to ignore specific type semantics.
`List<T>` and `Dictionary<Tkey, TValue>` are good examples of this usage in the .Net framework.

### Wrappers
Often there are wrapper classes that just forward data with some metadata attached. Events are a good example:

```csharp
public class SystemEvent<TEventType> 
{
    string EventId { get; set; }
    TEventType ReceiverPayload { get; set; }
}
```
In this case an event can carry payload information without type information being lost or the event type having to be aware of it's payload types.

### Performance
If you are using `object`, all value types (int, decimal, struct etc) are boxed. These boxing allocations are expensive and can come at a serious runtime performance penalty.

### Constraining Type Input

```csharp
public static void ReverseTime<TClock>(TClock clock) where TClock : IReversible, IClock
{
    Console.WriteLine($"Reversing {clock.ClockType}");
    clock.Reverse();
}
```

```csharp
internal interface IClock
{
    public string ClockType { get; set; }
}

internal interface IReversible
{
    void Reverse();
}
```

This ensures that only a reversible clock can be passed into the function at compile time, without the need to create an `IReversibleClock` interface.

## Reasons NOT to use Generics
- The biggest reason not to use generics is if you are not sure you need them.
- Generics can add complexity you don't want. If you use generics you will find that you have to start using them in places where you may not have wanted them.
- Generics are complicated when working with reflection, because generic calls need their type parameters at compile time.


## Further Reading

[1] https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
[2] https://www.artima.com/intv/generics.html