using ProjetConsommant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProjetConsommant.Controllers
{
    public class CategorieXrayController : Controller
    {
        // GET: CategorieXray
        public ActionResult Index()
        {
            IEnumerable<Categorie> x;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //4.getasync ;recuperer les resultats :
            HttpResponseMessage response = client.GetAsync("api/Categories").Result;
            x = response.Content.ReadAsAsync<IEnumerable<Categorie>>().Result;
            return View(x);
        }
        // GET: mvcProduitModels/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Categorie categorie)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //4.getasync ;recuperer les resultats :
            HttpResponseMessage response = client.PostAsJsonAsync("api/Categories", categorie).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Categorie x;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833");
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));

            HttpResponseMessage response = client.GetAsync("api/Categories/" + id.ToString()).Result;
            x = response.Content.ReadAsAsync<Categorie>().Result;
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(Categorie categorie)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833");
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));

                HttpResponseMessage response = client.PutAsJsonAsync("api/Categories/" + categorie.idCategorie, categorie).Result;
                TempData["SuccessMessage"] = "Update Succefully";
    

            return RedirectToAction("Index");

        }





        public ActionResult Delete(int id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833");
            client.DefaultRequestHeaders.Accept.
            Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));
            HttpResponseMessage response = client.DeleteAsync("api/Categories/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Delete Succefully";
            return RedirectToAction("Index");
        }







    }
}