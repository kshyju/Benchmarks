using Benchmarks.ConsoleApp;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Benchmarks.Tests
{
    public class JsonParseTests
    {
        [Fact]
        public void JObjectLoadAndDeserializeProduceSameOutput()
        {
            var jsonParseBenchmarks = new JsonParseBenchmarks();

            JObject resultFromLoad = jsonParseBenchmarks.UseLoad();
            JObject resultFromDeserialize = jsonParseBenchmarks.UseSerialize();
            JObject resultFromParse = jsonParseBenchmarks.UseParse();

            var dateOfBirthFromLoad = resultFromLoad["dateOfBirth"].Value<string>();
            var dateOfBirthFromDeserialize = resultFromDeserialize["dateOfBirth"].Value<string>();
            var dateOfBirthFromParse = resultFromParse["dateOfBirth"].Value<string>();

            var dateOfBirthFromLoadAsDateTime = resultFromLoad["dateOfBirth"].Value<DateTime>();

            var dateOfBirthFromParseAsDateTime = resultFromParse["dateOfBirth"].Value<DateTime>();

            Assert.Equal(dateOfBirthFromLoad, dateOfBirthFromDeserialize);
           // Assert.NotEqual(dateOfBirthFromLoad, dateOfBirthFromParse);
        }
    }
}