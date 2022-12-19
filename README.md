# Fuzzy Sharper

## Overview

Fuzzy Sharper is a library that allows you to create your own Fuzzy Expert System.

## About the project

This project aims to make the life of a knowledge engineer easier by providing the tools necessary to develop an expert
system regardless of the knowledge domain that is currently being developed in. It doesn't aim to perform the tasks that
are normally associated with a knowledge engineer, such as the acquisition and representation of the knowledge, but
rather to provide the tools to implement such knowledge in code.

## Prerequisites

The library uses .NET SDK 7.

The following open source libraries are used in this project:

- [SmartEnum](https://github.com/ardalis/SmartEnum)
- [Math.Net Numerics](https://github.com/mathnet/mathnet-numerics)

## Folder structure

```
TODO
```

## Usage

### Linguistic Variables

A Linguistic Variable is a variable whose values are expressed in natural language, and it can be declared as follows:

```csharp
var water = LinguisticVariable.Create(“Water”)
    .AddTrapezoidalFunction(“Cold”, 0, 0, 20, 40)
    .AddTriangularFunction(“Warm”, 30, 50, 70)
    .AddTrapezoidalFunction(“Hot”, 50, 80, 100, 100)
```

The first line declares the linguistic variable with the name "Water", and the two following lines declare the set of
linguistic terms that belong to it.
These terms must be unique in name, and they are intrinsically associated with membership functions (in this case,
triangular and trapezoidal ones).

The provided numerical values as parameters have a certain meaning depending on the shape that the Membership Function
describes.
For example, in the case of the triangular function, the values 30, 50, and 70 describe the coordinates of the
triangle: (0, 30), (50, 1), (70, 0).
The middle value is always situated at height 1 because all Membership Functions
are normal, that is, there's at least one *x* value such that μ(*x*) = 1.

### Working Memory

A Working Memory represents all the knowledge about the domain as facts, whether they are provided by the user or
inferred by the system.

A Working Memory can be instantiated as follows:

```csharp
var workingMemory = WorkingMemory.Create()
```

This will create an instance of a Working Memory with no given facts. All facts are stored in
a ``IDictionary<string, double>`` collection, and they are uniquely identifiable by their name. There cannot be two
facts with the same name.

If the user desires to preload a Working Memory with data, two methods can be used:

#### Load from a CSV file

The following method loads all data via a CSV file:

```csharp
var workingMemory = WorkingMemory.InitializeFromFile(folderPath);
```

``folderPath`` is a ``string`` indicating the route of the file. A file preloaded with data would look as follows:

| <!-- --> | <!-- --> |
|----------|----------|
| Age      | 18       |
| Height   | 175      |
| Weight   | 70       |

#### Load from code

The following method is marked as meant to be hidden by an implementing class:

```csharp
static abstract IWorkingMemory Initialize();
```

A class can be declared that inherits from ``WorkingMemory`` by initializing all data at instantiation. For example,
let's declare the following class:

```csharp
public class WorkingMemoryImpl : WorkingMemory
```

Now, we must hide the base method by re-declaring the base method and preload all data inside it.

```csharp
public new static IWorkingMemory Initialize()
{
        var workingMemory = Create();
        workingMemory.AddFact("Age", 18);
        workingMemory.AddFact("Height", 175);
        workingMemory.AddFact("Weight", 70);
        return workingMemory;
}
```