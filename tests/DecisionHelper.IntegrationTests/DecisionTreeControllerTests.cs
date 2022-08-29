using DecisionHelper.API;
using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;

namespace DecisionHelper.IntegrationTests
{
    [UsesVerify]
    public class DecisionTreeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        const string CorrectTreeId = "CorrectTreeId";
        const string IncorrectTreeId = "IncorrectTreeId";

        const string CorrectDecisionId = "690572a8-e286-479b-801d-c5fa3cff8573";
        const string IncorrectDecisionId = "253472a8-e286-479b-801d-c5fa3cff8573";

        const string TestFilesFolder = "Files";

        private readonly HttpClient _client;

        public DecisionTreeControllerTests(WebApplicationFactory<Program> webApplicationFactory)
        {
            var modifiedFactory = SetupFactory(webApplicationFactory);

            _client = modifiedFactory.CreateClient();

            VerifierSettings.DerivePathInfo(
                (sourceFile, projectDirectory, type, method) =>
                {
                    return new(
                        directory: Path.Combine(projectDirectory, TestFilesFolder),
                        typeName: type.Name,
                        methodName: method.Name);
                });
        }

        private static WebApplicationFactory<Program> SetupFactory(WebApplicationFactory<Program> factory)
        {
            var treeJson = File.ReadAllText(Path.Combine(TestFilesFolder, "Doughnut.json"));
            var tree = JsonSerializer.Deserialize<DecisionNode>(treeJson);
            var treeInfosJson = File.ReadAllText(Path.Combine(TestFilesFolder, "TreeInfos.json"));
            var treeInfos = JsonSerializer.Deserialize<List<DecisionTreeInfo>>(treeInfosJson);

            var redisClient = new Mock<IRedisClient>();
            var treesDb = new Mock<IRedisDatabase>();
            var nodesDb = new Mock<IRedisDatabase>();
            var treesInfoDb = new Mock<IRedisDatabase>();
            redisClient.Setup(r => r.GetDb(RedisConsts.TreesDb, null))
                .Returns(treesDb.Object);
            redisClient.Setup(r => r.GetDb(RedisConsts.NodesDb, null))
                .Returns(nodesDb.Object);
            redisClient.Setup(r => r.GetDb(RedisConsts.TreesInfoDb, null))
                .Returns(treesInfoDb.Object);
            treesDb.Setup(t => t.GetAsync<DecisionNode>(CorrectTreeId, CommandFlags.None))
                .Returns(Task.FromResult(tree));
            treesDb.Setup(t => t.GetAsync<DecisionNode>(IncorrectTreeId, CommandFlags.None))
                .Returns(Task.FromResult<DecisionNode?>(null));
            treesInfoDb.Setup(t => t.GetAsync<IReadOnlyCollection<DecisionTreeInfo>>(RedisConsts.TreesInfoListKey, CommandFlags.None))
                .Returns(Task.FromResult<IReadOnlyCollection<DecisionTreeInfo>?>(treeInfos));
            nodesDb.Setup(t => t.GetAsync<DecisionNode>(Guid.Parse(CorrectDecisionId).ToString("N"), CommandFlags.None))
                .Returns(Task.FromResult<DecisionNode?>(tree!.Children[0]));
            nodesDb.Setup(t => t.GetAsync<DecisionNode>(IncorrectDecisionId, CommandFlags.None))
                .Returns(Task.FromResult<DecisionNode?>(null));

            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(redisClient.Object);
                });
            });
        }

        [Fact]
        public async Task GetFirstDecisionAsync_ShouldReturnCorrectDecisionDto_WhenTreeIsFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/firstDecision?treeName={CorrectTreeId}");
            var result = await response.Content.ReadAsStringAsync();
            await Verify(result);
        }

        [Fact]
        public async Task GetFirstDecisionAsync_ShouldReturnNotFound_WhenTreeIsNotFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/firstDecision?treeName={IncorrectTreeId}");
            var result = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(result);
            jObject["traceId"] = null;
            await Verify(jObject.ToString());
        }

        [Fact]
        public async Task GetDecisionAsync_ShouldReturnCorrectDecisionDto_WhenTreeIsFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decision?decisionId={CorrectDecisionId}");
            var result = await response.Content.ReadAsStringAsync();
            await Verify(result);
        }

        [Fact]
        public async Task GetDecisionAsync_ShouldReturnNotFound_WhenTreeIsNotFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decision?decisionId={IncorrectDecisionId}");
            var result = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(result);
            jObject["traceId"] = null;
            await Verify(jObject.ToString());
        }

        [Fact]
        public async Task GetDecisionTreeAsync_ShouldReturnCorrectDecisionNodeDto_WhenTreeIsFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decisionTree?treeName={CorrectTreeId}");
            var result = await response.Content.ReadAsStringAsync();
            await Verify(result);
        }

        [Fact]
        public async Task GetDecisionTreeAsync_ShouldReturnNotFound_WhenTreeIsNotFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decisionTree?treeName={IncorrectTreeId}");
            var result = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(result);
            jObject["traceId"] = null;
            await Verify(jObject.ToString());
        }

        [Fact]
        public async Task GetDecisionTreesAsync_ShouldReturnSortedTreeInfos_WhenDataIsValid()
        {
            var response = await _client.GetAsync("/api/DecisionTree/decisionTrees");
            var result = await response.Content.ReadAsStringAsync();
            await Verify(result);
        }
    }
}