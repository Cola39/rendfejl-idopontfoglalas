using System;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using Dnn.Bce.Dnn.Idopontfoglalas.Services;
using DotNetNuke.Entities.Users;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Controllers
{
    public class ReservationController : DnnController
    {
        private readonly IReservationService _reservationService;

        public ReservationController()
        {
            _reservationService = new ReservationService();
        }

        // GET: /Reservation/Index
        public ActionResult Index()
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();
            bool isAdmin = currentUser.IsInRole("Administrators");
            int userId = currentUser.UserID;

            var reservations = _reservationService.GetReservations(isAdmin, userId);
            return View("Index", reservations);
        }

        // GET: /Reservation/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: /Reservation/Create
        [HttpPost]
        public ActionResult Create(ReservationEntity reservation)
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();

            if (ModelState.IsValid)
            {
                reservation.CreatedAt = DateTime.Now;
                reservation.IsActive = true;
                reservation.CreatedBy = currentUser.UserID;

                _reservationService.CreateReservation(reservation);

                return RedirectToAction("Index");
            }

            return View("Create", reservation);
        }

        // POST: /Reservation/Delete
        [HttpPost]
        public ActionResult Delete(int id)
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
