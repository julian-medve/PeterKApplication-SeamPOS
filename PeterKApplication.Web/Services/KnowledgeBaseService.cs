using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Services
{
    public interface IKnowledgeBaseService
    {
        ICollection<KnowledgeBaseDto> GetKnowledgeBase();
        Task AddKnowledgeBase(KnowledgeBaseDto knowledgeBase);
        Task UpdateKnowledgeBase(KnowledgeBaseDto knowledgeBase);
    }
    
    public class KnowledgeBaseService : IKnowledgeBaseService
    {
        private readonly AppDbContext _dbContext;

        public KnowledgeBaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<KnowledgeBaseDto> GetKnowledgeBase()
        {
            var knowledgeBases = _dbContext.KnowledgeBases.ToList();

            return knowledgeBases.Select(kb => new KnowledgeBaseDto
            {
                Image = kb.Image,
                Title = kb.Title,
                Description = kb.Description,
                VideoUri = kb.VideoUri,
                Id = kb.Id
            }).ToList();
        }

        public async Task AddKnowledgeBase(KnowledgeBaseDto knowledgeBase)
        {
            var kb = new KnowledgeBase
            {
                Image = knowledgeBase.Image,
                Title = knowledgeBase.Title,
                Description = knowledgeBase.Description,
                VideoUri = knowledgeBase.VideoUri
            };

            await _dbContext.KnowledgeBases.AddAsync(kb);
        }

        public async Task UpdateKnowledgeBase(KnowledgeBaseDto knowledgeBase)
        {
            var kb = await _dbContext.KnowledgeBases.FirstOrDefaultAsync(k => k.Id == knowledgeBase.Id);
            if (kb == null)
            {
                throw new AppException("Knowledge base entry not found");
            }

            kb.Image = knowledgeBase.Image;
            kb.Title = knowledgeBase.Title;
            kb.Description = knowledgeBase.Description;
            kb.VideoUri = knowledgeBase.VideoUri;

            await _dbContext.SaveChangesAsync();
        }
    }
}