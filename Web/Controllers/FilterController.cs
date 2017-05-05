using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using JessicasAquariumMonitor.Data.Base;
using JessicasAquariumMonitor.Data.Repositories;
using JessicasAquariumMonitor.Helpers.Caching;
using JessicasAquariumMonitor.Helpers.General;
using FilterTypeEntity = JessicasAquariumMonitor.Data.Entities.FilterType;
using JessicasAquariumMonitor.Types;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Filter")]
    public sealed class FilterTypeController : ApiController
    {
        private const string FilterTypesCacheKey = @"FilterTypes";

        private readonly ICacheProvider _cacheProvider;
        private readonly IConverter<FilterTypeEntity, FilterType> _filterTypeConverter;
        private readonly IFilterTypeRepository _filterTypeRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public FilterTypeController(IUnitOfWorkFactory unitOfWorkFactory, IFilterTypeRepository filterTypeRepository,
            ICacheProvider cacheProvider, IConverter<FilterTypeEntity, FilterType> filterTypeConverter)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _filterTypeRepository = filterTypeRepository;
            _cacheProvider = cacheProvider;
            _filterTypeConverter = filterTypeConverter;
        }

        private IEnumerable<FilterType> RefreshCache()
        {
            _cacheProvider.Remove(FilterTypesCacheKey);

            return GetCachedFilterTypes();
        }

        private IEnumerable<FilterType> GetCachedFilterTypes()
        {
            using (_unitOfWorkFactory.CreateAquariumReadOnlyUnitOfWork())
            {
                return _cacheProvider.GetOrAdd(FilterTypesCacheKey, GetFilterTypesFromDatabase, TimeSpan.FromDays(1));
            }
        }

        private IEnumerable<FilterType> GetFilterTypesFromDatabase() => _filterTypeRepository
            .GetAll()
            .Select(filterType => _filterTypeConverter.Convert(filterType));

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
        public IEnumerable<FilterType> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add(FilterType filterType)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, FilterType filterType)
        {
            throw new NotImplementedException();
        }

        [HttpDelete, Route(@"{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}