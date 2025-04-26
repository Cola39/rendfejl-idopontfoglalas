/*
' Copyright (c) 2025 Spongyabob Kft
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
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