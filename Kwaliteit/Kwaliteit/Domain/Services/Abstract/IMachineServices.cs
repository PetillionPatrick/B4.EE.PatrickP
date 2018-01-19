using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IMachineServices
    {
        Task DeleteMachineAsync(Guid machineId);
        Task SaveMachine(Machine machine, bool isNewItem = false);
        Task<IEnumerable<Machine>> GetMachineListAsync(Guid beukId);
    }
}
