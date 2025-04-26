using System;
using System.Web.Http;
using DotNetNuke.Entities.Users;
using DotNetNuke.Web.Api;
using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using Dnn.Bce.Dnn.Idopontfoglalas.Services;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Controllers
{
    public class ReservationApiController : DnnApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationApiController()
        {
            _reservationService = new ReservationService();
        }

        [HttpPost]
        public IHttpActionResult Create(ReservationEntity reservation)
        {
            try
            {
                var currentUser = UserController.Instance.GetCurrentUserInfo();

                // Ellenőrizd, hogy be van-e jelentkezve
                if (currentUser == null || currentUser.UserID <= 0)
                {
                    return Unauthorized(); // 401 hiba (nem engedélyezett)
                }

                if (reservation == null)
                {
                    return BadRequest("Érvénytelen adatok érkeztek.");
                }

                reservation.CreatedAt = DateTime.Now;
                reservation.IsActive = true;
                reservation.CreatedBy = currentUser.UserID;

                _reservationService.CreateReservation(reservation);

                return Ok(new { success = true, message = "Foglalás sikeresen elmentve." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
