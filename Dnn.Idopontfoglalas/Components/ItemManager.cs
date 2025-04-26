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

using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Dnn.Bce.Dnn.Idopontfoglalas.Models;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Components
{
    interface IItemManager
    {
        void CreateItem(ReservationEntity t);
        void DeleteItem(int itemId, int moduleId);
        void DeleteItem(ReservationEntity t);
        IEnumerable<ReservationEntity> GetItems(int moduleId);
        ReservationEntity GetItem(int itemId, int moduleId);
        void UpdateItem(ReservationEntity t);
    }

    class ItemManager: ServiceLocator<IItemManager, ItemManager>, IItemManager
    {
        public void CreateItem(ReservationEntity t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ReservationEntity > ();
                rep.Insert(t);
            }
        }

        public void DeleteItem(int itemId, int moduleId)
        {
            var t = GetItem(itemId, moduleId);
            DeleteItem(t);
        }

        public void DeleteItem(ReservationEntity t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ReservationEntity > ();
                rep.Delete(t);
            }
        }

        public IEnumerable<ReservationEntity> GetItems(int moduleId)
        {
            IEnumerable<ReservationEntity > t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ReservationEntity > ();
                t = rep.Get(moduleId);
            }
            return t;
        }

    public ReservationEntity GetItem(int itemId, int moduleId)
    {
            ReservationEntity t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ReservationEntity > ();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public void UpdateItem(ReservationEntity t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ReservationEntity > ();
                rep.Update(t);
            }
        }

        protected override System.Func<IItemManager> GetFactory()
        {
            return () => new ItemManager();
        }
    }
}
