using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.Models
{
    public class BuscaMovimentacaoViewModel
    {
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int[] Clientes { get; set; }
        public IEnumerable<SelectListItem> OpcoesClientes { get; set; }
    }
}