using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:[port]");
            var response = httpClient.GetAsync("api/values/key1").Result;
            if (!response.IsSuccessStatusCode) return Content("An error has occurred");
            var result = response.Content.ReadAsStringAsync().Result;
            return Content(result);

        }
    }
}