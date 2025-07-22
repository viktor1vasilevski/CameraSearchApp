﻿using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICameraRepository
{
    IEnumerable<Camera> LoadCsv();
}
