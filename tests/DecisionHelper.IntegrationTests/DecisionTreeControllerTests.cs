using DecisionHelper.API;
using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;

namespace DecisionHelper.IntegrationTests
{
    [UsesVerify]
    public class DecisionTreeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        const string CorrectTreeName = "CorrectTreeName";
        const string IncorrectTreeName = "IncorrectTreeName";

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
            var tree = JsonConvert.DeserializeObject<DecisionTree>(treeJson)!;

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
            treesDb.Setup(t => t.GetAsync<DecisionNode>(CorrectTreeName, CommandFlags.None))
                .Returns(Task.FromResult<DecisionNode?>(tree.Root));
            treesDb.Setup(t => t.GetAsync<DecisionNode>(IncorrectTreeName, CommandFlags.None))
                .Returns(Task.FromResult<DecisionNode?>(null));
            treesInfoDb.Setup(t => t.GetAsync<IReadOnlyCollection<DecisionTreeInfo>>(RedisConsts.TreesInfoListKey, CommandFlags.None))
                .Returns(Task.FromResult<IReadOnlyCollection<DecisionTreeInfo>?>(new[] { tree.ToDecisionTreeInfo(), new DecisionTreeInfo("Test info") }));
            nodesDb.Setup(t => t.GetAsync<DecisionNode>(Guid.Parse(CorrectDecisionId).ToString("N"), CommandFlags.None))
                .Returns(Task.FromResult<DecisionNode?>(tree.Root.Children[0]));
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
            var response = await _client.GetAsync($"/api/DecisionTree/firstDecision?treeName={CorrectTreeName}");
            await VerifyResponse(response);
        }

        [Fact]
        public async Task GetFirstDecisionAsync_ShouldReturnNotFound_WhenTreeIsNotFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/firstDecision?treeName={IncorrectTreeName}");
            await VerifyResponse(response);
        }

        [Fact]
        public async Task GetDecisionAsync_ShouldReturnCorrectDecisionDto_WhenTreeIsFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decision?decisionId={CorrectDecisionId}");
            await VerifyResponse(response);
        }

        [Fact]
        public async Task GetDecisionAsync_ShouldReturnNotFound_WhenTreeIsNotFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decision?decisionId={IncorrectDecisionId}");
            await VerifyResponse(response);
        }

        [Fact]
        public async Task GetDecisionTreeAsync_ShouldReturnCorrectDecisionNodeDto_WhenTreeIsFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decisionTree?treeName={CorrectTreeName}");
            await VerifyResponse(response);
        }

        [Fact]
        public async Task GetDecisionTreeAsync_ShouldReturnNotFound_WhenTreeIsNotFound()
        {
            var response = await _client.GetAsync($"/api/DecisionTree/decisionTree?treeName={IncorrectTreeName}");
            await VerifyResponse(response);
        }

        [Fact]
        public async Task GetDecisionTreesAsync_ShouldReturnSortedTreeInfos_WhenDataIsValid()
        {
            var response = await _client.GetAsync("/api/DecisionTree/decisionTrees");
            await VerifyResponse(response);
        }

        private static async Task VerifyResponse(HttpResponseMessage response)
        {
            var responseBodyStr = await response.Content.ReadAsStringAsync();
            var responseBody = ParseResponseBody(responseBodyStr);

            await Verify(new { response, responseBody });
        }

        private static JToken ParseResponseBody(string responseBodyStr)
        {
            var responseBody = JToken.Parse(responseBodyStr);
            if (responseBody is JObject responseBodyObj && responseBodyObj.ContainsKey("traceId"))
                responseBodyObj["traceId"] = null;

            return responseBody;
        }
    }
}