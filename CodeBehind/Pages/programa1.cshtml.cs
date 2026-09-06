using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBehind.Pages
{   //obtiene los valores directamente de los controles html
    [BindProperties]
    public class programa1cshtmlModel : PageModel
    {
        #region Entidad
        public int producto { get; set; }
        public int cantidad { get; set; }
        public int tipocliente { get; set; }
        public int envio { get; set; }
        public int descuento { get; set; }
        public int iva { get; set; }

        public string? ResultadoOk { get; set; }
        public string? ResultadoNoOk { get; set; }

        #endregion

        public void OnGet()
        //automaticamente se ejecuta el abrir la pagina
        {
            
        }
        //Accion del boton gaurdar
        public void OnPostGuardar()
        {
            //logica del boton 
            try
            {

                List<string> lstErrores = new List<string>();
                if (producto == 0)
                {
                    lstErrores.Add("Debe seleccionar un producto");
                }
                if (cantidad < 1 || cantidad > 100) {
                    lstErrores.Add("Cantidad no válida");
                }
                if (tipocliente == 0)
                {
                    lstErrores.Add("Debe seleccionar un tipo de cliente");
                }
                if (envio == 0)
                {
                    lstErrores.Add("Debe seleccionar un tipo de envio");
                }
                if (descuento < 0 || descuento > 50)
                {
                    lstErrores.Add("Descuento no válido");
                }
                if (iva != 16)
                {
                    lstErrores.Add("IVA no válido");
                }

                //validar si hay errores 
                if (lstErrores.Count == 0)
                {
                    decimal precio_producto = producto switch
                    {
                        1 => 15000m,
                        2 => 350m,
                        3 => 1200m,
                        4 => 3800m,
                        _ => 0
                        
                    };

                    decimal tipo_cliente = tipocliente switch
                    {
                        1 => 0m,
                        2 => 0.05m,
                        3 => 0.15m,
                        _ => 0m

                    };
                    decimal metodo_envio = envio switch
                    {
                        1 => 0m,
                        2 => 150m,
                        3 => 300m,
                        _ => 0m

                    };

                    decimal subtotal_base = precio_producto * cantidad;

                    decimal monto_descuento = precio_producto * tipo_cliente;

                    decimal subtotal_con_descuento = subtotal_base - monto_descuento;

                    decimal monto_iva = subtotal_con_descuento * iva / 100;

                    /*envio ya calculado */

                    decimal total_a_pagar = subtotal_con_descuento + monto_iva + metodo_envio;

                    ResultadoOk = 
                        "<div class='teble_responsive' > <table class='table table-sm'>" +
                        "<tr>" +
                        "   <td>Subtotal base  </td>" +
                        "   <td>Monto descuento  </td>" +
                        "<td>Subtototal descuento  </td>" +
                        "<td>Monto IVA </td>" +
                        "<td>Costo de envio </td>" +
                        "<td>Total a pagar  </td>"+
                        "</tr>"+
                        "<tr>"+
                        "<td><strong class=h4> $ " + subtotal_base + "<strong class=h4></td>" +
                        "<td><strong class=h4> $ " + monto_descuento + "<strong class=h4></td>" +
                        "<td> <strong class=h4> $ " + subtotal_con_descuento + "<strong class=h4></td>" +
                        "<td> <strong class=h4> $ " + monto_iva + "<strong class=h4></td>" +
                        "<td> <strong class=h4> $ " + metodo_envio + "<strong class=h4></td>" +
                        "<td> <strong class=h4> $ "+ total_a_pagar + " <strong class=h4> </td>" + "</tr>" + "</table> </div>" 




                        ;
                }
                else
                {
                    ResultadoNoOk = string.Join("<br>", lstErrores);

                }
            }
            catch (Exception e)
            {
                ResultadoNoOk = "Esto es un error" + e;

            }

        }
        //accion del boton limpiar
        public void OnPostLimpiar()
        {
            ModelState.Clear();
            
            producto = 0;
            cantidad = 0;
            tipocliente = 0;
            envio = 0;
            descuento = 0;
            ResultadoOk = string.Empty;
            ResultadoNoOk = string.Empty;
            
        }
    }
}
