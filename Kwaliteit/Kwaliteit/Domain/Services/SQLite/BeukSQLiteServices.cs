using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.SQLite
{
    public class BeukSQLiteServices : SQLiteServiceBase, IBeukServices
    {
        public async Task DeleteBeuk(Guid beukId)
        {
            await Task.Run(() =>
            {
                try
                {
                    int controle = 0;

                    Beuk beuk = connection.Table<Beuk>().Where(e => e.Id == beukId).FirstOrDefault();
                    if ((beuk.Machines == null) || (beuk.Machines.Count == 0))
                    {
                        controle = 1;
                        connection.Delete<Beuk>(beukId);
                        return controle;
                    }
                    else return controle;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Machine>> GetMachineList(Guid beukId)
        {
            return await Task.Run< IEnumerable<Machine>>(() =>
            {
                try
                {
                    var machines = connection.GetAllWithChildren<Machine>(t => t.BeukId == beukId, false).ToList();
                    return machines;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveBeuk(Beuk beuk, bool isNewItem = false)
        {
            await Task.Run(() =>
            {
                try
                {
                    beuk.Owner = null;
                    beuk.Machines = null;
                    connection.InsertOrReplace(beuk);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<Beuk> GetBeuk(Guid beukId)
        {
            return await Task.Run<Beuk>(() =>
            {
                try
                {
                    var b = connection.Find<Beuk>(beukId);
                    b.Machines = connection.GetAllWithChildren<Machine>(t => t.BeukId == beukId, false).ToList();
                    return b;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Beuk>> GetBeukList(Guid ownerId)
        {
            return await Task.Run< IEnumerable<Beuk>>( () =>
            {
                try
                {
                    var beuken = connection.GetAllWithChildren<Beuk>(t => t.OwnerId == ownerId).ToList();
                    return beuken;
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
