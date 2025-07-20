using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Repositories;

public class CameraRepository(IContext context) : BaseRepository<Camera>(context), ICameraRepository
{

}