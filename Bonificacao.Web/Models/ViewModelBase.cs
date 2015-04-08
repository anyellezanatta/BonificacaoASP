using Bonificacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonificacao.Web.Models
{
    public abstract class ViewModelBase
    {
        public TipoPessoa TipoUsuario { get; set; }
    }
}