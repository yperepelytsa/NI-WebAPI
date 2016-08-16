using System.Collections.Generic;

namespace CMSREST.Models
{
    public interface IRelatedPagesRepository
    {
        void Add(RelatedPages item);
        IEnumerable<RelatedPages> GetAll();
        RelatedPages Remove(int id);
        void Update(RelatedPages item);
        void Save();
    }
}
