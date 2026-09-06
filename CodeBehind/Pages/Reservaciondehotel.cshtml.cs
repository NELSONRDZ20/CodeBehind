using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeBehind.Pages
{
    [BindProperties]
    public class ReservaciondehotelModel : PageModel
    {
        #region Entidad
        public bool desayuno { get; set; }
        public bool estacionamiento { get; set; }
        public bool wifiPremium { get; set; }

  
        public bool accesoSpa { get; set; }
        public int tipohabitacion { get; set; }
        public int numerodenoches { get; set; }
        public int numerodehuespedes { get; set; }
        public int tipohuesped { get; set; }
        public int descuento { get; set; }
        public int iva { get; set; }
        public string? ResultadoOk { get; set; }
        public string? ResultadoNoOk { get; set; }
        //Declaro las listas necesarias
        public List <SelectListItem> ddlTipoHabitacion { get; set; }
        public List <SelectListItem> ddlTipoHuesped { get; set; }
        
        #endregion
        // funcion publica para cargar todos los ddl
        public void CargarDropDownList ()
        {
            ddlTipoHabitacion = new List<SelectListItem>
            {
                new SelectListItem{Value="0", Text="Seleccione"},
                new SelectListItem{Value="1", Text="Sencilla"}, 
                new SelectListItem{Value="2", Text="Doble"},
                new SelectListItem{Value="3", Text="Suite"},
            };
            ddlTipoHuesped = new List<SelectListItem>
            {
                new SelectListItem{Value="0", Text="Seleccione"},
                new SelectListItem{Value="1", Text="General"},
                new SelectListItem{Value="2", Text="Frecuente"},
                new SelectListItem{Value="3", Text="VIP"}
            };
        }
        
        public void OnGet()
        //automaticamente se ejecuta el abrir la pagina

        {
            CargarDropDownList();

        }
        //Accion del boton gaurdar
        public void OnPostGuardar()
        {
            CargarDropDownList();
            //logica del boton 
            try
            {

                List<string> lstErrores = new List<string>();
                
                //validar si hay errores 
                if (lstErrores.Count == 0)
                {
                    ResultadoOk = "Datos guardados correctamente";
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

            
            ResultadoOk = string.Empty;
            ResultadoNoOk = string.Empty;

        }
    }
}

