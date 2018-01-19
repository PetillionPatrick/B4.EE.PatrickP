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
    class LineInspecotrAPIService : ILineInspectorServices
    {
        HttpClient client;
        private Constants constant;

        public LineInspecotrAPIService()
        {


            client = new HttpClient();
            constant = new Constants();
        }

        public async Task DeleteLiAsync(Guid liId)
        {
            var uri = new Uri(string.Format(constant.Host + constant.Port + "LineInspectors/" + liId, string.Empty));
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

        public async Task<IEnumerable<LineInspector>> GetLiListAsync()
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "LineInspectors", string.Empty));
            List<LineInspector> li = new List<LineInspector>();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    li = JsonConvert.DeserializeObject<List<LineInspector>>(content);
                }
                return li;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<LineInspector> GetLirAsync(Guid LiId)
        {
            var uri = constant.Host + constant.Port + "LineInspectors/" + LiId;
            LineInspector lineInspector = new LineInspector();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    lineInspector = JsonConvert.DeserializeObject<LineInspector>(content);
                }
                return lineInspector;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveLiAsync(LineInspector li, bool isNewItem = false)
        {
            var uri = constant.Host + constant.Port + "LineInspectors";
            var json = JsonConvert.SerializeObject(li);


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
                    uri += "/" + li.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("li save ok");
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
