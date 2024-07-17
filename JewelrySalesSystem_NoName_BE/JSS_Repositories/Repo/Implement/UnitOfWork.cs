using JSS_DataAccessObjects;
using JSS_Repositories.Repo.Implement;
using JSS_Repositories.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace JSS_Repositories.Repo.Implement;


public class UnitOfWork : IUnitOfWork, IDisposable
{
    public JewelrySalesSystemContext Context { get; }

    public IProductRepository ProductRepository => new ProductRepository(Context);

    public IOrderRepository OrderRepository => new OrderRepository(Context);

    public IAccountRepository AccountRepository => new AccountRepository(Context);

    public ICategoryRepository CategoryRepository => new CategoryRepository(Context);

    public ITransactionRepository TransactionRepository => new TransactionRepository(Context);

    public IWarrantyRepository WarrantyRepository => new WarrantyRepository(Context);

    public IMaterialRepository MaterialRepository => new MaterialRepository(Context);

    public IMembershipRepository MembershipRepository => new MembershipRepository(Context);

    public IConditionWarrantyRepository ConditionWarrantyRepository => new ConditionWarrantyRepository(Context);

    public IGoldRateRepository GoldRateRepository => new GoldRateRepository(Context);

    public ISilverRateRepository SilverRateRepository => new SilverRateRepository(Context);

    public IOrderDetailRepository OrderDetailRepository => new OrderDetailRepository(Context);

    public IProcessPriceRepository ProcessPriceRepository => new ProcessPriceRepository(Context);

    public IPromotionRepository PromotionRepository => new PromotionRepository(Context);

    public IRoleRepository RoleRepository => new RoleRepository(Context);

    public IStallRepository StallRepository => new StallRepository(Context);

    public ISubProductsRepository SubProductsRepository => new SubProductsRepository(Context);

    public IDiamondRepository DiamondRepository => new DiamondRepository(Context);

    public IDiscountRepository DiscountRepository => new DiscountRepository(Context);

    public IMemberTypeRepository MemberTypeRepository => new MemberTypeRepository(Context);

    public IProductConditionGroupRepository ProductConditionGroupRepository => new ProductConditionGroupRepository(Context);
    
    public IPurchasePriceRepository PurchasePriceRepository => new PurchasePriceRepository(Context);

    public IWarrantyMappingConditionRepository WarrantyMappingConditionRepository => new WarrantyMappingConditionRepository(Context); 

    public UnitOfWork(JewelrySalesSystemContext context)
    {
        Context = context;
    }

    public void Dispose()
    {
        Context?.Dispose();
    }

    public int Commit()
    {
        TrackChanges();
        return Context.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        TrackChanges();
        return await Context.SaveChangesAsync();
    }

    private void TrackChanges()
    {
        var validationErrors = Context.ChangeTracker.Entries<IValidatableObject>()
            .SelectMany(e => e.Entity.Validate(null))
            .Where(e => e != ValidationResult.Success)
            .ToArray();
        if (validationErrors.Any())
        {
            var exceptionMessage = string.Join(Environment.NewLine,
                validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
            throw new Exception(exceptionMessage);
        }
    }
}