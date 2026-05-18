using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed class AddColumnCommandHandler : IRequestHandler<AddColumnCommand, ColumnResponseDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;


    public AddColumnCommandHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<ColumnResponseDto> Handle(AddColumnCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can manage projects");

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, ct)
            ?? throw new InvalidOperationException("Invalid Project Id");

        var column = project.AddColumn(request.Name);

        await _projectRepository.SaveColumnAsync(column, ct);

        return _mapper.Map<ColumnResponseDto>(column);
    }
}