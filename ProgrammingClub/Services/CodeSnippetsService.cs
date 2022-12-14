using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Services
{
    public class CodeSnippetsService : ICodeSnippetsService
    {

        private readonly ProgrammingClubDataContext _context;
         public CodeSnippetsService(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task CreateCodeSnippetAsync(CodeSnippet codeSnippet)
        {
            codeSnippet.IdCodeSnippet = Guid.NewGuid();
            codeSnippet.DateTimeAdded = DateTime.UtcNow;
            _context.Entry(codeSnippet).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCodeSnippetAsync(Guid id)
        {
            CodeSnippet? codeSnippet = await GetCodeSnippetByid(id);
            if(codeSnippet == null)
                return false;
            _context.CodeSnippets.Remove(codeSnippet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DbSet<CodeSnippet>> GetCodeSnippetAsync()
        {
            return  _context.CodeSnippets;
        }

        public async Task<CodeSnippet?> GetCodeSnippetByid(Guid id)
        {
            return await _context.CodeSnippets.FirstOrDefaultAsync(c => c.IdCodeSnippet == id);
        }


        public async Task memberExist(Guid id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.IdMember == id);
            if(member == null)
                throw new Exception("member does not exist");
        }


        public async Task UpdateCodeSnippetAsync(CodeSnippet codeSnippet)
        {
            _context.CodeSnippets.Update(codeSnippet);
            await _context.SaveChangesAsync();
        }
    }
}
