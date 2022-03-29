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
    public class CpuControllerUnitTests
    {
        private readonly CpuMetricsController _controller;

        private readonly Mock<ICpuMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<CpuMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public CpuControllerUnitTests()
        {
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>())).Returns(new List<CpuMetric>());
            var result = _controller.GetMetricsFromAllCluster(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>()), Times.AtMostOnce());
        }


    }
}