using Complaints.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Services
{
    public class ComplaintClassifier : IComplaintClassifier
    {
        public List<string> Classify(string texto, Dictionary<string, List<string>> categorias)
        {
            var resultado = new List<string>();
            var textoLower = texto.ToLower();

            foreach (var categoria in categorias)
            {
                foreach (var palavra in categoria.Value)
                {
                    if (textoLower.Contains(palavra.ToLower()))
                    {
                        resultado.Add(categoria.Key);
                        break;
                    }
                }
            }

            return resultado;
        }
    }
}
