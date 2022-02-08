using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiHelpDents.Domain.Entities;
using ApiHelpDents.Infraestructure.Repositories;
using ApiHelpDents.Domain.Interfaces;
using Microsoft.Extensions.Options;
using AutoMapper;
using ApiHelpDents.Domain.Dtos.Requests;
using ApiHelpDents.Domain.Dtos.Responses;

namespace ApiHelpDents.Controller{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase{

        private readonly IHttpContextAccessor _httpContext;
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IHttpContextAccessor httpContext, IUsuarioRepository repository, IMapper mapper){
            
            this._httpContext = httpContext;
            this._mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(){

            var query = await _repository.GetAll();
            var response = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<IActionResult> GetById(int id){

            var entity = await _repository.GetById(id);
            var respuesta = _mapper.Map<Usuario,UsuarioResponse>(entity);
            if(respuesta == null){
                return NoContent();
            }

            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateRequest usuario){
            
            var entity = _mapper.Map<UsuarioCreateRequest, Usuario>(usuario);
            var id = await _repository.Create(entity);
            if(id <= 0){
                return Conflict("No se puede realizar el registro");
            }

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/administrador/{id}";
            return Created(urlresult, id);
            
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> Update(int id, [FromBody]UsuarioUpdateRequest usuario){

            if(id <= 0 || !_repository.Exist(i => i.IdUsuario == id))
                return NotFound("El registro no fué encontrado, veifica tu información...");

            var entity = _mapper.Map<UsuarioUpdateRequest, Usuario>(usuario);
            var update = await _repository.Update(id, entity);

            if(!update)
                return Conflict("Ocurrió un fallo al intentar realizar la modificación...");

            return Ok("Se han actualizado los datos correctamente...");

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id){

            if(id <= 0 || !_repository.Exist(i => i.IdUsuario == id))
                return NotFound("El registro no fué encontrado, veifica tu información...");

            var delete = await _repository.Delete(id);

            if(!delete)
                return Conflict("Ocurrió un fallo al intentar eliminar el registro...");

            return Ok("Se ha eliminado el registro correctamente...");

        }
    }

}