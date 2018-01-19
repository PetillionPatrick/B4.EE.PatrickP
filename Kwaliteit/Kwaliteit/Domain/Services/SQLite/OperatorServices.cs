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
    public class OperatorServices : SQLiteServiceBase, IOperatorServices
    {
        public async Task DeleteOperatorAsync(Guid operatorId)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<Operator>(operatorId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Operator>> GetOperatorListAsync()
        {
            return await Task.Run<IEnumerable<Operator>>(() =>
            {
                try
                {
                    var Operatoren = connection.GetAllWithChildren<Operator>().ToList();
                    
                    return Operatoren;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Operator>> GetTechnischOpeListAsync()
        {
            return await Task.Run<IEnumerable<Operator>>(() =>
            {
                try
                {
                    var Operatoren = connection.GetAllWithChildren<Operator>(i => i.Technisch == true).ToList();

                    return Operatoren;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<Operator> GetOperatorAsync(Guid operatorId)
        {
            return await Task.Run<Operator>(() =>
            {
                try
                {
                    return connection.Find<Operator>(operatorId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveOperator(Operator ope, bool isNewItem = false)
        {
            await Task.Run(() =>
            {
                try
                {
                    ope.Status = null;
                    
                    connection.InsertOrReplace(ope);
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
