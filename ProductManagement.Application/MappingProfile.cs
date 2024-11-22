using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(destination => destination.Name, options =>
                            options.MapFrom(source => source.Name));

            CreateMap<CategoryDto, Category>()
                .ForMember(destination => destination.Name, options =>
                            options.MapFrom(source => source.Name));

            CreateMap<ProductDto, Product>()
                .ForMember(destination => destination.Name, options =>
                            options.MapFrom(source => source.Name))
                .ForMember(destination => destination.CategoryId, options =>
                            options.MapFrom(source => source.CategoryId));
            
            CreateMap<Product, ProductDto>()
                .ForMember(destination => destination.Name, options =>
                            options.MapFrom(source => source.Name))
                .ForMember(destination => destination.CategoryId, options =>
                            options.MapFrom(source => source.CategoryId));
           
            CreateMap<Product, UpdateProductDto>()
                .ForMember(destination => destination.Name, options =>
                            options.MapFrom(source => source.Name))
                .ForMember(destination => destination.CategoryId, options =>
                            options.MapFrom(source => source.CategoryId));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(destination => destination.Name, options =>
                            options.MapFrom(source => source.Name))
                .ForMember(destination => destination.CategoryId, options =>
                            options.MapFrom(source => source.CategoryId));
        }
    }
}
