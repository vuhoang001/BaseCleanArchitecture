using Application.Interfaces.Base;
using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository : IRepository<Order, int>
{
}