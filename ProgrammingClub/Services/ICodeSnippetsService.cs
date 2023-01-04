﻿using ProgrammingClub.Models;
using Microsoft.EntityFrameworkCore;
using ProgrammingClub.Models.CreateModels;

namespace ProgrammingClub.Services
{
    public interface ICodeSnippetsService
    {
        public Task<IEnumerable<CodeSnippet>> GetCodeSnippetsAsync();
        public Task<CodeSnippet?> GetCodeSnippetByIdAsync(Guid id);
        public Task CreateCodeSnippetAsync(CreateCodeSnippet codeSnippet);
        public Task<bool> DeleteCodeSnippetAsync(Guid id);
        public Task<CodeSnippet?> UpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet);
        public Task<CodeSnippet?> PartiallyUpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet);
        public Task<bool> CodeSnippetExistByIdAsync(Guid? id);
        public Task CodeSnippetIsValid(CodeSnippet codeSnippet);
        public Task CheckCodeSnippetExistance(Guid? id);
        public Task CheckMemberExistance(Guid? id);


    }
}
