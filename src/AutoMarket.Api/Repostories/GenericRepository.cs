﻿using AutoMarket.Api.Entities.Common;
using AutoMarket.Api.Enums;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AutoMarketDbContext _dbContext;

        public GenericRepository(AutoMarketDbContext dbContext, CancellationToken ct = default)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<IList<T>> GetAllAsync(bool hasTracking = false, CancellationToken ct = default, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbContext.Set<T>().Where(x => x.Status == RecordStatuses.ACTIVE);
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }

            return hasTracking
                ? await query.ToListAsync(ct)
                : await query.AsNoTracking().ToListAsync(ct);
        }

        public async Task<T> GetAsync(int id, CancellationToken ct = default)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.Status == RecordStatuses.ACTIVE);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool hasTracking = true, CancellationToken ct = default, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbContext.Set<T>().Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return hasTracking
                ? await query.FirstOrDefaultAsync(ct)
                : await query.AsNoTracking().FirstOrDefaultAsync(ct);
        }

        public async Task<bool> ExistAsync(int id, CancellationToken ct = default)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await Query().AnyAsync(predicate);
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            entity.Add();
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            entity.Update();
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            entity.Delete();
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangeAsync(CancellationToken ct = default)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
