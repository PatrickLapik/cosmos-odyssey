using AutoMapper;
using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using Pagination.EntityFrameworkCore.Extensions;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CompanyRoute, CompanyRouteResponse>()
            .ForMember(dest => dest.ValidUntil, opt => opt.MapFrom(src => src.TravelPrice.ValidUntil));

        CreateMap<List<CompanyRoute>, FullCompanyRoutesResponse>()
            .ForMember(dest => dest.CompanyRouteResponses, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.TotalDistance, opt => opt.MapFrom(src => src.Sum(d => d.Route!.Distance)))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Sum(p => p.Price)))
            .ForMember(dest => dest.TotalTravelMinutes,
                opt => opt.MapFrom(src => (src.Last().TravelEnd - src.First().TravelStart).TotalMinutes));

        CreateMap<Company, CompanyResponse>();
        CreateMap<Route, RouteResponse>()
            .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.ToDestination.Name))
            .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.FromDestination.Name))
            .ForMember(dest => dest.ToId, opt => opt.MapFrom(src => src.ToDestinationId))
            .ForMember(dest => dest.FromId, opt => opt.MapFrom(src => src.FromDestinationId));
        
        CreateMap(typeof(Pagination<>), typeof(Pagination<>)).ConvertUsing(typeof(PaginationTypeConverter<,>));
    }
}

public class PaginationTypeConverter<TSource, TDestination> : ITypeConverter<Pagination<TSource>, Pagination<TDestination>>
{
    public Pagination<TDestination> Convert(Pagination<TSource> source, Pagination<TDestination> destination, ResolutionContext context)
    {
        var mappedItems = context.Mapper.Map<List<TDestination>>(source.Results);

        return new Pagination<TDestination>
        {
            CurrentPage = source.CurrentPage,
            PreviousPage = source.PreviousPage,
            NextPage = source.NextPage,
            TotalItems = source.TotalItems,
            TotalPages = source.TotalPages,
            Results = mappedItems,
        };
    }
}