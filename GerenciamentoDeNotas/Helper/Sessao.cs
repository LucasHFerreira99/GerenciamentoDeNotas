using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Models.Usuario;
using Newtonsoft.Json;

namespace GerenciamentoDeNotas.Helper
{
    public class Sessao : ISessao
    {

        private readonly IHttpContextAccessor _context;

        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }

        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _context.HttpContext.Session.SetString("sessaoUsuarioLogado" , valor);
        }

        public void RemoverSessaoDoUsuario()
        {
            _context.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
