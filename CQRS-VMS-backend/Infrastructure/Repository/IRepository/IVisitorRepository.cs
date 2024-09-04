using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.IRepository
{
    public interface IVisitorRepository
    {
        Task<Visitor> CreateVisitorAsync(Visitor visitor);
       
      
        Task<IEnumerable<Visitor>> GetVisitorDetailsAsync();
    }

}
