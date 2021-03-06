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

    public class ComentarioController : ControllerBase{

        private readonly IHttpContextAccessor _httpContext;
        private readonly IComentarioRepository _repository;
        private readonly IUsuarioRepository _repositoryUser;
        private readonly IMapper _mapper;

        public ComentarioController(IHttpContextAccessor httpContext, IComentarioRepository repository, IUsuarioRepository repositoryUser, IMapper mapper){
            
            this._httpContext = httpContext;
            this._mapper = mapper;
            _repository = repository;
            _repositoryUser = repositoryUser;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(){

            var query = await _repository.GetAll();
            foreach(var entity in query){

                Usuario usuario = await _repositoryUser.GetById(entity.ClaveUsuario);

                entity.ClaveUsuarioNavigation.Nombres = usuario.Nombres;
                entity.ClaveUsuarioNavigation.Apellidos = usuario.Apellidos;
            }

            var response = _mapper.Map<IEnumerable<Comentario>, IEnumerable<ComentarioResponse>>(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("asesor/{id:int}")]
        public async Task<IActionResult> GetByIdAsesor(int id){

            var query = await _repository.GetByIdAsesor(id);
            if(query == null){
                return NoContent();
            }

            foreach(var entity in query){

                Usuario usuario = await _repositoryUser.GetById(entity.ClaveUsuario);
                entity.ClaveUsuarioNavigation.Nombres = usuario.Nombres;
                entity.ClaveUsuarioNavigation.Apellidos = usuario.Apellidos;
            }
            var response = _mapper.Map<IEnumerable<Comentario>, IEnumerable<ComentarioResponse2>>(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComentarioCreateRequest comentario){
            
            var entity = _mapper.Map<ComentarioCreateRequest, Comentario>(comentario);
            var id = await _repository.Create(entity);
            if(id <= 0){
                return Conflict("No se puede realizar el registro");
            }

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/comentario/{id}";
            return Created(urlresult, id);
            
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id){

            if(id <= 0 || !_repository.Exist(i => i.IdComentario == id))
                return NotFound("El registro no fu?? encontrado, veifica tu informaci??n...");

            var delete = await _repository.Delete(id);

            if(!delete)
                return Conflict("Ocurri?? un fallo al intentar eliminar el registro...");

            return Ok("Se ha eliminado el registro correctamente...");

        }
    }

}