namespace ProductosRepository;

using Microsoft.Data.Sqlite;

using Productos;


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
        conexion.Close();
    }
    public void UpdateProducto(int id, Productos producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
    }
    public List<Productos> GetAllProductos()
    {
        List<Productos> productos = [];
        string query = "SELEC * FROM Productos";
        using var conecction = new SqliteConnection(cadenaConexion);
        conecction.Open();
        var command = new SqliteCommand(query, conecction);
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
        conecction.Close();
        return productos;
    }
    /*public Productos GetbyIdProducto (int id)
    {
        
    }*/
    public void DeleteProducto (int id)
    {
        
    }
    
}