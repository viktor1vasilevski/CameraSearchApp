﻿using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICameraRepository
{
    Task<IEnumerable<Camera>> LoadCsvAsync();
}
