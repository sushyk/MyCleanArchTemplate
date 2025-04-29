namespace MyCleanArchTemplate.ArchitectureTests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "MyCleanArchTemplate.Domain";
        private const string ApplicationNamespace = "MyCleanArchTemplate.Application";
        private const string InfrastructureNamespace = "MyCleanArchTemplate.Persistence";
        private const string PresentationNamespace = "MyCleanArchTemplate.Adapter.WebApi";
        private const string CompositionRootNamespace = "MyCleanArchTemplate.Web";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(MyCleanArchTemplate.Domain.Entity).Assembly;

            string[] otherProjects = [
                ApplicationNamespace, InfrastructureNamespace, PresentationNamespace, CompositionRootNamespace,
            ];

            // Act

            // Assert

        }
    }
}
