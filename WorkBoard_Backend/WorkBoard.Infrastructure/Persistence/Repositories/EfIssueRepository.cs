using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfIssueRepository : IIssueRepository
{
    private readonly WorkBoardDbContext _context;

    public EfIssueRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Issue> CreateAsync(Issue issue, CancellationToken token)
    {
        await _context.Issues.AddAsync(issue, token);

        await _context.SaveChangesAsync(token);

        return issue;
    }

    public async Task<bool> DeleteAsync(Guid issueId, CancellationToken token)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == issueId, token);

        if (issue is null) return false;

        _context.Issues.Remove(issue);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<IEnumerable<Issue>> GetByProjectIdAsync(Guid projectId, CancellationToken token)
    {
        return await _context.Issues.Where(i => i.ProjectId == projectId)
            .Include(i => i.Column)
            .Include(i => i.Project)
            .ToListAsync(token);
    }

    public async Task<IEnumerable<Issue>> GetByColumnIdAsync(Guid columnId, CancellationToken token)
    {
        return await _context.Issues.Where(i => i.ColumnId == columnId)
            .Include(i => i.Column)
            .Include(i => i.Project)
            .ToListAsync(token);
    }

    public async Task<Issue?> GetByIdAsync(Guid issueId, CancellationToken token)
    {
        return await _context.Issues
            .Include(i => i.Column).ThenInclude(c => c.Issues)
            .Include(i => i.Project)
            .FirstOrDefaultAsync(i => i.Id == issueId, token);
    }

    public async Task SaveAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }
}