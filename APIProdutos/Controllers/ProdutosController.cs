using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using APIProdutos.Models;
using APIProdutos.Business;

namespace APIProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly ProdutosService _service;

        public ProdutosController(ILogger<ProdutosController> logger,
            ProdutosService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Produto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{codigoBarras}")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Produto> Get(string codigoBarras)
        {
            var produto = GetProduto(codigoBarras);
            if (produto == null)
                return NotFound();
            
            return produto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Resultado> Post(CadastroProduto cadastro)
        {
            var resultado = _service.Insert(cadastro);
            if (resultado.Inconsistencias.Count > 0)
            {
                _logger.LogError(GetJSONResultado(resultado));
                return BadRequest(resultado);
            }
            else
                _logger.LogInformation("Inclusao efetuada com sucesso");
            
            return resultado;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Resultado> Put(CadastroProduto cadastro)
        {
            if (GetProduto(cadastro.CodigoBarras) == null)
                return NotFound();

            var resultado = _service.Update(cadastro);
            if (resultado.Inconsistencias.Count > 0)
            {
                _logger.LogError(GetJSONResultado(resultado));
                return BadRequest(resultado);
            }
            else
                _logger.LogInformation("Alteracao efetuada com sucesso");
            
            return resultado;
        }

        [HttpDelete("{codigoBarras}")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Resultado> Delete(string codigoBarras)
        {
            var produto = GetProduto(codigoBarras);
            if (produto == null)
                return NotFound();

            return _service.Delete(produto.CodigoBarras);
        }

        private string GetJSONResultado(Resultado resultado)
        {
            return JsonSerializer.Serialize(resultado);
        }

        private Produto GetProduto(string codigoBarras)
        {
            var produto = _service.Get(codigoBarras);
            if (produto == null)
                _logger.LogError($"Nao foi encontrado um produto com o codigo {codigoBarras}");

            return produto;
        }
    }
}