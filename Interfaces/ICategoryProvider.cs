using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Interfaces
{
    public interface ICategoryProvider
    {
        Dictionary<string, List<string>> GetCategorias();
    }
}
