using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Models
{
    [TableName("Reservations")]
    [PrimaryKey(nameof(ID), AutoIncrement = true)]
    [Cacheable("Reservation", CacheItemPriority.Default, 20)]
    public class ReservationEntity
    {
        public int ID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Time { get; set; }
        public int? Quantity { get; set; }
    }

}
