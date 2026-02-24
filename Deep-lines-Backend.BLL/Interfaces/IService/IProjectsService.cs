using Deep_lines_Backend.BLL.DTOs.ProjectsEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IProjectsService
    {
        public Task AddProject(AddProjectsDTO projectDTO);
        public Task<List<Projects>> GetAll();
        public Task<Projects> GetById(int id);
        public Task<bool> UpdateProject(AddProjectsDTO projectDTO, int id);
        public Task<bool> DeleteProject(int id);
    }
}
