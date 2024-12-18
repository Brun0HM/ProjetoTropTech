﻿using System.ComponentModel.DataAnnotations;

namespace ProjetoBackend.Models
{
    public class ItemVenda
    {
        public Guid ItemVendaId { get; set; }
        [Required(ErrorMessage = "Venda")]
        [Display(Name = "Venda")]
        public Guid VendaId { get; set; }
        [Display(Name = "Venda")]
        public Venda? Venda { get; set; }
        [Required(ErrorMessage = "Produto")]
        [Display(Name = "Produto")]
        public Guid? ProdutoId { get; set; }
        [Display(Name = "Produto")]
        public Produto? Produto { get; set; }
        [Display(Name = "Quantidade")]
        public decimal Quantidade { get; set; }
        [Display(Name = "Valor")]
        public decimal ValorUnitario { get; set; }
        [Display(Name = "Total")]
        public decimal ValorTotal { get; set; }

    }
}
