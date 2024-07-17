using JSS_Repositories.Repo.Interface;
using Microsoft.EntityFrameworkCore;

namespace JSS_Repositories.Repo.Interface
{
	public interface IUnitOfWork : IDisposable
	{
		public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IAccountRepository AccountRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IWarrantyRepository WarrantyRepository { get; }
        public IMaterialRepository MaterialRepository { get; }
        public IMembershipRepository MembershipRepository { get; }
        public IConditionWarrantyRepository ConditionWarrantyRepository { get; }
        public IGoldRateRepository GoldRateRepository { get; }
        public ISilverRateRepository SilverRateRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public IProcessPriceRepository ProcessPriceRepository { get; }
        public IPromotionRepository PromotionRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IStallRepository StallRepository { get; }
        public ISubProductsRepository SubProductsRepository { get; }
        public IDiamondRepository DiamondRepository { get; }
        public IDiscountRepository DiscountRepository { get; }
        public IMemberTypeRepository MemberTypeRepository { get; }
        public IProductConditionGroupRepository ProductConditionGroupRepository { get; }
        public IPurchasePriceRepository PurchasePriceRepository { get; }
        public IWarrantyMappingConditionRepository WarrantyMappingConditionRepository { get; }

        int Commit();

		Task<int> CommitAsync();
	}
}
