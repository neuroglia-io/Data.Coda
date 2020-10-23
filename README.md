# Neuroglia.Data.Coda

.NET Standard 2.1 library for parsing Belgian CODA files

# Usage

[Nuget Package](https://www.nuget.org/packages/Neuroglia.Data.Coda/)

```
  dotnet add package Neuroglia.Data.Coda
```

## Sample Code

```C#
var parser = new CodaParser();
var doc = parser.Parse(File.ReadAllText("test.coda"));
foreach(var statement in doc.Statements)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine($"Statement {statement.Date}");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("--------------------------------------------");
    foreach(var transaction in statement.Transactions)
    {
        Console.WriteLine("{transaction.SequenceNumber} - {transaction.Type} {transaction.Amount}");
    }
    Console.WriteLine("--------------------------------------------");
}
```

# Contributing

Please see [CONTRIBUTING.md](https://github.com/neuroglia-io/Data.Coda/blob/master/CONTRIBUTING.md) for instructions on how to contribute.
