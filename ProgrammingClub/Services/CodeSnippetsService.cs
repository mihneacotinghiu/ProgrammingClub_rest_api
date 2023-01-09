﻿using AutoMapper;
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
            bool codeSnippetIsChanged = false, idMemberIsChanged = false, idPreviousCodeSnippetIsChanged = false;
            var codeSnippetFromDatabase = await GetCodeSnippetByIdAsync(id);
            var ExtraValidationForUpdatesIsNeeded = true;
            codeSnippet.IdCodeSnippet = id;

            if (codeSnippetFromDatabase == null)
            {
                return null;
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
            if (codeSnippet.IdMember.HasValue && codeSnippet.IdMember != codeSnippetFromDatabase.IdMember)
            {
                codeSnippetFromDatabase.IdMember = codeSnippet.IdMember;
                codeSnippetIsChanged = true;
                idMemberIsChanged= true;
            }
            if (codeSnippet.IdSnippetPreviousVersion != codeSnippetFromDatabase.IdSnippetPreviousVersion)
            {
                codeSnippetFromDatabase.IdSnippetPreviousVersion = codeSnippet.IdSnippetPreviousVersion;
                codeSnippetIsChanged = true;
                idPreviousCodeSnippetIsChanged= true;
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
            if(idMemberIsChanged || idPreviousCodeSnippetIsChanged)
            {
                await ValidateCodeSnippet(codeSnippetFromDatabase, ExtraValidationForUpdatesIsNeeded);
            }
            _context.CodeSnippets.Update(codeSnippetFromDatabase);
            await _context.SaveChangesAsync();
            return codeSnippetFromDatabase;
        }

        public async Task<CodeSnippet?> UpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet)
        {
            if(! await CodeSnippetExistByIdAsync(id)) 
            {
                return null;
            }
            var ExtraValidationForUpdatesIsNeeded = true;
            await ValidateCodeSnippet(codeSnippet, ExtraValidationForUpdatesIsNeeded);

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

        private async Task ValidateCodeSnippet(CodeSnippet codeSnippet,bool extraValidationForUpdatesIsNeeded)
        {
            Guid? idCSPrevious = codeSnippet.IdSnippetPreviousVersion;
            Guid? idMember = codeSnippet.IdMember;
            
            if (idCSPrevious.HasValue && !await CodeSnippetExistByIdAsync(idCSPrevious))
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.CodeSnippet.NoCodeSnippetFound);
            }
            if (!await _membersService.MemberExistByIdAsync(idMember))
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.Member.NoMemberFound);
            }
            if (extraValidationForUpdatesIsNeeded)
            {
                if(idCSPrevious.HasValue && idCSPrevious == codeSnippet.IdSnippetPreviousVersion)
                {
                    throw new ModelValidationException(Helpers.ErrorMessagesEnum.CodeSnippet.idCSPreviousIdenticalToIdCS);
                }
            }
        }
    }
}
