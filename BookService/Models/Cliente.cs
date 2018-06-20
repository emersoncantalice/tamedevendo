using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookService.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
    }
}