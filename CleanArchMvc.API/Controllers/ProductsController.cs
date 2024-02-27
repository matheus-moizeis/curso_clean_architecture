using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var produtos = await _productService.GetProducts();

            if (produtos == null) return NotFound("Categories not found");

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name ="GetProduct")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var produto = await _productService.GetById(id);

            if (produto == null) return NotFound("Categories not found");

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest("Invalid Data");

            await _productService.Add(productDTO);

            return new CreatedAtRouteResult("GetCategory", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id) return BadRequest();
            if (productDTO == null) return BadRequest();

            await _productService.Update(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var produto = await _productService.GetById(id);

            if (produto == null) return NotFound();

            await _productService.Remove(id);

            return Ok(produto);
        }
    }
}
