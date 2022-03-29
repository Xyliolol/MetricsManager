using AgentManager.Controllers;
using AgentManager.Interface;
using AgentManager.Models;
using AgentManager.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AgentManagerUnitTest
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;

        private readonly Mock<IHddMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<HddMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public HddControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<HddMetricsController>>();
            _mockRepository = new Mock<IHddMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new HddMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>())).Returns(new List<HddMetric>());
            var result = _controller.GetMetrics(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>()), Times.AtMostOnce());
        }
    }
}
