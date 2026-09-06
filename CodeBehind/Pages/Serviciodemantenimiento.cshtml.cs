using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBehind.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {

        #region Entidad
        public int tipoServicio { get; set; }
        public int tipoEquipo { get; set; }
        public int antiguedad { get; set; }
        public int numequipos { get; set; }
        public int garantia { get; set; }
        public string PrioridadServicio { get; set; } = "Normal";
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
                if (tipoServicio == 0)
                {
                    lstErrores.Add("Debe seleccionar un producto");
                }
                if (tipoEquipo == 0)
                {
                    lstErrores.Add("Debe seleccionar un tipo de equipo");
                }
                if (antiguedad < 0 || antiguedad > 30)
                {
                    lstErrores.Add("Antigüedad no válida");
                }
                if (numequipos <= 0 || numequipos > 50)
                {
                    lstErrores.Add("Cantidad no válida");
                }
                if (garantia == 0)
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
                    decimal precio_del_servicio = tipoServicio switch
                    {
                        1 => 350,
                        2 => 600,
                        3 => 450,
                        4 => 800,
                        _ => 0
                    };
                    decimal cargo_adicional_porequipo = tipoEquipo switch
                    {
                        1 => 0,
                        2 => 100,
                        3 => 150,
                        4 => 300,
                        _ => 0
                    };

                    decimal costo_garantia = garantia switch
                    {
                        1 => 0,
                        2 => 150,
                        3 => 300,

                        _ => 0
                    };

                    decimal costo_prioridad = PrioridadServicio switch
                    {
                        "Normal" => 0,
                        "Urgente" => 150,
                        "Express" => 300,
                        _ => 0
                    };

                    decimal costo_adicional_porantiguedad = 0;
                    if (antiguedad > 5)
                    {
                        costo_adicional_porantiguedad += precio_del_servicio * .1m * numequipos;
                    }

                    decimal costo_unitario_por_equipo = precio_del_servicio + cargo_adicional_porequipo + costo_garantia + costo_adicional_porantiguedad;
                    decimal subtotal_equipos = costo_unitario_por_equipo * numequipos;
                    decimal subtotal_base = subtotal_equipos + costo_prioridad; 
                    decimal total_iva = subtotal_base * iva/100;
                    decimal total_final = subtotal_base + total_iva;


                    ResultadoOk =
                        "<div class='teble_responsive' > <table class='table table-sm'>" +
                        "<tr>" +
                        "   <td>Cargo adicional por antiguedad  </td>" +
                        "   <td>Costo unitario por equipo  </td>" +
                        "<td>Subtototal de los equipos </td>" +
                        "<td>Monto IVA </td>" +
                        
                        "<td>Total a pagar  </td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td><strong class=h4> $ " + costo_adicional_porantiguedad + "<strong class=h4></td>" +
                        "<td><strong class=h4> $ " + costo_unitario_por_equipo + "<strong class=h4></td>" +
                        "<td> <strong class=h4> $ " + subtotal_equipos + "<strong class=h4></td>" +
                        "<td> <strong class=h4> $ " + total_iva + "<strong class=h4></td>" +
                       
                        "<td> <strong class=h4> $ " + total_final + " <strong class=h4> </td>" + "</tr>" + "</table> </div>";
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
        public void OnPostlimpiar()
        {
            ModelState.Clear();

            tipoServicio = 0;
            tipoEquipo = 0;
            antiguedad = 0;
            numequipos = 0;
            iva = 0;
            garantia = 0;
           
            ResultadoOk = string.Empty;
            ResultadoNoOk = string.Empty;

        }
    }
}
