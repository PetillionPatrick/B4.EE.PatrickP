using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.SQLite
{
    public class StatusServices: SQLiteServiceBase, IStatusServices
    {
        public async Task DeleteStatusAsync(Guid statusId)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<Status>(statusId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Status>> GetStatusListAsync(Guid machineId)
        {
            return await Task.Run<IEnumerable<Status>>(() =>
            {
                try
                {
                    var statussen = connection.GetAllWithChildren<Status>(t => t.MachineId == machineId, false).ToList();
                    return statussen;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<Status> GetStatusAsync(Guid statusId)
        {
            return await Task.Run<Status>(() =>
            {
                try
                {
                    return connection.Find<Status>(statusId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveStatus(Status Status, bool isNewItem = false)
        {
            await Task.Run( () =>
            {
                try
                {
                    var unit = Status.Units.ElementAt(0);
                    var order = unit.Order;


                    Status.Machine = null;
                    Status.Operator = null;
                    Status.Units = null;

                    connection.InsertOrReplace(order);
                    connection.InsertOrReplace(unit);
                    connection.InsertOrReplace(Status);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
