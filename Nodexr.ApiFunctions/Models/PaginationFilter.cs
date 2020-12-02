﻿using Microsoft.EntityFrameworkCore;
using Nodexr.ApiShared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodexr.ApiFunctions.Models
{
    public class PaginationFilter
    {
        public int Start { get; set; }
        public int Limit { get; set; }

        const int maxLimit = 100;

        public PaginationFilter(int start, int limit)
        {
            this.Start = start < 0 ? 0 : start;
            this.Limit = limit > maxLimit ? maxLimit : limit;
        }

        public async Task<Paged<T>> ApplyTo<T>(IQueryable<T> collection)
        {
            return new Paged<T>(
                await collection.Skip(Start).Take(Limit).ToListAsync(),
                collection.Count(),
                Start,
                Limit);
        }
    }
}