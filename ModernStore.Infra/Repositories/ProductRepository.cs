using System;
using System.Collections.Generic;
using System.Text;
using ModernStore.Domain.Entities;
using ModernStore.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Commands.Results;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreDataContext _context;

        public ProductRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Product Get(Guid id)
        {
            try
            {
                return _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            using (var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var query = "select * from product";

                conn.Open();
                return conn.Query<GetProductListCommandResult>(query);
            }
        }
    }
}
