using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface ILineInspectorServices
    {
        Task DeleteLiAsync(Guid liId);
        Task<IEnumerable<LineInspector>> GetLiListAsync();
        Task<LineInspector> GetLirAsync(Guid LiId);
        Task SaveLiAsync(LineInspector ope, bool isNewItem = false);
    }
}
