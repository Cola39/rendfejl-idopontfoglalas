using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Services
{
    public class ReservationService : IReservationService
    {
        public ReservationEntity GetReservationById(int id)
        {
            using (var context = DataContext.Instance())
            {
                var repo = context.GetRepository<ReservationEntity>();
                return repo.GetById(id);
            }
        }

        public IEnumerable<ReservationEntity> GetReservations(bool isAdmin, int userId)
        {
            using (var context = DataContext.Instance())
            {
                var repo = context.GetRepository<ReservationEntity>();

                if (isAdmin)
                {
                    return repo.Get().ToList();
                }
                else
                {
                    return repo.Find("WHERE CreatedBy = @0", userId).ToList();
                }
            }
        }


        public void CreateReservation(ReservationEntity reservation)
        {
            using (var context = DataContext.Instance())
            {
                var repo = context.GetRepository<ReservationEntity>();
                repo.Insert(reservation);
            }
        }

        public bool IsAvailable(int id)
        {
            var reservation = GetReservationById(id);
            return reservation != null && reservation.IsActive == true;
        }

        public void CancelReservation(int id)
        {
            var reservation = GetReservationById(id);
            if (reservation != null)
            {
                reservation.IsActive = false;
                using (var context = DataContext.Instance())
                {
                    var repo = context.GetRepository<ReservationEntity>();
                    repo.Update(reservation);
                }
            }
        }
    }
}
