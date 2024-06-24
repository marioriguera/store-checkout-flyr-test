using AutoMapper;
using StoreCheckout.Application.CheckoutUseCase.DTOs;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Utils
{
    /// <summary>
    /// AutoMapper profile for mapping between DTOs and domain models.
    /// </summary>
    internal class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ProductDTO, Product>();
        }
    }
}
