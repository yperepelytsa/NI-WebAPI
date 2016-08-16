using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace CMSApp.Models
{
    public class PageRepository:IPageRepository
    {   

        public PageRepository()
        {
           
        }

        public async Task<IEnumerable<Page>> GetAllPages(string searchTitle)
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (String.IsNullOrEmpty(searchTitle)) {
                    HttpResponseMessage response = await client.GetAsync("api/pages/list");

                    if (response.IsSuccessStatusCode)
                    {

                        return JsonConvert.DeserializeObject<Page[]>(await response.Content.ReadAsStringAsync());
                    }
                }
                else {
                    HttpResponseMessage response = await client.GetAsync("api/pages/list?title="+searchTitle);

                    if (response.IsSuccessStatusCode)
                    {

                        return JsonConvert.DeserializeObject<Page[]>(await response.Content.ReadAsStringAsync());
                    }

                }
                return null;
            }
        }



        public async Task<Page> GetPageByID(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/pages/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Page>(await response.Content.ReadAsStringAsync());
                }

                return null;
            }
        }

        public async Task InsertPage(Page Page)
        {
            var json = JsonConvert.SerializeObject(Page);
            

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders
       .Accept
       .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                HttpResponseMessage response = await client.PostAsync("api/pages/", new StringContent(json, Encoding.UTF8, "application/json"));

            }
        }

        public async Task DeletePage(int PageID)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders
       .Accept
       .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                HttpResponseMessage response = await client.DeleteAsync("api/pages/"+PageID);
      
            }
        }


        public async Task UpdatePage(Page Page)
        {

            var json = JsonConvert.SerializeObject(Page);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                var method = new HttpMethod("PATCH");

                var request = new HttpRequestMessage(method, "api/pages/"+Page.PageId)
                {
                    Content = new StringContent(json,Encoding.UTF8,"application/json")//CONTENT-TYPE header
                };

                HttpResponseMessage response = new HttpResponseMessage();
   
                try
                {
                    response = await client.SendAsync(request);

                }
                catch (TaskCanceledException e)
                {
                  Console.WriteLine("ERROR: " + e.ToString());
                }

            }

        }


    }
}
