Remove-Item -Path "./BenchmarkDotNet.Artifacts" -Recurse -ErrorAction Ignore

dotnet run -c Release -p benchmark/MockGen.Benchmark/MockGen.Benchmark.csproj -- -e GitHub -a ../..

Copy-Item -Path "./BenchmarkDotNet.Artifacts/results/MockGen.Benchmark.MockGenBenchmark-report-github.md" -Destination "./Benchmark.md"
