using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AngularCaching.Api.Core;
using AngularCaching.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AngularCaching.Api.Features
{
    public class GetUserById
    {
        public class Request: IRequest<Response>
        {
            public Guid UserId { get; set; }
        }

        public class Response: ResponseBase
        {
            public UserDto User { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IAngularCachingDbContext _context;
        
            public Handler(IAngularCachingDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    User = (await _context.Users.SingleOrDefaultAsync(x => x.UserId == request.UserId)).ToDto()
                };
            }
            
        }
    }
}
