using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Perisistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteCommandHandler (IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Cancel();

            await _projectRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
