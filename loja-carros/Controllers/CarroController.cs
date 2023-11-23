using loja_carros.Dal;
using Microsoft.AspNetCore.Mvc;
using loja_carros.Models;
using System;

public class CarroController : Controller
{
    private readonly IPostgresDal _postgresDal;
    
    

    public CarroController(IPostgresDal postgresDal)
    {
        _postgresDal = postgresDal;
        
    }

    public IActionResult Index()
    {
        var carros = _postgresDal.ListarCarros();
        return View(carros);
    }

    public IActionResult CadastrarCarro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult EnviarCarro(Carro carro)
    {
        if (ModelState.IsValid)
        {
            ViewBag.Mensagem = "Formulário enviado. Carro salvo com sucesso!";
            _postgresDal.InserirCarro(carro.Id,carro.Marca, carro.Modelo, carro.Ano, carro.Cor, carro.Preco);
        }

        return RedirectToAction("Index");
    }
    
    public IActionResult EditarCarro(int id)
    {
        return View();

    }

    [HttpPost]
    public IActionResult AtualizarCarro(Carro carro)
    {
        if (ModelState.IsValid)
        {
            _postgresDal.AtualizarCarro(carro.Id, carro.Marca, carro.Modelo, carro.Ano, carro.Cor, carro.Preco);
        }

        return RedirectToAction("Index");
    }

    public IActionResult DeletarCarro(int id)
    {
        var carro = _postgresDal.SelecionarCarro(id);

        if (carro == null)
        {
            return RedirectToAction("Index");
        }

        return View(carro);
    }

    [HttpPost]
    public IActionResult ExcluirCarroConfirmado(int id)
    {
        _postgresDal.DeletarCarro(id);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }


}