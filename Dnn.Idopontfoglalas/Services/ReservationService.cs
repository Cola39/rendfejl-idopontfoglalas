using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public int CountReservationsInHour(DateTime startTime, DateTime endTime)
        {
            using (var context = DataContext.Instance())
            {
                var repo = context.GetRepository<ReservationEntity>();
                var reservations = repo.Get();

                return reservations.Count(r =>
                    r.IsActive == true &&
                    r.StartTime.HasValue && r.EndTime.HasValue &&
                    (
                        (startTime >= r.StartTime && startTime < r.EndTime) ||  // new starts during existing
                        (endTime > r.StartTime && endTime <= r.EndTime) ||      // new ends during existing
                        (startTime <= r.StartTime && endTime >= r.EndTime)      // new fully covers existing
                    )
                );
            }
        }

        public List<DateTime> GetFullyBookedHours()
        {
            using (var context = DataContext.Instance())
            {
                var repo = context.GetRepository<ReservationEntity>();
                var reservations = repo.Get()
                    .Where(r => r.IsActive == true && r.StartTime.HasValue && r.EndTime.HasValue)
                    .ToList();

                var grouped = reservations
                    .Select(r => new
                    {
                        HourStart = new DateTime(r.StartTime.Value.Year, r.StartTime.Value.Month, r.StartTime.Value.Day, r.StartTime.Value.Hour, 0, 0),
                        r.StartTime,
                        r.EndTime
                    })
                    .GroupBy(x => x.HourStart)
                    .Where(g => g.Count() >= 3)
                    .Select(g => g.Key)
                    .ToList();

                return grouped;
            }
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
