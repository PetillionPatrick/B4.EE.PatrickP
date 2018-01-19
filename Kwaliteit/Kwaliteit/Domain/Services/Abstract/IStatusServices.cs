using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IStatusServices
    {
        Task DeleteStatusAsync(Guid statusId);
        Task<Status> GetStatusAsync(Guid statusId);
        Task SaveStatus(Status Status, bool isNewItem = false);
        Task<IEnumerable<Status>> GetStatusListAsync(Guid machineId);

    }
}
