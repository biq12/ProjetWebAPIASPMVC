using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetConsommant.Models
{
    public class Categorie
    {
        [Key]
        public int idCategorie { get; set; }
        public string nomCategorie { get; set; }
        public Boolean bestCategorie { get; set; } = false;
        public string grandCategorie { get; set; }
    }
}