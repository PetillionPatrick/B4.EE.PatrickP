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
    public class UnitServices : SQLiteServiceBase, IUnitServices
    {
        public async Task<Unit> GetUnitAsync(Guid unitId)
        {
            return await Task.Run<Unit>(() =>
            {
                try
                {
                    var unit = connection.Find<Unit>(o => o.Id == unitId);

                    return unit;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Unit>> GetUnitListAsync(Guid statusId)
        {
            return await Task.Run<IEnumerable<Unit>>(() =>
            {
                try
                {
                    var units = connection.GetAllWithChildren<Unit>(i => i.StatusId == statusId).ToList();

                    return units;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveUnit(Unit unit, bool isNewItem = false)
        {
            await Task.Run(() =>
            {
                try
                {
                    unit.Order = null;
                    unit.Status = null;

                    connection.InsertOrReplace(unit);
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
