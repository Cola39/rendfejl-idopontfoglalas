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

using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Web.Caching;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Models
    {
        [TableName("Reservations")]
        [PrimaryKey(nameof(ID), AutoIncrement = true)]
        [Cacheable("Reservation", CacheItemPriority.Default, 20)]
        public class ReservationEntity
        {
            public int ID { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? CreatedAt { get; set; }
            public bool? IsActive { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public int? Quantity { get; set; }
        }

    }
