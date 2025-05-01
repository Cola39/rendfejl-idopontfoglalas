using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using System;
using System.Collections.Generic;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Services
{
    public interface IReservationService
    {
        ReservationEntity GetReservationById(int id);
        IEnumerable<ReservationEntity> GetReservations(bool isAdmin, int userId);
        void CreateReservation(ReservationEntity reservation);
        int CountReservationsInHour(DateTime startTime, DateTime endTime);
        List<DateTime> GetFullyBookedHours();
        void CancelReservation(int id);

    }
}
