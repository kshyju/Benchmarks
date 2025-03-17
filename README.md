Compares different approaches to read a JSON file and populate a POCO.

Tests JSON.NET and STJ.


### Result from a local run

```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3476)
Unknown processor
.NET SDK 9.0.201
  [Host]     : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                                                 | Mean      | Ratio | Gen0   | Allocated | Alloc Ratio |
|------------------------------------------------------- |----------:|------:|-------:|----------:|------------:|
| UsingNewtonsoftJsonNet                                 | 101.39 μs |  1.00 | 0.7324 |  45.81 KB |        1.00 |
| BuildUsingSystemTextJsonDocumentAndReadText            |  82.01 μs |  0.81 | 0.2441 |     21 KB |        0.46 |
| BuildUsingSystemTextJsonDocument                       |  72.38 μs |  0.72 |      - |   5.36 KB |        0.12 |
| BuildUsingSystemTextJsonUtf8JsonReaderDirectly         |  62.28 μs |  0.62 |      - |   3.07 KB |        0.07 |
| BuildUsingSystemTextJsonUtf8JsonReaderWithJsonDocument |  70.67 μs |  0.70 |      - |   3.14 KB |        0.07 |