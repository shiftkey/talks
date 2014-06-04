<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

Action<long> fizzBuzz = x => {
 Console.Write(x + " - ");
 if (x % 3 == 0) Console.Write("fizz");
 if (x % 5 == 0) Console.Write("buzz");
 Console.WriteLine();
};

Observable.Interval(TimeSpan.FromSeconds(1))
  .Skip(1)
  .Take(10)
  .Subscribe(
     i => fizzBuzz(i),
     ex => {},
     () => { Console.WriteLine("done");});
