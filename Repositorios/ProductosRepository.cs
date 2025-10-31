namespace ProductosRepository;

using Microsoft.Data.Sqlite;

using Productos;
using SQLitePCL;

public class ProductosRepository
{
    private string cadenaConexion = "Data Source= Db/Tienda.db";
    
    public void InsertProducto (Productos producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
        using var comando = new SqliteCommand(query,conexion);

        comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.descripcion));
        comando.Parameters.Add(new SqliteParameter("@Precio", producto.precio));
        
        comando.ExecuteNonQuery();
    }
    public void UpdateProducto(int id, Productos producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "UPDATE Productos SET Descripcion = @Descripcion WHERE idProducto = @idProducto";
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.descripcion));
        comando.Parameters.Add(new SqliteParameter("@idProducto", id));
        comando.ExecuteNonQuery();
    }
    public List<Productos> GetAllProductos()
    {
        List<Productos> productos = [];
        string query = "SELECT * FROM Productos";
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        var command = new SqliteCommand(query, conexion);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var producto = new Productos
                {
                    idProducto = Convert.ToInt32(reader["idProducto"]),
                    descripcion = reader["Descripcion"].ToString(),
                    precio = Convert.ToInt32(reader["Precio"])
                };
                productos.Add(producto);
            }
        }
        return productos;
    }
    public Productos GetbyIdProducto (int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        
        string query = "SELECT idProducto, Descripcion, Precio FROM Productos WHERE idProducto = @id";
        
        using var comando = new SqliteCommand(query, conexion);
        
        comando.Parameters.Add(new SqliteParameter("@id", id));

        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            var producto = new Productos
            {
                idProducto = Convert.ToInt32(reader["idProducto"]),
                descripcion = reader["Descripcion"].ToString(),
                precio = Convert.ToInt32(reader["Precio"])
            };
            return producto;
            
        }  
        else
        {
            return null;
        }
        

    }
    public void DeleteProducto (int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "DELETE FROM Producto WHERE idProducto = @id";
        using var comando = new SqliteCommand(query,conexion);

        comando.Parameters.Add(new SqliteParameter("@id", id));
        comando.ExecuteNonQuery();
    }
    
}