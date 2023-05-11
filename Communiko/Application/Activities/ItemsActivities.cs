using Application.AppConfig;
using AutoMapper;
using BusinessDomain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class ItemsActivities
{
  public class Query : IRequest<ValidationResult<List<ActivenessDto>>> { }

  public class Handler : IRequestHandler<Query, ValidationResult<List<ActivenessDto>>>
  {
    private readonly DataContext context;
    private readonly IMapper mapper;

    public Handler(DataContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    public async Task<ValidationResult<List<ActivenessDto>>> Handle(Query request, CancellationToken token)
    {
      var items = await context.Activities
                               .Include(activeness => activeness.Participants)
                               .ThenInclude(user => user.AppUser)
                               .ToListAsync();

      var result = mapper.Map<List<ActivenessDto>>(items);
      return ValidationResult<List<ActivenessDto>>.Success(result);
    }
  }
}