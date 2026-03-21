using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;

namespace HomeInventory.Infrastructure.Repositories;

public class PurchaseOrderRepository(ApplicationDbContext context)
    : GenericRepository<PurchaseOrder>(context), IPurchaseOrderRepository;
