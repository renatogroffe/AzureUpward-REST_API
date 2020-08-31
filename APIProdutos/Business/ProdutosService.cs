using System;
using System.Collections.Generic;
using APIProdutos.Models;
using APIProdutos.Data;

namespace APIProdutos.Business
{
    public class ProdutosService
    {
        private readonly ProdutosRepository _repository;

        public ProdutosService(ProdutosRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _repository.GetAll();
        }

        public Produto Get(string codigoBarras)
        {
            return _repository.Get(codigoBarras);
        }


        public Resultado Insert(CadastroProduto cadastro)
        {
            var resultado = new Resultado()
            {
                Acao = "Inclusão de Produto"
            };

            if (_repository.Get(cadastro.CodigoBarras) != null)
            {
                resultado.Inconsistencias.Add(
                    "Código de Barras já cadastrado");
            }
            else
                _repository.Insert(cadastro);

            return resultado;
        }

        public Resultado Update(CadastroProduto cadastro)
        {
            var resultado = new Resultado()
            {
                Acao = "Atualização de Produto"
            };

            if (!_repository.Update(cadastro))
            {
                resultado.Inconsistencias.Add(
                    "Não foi possível alterar o Produto");
            }

            return resultado;
        }

        public Resultado Delete(string codigoBarras)
        {
            var resultado = new Resultado()
            {
                Acao = "Exclusão de Produto"
            };

            if (String.IsNullOrWhiteSpace(codigoBarras))
            {
                resultado.Inconsistencias.Add(
                    "Código de Barras não informado");
            }
            else if (!_repository.Delete(codigoBarras))
            {
                resultado.Inconsistencias.Add(
                    "Não foi possível excluir o Produto");
            }

            return resultado;
        }
    }
}