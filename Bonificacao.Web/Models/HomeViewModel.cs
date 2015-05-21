using Bonificacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonificacao.Web.Models
{
    public class HomeViewModel : ViewModelBase
    {
        public IEnumerable<Movimento> Movimentacoes { get; set; }
    }
}