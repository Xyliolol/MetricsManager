using AutoMapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Response.Responses;
using Microsoft.AspNetCore.Mvc;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
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

        [HttpGet("errors-count/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение количества ошибок за период: {fromTime}, {toTime} от {agentId}");

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

        [HttpGet("errors-count/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение количества ошибок за период: {fromTime}, {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeMilliseconds(), toTime.ToUnixTimeMilliseconds());

            var response = new DotNetGetMetricsFromAllClusterResponse()
            {
                Metrics = new List<DotNetMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricResponse>(metric));
            }
            return Ok(response);
        }
    }
}