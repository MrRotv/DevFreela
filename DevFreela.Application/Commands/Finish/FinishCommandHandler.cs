using DevFreela.Infrastructure.Perisistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Finish
{
    internal class FinishCommandHandler : IRequestHandler<FinishCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        public FinishCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(FinishCommand request, CancellationToken cancellationToken)
        {
            var project =  await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
            project.Finish();

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
