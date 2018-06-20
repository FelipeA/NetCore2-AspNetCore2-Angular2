using ModernStore.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly ModernStoreDataContext _context;

        public Uow(ModernStoreDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Roolback()
        {
            // Do nothing
        }
    }
}
