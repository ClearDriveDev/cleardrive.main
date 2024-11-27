﻿using CAS.mobil.Responses;
using CAS.mobil.Models;

namespace CAS.mobil.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
        public Task<ControllerResponse> InsertAsync(Position position);
        public Task<ControllerResponse> DeleteAsync(Guid guid);
    }
}