using EventsApi.APIs.Common;
using EventsApi.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindManyArgs : FindManyInput<User, UserWhereInput> { }
