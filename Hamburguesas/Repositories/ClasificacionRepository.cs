using Hamburguesas.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hamburguesas.Repositories
{
    public class ClasificacionRepository : Repository<Clasificacion>
    {
        public ClasificacionRepository(NeatContext context) : base(context)
        {
        }

        public IEnumerable<Clasificacion> GetClasificaciones()
        {
            return Context.Clasificacion.Include(x => x.Menu);
        }
    }
}
