using System;
using System.Collections.Generic;

namespace APIProdutos.Models
{
    public class CadastroProduto
    {
        public string CodigoBarras { get; set; }       
        public string Nome { get; set; }        
        public double? Preco { get; set; }
    }

    public class Produto
    {
        public string CodigoBarras { get; set; }       
        public string Nome { get; set; }        
        public double? Preco { get; set; }
        public DateTime DataAtualizacao { get; set; }        
    }

    public class Resultado
    {
        public string Acao { get; set; }

        public bool Sucesso
        {
            get { return Inconsistencias == null || Inconsistencias.Count == 0; }
        }

        public List<string> Inconsistencias { get; } = new List<string>();        
    }
}