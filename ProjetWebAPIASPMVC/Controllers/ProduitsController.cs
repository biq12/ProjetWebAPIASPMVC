using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProjetWebAPIASPMVC.Models;

namespace ProjetWebAPIASPMVC.Controllers
{
    public class ProduitsController : ApiController
    {
        private ProjetWebAPIASPMVCContext db = new ProjetWebAPIASPMVCContext();

        // GET: api/Produits
        public IQueryable<Produit> GetProduits()
        {
            return db.Produits.Include(m => m.categorie);
        }

        // GET: api/Produits/5
        [ResponseType(typeof(Produit))]
        public async Task<IHttpActionResult> GetProduit(int id)
        {
            Produit produit = await db.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            return Ok(produit);
        }

        // PUT: api/Produits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduit(int id, Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produit.idProduit)
            {
                return BadRequest();
            }

            db.Entry(produit).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produits
        [ResponseType(typeof(Produit))]
        public async Task<IHttpActionResult> PostProduit(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produits.Add(produit);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = produit.idProduit }, produit);
        }

        // DELETE: api/Produits/5
        [ResponseType(typeof(Produit))]
        public async Task<IHttpActionResult> DeleteProduit(int id)
        {
            Produit produit = await db.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            db.Produits.Remove(produit);
            await db.SaveChangesAsync();

            return Ok(produit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProduitExists(int id)
        {
            return db.Produits.Count(e => e.idProduit == id) > 0;
        }
    }
}