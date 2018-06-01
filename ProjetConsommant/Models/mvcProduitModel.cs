using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetConsommant.Models
{
    public class mvcProduitModel
    {
        [Key]
        public int idProduit { get; set; }
        public string designation { get; set; }
        public Boolean bestProduct { get; set; } = false;
        public string color { get; set; }
        public string nomPhoto { get; set; }
        public int quantite { get; set; }
        public float prix { get; set; }
        public int idCategorie { get; set; }
        [ForeignKey("idCategorie")]
        public Categorie categorie { get; set; }
    }
}