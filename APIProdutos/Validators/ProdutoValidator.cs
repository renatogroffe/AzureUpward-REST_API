using FluentValidation;
using APIProdutos.Models;

namespace APIProdutos.Validators
{
    public class ProdutoValidator : AbstractValidator<CadastroProduto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.CodigoBarras).NotEmpty().WithMessage("Preencha o campo 'CodigoBarras'")
                .MinimumLength(4).WithMessage("O campo 'CodigoBarras' deve possuir no mínimo 4 caracteres")
                .MaximumLength(13).WithMessage("O campo 'CodigoBarras' deve possuir no máximo 13 caracteres");

            RuleFor(p => p.Nome).NotEmpty().WithMessage("Preencha o campo 'Nome'")
                .MinimumLength(4).WithMessage("O campo 'Nome' deve possuir no mínimo 4 caracteres")
                .MaximumLength(100).WithMessage("O campo 'Nome' deve possuir no máximo 100 caracteres");

            RuleFor(c => c.Preco).NotEmpty().WithMessage("Preencha o campo 'Preco'")
                .GreaterThan(0).WithMessage("O campo 'Preco' deve ser maior do 0");
        }
    }
}