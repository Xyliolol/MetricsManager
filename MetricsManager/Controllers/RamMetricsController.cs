using AutoMapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Response.Responses;
using Microsoft.AspNetCore.Mvc;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        private readonly IRamMetricsRepository _repository;

        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики Ram на заданном диапазоне времени c ID агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/ram/agent/1/from/2022.03.14/to/2022.03.17
        /// </remarks> 
        /// <param name="agentId">ID агента</param> 
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик и ID агента, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение RAM от {fromTime} до {toTime} у {agentId}");

            return Ok();
        }

        /// <summary>
        /// Получает метрики Ram на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/ram/cluster/from/2022.03.14/to/2022.03.17
        /// </remarks>         
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailable([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение RAM от {fromTime} до {toTime} ");

            var metrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeMilliseconds(), toTime.ToUnixTimeMilliseconds());

            var response = new RamGetMetricsFromAgentResponse()
            {
                Metrics = new List<RamMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricResponse>(metric));
            }
            return Ok(response);
        }
    }
}