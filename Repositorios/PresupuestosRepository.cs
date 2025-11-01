namespace PresupuestosRepository;

using Microsoft.Data.Sqlite;

using Presupuestos;
using Productos;
using SQLitePCL;

public class PresupuestosRepository
{
    private string cadenaConexion = "Data Source= Db/Tienda.db";
    
    public void InsertPresupuesto (Presupuestos presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombreDestinatario, @fechaCreacion)";
        using var comando = new SqliteCommand(query,conexion);

        comando.Parameters.Add(new SqliteParameter("@nombreDestinatario", presupuesto.nombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@fechaCreacion", presupuesto.FechaCreacion));
        
        comando.ExecuteNonQuery();
    }
    /*public void InsertPresupuestoDetalle (int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "INSERT INTO ProsupuestosDetalles SET Descripcion = @Descripcion WHERE idProducto = @idProducto";
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.descripcion));
        comando.Parameters.Add(new SqliteParameter("@idProducto", id));
        comando.ExecuteNonQuery();
    }*/
    public List<Presupuestos> GetAllPresupuestos ()
    {
        List<Presupuestos> presupuestos = [];
        string query = "SELECT * FROM Presupuestos";
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        var command = new SqliteCommand(query, conexion);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var presupuesto = new Presupuestos
                {
                    IdPresupuesto = Convert.ToInt32(reader["idPresupuestos"]),
                    nombreDestinatario = reader["NombreDestinatario"].ToString(),
                    FechaCreacion = reader["FechaCreacion"].ToString()
                };
                presupuestos.Add(presupuesto);
            }
        }
        return presupuestos;
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