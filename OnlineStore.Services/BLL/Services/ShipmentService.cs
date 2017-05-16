using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class ShipmentService: IShipmentService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public ShipmentService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Shipment GetShipment(int? shipmentId)
        {
            using (_unitOfWork)
            {
                var shipment = _unitOfWork.GetRepository<Data.Entities.Shipment>().GetById(shipmentId);
                return shipment;
            }
        }

        public void UpdateShipment(Shipment shipment)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Shipment>().Update(shipment);
                _unitOfWork.Save();
            }
        }

        public void CreateShipment(Shipment shipment)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Shipment>().Create(shipment);
                _unitOfWork.Save();
            }
        }

        public void DeleteShipment(int? shipmentId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Shipment>().Delete(shipmentId);
                _unitOfWork.Save();
            }
        }

        public List<Shipment> GetShipments()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Shipment>().All();
                
                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
                    case "CreatedOn":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.CreatedOn) :
                                 query.OrderByDescending(c => c.CreatedOn));
                        break;
                    case "CreatedBy":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.CreatedBy) :
                                 query.OrderByDescending(c => c.CreatedBy));
                        break;
                    case "ModifiedOn":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ModifiedOn) :
                                 query.OrderByDescending(c => c.ModifiedOn));
                        break;
                    case "ModifiedBy":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ModifiedBy) :
                                 query.OrderByDescending(c => c.ModifiedBy));
                        break;
                }

                // Fitler
                if (!string.IsNullOrEmpty(Filter?.Keyword))
                {
                    //query = query.Where(x => x.Name.Contains(Filter.Keyword)
                    //                         || x.ShortDescrition.Contains(Filter.Keyword)
                    //                         || x.Description.Contains(Filter.Keyword));
                }

                // Paging
                Pagination.TotalRows = query.Count();
                Pagination.PageCount =
                    (int)Math.Ceiling((double)query.Count() / Pagination.PageSize);

                var pageIndex = Pagination.CurrentPage - 1;

                query = Pagination.PageSize == 0 ? query.AsQueryable() : query.AsQueryable().Skip(pageIndex * Pagination.PageSize).Take(Pagination.PageSize);

                return query.ToList();

            }
        }
    }
}
