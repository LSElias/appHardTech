using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //Administrador
       // [CustomAuthorize((int)Roles.Administrador)]
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

        public ActionResult vendDeficientes()
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetVendDeficiente(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Promedio de evaluación";
            grafico.tituloEtiquetas = "Top 3 peores vendedores evaluados";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "bar";
            ViewBag.grafico = grafico;
            return View();
            //return PartialView("_graficoOrden");
        }

        //Proveedor
        [CustomAuthorize((int)Roles.Proveedor)]
        public ActionResult MasVendidos()
        {
            Usuario oUsuario = (Usuario)Session["User"]; 
    
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetMasVendidos(out string etiquetas, out string valores, oUsuario.Id);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Cantidad Vendida";
            grafico.tituloEtiquetas = "Producto más vendido";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "bar";
            ViewBag.MasVendido = grafico;
            return View();
            //return PartialView("_graficoOrden");
        }

        public ActionResult Evaluacion()
        {
            Usuario oUsuario = (Usuario)Session["User"];

            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico6 = new ViewModelGrafico();
            _ServiceOrden.GetEvaluacionXProveedor(out string etiquetasM, out string valoresM, oUsuario.Id);
            grafico6.Etiquetas = etiquetasM;
            grafico6.Valores = valoresM;
            int cantidadValoresM = valoresM.Split(',').Length;
            grafico6.Colores = string.Join(",", grafico6.GenerateColors(cantidadValoresM));
            grafico6.titulo = "Cantidad Vendida";
            grafico6.tituloEtiquetas = "Producto más vendido";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico6.tipo = "bar";
            ViewBag.grafico6 = grafico6;
            return View();
            //return PartialView("_graficoOrden");
        }
    }
}