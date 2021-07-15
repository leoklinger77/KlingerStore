using AutoMapper;
using KlingerStore.Catalog.Application.ViewModels;
using KlingerStore.Catalog.Domain.Class;

namespace KlingerStore.Catalog.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Height, o => o.MapFrom(x => x.Dimensions.Height))
                .ForMember(d => d.Width, o => o.MapFrom(x => x.Dimensions.Width))
                .ForMember(d => d.Depth, o => o.MapFrom(x => x.Dimensions.Depth));
        }
    }    
}
