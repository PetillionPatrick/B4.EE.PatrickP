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
    public class MachineAPIService : IMachineServices
    {
        HttpClient client;
        private Constants constant;

        public MachineAPIService()
        {
            client = new HttpClient();
            constant = new Constants();
        }

        public async Task DeleteMachineAsync(Guid machineId)
        {
            var uri = new Uri(string.Format(constant.Host + constant.Port + "machines/" + machineId, string.Empty));
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

        public async Task<Machine> GetMachineAsync(Guid machineID)
        {
            var uri = constant.Host + constant.Port + "machines/" + machineID;
            Machine machine = new Machine();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    machine = JsonConvert.DeserializeObject<Machine>(content);
                }
                return machine;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Machine>> GetMachineListAsync(Guid beukId)
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "machines", string.Empty));
            List<Machine> machinen = new List<Machine>();
            List<Machine> m = new List<Machine>();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    machinen = JsonConvert.DeserializeObject<List<Machine>>(content);
                }
                m = machinen.FindAll(i => i.BeukId == beukId);
                return m;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        

        public async Task SaveMachine(Machine machine, bool isNewItem = false)
        {
            var uri = constant.Host + constant.Port + "machines";
            var json = JsonConvert.SerializeObject(machine);

            var order = new Order
            {
                Id = Guid.Empty
            };

           

            var status = new Status
            {
                Datum = DateTime.Now,
                Id = Guid.NewGuid(),
                MachineId = machine.Id,
                OperatorId = Guid.Empty,
                LiId = Guid.Empty,
                OrderId = order.Id
            };

            var unit = new Unit
            {
                Id = Guid.Empty,
                OrderId = order.Id,
                StatusId = status.Id
            };

            status.GekozenStatus = status.StatusKeuze.ElementAt(0);


            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));


                HttpResponseMessage response = null;
                if (isNewItem)
                {

                    response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

                    var u = new Uri(string.Format(constant.Host + constant.Port + "orders"));
                    json = JsonConvert.SerializeObject(order);
                    var responseOrder = await client.PostAsync(u, new StringContent(json, Encoding.UTF8, "application/json"));

                    if (responseOrder.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("order save ok");
                    }

                    u = new Uri(string.Format(constant.Host + constant.Port + "status"));
                    json = JsonConvert.SerializeObject(status);
                    var responseStatus = await client.PostAsync(u, new StringContent(json, Encoding.UTF8, "application/json"));

                    if (responseStatus.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("status save ok");
                    }

                    u = new Uri(string.Format(constant.Host + constant.Port + "units"));
                    json = JsonConvert.SerializeObject(unit);
                    var responseUnit = await client.PostAsync(u, new StringContent(json, Encoding.UTF8, "application/json"));

                    if (responseUnit.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("unit save ok");
                    }
                }
                else
                {
                    uri += "/" + machine.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("machine save ok");
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
