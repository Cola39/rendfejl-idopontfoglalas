using DotNetNuke.Framework;
using DotNetNuke.Framework.JavaScriptLibraries;
using Dnn.Bce.Dnn.Idopontfoglalas.Models;
using Dnn.Bce.Dnn.Idopontfoglalas.Services;
using System;
using System.Net;
using DotNetNuke.Web.Api;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Controllers.Api
{
    public class ReservationApiController : DnnApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationApiController()
        {
            _reservationService = new ReservationService();
        }

        [HttpGet]
        public HttpResponseMessage GetReservations()
        {
            try
            {
                bool isAdmin = UserInfo.IsInRole("Administrators");
                int userId = UserInfo.UserID;
                var reservations = _reservationService.GetReservations(isAdmin, userId);

                return Request.CreateResponse(HttpStatusCode.OK, reservations);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateReservation(ReservationEntity reservation)
        {
            try
            {                
                reservation.CreatedAt = DateTime.Now;
                reservation.IsActive = true;
                reservation.CreatedBy = UserInfo.UserID;

                _reservationService.CreateReservation(reservation);

                return Request.CreateResponse(HttpStatusCode.Created, reservation);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage CancelReservation(int id)
        {
            try
            {
                var reservation = _reservationService.GetReservationById(id);
                if (reservation == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Reservation not found.");
                }

                bool isAdmin = UserInfo.IsInRole("Administrators");
                if (reservation.CreatedBy == UserInfo.UserID || isAdmin)
                {
                    _reservationService.CancelReservation(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Unauthorized.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}