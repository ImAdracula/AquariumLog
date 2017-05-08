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
using ChemicalTypeEntity = JessicasAquariumMonitor.Data.Entities.ChemicalType;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Chemical")]
    public sealed class ChemicalTypeController : ApiController
    {
        private const string ChemicalTypesCacheKey = @"ChemicalTypes";

        private readonly ICacheProvider _cacheProvider;
        private readonly IConverter<ChemicalType, ChemicalTypeEntity> _chemicalTypeConverter;
        private readonly IChemicalTypeRepository _chemicalTypeRepository;
        private readonly IConverter<ChemicalTypeEntity, ChemicalType> _entityChemicalTypeConverter;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ChemicalTypeController(IUnitOfWorkFactory unitOfWorkFactory,
            IChemicalTypeRepository chemicalTypeRepository,
            ICacheProvider cacheProvider, IConverter<ChemicalTypeEntity, ChemicalType> entityChemicalTypeConverter,
            IConverter<ChemicalType, ChemicalTypeEntity> chemicalTypeConverter)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _chemicalTypeRepository = chemicalTypeRepository;
            _cacheProvider = cacheProvider;
            _entityChemicalTypeConverter = entityChemicalTypeConverter;
            _chemicalTypeConverter = chemicalTypeConverter;
        }

        [HttpGet, Route(@"{id}")]
        public ChemicalType Get(int id)
        {
            var chemical = GetCachedChemicalTypes().SingleOrDefault(chemicalType => chemicalType.Id == id);

            if (chemical == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return chemical;
        }

        [HttpGet, Route(@"All")]
        public IEnumerable<ChemicalType> Get() => GetCachedChemicalTypes().ToArray();

        [HttpPost, Route(@"")]
        public void Add(ChemicalType chemicalType)
        {
            ThrowIfInvalidChemicalType(chemicalType);

            var chemicalTypeEntity = _chemicalTypeConverter.Convert(chemicalType);

            try
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateAquariumReadWriteUnitOfWork())
                {
                    _chemicalTypeRepository.Add(chemicalTypeEntity);

                    unitOfWork.Commit();
                }
            }
            catch (DatabaseUpdateException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [HttpPatch, Route(@"{id}")]
        public void Update(int id, ChemicalType chemicalType)
        {
            ThrowIfInvalidChemicalType(chemicalType);

            try
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateAquariumReadWriteUnitOfWork())
                {
                    var chemicalTypeEntity = _chemicalTypeRepository.Get(id);

                    chemicalTypeEntity.Name = chemicalType.Name;

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
                    _chemicalTypeRepository.Delete(id);

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

        private static void ThrowIfInvalidChemicalType(ChemicalType chemicalType)
        {
            WebGuard.ThrowBadRequestIfNullOrWhitespace(chemicalType?.Name, nameof(chemicalType));
        }

        private void RefreshCache()
        {
            _cacheProvider.Remove(ChemicalTypesCacheKey);

            GetCachedChemicalTypes();
        }

        private IEnumerable<ChemicalType> GetCachedChemicalTypes()
            => _cacheProvider.GetOrAdd(ChemicalTypesCacheKey, GetChemicalTypesFromDatabase, TimeSpan.FromDays(1));

        private IEnumerable<ChemicalType> GetChemicalTypesFromDatabase()
        {
            using (_unitOfWorkFactory.CreateAquariumReadOnlyUnitOfWork())
            {
                return _chemicalTypeRepository
                    .GetAll()
                    .Select(chemicalType => _entityChemicalTypeConverter.Convert(chemicalType))
                    .ToArray();
            }
        }
    }
}