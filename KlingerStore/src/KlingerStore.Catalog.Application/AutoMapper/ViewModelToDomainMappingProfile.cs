using AutoMapper;
using KlingerStore.Catalog.Application.ViewModels;
using KlingerStore.Catalog.Domain.Class;

namespace KlingerStore.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));

            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p => new Product(p.CategoriaId, p.Name, p.Description, p.Active, p.Value, p.InsertDate, p.Image,
                                     new Dimensions(p.Height, p.Width, p.Depth)));
        }
    }
}
