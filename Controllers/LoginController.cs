using Login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Logins login)
        {

            var validUsers = new List<Logins>
            {
              new Logins {username = "admin", password = "admin"}
              };

            if (validUsers.FirstOrDefault(f =>
                    f.username.ToLower() == login.username.ToLower()
                    && f.password == login.password) != null)
            {


                return RedirectToAction("GetCar");
            }

            ViewBag.Error = "Invalid account credentials";
            return View();




        }
        // public ActionResult List()
        public async Task<ActionResult> GetCar()
        
        {
            var client = _httpClientFactory.CreateClient("Car" +
                "" +
                "Client");
            var response = await client.GetAsync("https://carinvetoryapp.azurewebsites.net/api/Car");
            response.EnsureSuccessStatusCode();
            string carJson = await response.Content.ReadAsStringAsync();
            var carjson1 =  JObject.Parse(carJson);
           List<CarDto> carModel = JsonConvert.DeserializeObject<List<CarDto>>(carjson1["result"].ToString());

            return View("List",carModel);

        }


        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
       

        // POST: HomeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> Create(CarDto carDto)
        {
            var client = _httpClientFactory.CreateClient("Car" +
                 "" +
                 "Client");
            HttpResponseMessage response = await client.PostAsJsonAsync("https://carinvetoryapp.azurewebsites.net/api/Car", carDto);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetCar");

        }

        // GET: HomeController/Edit/5
        [HttpGet]
        public async Task <ActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("Car" +
                 "" +
                 "Client");
            HttpResponseMessage response = await client.GetAsync("https://carinvetoryapp.azurewebsites.net/api/Car?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            string carJson = response.Content.ReadAsStringAsync().Result;
            var carjson1 = JObject.Parse(carJson);
            CarDto carModel = JsonConvert.DeserializeObject<CarDto>(carjson1["result"][0].ToString());
            return View(carModel);
            //HttpResponseMessage response = client1.GetAsync("api/showroom/GetProduct?id=" + id.ToString());
            //response.EnsureSuccessStatusCode();
            //CarDto products = response.Content.ReadAsStringAsync<CarDto>().Result;
            //ViewBag.Title = "All Products";
            //return View(products);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        public async Task <ActionResult> Edit(CarDto carDto)
        {
            var client = _httpClientFactory.CreateClient("Car" +
                 "" +
                 "Client");
            
           
            HttpResponseMessage response = await client.PutAsJsonAsync("https://carinvetoryapp.azurewebsites.net/api/Car?id=", carDto);
            response.EnsureSuccessStatusCode();
            
            return RedirectToAction("GetCar");
        }

        // GET: HomeController/Delete/5
       

        // POST: HomeController/Delete/5
        public async Task <ActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("Car" +
                 "" +
                 "Client");
            HttpResponseMessage response = await client.DeleteAsync("https://carinvetoryapp.azurewebsites.net/api/Car?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetCar");
        }
    }
}
