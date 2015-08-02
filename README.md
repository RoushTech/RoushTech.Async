#RoushTech.Async for .NET

A library for adding more powerful asynchronous calls for .NET, primarily developed around lessons learned developing async code on the node.js platform and finding some parts of the .NET async code lacking or cumbersome.

## Chaining async methods

Instead of using "ContinueWith", use "Then". Then simplifies the behavior of chaining methods, if one link in the chain faults, the rest will fault and be skipped until a Catch or Finally.

```C#
Task.Factory
  .StartNew(() => { /* Do some work here */ })
  .Then((t) => { /* Do some more work here */ })
  .Then((t) => { /* Do some more work here */ })
  .Then((t) => { /* Do some more work here */ });
```

## Catching async methods

Chained methods can be caught using the Catch() method on Task, caught chains will:

* Run the Action<Exception> passed into the method if and only if the previous task is faulted or canceled.
* Attempt to pass the result from the previous task onto the next task, be prepared for this value to not be populated.
* Start a new task chain to clear all faults.

```C#
Task.Factory
  .StartNew(() => { /* Do some work here */ })
  .Then((t) => { /* Do some more work here */ })
  .Catch((ex) => { /* Handle possible exception here */ });
```

## Using finally on async methods

Chained methods can also have a finally method applied to them, finally methods will:

* Run the Action passed into the method regardless of the task chain's status.
* Attempt to pass the result from the previous task onto the next task, be prepared for this value to not be populated.
* Start a new task chain to clear all faults.

```C#
Task.Factory
  .StartNew(() => { /* Do some work here */ })
  .Then((t) => { /* Do some more work here */ })
  .Catch((ex) => { /* Handle possible exception here */ })
  .Finally(() => { /* Runs regardless of catch being called or not */ });
```

Can be useful for a fault when catch is not used:

```C#
/* Declare connection */
Task.Factory
  .StartNew(() => { /* Open connection */ })
  .Then((t) => { /* Fault here */ })
  .Finally((t) => { /* Close connection */ });
```

#Contributing

Please make pull requests onto a currently in-development branch, do not make pull requests to master they will be rejected.

Tests are highly appreciated and code may be rejected if substantial changes are made and current tests do not cover the functionality added. 

#License

Please see LICENSE file for details.

#Contributors

* William Roush
* Rizal Almashoor (code base started from Rizal's gist
 located [here](https://gist.github.com/rizal-almashoor/2818038#file_license.txt)).