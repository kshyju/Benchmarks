
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests;

public class Tests
{
	[Theory]
	[InlineData("java|powershell|dotnet-isolated|python")]
	[InlineData("C#|F#|VB.NET")]
	[InlineData("one|two|three|four|five")]
	[InlineData("a|b|c|d|e|f|g")]
	[InlineData("single")]
	[InlineData("")]
	[InlineData("  java  | powershell |dotnet-isolated | python ")]
	public void HashSetMethods_ShouldReturnSameOutput(string input)
	{
		var resultSplit = StringUtils.CreateHashSetUsingStringSplit(input);
		var resultSpan = StringUtils.CreateHashSetOptimized(input);
		Assert.Equal(resultSplit, resultSpan);
	}
}