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
            ThrowIfInvalidFilterType(filterType);

            var filterTypeEntity = _filterTypeConverter.Convert(filterType);

            try
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateAquariumReadWriteUnitOfWork())
                {
                    _filterTypeRepository.Add(filterTypeEntity);

                    unitOfWork.Commit();
                }
            }
            catch (DatabaseUpdateException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [HttpPatch, Route(@"{id}")]
        public void Update(int id, FilterType filterType)
        {
            ThrowIfInvalidFilterType(filterType);

            try
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateAquariumReadWriteUnitOfWork())
                {
                    var filterTypeEntity = _filterTypeRepository.Get(id);

                    filterTypeEntity.Name = filterType.Name;

                    unitOfWork.Commit();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (DatabaseUpdateException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            RefreshCache();
        }

        [HttpDelete, Route(@"{id}")]
        public void Delete(int id)
        {
            try
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateAquariumReadWriteUnitOfWork())
                {
                    _filterTypeRepository.Delete(id);

                    unitOfWork.Commit();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (DatabaseUpdateException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            RefreshCache();
        }

        private static void ThrowIfInvalidFilterType(FilterType filterType)
        {
            WebGuard.ThrowBadRequestIfNullOrWhitespace(filterType?.Name, nameof(filterType));
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
    }
}