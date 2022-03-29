using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTest
{
    public class AgentsControllerUnitTests
    {

        private readonly AgentsController _controller;

        private readonly Mock<IAgentsRepository> _mockRepository;

        private readonly Mock<ILogger<AgentsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public AgentsControllerUnitTests()
        {
            _mockRepository = new Mock<IAgentsRepository>();
            _mockLogger = new Mock<ILogger<AgentsController>>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void RegisterAgent_ReturnOk()
        {
            //Arrange
            var agent = new AgentInfo();

            //act
            var result = _controller.RegisterAgent(agent);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //act
            var result = _controller.EnableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //act
            var result = _controller.DisableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


    }
}