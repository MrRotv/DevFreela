using Dapper;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Perisistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Start
{
    internal class StartCommandHandler : IRequestHandler<StartCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public StartCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

        }
        public async Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Start();

            await _projectRepository.StartAsync(project);

            return Unit.Value;
        }
    }
}
