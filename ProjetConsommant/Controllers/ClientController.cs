using PagedList;
using ProjetConsommant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProjetConsommant.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        // GET: Products 
        public ActionResult Products(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //produits and 
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentFilter = searchString;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Produits").Result;
            var x = response.Content.ReadAsAsync<IEnumerable<mvcProduitModel>>().Result;

            ///recuper les catégories :)
            IEnumerable<Categorie> x2;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //4.getasync ;recuperer les resultats :
            response = client.GetAsync("api/Categories").Result;
            x2 = response.Content.ReadAsAsync<IEnumerable<Categorie>>().Result;
            ViewBag.AllCategories = x2.ToList();





            int pageNumber = 1;
            int pageSize = 6;
            return View(x.ToPagedList(1, pageSize));
        }



















    }
}