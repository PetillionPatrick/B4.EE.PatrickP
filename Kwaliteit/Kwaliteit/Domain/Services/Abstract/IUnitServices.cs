using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IUnitServices
    {
        Task<Unit> GetUnitAsync(Guid unitId);
        Task<IEnumerable<Unit>> GetUnitListAsync(Guid statusId);
        Task SaveUnit(Unit unit, bool isNewItem = false);
    }
}
