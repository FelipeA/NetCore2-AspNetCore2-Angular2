using System;
using System.Collections.Generic;
using System.Text;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Entities;
using ModernStore.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ModernStore.Domain.Commands.Results;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public bool DocumentExists(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }

        public Customer Get(Guid id)
        {
            return _context
                .Customers
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public GetCustomerCommandResult Get(string userName)
        {
            /*
             * Usando o EF
            return _context.Customers
                .Include(x => x.User)
                .Select(x => new GetCustomerCommandResult
                {
                    Name = x.ToString()
                    , Document = x.Document.Number
                    , Active = x.User.Active
                    , Email = x.Email.Address
                    , Password = x.User.Password
                    , UserName = x.User.Username
                })
                .FirstOrDefault(y => y.UserName == userName);
            */

            using (var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var query = "SELECT * FROM GetCustomerInfoView WHERE Active = 1 AND Username = '@username'";

                conn.Open();
                return conn.Query<GetCustomerCommandResult>(query, new { userName = userName }).FirstOrDefault();
            }
        }

        public Customer GetByUsername(string username)
        {
            try
            {
                return _context
                    .Customers
                    .Include(x => x.User)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.User.Username == username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
    }
}
