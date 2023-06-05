namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class Carrito
    {
        private Producto _producto;
        public Producto Producto { get { return _producto; } set { _producto = value; } }

        private int _cantidad;

        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }

        public Carrito(Producto producto, int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;
        }

    }
}
