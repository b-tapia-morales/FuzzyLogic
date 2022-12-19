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

Now, we must hide the base method by redeclaring the base method and preload all data inside it.

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