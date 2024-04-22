using Microsoft.AspNetCore.Mvc;

namespace zadanie6.Controllers;

public class AnimalsController  : ControllerBase
{
    
    

[HttpPost]
public IActionResult Test(AnimalsController c)
{
    return Ok();
}

}