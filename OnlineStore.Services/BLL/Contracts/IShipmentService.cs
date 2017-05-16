using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IShipmentService
    {
        Shipment GetShipment(int? shipmentId);
        void UpdateShipment(Shipment shipment);
        void CreateShipment(Shipment shipment);
        void DeleteShipment(int? shipmentId);
        List<Shipment> GetShipments();
    }
}
