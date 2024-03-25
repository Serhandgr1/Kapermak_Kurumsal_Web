using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface ICommandService
    {
        Task Create(CommentDTO entites);
        Task Delete(int Id);
        Task<List<CommentDTO>> GetAllData();
        Task Update(CommentDTO entites);
        Task<CommentDTO> GetById(int Id);
    }
}
