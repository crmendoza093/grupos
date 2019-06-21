using Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Grupos.Controllers
{
    public class HomeController : Controller
    {
        Random random = new Random();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RequestModel model)
        {
            if (ModelState.IsValid)
            {     
                    var Body = model.fullName + model.destination;
                    var To = "operaciones@grupos.co";
                    //var To = "johanmartinucc@gmail.com";

                    string html = @"
                            <!DOCTYPE html>
                            <html>
                                <head>
                                <style>
                                table, th, td {
                                    border: 1px solid black;
                                    border-collapse: collapse;
                                }
                                table {
                                width:100%;
                                }
                                tr {
                                background-color: #fff;
                                }
                                th{
                                    padding-top: 8px;
                                    padding-bottom: 8px;
                                    text-align: left;
                                    background-color: #111;
                                    color: white;
                                }
                                </style>
                                </head>
                                <body>
                                    <p>Se ha generado una nueva solicitud.</p>
                                    <table>
                                        <tr>
                                            <th>Origen</th>
                                            <th>Destino</th>
                                        </tr>      
                                        <tr>
                                            <td>[origen]</td>
                                            <td>[destino]</td>
                                        </tr>  
                                        <tr>
                                            <th>Fecha Llegada</th>
                                            <th>Jornada de Llegada</th>
                                        </tr>      
                                        <tr>
                                            <td>[fecha_llegada]</td>
                                            <td>[jornada_llegada]</td>
                                        </tr>  
                                        <tr>
                                            <th>Fecha Regreso</th>
                                            <th>Jornada de Regreso</th>
                                        </tr>      
                                        <tr>
                                            <td>[fecha_regreso]</td>
                                            <td>[jornada_regreso]</td>
                                        </tr>  
                                        <tr>
                                            <th>Número de adultos</th>
                                            <th>Número de niños</th>
                                        </tr>      
                                        <tr>
                                            <td>[numero_adultos]</td>
                                            <td>[numero_ninos]</td>
                                        </tr>  
                                        <tr>
                                            <th>Tipo de Grupo</th>
                                            <th>Es agencia</th>
                                        </tr>      
                                        <tr>
                                            <td>[tipo_grupo]</td>
                                            <td>[agencia]</td>
                                        </tr>  
                                        <tr>
                                            <th>Nombre de la Agencia</th>
                                            <th>Observaciones</th>
                                        </tr>      
                                        <tr>
                                            <td>[nombre_agencia]</td>
                                            <td>[observaciones]</td>
                                        </tr> 
                                        <tr>
                                            <th>Nombres y Apellidos</th>
                                            <th>Email</th>
                                        </tr>      
                                        <tr>
                                            <td>[nombres_apellidos]</td>
                                            <td>[email]</td>
                                        </tr> 
                                        <tr>
                                            <th>Número de Contacto</th
                                        </tr>      
                                        <tr>
                                            <td>[numero]</td>
                                        </tr> 
                                    </table>
                                    <p>solictud generada el [fecha_solicitud].</p>
                                </body>
                            </html>
                                                        ";
                    StringBuilder stringBuilder = new StringBuilder(html);
                    stringBuilder.Replace("[origen]", model.source);
                    stringBuilder.Replace("[destino]", model.destination);
                    stringBuilder.Replace("[fecha_llegada]", model.arrivedDate.ToString());
                    stringBuilder.Replace("[jornada_llegada]", model.arrivedDayTime[0].ToString());
                    stringBuilder.Replace("[fecha_regreso]", model.returnDate.ToString());
                    stringBuilder.Replace("[jornada_regreso]", model.returnDayTime[0].ToString());
                    stringBuilder.Replace("[numero_adultos]", model.adultsNumber.ToString());
                    stringBuilder.Replace("[numero_ninos]", model.childsNumber.ToString());
                    stringBuilder.Replace("[tipo_grupo]", model.groupType[0].ToString());
                    stringBuilder.Replace("[agencia]", model.isAgency.ToString());
                    stringBuilder.Replace("[nombre_agencia]", model.agencyName);
                    stringBuilder.Replace("[observaciones]", model.Observations);
                    stringBuilder.Replace("[nombres_apellidos]", model.fullName);
                    stringBuilder.Replace("[email]", model.Email);
                    stringBuilder.Replace("[numero]", model.phoneNumber);
                    stringBuilder.Replace("[fecha_solicitud]", DateTime.Now.ToString());

                    MailMessage message = new MailMessage();
                    message.To.Add(To);
                    //message.From = new MailAddress("pruebadecorreojohan@gmail.com", "Johan", System.Text.Encoding.UTF8);
                    message.From = new MailAddress("noreply@grupos.co", "Grupos", System.Text.Encoding.UTF8);
                    message.Subject = consecutiveNumber().ToString() + " - " + model.fullName;
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.Body = stringBuilder.ToString();
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    //client.Credentials = new NetworkCredential("pruebadecorreojohan@gmail.com", "Pr0t3g1d4");
                    client.Credentials = new NetworkCredential("noreply@grupos.co", "Gerencia@2019");
                    client.Port = 25;
                    //client.Port = 465;
                    //client.Host = "smtp.gmail.com";
                    client.Host = "asael.colombiahosting.com.co";
                    client.EnableSsl = false; //Esto es para que vaya a través de SSL que es obligatorio con GMail
                    try
                    {
                        TempData["msg"] = "<script>alert('Su solicitud ha sido procesada satisfactoriamente, uno de nuestros asesores se contactará con usted.');</script>";
                        client.Send(message);
                        ModelState.Clear();
                    }
                    catch (SmtpException ex)
                    {
                        TempData["msg"] = "<script>alert('Ocurrió unerror procesando su solicitud, por favor intente mas tarde');</script>";
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }                
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Grupos()
        {
            return View();
        }
        public int consecutiveNumber()
        {
            var consecutive = 0;
            consecutive = random.Next(1, 1000000000);
            if (consecutive != 0)
            {
                return consecutive;
            }
            else
            {
                return 0;
            }
        }
    }
}