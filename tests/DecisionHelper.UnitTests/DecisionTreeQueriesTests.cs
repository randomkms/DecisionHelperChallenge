using DecisionHelper.API.Models;
using DecisionHelper.API.Services;
using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;

namespace DecisionHelper.UnitTests
{
    [UsesVerify]
    public class DecisionTreeQueriesTests
    {
        const string TestFilesFolder = "Files";
        private readonly DecisionTreeQueries _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IDecisionTreeRepository> _decisionTreeRepositoryMock = new();

        public DecisionTreeQueriesTests()
        {
            _sut = new DecisionTreeQueries(_decisionTreeRepositoryMock.Object);
            _fixture = new Fixture();
            VerifierSettings.DerivePathInfo(
                (sourceFile, projectDirectory, type, method) =>
                {
                    return new(
                        directory: Path.Combine(projectDirectory, TestFilesFolder),
                        typeName: type.Name,
                        methodName: method.Name);
                });
            _fixture.Behaviors.Clear();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetDecisionTreesAsync_ShouldReturnSortedTreeInfos_WhenDataIsValid()
        {
            var treesInfos = new[]
            {
                new DecisionTreeInfo("IceCream")
                {
                    ImageUrl = _fixture.Create<string>(),
                    Description = _fixture.Create<string>(),
                },
                new DecisionTreeInfo("Doughnut"),
            };
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreesAsync())
                .ReturnsAsync(treesInfos);

            var result = await _sut.GetDecisionTreesAsync();

            Assert.Equivalent(new[] { treesInfos[1], treesInfos[0] }, result);
        }

        [Fact]
        public async Task GetDecisionTreesAsync_ShouldReturnEmptyCollection_WhenNoTreesExist()
        {
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreesAsync())
                .ReturnsAsync(Array.Empty<DecisionTreeInfo>());

            var result = await _sut.GetDecisionTreesAsync();

            Assert.Equivalent(Array.Empty<string>(), result);
        }

        [Fact]
        public async Task GetFirstDecisionAsync_ShouldReturnCorrectDecisionDto_WhenTreeIsFound()
        {
            var tree = _fixture.Build<DecisionNode>()
                .With(p => p.Children, () => _fixture.CreateMany<DecisionNode>(2).ToArray())
                .Create();
            var treeName = _fixture.Create<string>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreeRootAsync(treeName))
                .ReturnsAsync(tree);

            var result = await _sut.GetFirstDecisionAsync(treeName);

            var expectedDto = new DecisionDto(tree.Id, tree.Question, tree.Result,
                new[] {
                    new PossibleAnswerDto(tree.Children[0].Id, tree.Children[0].Answer),
                    new PossibleAnswerDto(tree.Children[1].Id, tree.Children[1].Answer)
                });
            Assert.Equivalent(expectedDto, result);
        }

        [Fact]
        public async Task GetFirstDecisionAsync_ShouldReturnNodeWithNoPossibleAnswers_WhenTreeHasNoChildren()
        {
            var tree = _fixture.Create<DecisionNode>();
            var treeName = _fixture.Create<string>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreeRootAsync(treeName))
                .ReturnsAsync(tree);

            var result = await _sut.GetFirstDecisionAsync(treeName);

            Assert.Empty(result!.PossibleAnswers);
        }

        [Fact]
        public async Task GetFirstDecisionAsync_ShouldReturnNull_WhenTreeIsNotFound()
        {
            var treeName = _fixture.Create<string>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreeRootAsync(treeName))
                .ReturnsAsync((DecisionNode?)null);

            var result = await _sut.GetFirstDecisionAsync(treeName);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetDecisionByIdAsync_ShouldReturnCorrectDecisionDto_WhenTreeIsFound()
        {
            var tree = _fixture.Build<DecisionNode>()
                .With(p => p.Children, () => _fixture.CreateMany<DecisionNode>(2).ToArray())
                .Create();
            var treeId = _fixture.Create<Guid>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionByIdAsync(treeId))
                .ReturnsAsync(tree);

            var result = await _sut.GetDecisionByIdAsync(treeId);

            var expectedDto = new DecisionDto(tree.Id, tree.Question, tree.Result,
                new[] {
                    new PossibleAnswerDto(tree.Children[0].Id, tree.Children[0].Answer),
                    new PossibleAnswerDto(tree.Children[1].Id, tree.Children[1].Answer)
                });
            Assert.Equivalent(expectedDto, result);
        }

        [Fact]
        public async Task GetDecisionByIdAsync_ShouldReturnNodeWithNoPossibleAnswers_WhenTreeHasNoChildren()
        {
            var tree = _fixture.Create<DecisionNode>();
            var treeId = _fixture.Create<Guid>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionByIdAsync(treeId))
                .ReturnsAsync(tree);

            var result = await _sut.GetDecisionByIdAsync(treeId);

            Assert.Empty(result!.PossibleAnswers);
        }

        [Fact]
        public async Task GetDecisionByIdAsync_ShouldReturnNull_WhenTreeIsNotFound()
        {
            var treeId = _fixture.Create<Guid>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionByIdAsync(treeId))
                .ReturnsAsync((DecisionNode?)null);

            var result = await _sut.GetDecisionByIdAsync(treeId);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("Doughnut.json")]
        [InlineData("SmallTree.json")]
        public async Task GetDecisionTreeAsync_ShouldReturnCorrectDecisionNodeDto_WhenTreeIsFound(string treeFileName)
        {
            var treeJson = File.ReadAllText(Path.Combine(TestFilesFolder, treeFileName));
            var tree = JsonConvert.DeserializeObject<DecisionTree>(treeJson)!;
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreeRootAsync(tree.Name))
                .ReturnsAsync(tree.Root);

            var result = await _sut.GetDecisionTreeAsync(tree.Name);

            await Verify(result)
                .UseParameters(treeFileName);
        }

        [Fact]
        public async Task GetDecisionTreeAsync_ShouldReturnNull_WhenTreeIsNotFound()
        {
            var treeName = _fixture.Create<string>();
            _decisionTreeRepositoryMock.Setup(r => r.GetDecisionTreeRootAsync(treeName))
                .ReturnsAsync((DecisionNode?)null);

            var result = await _sut.GetDecisionTreeAsync(treeName);

            Assert.Null(result);
        }
    }
}