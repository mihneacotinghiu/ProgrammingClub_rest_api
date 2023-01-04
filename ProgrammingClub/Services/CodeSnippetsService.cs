using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using System.Runtime.CompilerServices;

namespace ProgrammingClub.Services
{
    public class CodeSnippetsService : ICodeSnippetsService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMembersService _membersService;
        private readonly IMapper _mapper;

        public CodeSnippetsService(ProgrammingClubDataContext context, IMapper mapper, IMembersService membersService) {
            _context= context;
            _mapper= mapper;
            _membersService= membersService;
        }

        public async Task CreateCodeSnippetAsync(CreateCodeSnippet codeSnippet)
        {
            var newCodeSnippet = _mapper.Map<CodeSnippet>(codeSnippet);

            await ValidateCodeSnippet(newCodeSnippet);

            newCodeSnippet.IdCodeSnippet = Guid.NewGuid();
            _context.Entry(newCodeSnippet).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCodeSnippetAsync(Guid id)
        {
            if (!await CodeSnippetExistByIdAsync(id)) { return false; }

            _context.CodeSnippets.Remove(new CodeSnippet { IdCodeSnippet = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CodeSnippet>> GetCodeSnippetsAsync()
        {
            return await _context.CodeSnippets.ToListAsync();
        }

        public async Task<CodeSnippet?> GetCodeSnippetByIdAsync(Guid id)
        {
            return await _context.CodeSnippets.FirstOrDefaultAsync(c => c.IdCodeSnippet == id);
        }

        public async Task<CodeSnippet?> PartiallyUpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet)
        {
            codeSnippet.IdCodeSnippet = id;
            await ValidateCodeSnippet(codeSnippet);

            var CodeSnippetFromDatabase = await GetCodeSnippetByIdAsync(id);
            if(CodeSnippetFromDatabase == null) { return null; }

            if (codeSnippet.isPublished != null){ CodeSnippetFromDatabase.isPublished = codeSnippet.isPublished; }
            if (codeSnippet.Title != null) { CodeSnippetFromDatabase.Title= codeSnippet.Title; }
            if (codeSnippet.IdMember != null) { CodeSnippetFromDatabase.IdMember= codeSnippet.IdMember; }
            if (codeSnippet.IdSnippetPreviousVersion != null) { CodeSnippetFromDatabase.IdSnippetPreviousVersion= codeSnippet.IdSnippetPreviousVersion; }
            if (codeSnippet.Revision != null) { CodeSnippetFromDatabase.Revision=codeSnippet.Revision; }
            if (codeSnippet.DateTimeAdded != null) { CodeSnippetFromDatabase.DateTimeAdded= codeSnippet.DateTimeAdded; }

            _context.CodeSnippets.Update(CodeSnippetFromDatabase);
            await _context.SaveChangesAsync();
            return CodeSnippetFromDatabase;

        }

        public async Task<CodeSnippet?> UpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet)
        {
            if(! await CodeSnippetExistByIdAsync(id)) { return null; }

            await ValidateCodeSnippet(codeSnippet);

            codeSnippet.IdCodeSnippet= id;
            _context.CodeSnippets.Update(codeSnippet);   
            await _context.SaveChangesAsync();
            return codeSnippet;
        }

        public async Task<bool> CodeSnippetExistByIdAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return false;
            }    
            return await _context.CodeSnippets.AnyAsync(c => c.IdCodeSnippet == id);
        }

        private async Task ValidateCodeSnippet(CodeSnippet codeSnippet)
        {
            Guid? idCS = codeSnippet.IdSnippetPreviousVersion;
            Guid? idMember = codeSnippet.IdMember;
            var codeSnippetExistTask = CodeSnippetExistByIdAsync(idCS);
            var memberExistTask = _membersService.MemberExistByIdAsync(idMember);
            await Task.WhenAll(codeSnippetExistTask, memberExistTask);

            if (!idCS.HasValue || codeSnippetExistTask.Result)
            {
                throw new NotImplementedException();
            }
            if (memberExistTask.Result)
            {
                throw new NotImplementedException();
            }
        }
    }
}
