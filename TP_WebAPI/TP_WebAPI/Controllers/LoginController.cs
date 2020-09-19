using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TP_WebAPI.Controllers
{

    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Login([FromBody] JsonResult model)
        {
            return null;
        }

        [HttpPost]
        [Route("register")]
        public string Register([FromBody] UserModel model)
        {
            string filename = "user.csv";
            string token = "";
            try
            {
                token = Guid.NewGuid().ToString();
            
           

                if (!System.IO.File.Exists(filename))
                {
                    var writer = new StreamWriter(filename, true);
                    writer.WriteLine($"{model.username},{model.password},{token}");
                    writer.Close();
                }
                else
                {
                    var reader = new StreamReader(System.IO.File.OpenRead(filename));
                    bool check = false;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (values[0] == model.username)
                        {
                            check = true;
                            break;
                        }
                    }
                    reader.Close();
                  
                    if (!check)
                    {
                        var writer = new StreamWriter(filename, true);
                        writer.WriteLine($"{model.username}, {model.password}, {token}");
                        writer.Close();
                    }
                    else
                    {
                        return "User exisits";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return "Error"!;
            }
            return token;
        }
    }
}
