namespace Virtus.Application.Alunos.Commands;

public class AtualizarAlunoCommand
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }

    public bool EhValido(out List<string> erros)
    {
        erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(Email))
            erros.Add("O e-mail é obrigatório.");

        if (DataNascimento == default)
            erros.Add("A data de nascimento é inválida.");

        return erros.Count == 0;
    }
}
