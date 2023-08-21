using System.Data;
using GalloTube.Interfaces;
using GalloTube.Models;
using MySql.Data.MySqlClient;

namespace GalloTube.Repositories;

public class VideotagRepository : IVideotagRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=GalloTubedb;uid=root;pwd=''";

    public void Create(int VideoId, byte tagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Videotag(VideoId, tagId) values (@VideoId, @tagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@tagId", tagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId, byte tagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Videotag where VideoId = @VideoId and tagId = @tagId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@tagId", tagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Videotag where VideoId = @VideoId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<tag> ReadtagsByVideo(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from tag where id in "
                   + "(select tagId from Videotag where VideoId = @VideoId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        List<tag> tags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            tag tag = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            tags.Add(tag);
        }
        connection.Close();
        return tags;
    }

    public List<Videotag> ReadVideotag()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Videotag";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<Videotag> Videotags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Videotag Videotag = new()
            {
                VideoId = reader.GetInt32("VideoId"),
                tagId = reader.GetByte("tagId")
            };
            Videotags.Add(Videotag);
        }
        connection.Close();
        return Videotags;
    }

    public List<Video> ReadVideosBytag(byte tagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video where id in "
                   + "(select VideoId from Videotag where tagId = @tagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@tagId", tagId);
        
        List<Video> Videos = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video Video = new()
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                OriginalTitle = reader.GetString("originalTitle"),
                Synopsis = reader.GetString("synopsis"),
                VideoYear = reader.GetInt16("VideoYear"),
                Duration = reader.GetInt16("duration"),
                AgeRating = reader.GetByte("ageRating"),
                Image = reader.GetString("image")
            };
            Videos.Add(Video);
        }
        connection.Close();
        return Videos;
    }
}
