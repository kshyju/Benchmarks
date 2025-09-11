
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests;

public class Tests
{
	[Fact]
	public void BothImmutableDictionaryImplementations_ReturnSameOutput()
	{
		// Arrange
		var input = new[] { "java", "powershell", "dotnet-isolated", "python" };

		// Act
		var dict1 = ImmutableBenchmarks.CreateUsingDictionaryThenToImmutableDictionaryCall(input);
		var dict2 = ImmutableBenchmarks.CreateImmutableDictionaryUsingBuilder(input);

		// Assert
		Assert.Equal(dict1.Count, dict2.Count);
		foreach (var kvp in dict1)
		{
			Assert.True(dict2.ContainsKey(kvp.Key));
			Assert.Equal(kvp.Value, dict2[kvp.Key]);
		}
	}
}