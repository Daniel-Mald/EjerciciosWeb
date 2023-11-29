using Hamburguesas.Models.Entities;

namespace Hamburguesas.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
        }
        public Menu? GetHamburguesaPorNombre(string id)
        {
            return Context.Menu.Where(x=>x.Nombre == id).FirstOrDefault();
        }
        public IEnumerable<Menu>? GetPromociones()
        {
            return Context.Menu.Where(x=>x.PrecioPromocion != null || x.PrecioPromocion > 0).OrderBy(x=>x.Nombre);
        }
    }
}
