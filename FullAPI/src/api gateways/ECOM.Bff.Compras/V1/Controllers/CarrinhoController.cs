﻿using ECOM.Bff.Compras.Models;
using ECOM.Bff.Compras.Services;
using ECOM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.Bff.Compras.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICatalogoService _catalogoService;
        private readonly IPedidoService _pedidoService;

        public CarrinhoController(ICarrinhoService carrinhoService, 
                                  ICatalogoService catalogoService,
                                  IPedidoService pedidoService)
        {
            _carrinhoService = carrinhoService;
            _catalogoService = catalogoService;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        [Route("compras/carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _carrinhoService.ObterCarrinho());
        }

        [HttpGet]
        [Route("compras/carrinho-quantidade")]
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var quantidade = await _carrinhoService.ObterCarrinho();
            return quantidade?.Items.Sum(i => i.Amount) ?? 0;
        }

        [HttpPost]
        [Route("compras/carrinho/items")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(itemProduto.ProductId);

            await ValidarItemCarrinho(produto, itemProduto.Amount);
            if (!OperacaoValida()) return CustomResponse();

            itemProduto.Name = produto.Name;
            itemProduto.Price = produto.Price;
            itemProduto.Image = produto.Image;

            var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

            return CustomResponse(resposta);
        }

        [HttpPut]
        [Route("compras/carrinho/items/{productId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid productId, ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(productId);

            await ValidarItemCarrinho(produto, itemProduto.Amount);
            if (!OperacaoValida()) return CustomResponse();

            var resposta = await _carrinhoService.AtualizarItemCarrinho(productId, itemProduto);
            
            return CustomResponse(resposta);
        }

        [HttpDelete]
        [Route("compras/carrinho/items/{productId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid productId)
        {
            var produto = await _catalogoService.ObterPorId(productId);

            if (produto == null)
            {
                AdicionarErroProcessamento("Produto inexistente!");
                return CustomResponse();
            }

            var resposta = await _carrinhoService.RemoverItemCarrinho(productId);

            return CustomResponse(resposta);
        }

        [HttpPost]
        [Route("compras/carrinho/aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
        {
            var voucher = await _pedidoService.ObterVoucherPorCodigo(voucherCodigo);
            if (voucher is null)
            {
                AdicionarErroProcessamento("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }

            var resposta = await _carrinhoService.AplicarVoucherCarrinho(voucher);

            return CustomResponse(resposta);
        }

        private async Task ValidarItemCarrinho(ItemProdutoDTO produto, int quantidade)
        {
            if (produto == null) AdicionarErroProcessamento("Produto inexistente!");
            if(quantidade < 1) AdicionarErroProcessamento($"Escolha ao menos uma unidade do produto {produto.Name}");

            var carrinho = await _carrinhoService.ObterCarrinho();
            var itemCarrinho = carrinho.Items.FirstOrDefault(p => p.ProductId == produto.Id);

            if (itemCarrinho != null && itemCarrinho.Amount + quantidade > produto.Amount)
            {
                AdicionarErroProcessamento
                    ($"O produto {produto.Name} possui {produto.Amount} unidades em estoque, você selecionou {quantidade}");
            }

            if (quantidade > produto.Amount) AdicionarErroProcessamento
                    ($"O produto {produto.Name} possui {produto.Amount} unidades em estoque, você selecionou {quantidade}");
        }
    }
}
