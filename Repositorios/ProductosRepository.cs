using Microsoft.Data.Sqlite;

using Productos;


public class ProdcutosRepository
{
    private string cadenaConexion = "Data Source= Db/Tienda.db";
    
    public void InsertProducto (Productos producto)
    {
        
    }
    public void UpdateProducto(int id, Productos producto)
    {

    }
    public List<Productos> GetAllProductos()
    {
        List<Productos> productos = [];
        string query = "SELEC * FROM productos";
        using var conecction = new SqliteConnection(cadenaConexion);
        conecction.Open();
        var command = new SqliteCommand(query, conecction);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var producto = new Productos
                {
                    idProducto = Convert.ToInt32(reader["id"]),
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