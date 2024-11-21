﻿using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;

namespace CAS.backend.Repos
{
    public interface IAdministratorRepo
    {
        Task<List<Administrator>> GetAll();
        Task<Administrator?> GetBy(Guid id);
        Task<ControllerResponse> UpdateAdminAsync(Administrator administrator);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}