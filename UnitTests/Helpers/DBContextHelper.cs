using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Models.Builders;

namespace UnitTests.Helpers
{
    public class DBContextHelper
    {
        public static async Task<Member> AddTestMember(ProgrammingClubDataContext context)
        {
            var testMember = new MemberBuilder().Build();
            context.Add(testMember);
            await context.SaveChangesAsync();
            context.Entry(testMember).State = EntityState.Detached;
            return testMember;
        }
    }
}
