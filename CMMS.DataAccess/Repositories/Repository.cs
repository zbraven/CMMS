﻿using CMMS.DataAccess.Context;
using CMMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CMMSDbContext _context;

        public Repository(CMMSDbContext context)
        {
            _context = context;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        //Saydirma
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Set<T>().CountAsync();
            }
            else
            {
                return await _context.Set<T>().CountAsync(predicate);
            }
        }
    }
}
