using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.API
{
    class StausAPIService : IStatusServices
    {
        HttpClient client;
        private Constants constant;

        public StausAPIService()
        {


            client = new HttpClient();
            constant = new Constants();
        }

        public async Task DeleteStatusAsync(Guid statusId)
        {
            var uri = new Uri(string.Format(constant.Host + constant.Port + "beuks/" + statusId, string.Empty));
            try
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Delete is ok");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Status> GetStatusAsync(Guid statusId)
        {
            var uri = constant.Host + constant.Port + "status/" + statusId;
            Status status = new Status();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    status = JsonConvert.DeserializeObject<Status>(content);
                }
                return status;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Status>> GetStatusListAsync(Guid machineId)
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "status", string.Empty));
            List<Status> statussen = new List<Status>();
            List<Status> s = new List<Status>();

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    statussen = JsonConvert.DeserializeObject<List<Status>>(content);
                }
                s = statussen.FindAll(i => i.MachineId == machineId);
                s = s.OrderByDescending(t => t.Datum).ToList();
                return s;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveStatus(Status Status, bool isNewItem = false)
        {
            var uri = constant.Host + constant.Port + "status";
            var json = JsonConvert.SerializeObject(Status);

            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));


                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    json = JsonConvert.SerializeObject(Status);
                    response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }
                else
                {
                    uri += "/" + Status.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("status save ok");
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
