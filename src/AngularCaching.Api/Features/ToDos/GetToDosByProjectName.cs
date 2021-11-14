using AngularCaching.Api.Core;
using AngularCaching.Api.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AngularCaching.Api.Features
{
    public class GetToDosByProjectName
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
        }

        public class Response : ResponseBase
        {
            public List<ToDoDto> ToDos { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAngularCachingDbContext _context;

            public Handler(IAngularCachingDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new()
                {
                    ToDos = await _context.ToDos.Where(x => x.ProjectName == request.Name).Select(x => x.ToDto()).ToListAsync()
                };
            }

        }
    }
}