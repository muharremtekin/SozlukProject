using AutoMapper;
using MediatR;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Features.Commands.EntryComment.Create;
public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
{
    private readonly IEntryCommentRepository _entryCommentRepository;
    private readonly IMapper _mapper;

    public CreateEntryCommentCommandHandler(IEntryCommentRepository entryCommentRepository, IMapper mapper)
    {
        _entryCommentRepository = entryCommentRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
    {
        var dbComment = _mapper.Map<Domain.Models.EntryComment>(request);

        await _entryCommentRepository.AddAsync(dbComment);

        return dbComment.Id;
    }
}

