using AgentManager.Models;
using AgentManager.Request;
using AutoMapper;

namespace MetricsAgent.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricsDto>();

            CreateMap<DotNetMetric, DotNetMetricsDto>();

            CreateMap<HddMetric, HddMetricsDto>();

            CreateMap<NetworkMetric, NetworkMetricsDto>();

            CreateMap<RamMetric, RamMetricsDto>();
        }
    }
}