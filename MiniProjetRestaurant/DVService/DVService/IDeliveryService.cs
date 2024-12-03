using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DVService
{
    [ServiceContract]
    public interface IDeliveryService
    {
        [OperationContract]
        Delivery GetDeliveryDetails(long id);

        [OperationContract]
        List<Delivery> GetDeliveriesByOrderId(long orderId);

        [OperationContract]
        string UpdateDeliveryStatus(long id, string status);

        [OperationContract]

        string AddDelivery(Delivery newDelivery);
    }

    public class Delivery
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string DeliveryAddress { get; set; }
        public string Contact { get; set; }
        public string EstimatedTime { get; set; }
        public string Status { get; set; } // Scheduled, In Transit, Delivered, Cancelled

    
        
    }
}
