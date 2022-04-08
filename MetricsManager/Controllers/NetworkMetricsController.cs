using AutoMapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Response.Responses;
using Microsoft.AspNetCore.Mvc;


namespace MetricsManager.Controllers
{

    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени c ID агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/network/agent/1/from/2022.03.14/to/2022.03.17
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
            _logger.LogInformation($"Получение метрик за период: {fromTime}, {toTime} от {agentId}");

            return Ok();
        }

        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:       
        /// GET api/metrics/network/cluster/from/2022.03.14/to/2022.03.17
        /// </remarks>         
        /// <param name="fromTime">начальная метрика времени в форме даты с 01.01.1970</param>  
        /// <param name="toTime">конечная метрика времени в форме даты с 01.01.1970</param>
        /// <returns>Список метрик, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Если всё хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>


        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Получение метрик за период: {fromTime}, {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeMilliseconds(), toTime.ToUnixTimeMilliseconds());

            var response = new NetworkGetMetricsFromAllClusterResponse()
            {
                Metrics = new List<NetworkMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricResponse>(metric));
            }
            return Ok(response);
        }
    }
}
