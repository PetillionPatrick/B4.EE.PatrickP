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
    public class MachineSQLiteServices: SQLiteServiceBase, IMachineServices
    {
        public async Task DeleteMachineAsync(Guid machineId)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<Machine>(machineId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Machine>> GetMachineListAsync(Guid beukId)
        {
            return await Task.Run<IEnumerable<Machine>>(() =>
            {
                try
                {
                    var machines = connection.GetAllWithChildren<Machine>(t => t.BeukId == beukId, false).ToList();
                    //var m = machines.OrderBy(s => s.Naam).ToList();
                    return machines;

                    //Beuk beuk = connection.Table<Beuk>().Where(e => e.Id == beukId).FirstOrDefault();
                    //if (beuk != null)
                    //    connection.GetChildren<Beuk>(beuk, true);
                    //return beuk;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        

        public async Task<Machine> GetMachineAsync(Guid machineID)
        {
            return await Task.Run<Machine>(() =>
            {
                try
                {
                    return connection.Find<Machine>(machineID);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveMachine(Machine machine, bool isNewItem = false)
        {
            await Task.Run(() =>
            {
                try
                {
                    var order = new Order
                    {
                        Id = Guid.Empty
                    };

                    var unit = new Unit
                    {
                        Id = Guid.Empty,
                        OrderId = order.Id
                    };


                    var status = new Status
                    {
                        Datum = DateTime.Now,
                        Id = Guid.NewGuid(),
                        MachineId = machine.Id,
                        Operator = new Operator(),
                        OperatorId = Guid.Empty,
                    };

                    status.GekozenStatus = status.StatusKeuze.ElementAt(0);
                    machine.Beuk = null;
                    machine.Statussen = null;
                    connection.InsertOrReplace(order);
                    connection.InsertOrReplace(unit);
                    connection.InsertOrReplace(machine);
                    connection.InsertOrReplace(status);

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
