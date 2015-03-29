using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonificacao.Data
{
    public interface IChangeTracker
    {
        DateTimeOffset DataCriacao { get; set; }
        DateTimeOffset? DataModificacao { get; set; }
    }

    public enum TipoMovimento { RecebimentoBonus = 1, DescontoBonus = 2, Ajustes = 3 }

    public enum TipoPessoa { Cliente = 1, Administrador = 2, Frentista = 3 }

    public class EntityBase : IChangeTracker
    {
        public int Id { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataModificacao { get; set; }
    }

    public class Configuracao : EntityBase
    {
        public decimal BonusPorLitro { get; set; }
        public int NivelBonificacao { get; set; }
    }

    public class Estabelecimento : EntityBase
    {
        public string Nome { get; set; }
        public int? GrupoEstabelecimentoId { get; set; }
        public virtual GrupoEstabelecimento GrupoEstabelecimento { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
    }

    public class GrupoEstabelecimento : EntityBase
    {
        public int Nome { get; set; }
        public virtual ICollection<Estabelecimento> Estabelecimentos { get; set; }
    }

    public class Pessoa : EntityBase
    {
        public TipoPessoa Tipo { get; set; }
        public string Nome { get; set; }

        [Index(IsUnique = true)]
        public string Usuario { get; set; }
        public string Senha { get; set; } //salvar usando alguma criptografia
        public virtual ICollection<Indicacao> Indicacoes { get; set; }
        public virtual ICollection<Indicacao> IndicadoPor { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
    }

    public class Indicacao : IChangeTracker
    {
        public int PessoaOrigemId { get; set; }
        public int PessoaIndicadaId { get; set; }
        public virtual Pessoa PessoaOrigem { get; set; }
        public virtual Pessoa PessoaIndicada { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataModificacao { get; set; }
    }

    public class Produto : EntityBase
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
    }

    public class Movimento : EntityBase
    {
        public TipoMovimento TipoMovimento { get; set; }
        public int ClienteId { get; set; }
        public int EstabelecimentoId { get; set; }
        public int FrentistaId { get; set; }
        public int ProdutoId { get; set; }
        public decimal? Debito { get; set; }
        public decimal? Credito { get; set; }
        public decimal ValorPago { get; set; }
        public DateTimeOffset DataHoraMovimento { get; set; }
        public virtual Pessoa Cliente { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }
        public virtual Pessoa Frentista { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
