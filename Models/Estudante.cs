using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("estudantes")]
public class Estudante
{
    [Column("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [Column("nome")]

    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
    [Column("datadenascimento")]

    public DateTime DataDeNascimento { get; set; }

    [Required(ErrorMessage = "O campo Nome da Mãe é obrigatório.")]
    [Column("nomedamae")]

    public string? NomeDaMae { get; set; }

    [Required(ErrorMessage = "O campo Período de Ingresso é obrigatório.")]
    [Column("periododeingresso")]

    public string? PeriodoDeIngresso { get; set; }

    [Column("dataultimaedicao")] 
    public DateTime? DataUltimaEdicao { get; set; }
}
