using System;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using Dnn.Bce.Dnn.Idopontfoglalas.Services;
using DotNetNuke.Entities.Users;
using System.Linq;
using System.Diagnostics.Eventing.Reader;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Controllers
{
    public class ReservationController : DnnController
    {
        private readonly IReservationService _reservationService;

        public ReservationController()
        {
            _reservationService = new ReservationService();
        }

        public ActionResult Index()
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();
            bool isAdmin = currentUser.IsInRole("Administrators");
            int userId = currentUser.UserID;

            var reservations = _reservationService.GetReservations(isAdmin, userId).OrderByDescending(r => r.StartTime);
            return View("Index", reservations);
        }

        public ActionResult Create()
        {
            UserInfo user = UserController.Instance.GetCurrentUserInfo();
            if (user != null && user.UserID > 0)
            {
                return View("Create");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult GetFullHours()
        {
            var service = new ReservationService();
            var fullHours = service.GetFullyBookedHours();

            return Json(fullHours, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(ReservationEntity reservation)
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();

            if (ModelState.IsValid)
            {
                reservation.CreatedAt = DateTime.Now;
                reservation.IsActive = true;
                reservation.CreatedBy = currentUser.UserID;
                reservation.EndTime = reservation.StartTime?.AddHours(2);

                var conflictingReservations = _reservationService.GetFullyBookedHours();
                var existingCount = _reservationService.CountReservationsInHour(reservation.StartTime.Value, reservation.EndTime.Value);
                if (existingCount >= 3)
                {
                    var occupiedTimes = string.Join(", ", conflictingReservations
                        .Where(r => r > DateTime.Now)
                        .OrderBy(r => r)
                        .Select(r => r.ToString("yyyy. MMMM dd. HH:mm", new System.Globalization.CultureInfo("hu-HU"))));

                    ModelState.AddModelError("", $"Erre az időpontra már nem tudsz foglalni. Az időpontod vagy múltbeli, vagy pedig a kiválasztott időpontra már nincs több szabad hely. Az alábbi időpontok foglaltak: {occupiedTimes}");
                    return View("Create", reservation);
                }

                _reservationService.CreateReservation(reservation);
                return RedirectToAction("Index");
            }

            return View("Create", reservation);
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();
            var reservation = _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            bool isAdmin = currentUser.IsInRole("Administrators");

            if (reservation.CreatedBy == currentUser.UserID || isAdmin)
            {
                _reservationService.CancelReservation(id);
            }

            return RedirectToAction("Index");
        }
    }
}
