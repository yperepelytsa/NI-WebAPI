using System.Collections.Generic;

namespace CMSREST.Models
{
    public interface INavLinkRepository
    {
        void Add(NavLink item);
        IEnumerable<NavLink> GetAll();
        NavLink Remove(int id);
        void Update(NavLink item);
        void Save();
    }
}