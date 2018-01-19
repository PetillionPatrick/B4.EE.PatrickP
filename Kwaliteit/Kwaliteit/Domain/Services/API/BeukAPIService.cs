using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.API
{
    public class BeukAPIService : IBeukServices
    {
        HttpClient client;
        private Constants constant;

        public BeukAPIService()
        {
            

            client = new HttpClient();
            constant = new Constants();
        }

        public async Task DeleteBeuk(Guid beukId)
        {
            var uri = new Uri(string.Format(constant.Host + constant.Port + "beuks/" + beukId, string.Empty));
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

        public async Task<Beuk> GetBeuk(Guid beukId)
        {
            var uri = constant.Host + constant.Port + "beuks/" + beukId;
            Beuk beuk = new Beuk();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    beuk = JsonConvert.DeserializeObject<Beuk>(content);
                }
                return beuk;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Beuk>> GetBeukList(Guid owner)
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "beuks", string.Empty));
            List<Beuk> beuken = new List<Beuk>();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    beuken = JsonConvert.DeserializeObject<List<Beuk>>(content);
                }
                return beuken;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        

        public async Task SaveBeuk(Beuk beuk, bool isNewItem = false)
        {
            var uri = constant.Host + constant.Port + "beuks";
            var json = JsonConvert.SerializeObject(beuk);


            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");
                
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));
               

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    
                    response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }
                else
                {
                    uri += "/" + beuk.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Beuk save ok");
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
