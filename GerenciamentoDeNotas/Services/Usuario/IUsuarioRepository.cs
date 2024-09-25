using GerenciamentoDeNotas.Models.Usuario;

namespace GerenciamentoDeNotas.Services.Usuario
{
    public interface IUsuarioRepository
    {

        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorId(int id);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Editar(UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        bool Apagar(int id);
    }
}
