using DecisionHelper.Domain.Models;

namespace DecisionHelper.UnitTests
{
    public class DecisionNodeTests
    {
        private readonly DecisionNode _sut;
        private readonly Fixture _fixture;

        public DecisionNodeTests()
        {
            var tree = Helpers.GetDecisionTreeFromFile(Helpers.DoughnutTreeFile);
            _sut = tree.Root;
            _fixture = new Fixture();
            _fixture.Behaviors.Clear();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void CloneWithOnlyDirectChildren_ShouldReturnNewObjects_ForRootAndChildren()
        {
            var resultedClone = _sut.CloneWithOnlyDirectChildren();

            Assert.NotEqual(_sut, resultedClone);
            Assert.Equal(_sut.Children.Count, resultedClone.Children.Count);
            for (int i = 0; i < resultedClone.Children.Count; i++)
            {
                Assert.NotEqual(_sut.Children[i], resultedClone.Children[i]);
            }
        }

        [Fact]
        public void CloneWithOnlyDirectChildren_ShouldNotReturnIndirectChildren()
        {
            var resultedClone = _sut.CloneWithOnlyDirectChildren();

            foreach (var clonedChild in resultedClone.Children)
            {
                Assert.Empty(clonedChild.Children);
            }
        }

        [Fact]
        public void CloneWithOnlyDirectChildren_ShouldNotThrow_ForNodeWithoutChildren()
        {
            var sut = new DecisionNode
            {
                Answer = _fixture.Create<string>(),
                Result = _fixture.Create<string>(),
                Children = Array.Empty<DecisionNode>()
            };

            var resultedClone = sut.CloneWithOnlyDirectChildren();

            Assert.Empty(resultedClone.Children);
        }
    }
}
