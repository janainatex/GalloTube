using GalloTube.Models;

namespace GalloTube.Interfaces;

public interface IFilmeRepository : IRepository<Filme>
{
    List<Filme> ReadAllDetailed();

    Filme ReadByIdDetailed(int id);
}

