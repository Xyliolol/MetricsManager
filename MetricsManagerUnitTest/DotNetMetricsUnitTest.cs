using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTest
{
    public class DotNetControllerUnitTests
    {
        private readonly DotNetMetricsController _controller;

        private readonly Mock<IDotNetMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<DotNetMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public DotNetControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockRepository = new Mock<IDotNetMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>())).Returns(new List<DotNetMetric>());
            var result = _controller.GetErrorsCountFromAllCluster(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>()), Times.AtMostOnce());
        }
    }
}