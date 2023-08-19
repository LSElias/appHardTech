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

        public ActionResult graficoOrden()
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetOrdenByDia(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.tituloEtiquetas = "Compras realizadas durante la semana";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "pie";
            ViewBag.grafico = grafico;
            return View();
            //return PartialView("_graficoOrden");
        }

        public ActionResult productsTop()
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetProductosTop(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Cantidad vendida";
            grafico.tituloEtiquetas = "Top 5 de productos del mes";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "doughnut";
            ViewBag.grafico = grafico;
            return View();
            //return PartialView("_graficoOrden");
        }

        public ActionResult vendedoresTop()
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetVendedoresTop(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Promedio de evaluación";
            grafico.tituloEtiquetas = "Top 5 vendedores con mejor evaluación";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "bar";
            ViewBag.grafico = grafico;
            return View();
            //return PartialView("_graficoOrden");
        }

    }
}