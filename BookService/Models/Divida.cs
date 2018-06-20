using System;
using System.ComponentModel.DataAnnotations;

namespace BookService.Models
{
    public class Divida
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public decimal Preco { get; set; }
        public bool Status { get; set; }

        // Foreign Key
        public int ClienteId { get; set; }
        // Navigation property
        public Cliente Cliente { get; set; }
    }
}