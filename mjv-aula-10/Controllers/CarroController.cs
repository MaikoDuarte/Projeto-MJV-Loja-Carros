using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using mjv_aula_10.Models;

public class CarroController : Controller
{
    private const string filePath = "carros.json";

    public IActionResult Index()
    {
        List<Carro> carros = LerDadosDoArquivo();
        return View(carros);
    }

    [HttpPost]
    public IActionResult Index(Carro carro)
    {
        if (ModelState.IsValid)
        {
            ViewBag.Mensagem = "Formulário enviado. Carro salvo com sucesso!";
            SalvarDadosNoArquivo(carro);
        }

        return View();
    }

    private List<Carro> LerDadosDoArquivo()
    {
        if (System.IO.File.Exists(filePath))
        {
            string jsonContent = System.IO.File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Carro>>(jsonContent);
        }
        return new List<Carro>();
    }

    private void SalvarDadosNoArquivo(Carro carro)
    {
        List<Carro> carros = LerDadosDoArquivo();
        carros.Add(carro);
        string jsonContent = JsonSerializer.Serialize(carros, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(filePath, jsonContent);
    }
}