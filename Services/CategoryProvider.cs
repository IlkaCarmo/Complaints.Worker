using Complaints.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Services
{
    public class CategoryProvider : ICategoryProvider
    {
        public Dictionary<string, List<string>> GetCategorias()
        {
            return new Dictionary<string, List<string>>
        {
            { "imobiliário", new() { "credito imobiliario", "casa", "apartamento" } },
            { "socorros", new() { "resgate", "capitalizacao", "socorro" } },
            { "banco", new() { "conta", "bancaria", "valor", "inédito" } },
            { "acesso", new() { "acessar", "login", "senha" } },
            { "aplicativo", new() { "aplicativo", "travando", "erro" } },
            { "fraude", new() { "fraude", "nao reconhece divida" } }
        };
        }
    }
}
