``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-5700HQ CPU 2.70GHz (Broadwell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  DefaultJob : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT


```
|      Method | Categories |       Mean |     Error |     StdDev |     Median |  Ratio | RatioSD |
|------------ |----------- |-----------:|----------:|-----------:|-----------:|-------:|--------:|
|     MockGen |    Returns |   1.293 μs | 0.0041 μs |  0.0034 μs |   1.293 μs |   1.00 |    0.00 |
| NSubstitute |    Returns | 147.826 μs | 8.4756 μs | 24.7237 μs | 139.900 μs | 125.69 |   23.67 |
|         Moq |    Returns |  15.325 μs | 0.2390 μs |  0.2235 μs |  15.302 μs |  11.87 |    0.19 |
|             |            |            |           |            |            |        |         |
|     MockGen |     Assert |   6.390 μs | 0.0282 μs |  0.0264 μs |   6.398 μs |   1.00 |    0.00 |
| NSubstitute |     Assert | 163.661 μs | 0.3354 μs |  0.2974 μs | 163.659 μs |  25.60 |    0.11 |
|         Moq |     Assert | 287.502 μs | 1.2443 μs |  1.1031 μs | 287.521 μs |  44.97 |    0.18 |
