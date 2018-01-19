using Kwaliteit.Domain.Models;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IAfkeurServices
    {
        Task<Position> HuidigeLocatie();
        Task SetOptionsBarcode();
    }
}
