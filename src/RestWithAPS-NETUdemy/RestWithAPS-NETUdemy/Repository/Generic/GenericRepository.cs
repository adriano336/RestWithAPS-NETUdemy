using Evolve.Configuration;
using Microsoft.EntityFrameworkCore;
using RestWithAPS_NETUdemy.Model.Base;
using RestWithAPS_NETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAPS_NETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MySQLContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public T Create(T entity)
        {
            try
            {
                dataset.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (result != null) dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return dataset.Any(e => e.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(e => e.Id == id);
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            long result = default(long);
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    result = (long)cmd.ExecuteScalar();
                }
            }

            return (int)result;
        }

        public T Update(T entity)
        {
            if (!Exists(entity.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(entity.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }                
    }
}
