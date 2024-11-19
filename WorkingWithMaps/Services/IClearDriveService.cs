﻿using WorkingWithMaps.Models;

namespace WorkingWithMaps.Services
{
    public interface IClearDriveService
    {
        public Task<List<Position>> SelectAll();
        public Task<string> InsertAsync(Position position);
        public Task<string> DeleteAsync(Guid guid);
    }
}
