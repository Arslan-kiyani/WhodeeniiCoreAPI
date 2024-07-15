﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class AttachDocumentsRepository : IAttachDocumentsRepository
    {

        private readonly WhoDeeniiDbContext _context;

        public AttachDocumentsRepository(WhoDeeniiDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDocumentAsync(AttachDocuments document)
        {
           await _context.attachDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
            return true;    
        }
    }
}
