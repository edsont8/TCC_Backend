﻿using FluentValidation;
using Newtonsoft.Json;
using System;

namespace ECOM.API.Carrinho.Model
{
    public class CarrinhoItem
    {
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid CarrinhoId { get; set; }

        [JsonIgnore]
        public CarrinhoCliente CarrinhoCliente { get; set; }

        internal void AssociarCarrinho(Guid carrinhoId)
        {
            CarrinhoId = carrinhoId;
        }

        internal decimal CalcularValor()
        {
            return Amount * Price;
        }

        internal void AdicionarUnidades(int amount)
        {
            Amount += amount;
        }

        internal void AtualizarUnidades(int amount)
        {
            Amount = amount;
        }

        internal bool EhValido()
        {
            return new ItemCarrinhoValidation().Validate(this).IsValid;
        }

        public class ItemCarrinhoValidation : AbstractValidator<CarrinhoItem>
        {
            public ItemCarrinhoValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido!");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado!");

                RuleFor(c => c.Amount)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade mínima {item.Name} é 1");

                RuleFor(c => c.Amount)
                    .LessThanOrEqualTo(CarrinhoCliente.MAX_QUANTIDADE_ITEM)
                    .WithMessage(item => $"A quantidade máxima do produto {item.Name} é {CarrinhoCliente.MAX_QUANTIDADE_ITEM}");

                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage(item => $"O valor do produto {item.Name} precisa ser maior que 0");
            }
        }
    }
}
