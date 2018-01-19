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
    class OrderAPIService : IOrderServices
    {
        HttpClient client;
        private Constants constant;

        public OrderAPIService()
        {


            client = new HttpClient();
            constant = new Constants();
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            var uri = constant.Host + constant.Port + "orders/" + orderId;
            Order order = new Order();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<Order>(content);
                }
                return order;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveOrder(Order order, bool isNewItem= false)
        {
            var uri = constant.Host + constant.Port + "orders";
            var json = JsonConvert.SerializeObject(order);


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
                    uri += "/" + order.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("order save ok");
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
