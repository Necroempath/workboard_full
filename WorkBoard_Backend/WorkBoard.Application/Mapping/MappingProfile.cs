using AutoMapper;
using WorkBoard.Application.Features.Authentication;
using WorkBoard.Application.Features.Columns;
using WorkBoard.Application.Features.Issues;
using WorkBoard.Application.Features.Projects;
using WorkBoard.Application.Features.WorkspaceMemberships;
using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Mapping;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Workspace 
        CreateMap<Workspace, WorkspaceResponseDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Members.FirstOrDefault()!.Role));

        CreateMap<CreateWorkspaceRequest, Workspace>();

        // WorkspaceMembership
        CreateMap<WorkspaceMembership, WorkspaceMembershipResponseDto>();

        // Auth
        CreateMap<User, AuthResponseDto>()
            .ForMember(dest => dest.Roles, 
            opt => opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));

        // Project
        CreateMap<Project, ProjectResponseDto>();

        // Column
        CreateMap<Column, ColumnResponseDto>();

        // Issue
        CreateMap<Issue, IssueResponseDto>();
        CreateMap<CreateIssueRequest, Issue>();
        CreateMap<UpdateIssueRequest, Issue>();
    }
}
