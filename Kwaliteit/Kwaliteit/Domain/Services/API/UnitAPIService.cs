using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.API
{
    class UnitAPIService : IUnitServices
    {
        HttpClient client;
        private Constants constant;

        public UnitAPIService()
        {


            client = new HttpClient();
            constant = new Constants();
        }

        public async Task<Unit> GetUnitAsync(Guid unitId)
        {
            var uri = constant.Host + constant.Port + "units/" + unitId;
            Unit unit = new Unit();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    unit = JsonConvert.DeserializeObject<Unit>(content);
                }
                return unit;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Unit>> GetUnitListAsync(Guid statusId)
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "units", string.Empty));
            List<Unit> units = new List<Unit>();
            List<Unit> u = new List<Unit>();


            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    units = JsonConvert.DeserializeObject<List<Unit>>(content);



                }
                u = units.FindAll(i => i.StatusId == statusId);
                return units;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveUnit(Unit unit, bool isNewItem = false)
        {
            var uri = constant.Host + constant.Port + "units";
            var json = JsonConvert.SerializeObject(unit);


            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));


                HttpResponseMessage response = null;
                if (isNewItem)
                {

                    response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }
                else
                {
                    uri += "/" + unit.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("unit save ok");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
