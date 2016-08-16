using System.Collections.Generic;

namespace CMSREST.Models
{
    public interface IPageRepository
    {
        void Add(Page item);
        IEnumerable<Page> GetAll();
        Page Remove(int id);
        void Update(Page item);
        void Save();
    }
}