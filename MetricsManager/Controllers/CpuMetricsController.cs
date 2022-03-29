using AutoMapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Response.Responses;
using Microsoft.AspNetCore.Mvc;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени c id агентом
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/cpu/agent/1/from/2022.03.14/to/2022.03.17
        /// </remarks>
        /// <param name="agentId">ID агента</param>  
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик и ID агента, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запуск CpuMetricsController.GetMetrics с параметрами: {fromTime}, {toTime} от {agentId}.");

            return Ok();
        }

        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/cpu/cluster/from/2022.03.14/to/2022.03.17
        /// </remarks>        
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>


        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение показателей ЦП за период: {fromTime}, {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeMilliseconds(), toTime.ToUnixTimeMilliseconds());

            var response = new CpuGetMetricsFromAllClusterResponse()
            {
                Metrics = new List<CpuMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricResponse>(metric));
            }
            return Ok(response);

        }
    }
}