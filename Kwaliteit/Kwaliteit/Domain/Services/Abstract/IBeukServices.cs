using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IBeukServices
    {
        Task DeleteBeuk(Guid beukId);
        Task<IEnumerable<Beuk>> GetBeukList(Guid owner);
        Task<Beuk> GetBeuk(Guid beukId);
        Task SaveBeuk(Beuk beuk, bool isNewItem = false);
    }
}
