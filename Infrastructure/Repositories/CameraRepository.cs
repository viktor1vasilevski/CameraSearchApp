using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CameraRepository(IContext context) : BaseRepository<Camera>(context), ICameraRepository
{

}