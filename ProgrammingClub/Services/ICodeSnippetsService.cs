using ProgrammingClub.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingClub.Services
{
    public interface ICodeSnippetsService
    {
        public Task<DbSet<CodeSnippet>> GetCodeSnippetAsync();
        public Task CreateCodeSnippetAsync(CodeSnippet codeSnippet);
        public Task<bool> DeleteCodeSnippetAsync(Guid id);
        public Task UpdateCodeSnippetAsync(CodeSnippet codeSnippet);
        public Task<CodeSnippet?> GetCodeSnippetByid(Guid id);

        public Task memberExist(Guid id);
       

    }
}
