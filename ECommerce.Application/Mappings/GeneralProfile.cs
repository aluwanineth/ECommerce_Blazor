using AutoMapper;
using ECommerce.Application.Features.OrderDetail.Commands.CreateOrderDetail;
using ECommerce.Application.Features.Orders.Commands;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();

            //order
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<Order, CreateCommandViewModel>().ReverseMap(); 

            //Order Detail
             CreateMap<CreateOrderItemCommand, OrderItem>();
        }
    }
}