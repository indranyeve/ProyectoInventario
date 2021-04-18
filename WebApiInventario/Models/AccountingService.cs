using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace WebApiInventario.Models
{
    public class AccountingParameters
    {
        public int id { get; set; }
        public string description { get; set; }
        public int idAuxiliarSystem { get; set; }
        public string movementType { get; set; }
        public int account { get; set; }
        public decimal seatAmount { get; set; }
        public DateTime? entreyDate { get; set; }
        public bool state { get; set; }
    }

    public class AccountingService
    {
        const string URL = "https://b02767518087.ngrok.io/api/accountingEntry";
        public AccountingParameters Contabilizar(AccountingParameters model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(URL);
                var jsonModel = JsonConvert.SerializeObject(model);
                var resquestContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                var response = client.PostAsync("", resquestContent).Result;
                var contents = response.Content.ReadAsStringAsync();
                var mdl = JsonConvert.DeserializeObject<AccountingParameters>(contents.Result);
                return mdl;
            }
        }
    }
}