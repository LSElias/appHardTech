using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEscala : IServiceEscala
    {
        public IEnumerable<Escala> GetEscala()
        {
            IRepositoryEscala oRep = new RepositoryEscala();
            return oRep.GetEscala();
        }

        public Escala GetEscalaByID(int Id)
        {
            IRepositoryEscala oRep = new RepositoryEscala();
            return oRep.GetEscalaByID(Id);
        }
    }
}
