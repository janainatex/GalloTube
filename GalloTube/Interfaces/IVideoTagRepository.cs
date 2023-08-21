using GalloTube.Models;

namespace GalloTube.Interfaces;

public interface IvideotagRepository
{
    void Create(int videoId, byte tagId);

    void Delete(int videoId, byte tagId);

    void Delete(int videoId);

    List<videotag> Readvideotag();

    List<video> ReadvideosBytag(byte tagId);

    List<tag> ReadtagsByvideo(int videoId);
}
