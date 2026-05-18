using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfProjectRepository : IProjectRepository
{
    private readonly WorkBoardDbContext _context;

    public EfProjectRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Project> CreateAsync(Project project, CancellationToken token)
    {
        await _context.Projects.AddAsync(project, token);

        return project;
    }

    public async Task DeleteAsync(Guid projectId, CancellationToken token)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId, token);

        if (project is null) return;

        _context.Remove(project);

        await _context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<Project>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken token)
    {
        return await _context.Projects.Where(p => p.WorkspaceId == workspaceId).ToListAsync(token);
    }

    public async Task<Project?> GetByIdAsync(Guid projectId, CancellationToken token)
    {
        return await _context.Projects
            .Include(p => p.Columns)
            .ThenInclude(c => c.Issues)
            .FirstOrDefaultAsync(p => p.Id == projectId, token);
    }

    public async Task SaveAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }

    public async Task SaveColumnAsync(Column column, CancellationToken token)
    {
        await _context.Columns.AddAsync(column, token);

        await _context.SaveChangesAsync(token);
    }

    public async Task<Column?> GetColumnByIdAsync(Guid columnId, CancellationToken token)
    {
        return await _context.Columns
            .Include(c => c.Project)
            .ThenInclude(p => p.Columns)
            .Include(c => c.Issues)
            .FirstOrDefaultAsync(c => c.Id == columnId, token);
    }
}