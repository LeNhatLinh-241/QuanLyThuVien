using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO;

public class UserDAO
{
    private DatabaseConnect db = new DatabaseConnect();
    public List<UserDTO> GetAllUsers()
    {
        List<UserDTO> list = new List<UserDTO>();
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE IsActive = 1 ORDER BY ID DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserDTO
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Role = reader.GetString(3),
                            HoTen = reader.GetString(4),
                            SoDienThoai = reader.IsDBNull(5) ? null : reader.GetString(5),
                            IsActive = reader.GetInt32(6)

                        });
                    }
                }
                

            }
        }

        return list;
    }

    public bool InsertUser(UserDTO user)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO Users (Role, Hoten, SoDienThoai , IsActive) VALUES ('DocGia', @hoten, @sdt,1)";
                command.Parameters.AddWithValue("@hoten", user.HoTen);
                command.Parameters.AddWithValue("@sdt", user.SoDienThoai ?? (object)DBNull.Value);
                return command.ExecuteNonQuery() > 0;

            }
        }
        
    }

    public bool UpdateUser(UserDTO user)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET HoTen = @hoten, SoDienThoai = @sdt WHERE ID = @id";
                command.Parameters.AddWithValue("@hoten", user.HoTen);
                command.Parameters.AddWithValue("@sdt", user.SoDienThoai ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id", user.ID);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    public bool DeleteUser(int id)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET IsActive = 0 WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
    
    
    
}