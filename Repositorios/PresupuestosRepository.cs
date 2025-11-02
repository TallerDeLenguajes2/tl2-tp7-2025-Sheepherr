namespace PresupuestosRepository;

using Microsoft.Data.Sqlite;

using Presupuestos;
using Productos;
using PresupuestoDetalle;
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
    public Presupuestos GetbyIdProducto (int id)
    {
        Presupuestos presupuesto = null;
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        
        string query = "SELECT p.NombreDestinatario AS idPres, p.FechaCreacion, pr.Descripcion, d.Cantidad, pr.Precio FROM Presupuestos p JOIN PresupuestosDetalle d ON p.idPresupuestos = d.idPresupuestos JOIN Productos pr ON d.idProducto = pr.idProducto WHERE p.idPresupuestos = @id";
        
        using var comando = new SqliteCommand(query, conexion);
        
        comando.Parameters.Add(new SqliteParameter("@id", id));

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
                if (presupuesto == null)
                {
                    presupuesto.IdPresupuesto = id;
                    presupuesto.nombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = reader["FechaCreacion"].ToString();
                    presupuesto.detalle = new List<PresupuestoDetalle>();
                }
            var producto = new Productos
            {
                idProducto = Convert.ToInt32(reader["idProducto"]),
                descripcion = reader["Descripcion"].ToString(),
                precio = Convert.ToInt32(reader["Precio"])
            };
            var detalle = new PresupuestoDetalle
            {
                producto = producto,
                cantidad = Convert.ToInt32(reader["Cantidad"])
            };
            presupuesto.detalle.Add(detalle);
            
        } 
        
        return presupuesto;
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