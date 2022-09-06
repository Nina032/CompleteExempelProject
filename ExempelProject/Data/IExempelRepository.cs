using System.Collections.Generic;
using ExempelProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExempelProject.Data
{
  public interface IExempelRepository
  {
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> GetProductsByCategory(string category);

    IEnumerable<Order> GetAllOrders(bool includeItems);
    IEnumerable<Order> GetOrdersByUser(string username, bool includeItems);
    Order GetOrderById(string username, int id);

    void AddEntity(object entity);
    bool SaveAll();
  }
}