using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GraficoOrden()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetOrdenByDia(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.titulo = "Ordenes por día";
            grafico.tituloEtiquetas = "Cantidad de ordenes";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "line";
            ViewBag.grafico = grafico;
            return View();
        }
    }
}