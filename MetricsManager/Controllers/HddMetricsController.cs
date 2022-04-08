using AutoMapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Response.Responses;
using Microsoft.AspNetCore.Mvc;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        /// <summary>
        /// Получает метрики Hdd на заданном диапазоне времени c ID агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/hdd/agent/1/from/2022.03.14/to/2022.03.17
        /// </remarks> 
        /// <param name="agentId">ID агента</param> 
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик и ID агента, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeHDDSpaceFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение свободного места на HDD у {agentId} c {fromTime} до {toTime}");

            return Ok();
        }

        /// <summary>
        /// Получает метрики Hdd на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/hdd/cluster/from/2022.03.14/to/2022.03.17
        /// </remarks>         
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeHDDSpace([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение свободного места на HDD  c {fromTime} до {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeMilliseconds(), toTime.ToUnixTimeMilliseconds());

            var response = new HddGetMetricFromAgentResponse()
            {
                Metrics = new List<HddMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricResponse>(metric));
            }
            return Ok(response);
        }
    }
}