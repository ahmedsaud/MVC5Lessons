using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.DataLayer
{
    public static class Helpers
    {
        public static EntityState ConvertState(ObjectState _objectState)
        {
            switch (_objectState)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        public static void ApplyChanges(this DbContext _context)
        {
            foreach (var entry in _context.ChangeTracker.Entries<IObjectWithState>())
            {
                entry.State = ConvertState(((IObjectWithState)entry.Entity).ObjectState);
            }
        }
    }
}
