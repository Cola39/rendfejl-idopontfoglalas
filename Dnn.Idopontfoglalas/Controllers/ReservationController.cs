using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using Dnn.Bce.Dnn.Idopontfoglalas.Services;

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
            bool isAdmin = User.IsInRole("Administrators");
            int userId = User.UserID;

            var reservations = _reservationService.GetReservations(isAdmin, userId);

            return View(reservations);
        }


    }

}