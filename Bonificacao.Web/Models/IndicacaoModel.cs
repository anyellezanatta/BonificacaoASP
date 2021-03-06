﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bonificacao.Web.Models
{
    public class IndicacaoModel
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Estabelecimento")]
        [Required]
        public int? EstabelecimentoSelecionado { get; set; }
    }
}