﻿using ECOM.Core.Data;
using ECOM.Pedidos.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECOM.Pedidos.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidosContext _context;

        public VoucherRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Voucher> ObterVoucherporCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}