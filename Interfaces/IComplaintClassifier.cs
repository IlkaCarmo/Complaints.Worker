using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Interfaces
{
    public interface IComplaintClassifier
    {
        List<string> Classify(string texto, Dictionary<string, List<string>> categorias);
    }
}
