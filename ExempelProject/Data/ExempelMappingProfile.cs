using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExempelProject.Data.Entities;
using ExempelProject.ViewModels;

namespace ExempelProject.Data
{
  public class ExempelMappingProfile : Profile
  {
    public ExempelMappingProfile()
    {
      CreateMap<Order, OrderViewModel>()
        .ForMember(o => o.OrderId, ex => ex.MapFrom(i => i.Id))
        .ReverseMap();

      CreateMap<OrderItem, OrderItemViewModel>()
        .ReverseMap()
        .ForMember(m => m.Product, opt => opt.Ignore());
    }
  }
}
