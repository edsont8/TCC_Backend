﻿using ECOM.Business.Interfaces;
using ECOM.Business.Models;
using ECOM.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace ECOM.Business.Services
{
    public class AssociatedProductsService : BaseService, IAssociatedProductsService
    {
        private readonly IAssociatedProductsRepository _associatedProductsRepository;

        public AssociatedProductsService(IAssociatedProductsRepository associatedProductsRepository,
                                         INotificador notificador) : base(notificador)
        {
            _associatedProductsRepository = associatedProductsRepository;
        }


        public async Task Adicionar(AssociatedProducts associatedProducts)
        {           
            if (!ExecutarValidacao(new AssociatedProductsValidation(), associatedProducts)) return;
            await _associatedProductsRepository.Adicionar(associatedProducts);
        }

        public async Task Atualizar(AssociatedProducts associatedProducts)
        {
            if (!ExecutarValidacao(new AssociatedProductsValidation(), associatedProducts)) return;
            await _associatedProductsRepository.Atualizar(associatedProducts);
        }

        public void Dispose()
        {
            _associatedProductsRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _associatedProductsRepository.Remover(id);
        }
    }
}
