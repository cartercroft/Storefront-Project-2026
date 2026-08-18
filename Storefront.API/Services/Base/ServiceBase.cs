using AutoMapper;
using Storefront.API.Classes;
using Storefront.API.Data;
using Storefront.API.Data.Base;

namespace Storefront.API.Services.Base
{
    public abstract class ServiceBase<TViewModel, TModel, TKey>
        where TViewModel: class
        where TModel : class
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
    {
        private readonly IMapper _mapper;
        private readonly RepositoryBase<TModel, TKey, StorefrontContext> _repository;
        private readonly Serilog.ILogger _logger;
        public ServiceBase(RepositoryBase<TModel, TKey, StorefrontContext> repository, IMapper mapper, Serilog.ILogger logger) 
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual async Task<Response<TViewModel?>> GetById(TKey id)
        {
            Response<TViewModel?> response = new Response<TViewModel?>();
            try
            {
                TModel? model = await _repository.GetById(id);
                if (model == null)
                {
                    response.ErrorMessages.Add($"No {typeof(TViewModel)} found with key {id}");
                    return response;
                }
                response.Result = MapDataModelToViewModel(model);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "");
                response.ErrorMessages.Add("An unknown error occurred.");
            }
            return response;
        }
        public async Task<Response<IEnumerable<TViewModel>>> GetAll()
        {
            IEnumerable<TViewModel> list = MapDataModelListToViewModelList(await _repository.GetAll());
            return new Response<IEnumerable<TViewModel>>(list);
        }
        public virtual async Task<Response<TViewModel>> Save(TViewModel viewModel)
        {
            TModel? model = MapViewModelToDataModel(viewModel);
            if (model == null)
            {
                throw new ArgumentException($"{nameof(model)} is null");
            }

            model = await _repository.Save(model);

            TViewModel? newViewModel = MapDataModelToViewModel(model);

            if(newViewModel == null)
            {
                throw new InvalidCastException($"Mapping failed from type {typeof(TModel)} to type {typeof(TViewModel)}");
            }

            return new Response<TViewModel>(newViewModel);
        }
        public virtual Response<bool> Delete(TViewModel viewModel)
        {
            TModel? model = MapViewModelToDataModel(viewModel);

            Response<bool> response = new Response<bool>();
            if(model == null)
            {
                response.Result = false;
                response.ErrorMessages.Add($"No object found to delete.");
                return response;
            }

            try
            {
                _repository.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "");
                response.ErrorMessages.Add($"Error when attempting to delete {nameof(TModel)}.");
            }
            return response;
        }
        public IEnumerable<TViewModel> MapDataModelListToViewModelList(IEnumerable<TModel> list)
        {
            return list.Select(MapDataModelToViewModel);
        }
        public IEnumerable<TModel> MapViewModelListToDataModelList(IEnumerable<TViewModel> list) 
        {
            return list.Select(MapViewModelToDataModel);
        }
        public TViewModel MapDataModelToViewModel(TModel dataModel)
        {
            return _mapper.Map<TViewModel>(dataModel);
        }
        public TModel MapViewModelToDataModel(TViewModel viewModel)
        {
            return _mapper.Map<TModel>(viewModel);
        }

    }
}
