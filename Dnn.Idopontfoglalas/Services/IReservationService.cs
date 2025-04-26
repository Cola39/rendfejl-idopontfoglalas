using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Services
{
    public interface IReservationService
    {
        ReservationEntity GetReservationById(int id);
        IEnumerable<ReservationEntity> GetReservations(bool isAdmin, int userId);
        void CreateReservation(ReservationEntity reservation);
        bool IsAvailable(int id);
        void CancelReservation(int id);

    }
}
