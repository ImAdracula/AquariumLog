using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using JessicasAquariumMonitor.Data.Base;
using JessicasAquariumMonitor.Data.Repositories;
using JessicasAquariumMonitor.Helpers.Caching;
using JessicasAquariumMonitor.Helpers.General;
using JessicasAquariumMonitor.Types;
using FilterTypeEntity = JessicasAquariumMonitor.Data.Entities.FilterType;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Filter")]
    public sealed class FilterTypeController : ApiController
    {
        private const string FilterTypesCacheKey = @"FilterTypes";

        private readonly ICacheProvider _cacheProvider;
        private readonly IConverter<FilterTypeEntity, FilterType> _entityFilterTypeConverter;
        private readonly IConverter<FilterType, FilterTypeEntity> _filterTypeConverter;
        private readonly IFilterTypeRepository _filterTypeRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public FilterTypeController(IUnitOfWorkFactory unitOfWorkFactory, IFilterTypeRepository filterTypeRepository,
            ICacheProvider cacheProvider, IConverter<FilterTypeEntity, FilterType> entityFilterTypeConverter,
            IConverter<FilterType, FilterTypeEntity> filterTypeConverter)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _filterTypeRepository = filterTypeRepository;
            _cacheProvider = cacheProvider;
            _entityFilterTypeConverter = entityFilterTypeConverter;
            _filterTypeConverter = filterTypeConverter;
        }

        private void RefreshCache()
        {
            _cacheProvider.Remove(FilterTypesCacheKey);

            GetCachedFilterTypes();
        }

        private IEnumerable<FilterType> GetCachedFilterTypes()
            => _cacheProvider.GetOrAdd(FilterTypesCacheKey, GetFilterTypesFromDatabase, TimeSpan.FromDays(1));

        private IEnumerable<FilterType> GetFilterTypesFromDatabase()
        {
            using (_unitOfWorkFactory.CreateAquariumReadOnlyUnitOfWork())
            {
                return _filterTypeRepository
                    .GetAll()
                    .Select(filterType => _entityFilterTypeConverter.Convert(filterType))
                    .ToArray();
            }
        }

        [HttpGet, Route(@"{id}")]
        public FilterType Get(int id)
        {
            var filter = GetCachedFilterTypes().SingleOrDefault(filterType => filterType.Id == id);

            if (filter == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return filter;
        }

        [HttpGet, Route(@"All")]
        public IEnumerable<FilterType> Get() => GetCachedFilterTypes().ToArray();

        [HttpPost, Route(@"")]
        public void Add(FilterType filterType)
        {
            var filterTypeEntity = _filterTypeConverter.Convert(filterType);

            using (var unitOfWork = _unitOfWorkFactory.CreateAquariumReadWriteUnitOfWork())
            {
                _filterTypeRepository.Add(filterTypeEntity);

                unitOfWork.Commit();
            }

            RefreshCache();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, FilterType filterType)
        {
            RefreshCache();
        }

        [HttpDelete, Route(@"{id}")]
        public void Delete(int id)
        {
            RefreshCache();
        }
    }
}