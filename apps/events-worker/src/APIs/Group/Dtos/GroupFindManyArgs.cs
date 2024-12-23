using EventsWorker.APIs.Common;
using EventsWorker.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class GroupFindManyArgs : FindManyInput<Group, GroupWhereInput> { }
