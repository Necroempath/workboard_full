using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed class UpdateColumnCommandHandler : IRequestHandler<UpdateColumnCommand, ColumnResponseDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public UpdateColumnCommandHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<ColumnResponseDto> Handle(UpdateColumnCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can manage projects");

        var column = await _projectRepository.GetColumnByIdAsync(request.ColumnId, ct)
            ?? throw new InvalidOperationException("Invalid Column Id");

        column.Rename(request.Name);

        await _projectRepository.SaveAsync(ct);

        return _mapper.Map<ColumnResponseDto>(column);
    }
}
