
using System;

namespace BookService.Models
{
    public class DividaDetailDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public bool Status { get; set; }
        public string ClienteNome { get; set; }
    }
}