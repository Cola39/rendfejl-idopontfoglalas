using System;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using Dnn.Bce.Dnn.Idopontfoglalas.Services;
using DotNetNuke.Entities.Users;
using System.Linq;

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

            var reservations = _reservationService.GetReservations(isAdmin, userId);
            return View("Index", reservations);
        }

        public ActionResult Create()
        {
            return View("Create");
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

                var existingCount = _reservationService.CountReservationsInHour(reservation.StartTime.Value, reservation.EndTime.Value);
                if (existingCount >= 3)
                {
                    ModelState.AddModelError("", "A kiválasztott időpontra már nincs több szabad hely.");
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
