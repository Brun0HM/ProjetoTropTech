using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBackend.Models
{
    public class Compra
    {
        public Guid CompraId { get; set; }
        [Required(ErrorMessage = "Fornecedor não pode estar vazio")]
        [Display(Name = "Fornecedor")]
        public Guid FornecedorId { get; set; }
        [Required(ErrorMessage = "Produto não pode estar vazio")]
        [Display(Name = "Produto")]
        public Guid ProdutoId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        [Display(Name = "Data da Compra")]
        public DateTime? DataCompra { get; set; } = DateTime.Now;
        public decimal? ValorTotal { get; set; } = 0;

    }
}
