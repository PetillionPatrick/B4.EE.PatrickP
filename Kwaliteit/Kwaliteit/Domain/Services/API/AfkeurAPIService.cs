using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;


namespace Kwaliteit.Domain.Services.API
{
    public class AfkeurAPIService : IAfkeurServices
    {
        public async Task<Position> HuidigeLocatie()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetLastKnownLocationAsync();
            return position;
        }

        public async Task SetOptionsBarcode()
        {
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
    ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
};

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan(options);
        }

    }
}
