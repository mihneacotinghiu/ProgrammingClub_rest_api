using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Exceptions;
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
            var ExtraValidationForUpdatesIsNeeded = false;
            await ValidateCodeSnippet(newCodeSnippet, ExtraValidationForUpdatesIsNeeded);

            newCodeSnippet.IdCodeSnippet = Guid.NewGuid();
            _context.Entry(newCodeSnippet).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCodeSnippetAsync(Guid id)
        {
            if (!await CodeSnippetExistByIdAsync(id)) 
            {
                return false;
            }
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
            bool codeSnippetIsChanged = false;
            bool idMemberIsChanged = false;
            bool idPreviousCodeSnippetIsChanged = false;
            bool ExtraValidationForUpdatesIsNeeded = true;
            var codeSnippetFromDatabase = await GetCodeSnippetByIdAsync(id);
            codeSnippet.IdCodeSnippet = id;

            if (codeSnippetFromDatabase == null)
            {
                return null;
            }
            if (codeSnippet.IdSnippetPreviousVersion != codeSnippetFromDatabase.IdSnippetPreviousVersion)
            {
                codeSnippetFromDatabase.IdSnippetPreviousVersion = codeSnippet.IdSnippetPreviousVersion;
                codeSnippetIsChanged = true;
                idPreviousCodeSnippetIsChanged= true;
            }
            if (codeSnippet.IdMember.HasValue && codeSnippet.IdMember != codeSnippetFromDatabase.IdMember)
            {
                codeSnippetFromDatabase.IdMember = codeSnippet.IdMember;
                codeSnippetIsChanged = true;
                idMemberIsChanged = true;
            }
            if (idMemberIsChanged || idPreviousCodeSnippetIsChanged)
            {
                await ValidateCodeSnippet(codeSnippetFromDatabase, ExtraValidationForUpdatesIsNeeded);
            }
            if (codeSnippet.isPublished.HasValue && codeSnippet.isPublished != codeSnippetFromDatabase.isPublished)
            {
                codeSnippetFromDatabase.isPublished = codeSnippet.isPublished;
                codeSnippetIsChanged = true;
            }
            if (!string.IsNullOrEmpty(codeSnippet.Title) && codeSnippet.Title != codeSnippetFromDatabase.Title)
            {
                codeSnippetFromDatabase.Title = codeSnippet.Title;
                codeSnippetIsChanged = true;
            }
            if (codeSnippet.Revision.HasValue && codeSnippet.Revision != codeSnippetFromDatabase.Revision)
            {
                codeSnippetFromDatabase.Revision = codeSnippet.Revision;
                codeSnippetIsChanged = true;
            }
            if (codeSnippet.DateTimeAdded.HasValue && codeSnippet.DateTimeAdded != codeSnippetFromDatabase.DateTimeAdded)
            {
                codeSnippetFromDatabase.DateTimeAdded = codeSnippet.DateTimeAdded;
                codeSnippetIsChanged = true;
            }
            if (!codeSnippetIsChanged) 
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.ZeroUpdatesToSave);
            }
          
            _context.CodeSnippets.Update(codeSnippetFromDatabase);
            await _context.SaveChangesAsync();
            return codeSnippetFromDatabase;
        }

        public async Task<CodeSnippet?> UpdateCodeSnippetAsync(Guid id, CreateCodeSnippet codeSnippet)
        {
            if (! await CodeSnippetExistByIdAsync(id)) 
            {
                return null;
            }
            var codeSnippetUpdated = _mapper.Map<CodeSnippet>(codeSnippet);
            codeSnippetUpdated.IdCodeSnippet = id;
            var ExtraValidationForUpdatesIsNeeded = true;
            await ValidateCodeSnippet(codeSnippetUpdated, ExtraValidationForUpdatesIsNeeded);

            codeSnippet.IdCodeSnippet= id;
            _context.CodeSnippets.Update(codeSnippetUpdated);   
            await _context.SaveChangesAsync();
            return codeSnippetUpdated;
        }

        public async Task<bool> CodeSnippetExistByIdAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return false;
            }
            return await _context.CodeSnippets.AnyAsync(c => c.IdCodeSnippet == id);
        }

        private async Task ValidateCodeSnippet(CodeSnippet codeSnippet,bool extraValidationForUpdatesIsNeeded)
        {
            Guid? idCSPrevious = codeSnippet.IdSnippetPreviousVersion;
            Guid? idMember = codeSnippet.IdMember;
            
            if (idCSPrevious.HasValue && !await CodeSnippetExistByIdAsync(idCSPrevious))
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.CodeSnippet.NotFound);
            }
            if (!await _membersService.MemberExistByIdAsync(idMember))
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.Member.NoMemberFound);
            }
            if (extraValidationForUpdatesIsNeeded)
            {
                if(idCSPrevious.HasValue && idCSPrevious == codeSnippet.IdSnippetPreviousVersion)
                {
                    throw new ModelValidationException(Helpers.ErrorMessagesEnum.CodeSnippet.IdCSPreviousIdenticalToIdCS);
                }
            }
        }
    }
}
