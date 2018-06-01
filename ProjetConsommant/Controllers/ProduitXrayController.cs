using ProjetConsommant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProjetConsommant.Controllers
{
    public class ProduitXrayController : Controller
    {
        // GET: ProduitXray
        public ActionResult Index()
        {
            IEnumerable<mvcProduitModel> x;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //4.getasync ;recuperer les resultats :
            HttpResponseMessage response = client.GetAsync("api/Produits").Result;
            x = response.Content.ReadAsAsync<IEnumerable<mvcProduitModel>>().Result;
            return View(x);
        }

        public ActionResult Create()
        {
            IEnumerable<Categorie> x;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //4.getasync ;recuperer les resultats :
            HttpResponseMessage response = client.GetAsync("api/Categories").Result;
            x = response.Content.ReadAsAsync<IEnumerable<Categorie>>().Result;
            ViewBag.idCategorie = new SelectList(x, "idCategorie", "nomCategorie");
            return View();
        }
        [HttpPost]
        public ActionResult Create(mvcProduitModel produit, HttpPostedFileBase ImageFile)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833/");//////////////////
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //4.getasync ;recuperer les resultats :
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            produit.nomPhoto = "~/Image/" + fileName;
            string path = Path.Combine(Server.MapPath("~/Image/"), fileName);
            ImageFile.SaveAs(path);

            HttpResponseMessage response = client.PostAsJsonAsync("api/Produits", produit).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            mvcProduitModel x;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833");
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));

                HttpResponseMessage response = client.GetAsync("api/Produits/" + id.ToString()).Result;
                x = response.Content.ReadAsAsync<mvcProduitModel>().Result;
                return View(x);
        }

        [HttpPost]
        public ActionResult Edit(mvcProduitModel produit, HttpPostedFileBase ImageFile)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833");
            client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));

            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            produit.nomPhoto = "~/Image/" + fileName;
            string path = Path.Combine(Server.MapPath("~/Image/"), fileName);
            ImageFile.SaveAs(path);
                HttpResponseMessage response = client.PutAsJsonAsync("api/Produits/" + produit.idProduit, produit).Result;
                TempData["SuccessMessage"] = "Update Succefully";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1833");
            client.DefaultRequestHeaders.Accept.
            Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));
            HttpResponseMessage response = client.DeleteAsync("api/Produits/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Delete Succefully";
            return RedirectToAction("Index");
        }














    }
}