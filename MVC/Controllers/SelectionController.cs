using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class SelectionController : ControllerBase
    {
        [HttpPost]
        public int AddItemToSelection(int id, bool mode)
        {
            if (mode == true)
            {
                List<int> currentSelection = new List<int>();
                string selectionJson = HttpContext.Session.GetString("productsSelection");
                if (!string.IsNullOrEmpty(selectionJson))
                {
                    currentSelection = JsonConvert.DeserializeObject<List<int>>(selectionJson);
                }
                if (!currentSelection.Contains(id))
                {
                    currentSelection.Add(id);
                }
                selectionJson = JsonConvert.SerializeObject(currentSelection);
                HttpContext.Session.SetString("productsSelection", selectionJson);
                Console.WriteLine(HttpContext.Session.GetString("productsSelection"));
                Console.WriteLine(HttpContext.Session.GetString("productsSelection"));
                Console.WriteLine(HttpContext.Session.GetString("productsSelection"));
            }
            else
            {
                List<int> currentSelection = new List<int>();
                string selectionJson = HttpContext.Session.GetString("productsSelection");
                if (!string.IsNullOrEmpty(selectionJson))
                {
                    currentSelection = JsonConvert.DeserializeObject<List<int>>(selectionJson);
                    currentSelection.Remove(id);
                    selectionJson = JsonConvert.SerializeObject(currentSelection);
                    HttpContext.Session.SetString("productsSelection", selectionJson);
                }
            }
            return 0;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetSelection([FromServices] IHttpClientFactory httpClientFactory)
        {
            var selectedProducts = HttpContext.Session.GetString("productsSelection");
            var client = httpClientFactory.CreateClient();
            var content = new StringContent('"' + selectedProducts + '"', Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://www.alevelwebsite.com:5003/api/v1/BasketBff/AddProduct", content);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var cars = JsonConvert.DeserializeObject<List<CatalogItem>>(await response.Content.ReadAsStringAsync());
                foreach (var item in cars)
                {
                    Console.WriteLine(item.CarName);
                }
                return new JsonResult(cars);
            }
            return new JsonResult(null);
        }

    }
}
