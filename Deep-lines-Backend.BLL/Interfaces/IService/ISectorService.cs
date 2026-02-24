using Deep_lines_Backend.BLL.DTOs.SectorEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface ISectorService
    {
        public Task AddSector(AddSectorDTO sectorDTO);
        public Task<List<Sector>> GetAll();
        public Task<Sector> GetById(int id);
        public Task<bool> UpdateSector(AddSectorDTO sectorDTO, int id);
        public Task<bool> DeleteSector(int id);
    }
}
