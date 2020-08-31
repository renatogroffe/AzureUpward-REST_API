using System;
using MongoDB.Bson;

namespace APIProdutos.Documents
{
    public class ItemCatalogoDocument
    {
        public ObjectId _id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public double? Preco { get; set; }
        public DateTime UltimaAtualizacao { get; set; }        
    }

}