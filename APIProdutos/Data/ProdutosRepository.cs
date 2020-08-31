using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using MongoDB.Driver;
using APIProdutos.Models;
using APIProdutos.Documents;

namespace APIProdutos.Data
{
    public class ProdutosRepository
    {
        private readonly IMapper _mapper;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<ItemCatalogoDocument> _collection;

        public ProdutosRepository(IConfiguration configuration,
            IMapper mapper)
        {
            _mapper = mapper;
            _client = new MongoClient(configuration["MongoDB:Connection"]);
            _db = _client.GetDatabase(configuration["MongoDB:Database"]);
            _collection = _db.GetCollection<ItemCatalogoDocument>(
                configuration["MongoDB:Collection"]);
        }

        public Produto Get(string codigoBarras)
        {
            ItemCatalogoDocument document = null;
            
            codigoBarras = codigoBarras?.Trim().ToUpper();
            if (!String.IsNullOrWhiteSpace(codigoBarras))
            {
                document = _collection.Find(
                    GetFilterProduto(codigoBarras)).FirstOrDefault();
            }
            
            if (document != null)
                return _mapper.Map<Produto>(document);
            else
                return null;
        }

        private FilterDefinition<ItemCatalogoDocument> GetFilterProduto(string codigoBarras)
        {
            return Builders<ItemCatalogoDocument>.Filter.Eq("Codigo", codigoBarras) &
                Builders<ItemCatalogoDocument>.Filter.Eq("Tipo", "PRODUTO");
        }

        public List<Produto> GetAll()
        {
            return _mapper.Map<List<Produto>>(
                _collection.Find(all => true).ToEnumerable()
                .OrderBy(p => p.Codigo).ToList());
        }

        public void Insert(CadastroProduto cadastro)
        {
            _collection.InsertOne(
                _mapper.Map<ItemCatalogoDocument>(cadastro));
        }

        public bool Update(CadastroProduto cadastro)
        {
            var update = Builders<ItemCatalogoDocument>.Update
                .Set("Nome", cadastro.Nome)
                .Set("Preco", cadastro.Preco)
                .Set("UltimaAtualizacao", DateTime.Now);

            return _collection.UpdateOne(
                GetFilterProduto(cadastro.CodigoBarras),
                update).ModifiedCount > 0;
        }        

        public bool Delete(string codigoBarras)
        {
            return _collection.DeleteOne(
                GetFilterProduto(codigoBarras)).DeletedCount > 0;
        }
    }
}