using Tutorias.Service.DatabaseContext;

namespace Sistema_De_Tutorias.Utility;
public sealed class CredencialesUsuario
{
    private static readonly object padlock = new object();
    private Usuario _usuario;
    private static CredencialesUsuario instance = null;

    

    public Usuario Usuario
    {
        get { return _usuario; }
        set { _usuario = value; }
    }

    private CredencialesUsuario()
    {
    }

    public static CredencialesUsuario Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CredencialesUsuario();
                }
                return instance;
            }
        }
    }
}