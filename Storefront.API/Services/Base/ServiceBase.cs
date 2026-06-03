using Storefront.API.Classes;
using Storefront.API.Data;

namespace Storefront.API.Services.Base
{
    public class ServiceBase<TModel, TKey> : IDisposable
        where TModel : class
        where TKey : struct, IEquatable<TKey>
    {
        private UnitOfWork _unitOfWork;
        public ServiceBase(UnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }

    public abstract class ServiceBase<TViewModel, TModel, TKey> : ServiceBase<TModel, TKey>
        where TViewModel: class
        where TModel : class
        where TKey : struct, IEquatable<TKey>
    {
        public ServiceBase(UnitOfWork unitOfWork) : base(unitOfWork){}
        public virtual Task<Response<TViewModel>> Add(TViewModel viewModel)
        {
            throw new NotImplementedException(nameof(Add));
        }
        public virtual Task<Response<TViewModel>> GetById(TKey id)
        {
            throw new NotImplementedException(nameof(GetById));
        }
    }
}
