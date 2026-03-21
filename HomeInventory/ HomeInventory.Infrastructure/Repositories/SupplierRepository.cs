using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;

namespace HomeInventory.Infrastructure.Repositories;

public class SupplierRepository(ApplicationDbContext context)
    : GenericRepository<Supplier>(context), ISupplierRepository;
