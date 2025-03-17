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
| Method                                                 | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------------------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| UsingNewtonsoftJsonNet                                 | 96.89 μs | 2.095 μs | 6.111 μs |  1.00 |    0.09 | 0.7324 |  45.81 KB |        1.00 |
| BuildUsingSystemTextJsonDocumentAndReadText            | 82.56 μs | 1.561 μs | 1.384 μs |  0.86 |    0.06 | 0.2441 |     21 KB |        0.46 |
| BuildUsingSystemTextJsonDocument                       | 74.73 μs | 1.482 μs | 2.856 μs |  0.77 |    0.06 |      - |   5.36 KB |        0.12 |
| BuildUsingSystemTextJsonUtf8JsonReaderDirectly         | 67.70 μs | 1.341 μs | 2.708 μs |  0.70 |    0.06 |      - |   3.07 KB |        0.07 |
| BuildUsingSystemTextJsonUtf8JsonReaderWithJsonDocument | 76.37 μs | 1.514 μs | 2.768 μs |  0.79 |    0.06 |      - |   3.14 KB |        0.07 |
