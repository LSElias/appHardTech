using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Administrador()
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            ViewModelGrafico grafico2 = new ViewModelGrafico();
            ViewModelGrafico grafico3 = new ViewModelGrafico();
            ViewModelGrafico grafico4 = new ViewModelGrafico();

            _ServiceOrden.GetOrdenByDia(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.tituloEtiquetas = "Compras realizadas durante la semana";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "pie";
            ViewBag.OrdenByDia = grafico;

            
            _ServiceOrden.GetProductosTop(out string etiquetasT, out string valoresT);
            grafico2.Etiquetas = etiquetasT;
            grafico2.Valores = valoresT;
            int cantidadValoresT = valoresT.Split(',').Length;
            grafico2.Colores = string.Join(",", grafico.GenerateColors(cantidadValoresT));
            grafico2.titulo = "Cantidad vendida";
            grafico2.tituloEtiquetas = "Top 5 de productos del mes";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico2.tipo = "doughnut";
            ViewBag.ProductosTop = grafico2;

            _ServiceOrden.GetVendedoresTop(out string etiquetasV, out string valoresV);
            grafico3.Etiquetas = etiquetasV;
            grafico3.Valores = valoresV;
            int cantidadValoresV = valoresV.Split(',').Length;
            grafico3.Colores = string.Join(",", grafico.GenerateColors(cantidadValoresV));
            grafico3.titulo = "Promedio de evaluación";
            grafico3.tituloEtiquetas = "Top 5 vendedores con mejor evaluación";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico3.tipo = "bar";
            ViewBag.VendedoresTop = grafico3;

            _ServiceOrden.GetVendDeficiente(out string etiquetasD, out string valoresD);
            grafico4.Etiquetas = etiquetasD;
            grafico4.Valores = valoresD;
            int cantidadValoresD = valoresD.Split(',').Length;
            grafico4.Colores = string.Join(",", grafico.GenerateColors(cantidadValoresD));
            grafico4.titulo = "Promedio de evaluación";
            grafico4.tituloEtiquetas = "Top 3 peores vendedores evaluados";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico4.tipo = "bar";
            ViewBag.VendDeficientes = grafico4;


            return View();

        }


        public ActionResult Proveedor()
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico5 = new ViewModelGrafico();
            ViewModelGrafico grafico6 = new ViewModelGrafico();
          //  ViewModelGrafico grafico7 = new ViewModelGrafico();

            Usuario oUsuario = (Usuario)Session["User"];

            _ServiceOrden.GetMasVendidos(out string etiquetasQ, out string valoresQ, oUsuario.Id);
            grafico5.Etiquetas = etiquetasQ;
            grafico5.Valores = valoresQ;
            int cantidadValoresQ = valoresQ.Split(',').Length;
            grafico5.Colores = string.Join(",", grafico5.GenerateColors(cantidadValoresQ));
            grafico5.titulo = "Cantidad Vendida";
            grafico5.tituloEtiquetas = "Producto más vendido";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico5.tipo = "bar";
            ViewBag.MasVendido = grafico5;

            _ServiceOrden.GetEvaluacionXProveedor(out string etiquetasM, out string valoresM, oUsuario.Id);
            grafico6.Etiquetas = etiquetasM;
            grafico6.Valores = valoresM;
            int cantidadValoresM = valoresM.Split(',').Length;
            grafico6.Colores = string.Join(",", grafico6.GenerateColors(cantidadValoresM));
            grafico6.titulo = "Cantidad";
            grafico6.tituloEtiquetas = "Evaluaciones recibidas";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico6.tipo = "bar";
            ViewBag.Evaluacion = grafico6;

            return View();

        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
