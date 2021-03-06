﻿using Kwaliteit.Domain.Models;
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
    class OperatorAPIService : IOperatorServices
    {
        HttpClient client;
        private Constants constant;

        public OperatorAPIService()
        {


            client = new HttpClient();
            constant = new Constants();
        }

        public async Task DeleteOperatorAsync(Guid operatorId)
        {
            var uri = new Uri(string.Format(constant.Host + constant.Port + "beuks/" + operatorId, string.Empty));
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

        public async Task<Operator> GetOperatorAsync(Guid operatorId)
        {
            var uri = constant.Host + constant.Port + "operators/" + operatorId;
            Operator ope = new Operator();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ope = JsonConvert.DeserializeObject<Operator>(content);
                }
                return ope;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Operator>> GetOperatorListAsync()
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "operators", string.Empty));
            List<Operator> operatoren = new List<Operator>();
            
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    operatoren = JsonConvert.DeserializeObject<List<Operator>>(content);
                    


                }
                return operatoren;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Operator>> GetTechnischOpeListAsync()
        {
            client = new HttpClient();
            constant = new Constants();
            var uri = new Uri(string.Format(constant.Host + constant.Port + "operators", string.Empty));
            List<Operator> operatoren = new List<Operator>();
            List<Operator> techni = new List<Operator>();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    operatoren = JsonConvert.DeserializeObject<List<Operator>>(content);
                    techni = operatoren.FindAll(i => i.Technisch == true).ToList();


                }
                return techni;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveOperator(Operator ope, bool isNewItem = false)
        {
            var uri = constant.Host + constant.Port + "operators";
            var json = JsonConvert.SerializeObject(ope);


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
                    uri += "/" + ope.Id;
                    response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("operator save ok");
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
