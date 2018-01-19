using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IOperatorServices
    {
        Task DeleteOperatorAsync(Guid operatorId);
        Task<IEnumerable<Operator>> GetOperatorListAsync();
        Task<Operator> GetOperatorAsync(Guid operatorId);
        Task SaveOperator(Operator ope, bool isNewItem = false);
        Task<IEnumerable<Operator>> GetTechnischOpeListAsync();
    }
}
